param(
    [string]$ManifestPath,
    [string]$OutputPath
)

$ErrorActionPreference = 'Stop'

$loopExceptions = @()

if(-not (Test-Path -Path $ManifestPath))
{
    throw "Manifest not found at location $ManifestPath"
}

$g = Get-Content -Path $ManifestPath -Raw | ConvertFrom-Json

$tmpId = $(Get-Random)
$tempFolder = Join-Path -Path $Env:TEMP -ChildPath $tmpId
New-Item -Path $env:TEMP -Name $tmpId -ItemType Directory | Out-Null

if($SkipCleanup)
{
    Write-Host "Temporary Directory: $tempFolder"
}

$OutPath = Join-Path -Path $OutputPath -ChildPath "Modules"

if(-not (Test-Path -Path $OutPath))
{
    New-Item -Path $OutputPath -Name "Modules" -ItemType Directory | Out-Null
}

try
{
    foreach($package in $g)
    {
        try
        {
            Write-Host "Processing Package: $($package.Name)"
            Save-Module -Name $package.Name -RequiredVersion $package.Version -Path $tempFolder -Repository 'DSCResources' | Out-Null

            $packagePath = Join-Path -Path $tempFolder -ChildPath $(Join-Path -Path $package.Name -ChildPath $package.Version)
            $packagePath += "\*"
            $FileName = "$($package.Name)_$($package.Version).zip"
            Compress-Archive -Path $packagePath -DestinationPath $(Join-Path -Path $OutPath -ChildPath $FileName) -Force | Out-Null
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
    if($env:SkipCleanup -ne 1)
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