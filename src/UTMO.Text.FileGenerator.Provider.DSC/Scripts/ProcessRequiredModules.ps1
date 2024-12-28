param(
    [string]$ManifestPath,
    [string]$OutputPath,
    [switch]$NoArchive
)

$ErrorActionPreference = 'Stop'

$loopExceptions = @()

if(-not (Test-Path -Path $ManifestPath))
{
    throw "Manifest not found at location $ManifestPath"
}

$g = Get-Content -Path $ManifestPath -Raw | ConvertFrom-Json

if (-not $NoArchive)
{
    $tmpId = $(Get-Random)
    $tempFolder = Join-Path -Path $Env:TEMP -ChildPath $tmpId
    New-Item -Path $env:TEMP -Name $tmpId -ItemType Directory | Out-Null
}

$OutPath = Join-Path -Path $OutputPath -ChildPath "Modules"

if(-not (Test-Path -Path $OutPath))
{
    New-Item -Path $OutPath -ItemType Directory | Out-Null
}

try
{
    foreach($package in $g)
    {
        try
        {
            Write-Host "Processing Package: $($package.Name)"
            
            $params = @{
                Name = $package.Name
                RequiredVersion = $package.Version
                Repository = 'DSCResources'
            }
            $gciParams = @{
                Recurse = $true
                Directory = $true
                'Filter' = '.git'
            }
            
            if($NoArchive)
            {
                $params.Add('Path', $OutPath)
                $gciParams.Add('Path', $OutPath)
                New-Item -Path $OutPath -ItemType Directory -ErrorAction SilentlyContinue | Out-Null
            }
            else
            {
                $gciParams.Add('Path', $tempFolder)
                $params.Add('Path', $tempFolder)
            }
            
            Save-Module @params | Out-Null
            Get-ChildItem @gciParams | Remove-Item -Recurse -Force -Confirm:$false | Out-Null

            if(-not $NoArchive)
            {
                $packagePath = Join-Path -Path $tempFolder -ChildPath $(Join-Path -Path $package.Name -ChildPath $package.Version)
                $packagePath += "\*"

                $FileName = "$($package.Name)_$($package.Version).zip"
                Compress-Archive -Path $packagePath -DestinationPath $(Join-Path -Path $OutPath -ChildPath $FileName) -Force | Out-Null
            }
        }
        catch
        {
            $loopExceptions += $_
            Write-Warning -Message "Unable to procees module $($package.Name) due to exception of type $($_.GetType.Name) with message $($_.Exception.Message)"
        }
    }
}
finally
{
    if($env:SkipCleanup -ne 1 -and -not $NoArchive)
    {
        Write-Host "Cleaning Up"
        Remove-Item -Path $tempFolder -Recurse -Force -Confirm:$false | Out-Null
    }
    else
    {
        Write-Host "Skipping Cleanup"
    }
}

if($loopExceptions.Count -gt 0)
{
    $aggEx = [AggregateException]::new($loopExceptions)

    throw $aggEx

    [Environment]::ExitCode(1)
}