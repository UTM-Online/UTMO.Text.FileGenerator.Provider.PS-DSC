[CmdletBinding()]
param(
    [Parameter(Mandatory=$true)]
    [string]$moduleManifestPath
)

$ErrorActionPreference = 'Stop'

# Ensure PowerShell module paths are correctly set for current user
$userModulesPath = Join-Path -Path $env:USERPROFILE -ChildPath "Documents\WindowsPowerShell\Modules"
if (-not (Test-Path $userModulesPath)) {
    New-Item -Path $userModulesPath -ItemType Directory -Force | Out-Null
}

$currentPSModulePath = $env:PSModulePath
if (-not $currentPSModulePath.Contains($userModulesPath)) {
    $env:PSModulePath = "$userModulesPath;$currentPSModulePath"
    Write-Output "Updated PSModulePath to include user modules directory: $userModulesPath"
}

Write-Output "Current PSModulePath: $($env:PSModulePath)"
Write-Output "User modules directory: $userModulesPath"

Write-Output "Loading Module Manifest File"

$moduleManifest = Get-Content $moduleManifestPath | ConvertFrom-Json

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

$repoExists = Get-PSRepository -Name $Repository -ErrorAction SilentlyContinue

if(-not $repoExists)
{
    Register-PSRepository -Name "DSCResources" -SourceLocation "https://packages.public.utmonline.net/nuget/DSCResources/" -InstallationPolicy Trusted
}

Write-Output "Validate and install required modules"
$moduleErrors = @()

$totalModules = $moduleManifest.Count
$currentModule = 0

foreach($module in $moduleManifest)
{
    $currentModule++
    $Name = $module.Name

    # Determine which version to use based on UseAlternateFormat property
    if($module.UseAlternateFormat -eq $true -and $module.AlternateVersion)
    {
        $Version = $module.AlternateVersion
        Write-Information "Using alternate version for $Name`: $Version" -InformationAction Continue
    }
    else
    {
        $Version = $module.Version
    }

    Write-Progress -Activity "Installing Required Modules" -Status "Processing $Name v$Version" -PercentComplete (($currentModule / $totalModules) * 100)

    $fullyQualifiedName = @{ModuleName=$Name;ModuleVersion=$Version}

    $installedModule = Get-InstalledModule -Name $Name -RequiredVersion $Version -ErrorAction SilentlyContinue

    if(-not $installedModule)
    {
        Write-Information "Installing Module $Name" -InformationAction Continue
        $loopCount = 0

        do
        {
            try
            {
                Write-Verbose "Attempt #$loopCount for module $Name" -Verbose

                # Determine version to use - append .0 if this is the second attempt and version is 3-digit format
                $versionToUse = $Version
                if($loopCount -eq 1)
                {
                    # Count the number of dots to determine if it's a 3-digit version (2 dots) vs 4-digit version (3 dots)
                    $dotCount = ($Version.ToCharArray() | Where-Object { $_ -eq '.' }).Count
                    if($dotCount -eq 2)
                    {
                        $versionToUse = "$Version.0"
                        Write-Information "Attempting with modified version: $versionToUse" -InformationAction Continue
                    }
                }

                $parameters = @{Name = $Name; RequiredVersion = $versionToUse; Repository = $Repository; Scope = 'CurrentUser'; ErrorAction = 'Stop'}

                if($module.AllowClobber)
                {
                    $parameters.Add("AllowClobber",$true)
                }

                Install-Module @parameters
                $loopCount = $MaxRetryCount + 1
                Write-Information "Finished installing $Name" -InformationAction Continue
            }
            catch
            {
                $loopCount++
                $errorDetails = @{
                    ModuleName = $Name
                    ModuleVersion = $Version
                    Attempt = $loopCount
                    Exception = $_
                    ErrorType = $_.Exception.GetType().Name
                    ErrorMessage = $_.Exception.Message
                }
                $moduleErrors += $errorDetails
                Write-Warning "Failed To Install $Name (Attempt $loopCount)"
                Write-Warning "ErrorType: $($_.Exception.GetType().Name)"
                Write-Warning "Error Message: $($_.Exception.Message)"
            }
        }
        while($loopCount -le $MaxRetryCount)
    }
    else
    {
        Write-Information "$Name already installed" -InformationAction Continue
    }
}

Write-Progress -Activity "Installing Required Modules" -Completed

if(-not $repoExists)
{
    Unregister-PSRepository -Name "DSCResources"
}

Write-Output "Verifying module installations..."

# Verify all modules are properly installed and accessible
$verificationErrors = @()
foreach($module in $moduleManifest)
{
    $Name = $module.Name

    # Determine which version to use based on UseAlternateFormat property
    if($module.UseAlternateFormat -eq $true -and $module.AlternateVersion)
    {
        $Version = $module.AlternateVersion
    }
    else
    {
        $Version = $module.Version
    }

    # Check if module is installed
    $installedModule = Get-InstalledModule -Name $Name -RequiredVersion $Version -ErrorAction SilentlyContinue
    if(-not $installedModule)
    {
        # Try different version formats to handle installation/directory naming mismatches
        $versionsToTry = @()

        # If module not found with alternate version, try with standard version
        if($module.UseAlternateFormat -eq $true -and $module.AlternateVersion -and $Version -eq $module.AlternateVersion)
        {
            Write-Information "Module $Name v$Version not found, trying standard version $($module.Version)" -InformationAction Continue
            $versionsToTry += $module.Version
        }

        # Try truncated version (remove trailing .0) to handle directory naming differences
        if($Version -match '\.0$')
        {
            $truncatedVersion = $Version -replace '\.0$', ''
            Write-Information "Module $Name v$Version not found, trying truncated version $truncatedVersion" -InformationAction Continue
            $versionsToTry += $truncatedVersion
        }

        # Try extended version (add .0) in case the manifest has shorter version
        if(($Version.Split('.').Count -eq 3) -and ($Version -notmatch '\.0$'))
        {
            $extendedVersion = "$Version.0"
            Write-Information "Module $Name v$Version not found, trying extended version $extendedVersion" -InformationAction Continue
            $versionsToTry += $extendedVersion
        }

        # Try each version format
        foreach($versionToTry in $versionsToTry)
        {
            $installedModule = Get-InstalledModule -Name $Name -RequiredVersion $versionToTry -ErrorAction SilentlyContinue
            if($installedModule)
            {
                Write-Information "Found module $Name with version $versionToTry" -InformationAction Continue
                $Version = $versionToTry
                break
            }
        }

        if(-not $installedModule)
        {
            $verificationErrors += "Module $Name v$Version was not found after installation (also tried versions: $($versionsToTry -join ', '))"
            continue
        }
    }

    # Try to import the module to verify it's accessible
    try
    {
        Import-Module -Name $Name -RequiredVersion $Version -Force -ErrorAction Stop
        Write-Information "Successfully verified module $Name v$Version" -InformationAction Continue
    }
    catch
    {
        $importSuccess = $false
        $versionsAttempted = @($Version)

        # Build list of version formats to try
        $importVersionsToTry = @()

        # If using alternate version, try standard version
        if($module.UseAlternateFormat -eq $true -and $module.AlternateVersion -and $Version -eq $module.AlternateVersion)
        {
            $importVersionsToTry += $module.Version
        }
        elseif($module.UseAlternateFormat -eq $true -and $module.AlternateVersion -and $Version -eq $module.Version)
        {
            $importVersionsToTry += $module.AlternateVersion
        }

        # Try truncated version (remove trailing .0)
        if($Version -match '\.0$')
        {
            $truncatedVersion = $Version -replace '\.0$', ''
            $importVersionsToTry += $truncatedVersion
        }

        # Try extended version (add .0)
        if(($Version.Split('.').Count -eq 3) -and ($Version -notmatch '\.0$'))
        {
            $extendedVersion = "$Version.0"
            $importVersionsToTry += $extendedVersion
        }

        # Remove duplicates and try each version
        $importVersionsToTry = $importVersionsToTry | Select-Object -Unique

        foreach($versionToImport in $importVersionsToTry)
        {
            try
            {
                Write-Information "Import failed for $Name v$Version, trying version $versionToImport" -InformationAction Continue
                Import-Module -Name $Name -RequiredVersion $versionToImport -Force -ErrorAction Stop
                Write-Information "Successfully verified module $Name v$versionToImport" -InformationAction Continue
                $importSuccess = $true
                $versionsAttempted += $versionToImport
                break
            }
            catch
            {
                $versionsAttempted += $versionToImport
                # Continue to next version
            }
        }

        if(-not $importSuccess)
        {
            $verificationErrors += "Module $Name could not be imported with any version formats tried: $($versionsAttempted -join ', '). Last error: $($_.Exception.Message)"
        }
    }
}

Write-Output "Module installation verification complete"
Write-Output "PSModulePath: $($env:PSModulePath)"
Write-Output "Current user modules path: $($env:USERPROFILE)\Documents\WindowsPowerShell\Modules"

if($moduleErrors.Count -gt 0)
{
    $errorSummary = $moduleErrors | ForEach-Object { "Module: $($_.ModuleName) v$($_.ModuleVersion) - Attempt $($_.Attempt): $($_.ErrorMessage)" }
    $combinedMessage = "Failed to install modules:`n" + ($errorSummary -join "`n")
    $aggEx = [System.Exception]::new($combinedMessage)
    [Environment]::ExitCode = 1
    throw $aggEx
}

if($verificationErrors.Count -gt 0)
{
    $combinedMessage = "Module verification failed:`n" + ($verificationErrors -join "`n")
    $aggEx = [System.Exception]::new($combinedMessage)
    [Environment]::ExitCode = 1
    throw $aggEx
}

Write-Output "Finished Installing and Verifying Modules"

