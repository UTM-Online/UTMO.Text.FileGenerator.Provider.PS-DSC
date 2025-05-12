    [CmdletBinding()]
    param(
        [Parameter(Mandatory=$true)]
        [string]$moduleManifestPath
    )

    $ErrorActionPreference = 'Stop'

    Write-Output "Loading Module Manifest File"

    $moduleManifest = Get-Content $moduleManifestPath | ConvertFrom-Json

    $ModulesToInstall = @()

    $MaxRetryCount = 5

    Write-Output "Validate and Configure DSC Module Repository"

    switch($Env:COMPUTERNAME)
    {
        "JOIRWILT2007"
        {
            $Repository = "UTMO-DSCResources"
            break
        }

        default
        {
            $Repository = "DSCResources"
        }
    }

    $repoExsists = Get-PSRepository -Name $Repository -ErrorAction SilentlyContinue

    if(-not $repoExsists)
    {
        Register-PSRepository -Name "DSCResources" -SourceLocation "https://packages.public.utmonline.net/nuget/DSCResources/" -InstallationPolicy Trusted
    }

    Write-Output "Validate and install required modules"
    $loopExceptions = @()

    foreach($module in $moduleManifest)
    {
        $Name = $module.Name
        $Version = $module.Version

        $fullyQualifiedName = @{ModuleName=$Name;ModuleVersion=$Version}

        $installCount = Get-Module -FullyQualifiedName $fullyQualifiedName -ListAvailable | Measure-Object | Select-Object -ExpandProperty Count

        if($installCount -eq 0)
        {
            Write-Host "Installing Module $Name"
            $loopCount = 0
            $installFinished = $false

            do
            {
                try
                {
                    Write-Host "Attempt #$loopCount" -ForegroundColor Cyan
                    $parameters = @{Name = $Name; RequiredVersion = $Version; Repository = $Repository; Scope = 'CurrentUser'; ErrorAction = 'Stop'}

                    if($module.AllowClobber)
                    {
                        $parameters.Add("AllowClobber",$true)
                    }

                    Install-Module @parameters
                    $loopCount = $MaxRetryCount + 1
                    Write-Host "Finished installing $Name" -ForegroundColor Green
                }
                catch
                {
                    $loopCount++
                    $loopExceptions += $_
                    Write-Host "Failed To Install $Name"
                    Write-Host "ErrorType: $($_.Exception.GetType().Name)"
                    Write-Host "Error Message: $($_.Exception.Message)"
                }
            }
            while($loopCount -le $MaxRetryCount)
        }
        else
        {
            Write-Host "$Name already installed" -ForegroundColor Green
        }
    }

    if(-not $repoExsists)
    {
        Unregister-PSRepository -Name "DSCResources"
    }

    if($loopExceptions.Count -gt 0)
    {
        $aggEx = [AggregateException]::new($loopExceptions)

        throw $aggEx

        [Environment]::ExitCode(1)
    }
    else
    {
        Write-Output "Finished Installing Modules"
    }