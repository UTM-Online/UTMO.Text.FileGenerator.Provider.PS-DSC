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
    $Version = $module.Version

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
                $parameters = @{Name = $Name; RequiredVersion = $Version; Repository = $Repository; Scope = 'CurrentUser'; ErrorAction = 'Stop'}

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
    $Version = $module.Version
    
    # Check if module is installed
    $installedModule = Get-InstalledModule -Name $Name -RequiredVersion $Version -ErrorAction SilentlyContinue
    if(-not $installedModule)
    {
        $verificationErrors += "Module $Name v$Version was not found after installation"
        continue
    }
    
    # Try to import the module to verify it's accessible
    try
    {
        Import-Module -Name $Name -RequiredVersion $Version -Force -ErrorAction Stop
        Write-Information "Successfully verified module $Name v$Version" -InformationAction Continue
    }
    catch
    {
        $verificationErrors += "Module $Name v$Version could not be imported: $($_.Exception.Message)"
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

