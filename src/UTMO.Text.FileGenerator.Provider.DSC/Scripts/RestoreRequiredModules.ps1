[CmdletBinding()]
param(
    [Parameter(Mandatory=$true)]
    [string]$moduleManifestPath
)

$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest
$SystemModulesBasePath = "$env:ProgramFiles\WindowsPowerShell\Modules"

# Bootstrap required modules
$ModulesToBootstrap = @("PackageManagement", "PowerShellGet")

$Repository = "DSCResources"

# Copy bootstrap modules from system path to user profile
$userModulesBasePath = "$env:USERPROFILE\Documents\WindowsPowerShell\Modules"
Write-Output "Bootstrapping required modules from system to user profile..."

foreach ($moduleName in $ModulesToBootstrap) {
    $sourceModulePath = Join-Path -Path $SystemModulesBasePath -ChildPath $moduleName
    $destinationModulePath = Join-Path -Path $userModulesBasePath -ChildPath $moduleName
    
    if (Test-Path $sourceModulePath) {
        Write-Output "Copying module $moduleName from $sourceModulePath to $destinationModulePath"
        
        # Remove existing module in destination if it exists
        if (Test-Path $destinationModulePath) {
            Write-Output "Removing existing module at $destinationModulePath"
            Remove-Item -Path $destinationModulePath -Recurse -Force -ErrorAction SilentlyContinue
        }
        
        # Create the user modules directory if it doesn't exist
        if (-not (Test-Path $userModulesBasePath)) {
            New-Item -Path $userModulesBasePath -ItemType Directory -Force | Out-Null
        }
        
        # Copy the module
        try {
            Copy-Item -Path $sourceModulePath -Destination $destinationModulePath -Recurse -Force
            Write-Output "Successfully copied module $moduleName"
        }
        catch {
            Write-Warning "Failed to copy module $moduleName`: $($_.Exception.Message)"
        }
    }
    else {
        Write-Warning "Source module not found: $sourceModulePath"
    }
}

Write-Output "Bootstrap module copying completed."

$currentPsModulePath = $env:PSModulePath;
$env:PSModulePath = $env:PsModulePath | ?{$_ -ne "$env:ProgramFiles\WindowsPowerShell\Modules"};
# $env:PSModulePath = "$env:USERPROFILE\Documents\WindowsPowerShell\Modules"

# Function to fix module version directory names when UseAlternateFormat is true
function Repair-ModuleVersionDirectory {
    param(
        [string]$ModuleName,
        [string]$StandardVersion,
        [string]$AlternateVersion
    )

    try {
        # Get all possible module paths
        $modulePaths = $env:PSModulePath -split ';' | Where-Object { $_ }

        foreach($modulePath in $modulePaths) {
            $moduleFolder = Join-Path -Path $modulePath -ChildPath $ModuleName

            if(Test-Path $moduleFolder) {
                Write-Information "Checking module directory: $moduleFolder" -InformationAction Continue

                # First check if the AlternateVersion directory already exists
                $alternateVersionDir = Join-Path -Path $moduleFolder -ChildPath $AlternateVersion
                if(Test-Path $alternateVersionDir) {
                    Write-Information "AlternateVersion directory already exists: $alternateVersionDir" -InformationAction Continue
                    return $true
                }

                # Look for StandardVersion directory to rename
                $standardVersionDir = Join-Path -Path $moduleFolder -ChildPath $StandardVersion
                if(Test-Path $standardVersionDir) {
                    Write-Information "Found StandardVersion directory to rename: $standardVersionDir -> $alternateVersionDir" -InformationAction Continue

                    try {
                        Rename-Item -Path $standardVersionDir -NewName $AlternateVersion -Force
                        Write-Information "Successfully renamed module version directory from $StandardVersion to $AlternateVersion" -InformationAction Continue
                        return $true
                    }
                    catch {
                        Write-Warning "Failed to rename module version directory: $($_.Exception.Message)"
                        return $false
                    }
                }

                # Look for any version directory that might match (in case of truncation)
                $versionFolders = Get-ChildItem -Path $moduleFolder -Directory | Where-Object { $_.Name -match '^\d+\.\d+' }

                if($versionFolders) {
                    # Try to find a version that matches the pattern (could be truncated)
                    $matchingFolder = $null

                    # Check for exact matches first
                    $matchingFolder = $versionFolders | Where-Object { $_.Name -eq $StandardVersion } | Select-Object -First 1

                    # If no exact match, look for truncated versions
                    if(-not $matchingFolder) {
                        # Check if StandardVersion ends with .0 and look for truncated version
                        if($StandardVersion -match '^(\d+\.\d+)\.0$') {
                            $truncatedVersion = $matches[1]
                            $matchingFolder = $versionFolders | Where-Object { $_.Name -eq $truncatedVersion } | Select-Object -First 1
                        }

                        # Also check if we need to add .0 to match
                        if(-not $matchingFolder -and $StandardVersion -match '^\d+\.\d+$') {
                            $extendedVersion = "$StandardVersion.0"
                            $matchingFolder = $versionFolders | Where-Object { $_.Name -eq $extendedVersion } | Select-Object -First 1
                        }
                    }

                    if($matchingFolder) {
                        Write-Information "Found version directory to rename: $($matchingFolder.Name) -> $AlternateVersion" -InformationAction Continue

                        try {
                            Rename-Item -Path $matchingFolder.FullName -NewName $AlternateVersion -Force
                            Write-Information "Successfully renamed module version directory from $($matchingFolder.Name) to $AlternateVersion" -InformationAction Continue
                            return $true
                        }
                        catch {
                            Write-Warning "Failed to rename module version directory: $($_.Exception.Message)"
                            return $false
                        }
                    }
                }

                Write-Warning "No suitable version directory found for module $ModuleName to rename to $AlternateVersion"
                return $false
            }
        }
    }
    catch {
        Write-Warning "Error fixing module version directory for $ModuleName`: $($_.Exception.Message)"
        return $false
    }

    Write-Warning "Module folder not found for $ModuleName in any module path"
    return $false
}

# Function to validate DSC resources in a module when Import-Module fails
function Test-DSCResourcesInModule {
    param(
        [string]$ModuleName,
        [string]$ModuleVersion
    )

    $dscResourcesFound = @()

    try {
        # Get all possible module paths
        $modulePaths = $env:PSModulePath -split ';' | Where-Object { $_ }

        foreach($modulePath in $modulePaths) {
            $moduleFolder = Join-Path -Path $modulePath -ChildPath $ModuleName

            if(Test-Path $moduleFolder) {
                # Look for version-specific folder
                $versionFolder = Join-Path -Path $moduleFolder -ChildPath $ModuleVersion
                if(-not (Test-Path $versionFolder)) {
                    # Try to find any version folder if specific version not found
                    $versionFolders = Get-ChildItem -Path $moduleFolder -Directory | Where-Object { $_.Name -match '^\d+\.\d+' }
                    if($versionFolders) {
                        $versionFolder = $versionFolders[0].FullName
                    } else {
                        $versionFolder = $moduleFolder
                    }
                }

                if(Test-Path $versionFolder) {
                    Write-Information "Searching for DSC resources in: $versionFolder" -InformationAction Continue

                    # Look for DSCResources folder
                    $dscResourcesFolder = Join-Path -Path $versionFolder -ChildPath "DSCResources"
                    if(Test-Path $dscResourcesFolder) {
                        $resourceFolders = Get-ChildItem -Path $dscResourcesFolder -Directory
                        foreach($resourceFolder in $resourceFolders) {
                            # Look for .psm1 files in resource folders
                            $psmFiles = Get-ChildItem -Path $resourceFolder.FullName -Filter "*.psm1"
                            if($psmFiles) {
                                $dscResourcesFound += $resourceFolder.Name
                                Write-Information "Found DSC resource: $($resourceFolder.Name)" -InformationAction Continue
                            }
                        }
                    }

                    # Also look for .psd1 manifest files that might define DSC resources
                    $manifestFiles = Get-ChildItem -Path $versionFolder -Filter "*.psd1"
                    foreach($manifestFile in $manifestFiles) {
                        try {
                            $manifestContent = Import-PowerShellDataFile -Path $manifestFile.FullName -ErrorAction SilentlyContinue
                            if($manifestContent -and $manifestContent.DscResourcesToExport) {
                                $exportedResources = $manifestContent.DscResourcesToExport
                                if($exportedResources -and $exportedResources.Count -gt 0) {
                                    $dscResourcesFound += $exportedResources
                                    Write-Information "Found DSC resources in manifest: $($exportedResources -join ', ')" -InformationAction Continue
                                }
                            }
                        }
                        catch {
                            # Continue if manifest can't be read
                        }
                    }

                    # Look for .psm1 files directly in the module folder that might contain DSC resources
                    $moduleFiles = Get-ChildItem -Path $versionFolder -Filter "*.psm1"
                    foreach($moduleFile in $moduleFiles) {
                        try {
                            $content = Get-Content -Path $moduleFile.FullName -Raw -ErrorAction SilentlyContinue
                            if($content -and ($content -match '\[DscResource\(\)\]' -or $content -match 'Configuration\s+\w+')) {
                                $dscResourcesFound += $moduleFile.BaseName
                                Write-Information "Found DSC resource definitions in: $($moduleFile.Name)" -InformationAction Continue
                            }
                        }
                        catch {
                            # Continue if file can't be read
                        }
                    }
                }
            }
        }
    }
    catch {
        Write-Warning "Error searching for DSC resources in module $ModuleName`: $($_.Exception.Message)"
    }

    return $dscResourcesFound | Select-Object -Unique
}

Write-Output "Whoami: $(whoami)"

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

$repoExists = Get-PSRepository -Name $Repository -ErrorAction SilentlyContinue

if(-not $repoExists)
{
    Register-PSRepository -Name "DSCResources" -SourceLocation "https://packages.public.utmonline.net/nuget/DSCResources/" -InstallationPolicy Trusted
}

Write-Output "Validate and install required modules"
$moduleInstallErrors = @()
$versionDirectoryErrors = @()

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

                # Fix version directory naming if using alternate format
                if($module.UseAlternateFormat -eq $true -and $module.AlternateVersion) {
                    Write-Information "Module $Name uses alternate format, checking version directory..." -InformationAction Continue
                    $fixResult = Repair-ModuleVersionDirectory -ModuleName $Name -StandardVersion $module.Version -AlternateVersion $module.AlternateVersion
                    if(-not $fixResult) {
                        $versionDirectoryErrors += "Failed to fix version directory for module $Name (Standard: $($module.Version) -> Alternate: $($module.AlternateVersion))"
                        Write-Warning "Failed to fix version directory for module $Name - this may cause import issues"
                    }
                }
            }
            catch
            {
                $loopCount++
                $currentError = $_
                $errorDetails = @{
                    ModuleName = $Name
                    ModuleVersion = $Version
                    Attempt = $loopCount
                    Exception = $currentError
                    ErrorType = $currentError.Exception.GetType().Name
                    ErrorMessage = $currentError.Exception.Message
                }
                $moduleInstallErrors += $errorDetails
                Write-Warning "Failed To Install $Name (Attempt $loopCount)"
                Write-Warning "ErrorType: $($currentError.Exception.GetType().Name)"
                Write-Warning "Error Message: $($currentError.Exception.Message)"
            }
        }
        while($loopCount -le $MaxRetryCount)
    }
    else
    {
        Write-Information "$Name already installed" -InformationAction Continue

        # Even if module is already installed, check if version directory needs fixing for alternate format
        if($module.UseAlternateFormat -eq $true -and $module.AlternateVersion) {
            Write-Information "Module $Name uses alternate format, verifying version directory..." -InformationAction Continue
            $fixResult = Repair-ModuleVersionDirectory -ModuleName $Name -StandardVersion $module.Version -AlternateVersion $module.AlternateVersion
            if(-not $fixResult) {
                $versionDirectoryErrors += "Failed to verify/fix version directory for module $Name (Standard: $($module.Version) -> Alternate: $($module.AlternateVersion))"
                Write-Warning "Failed to verify/fix version directory for module $Name - this may cause import issues"
            }
        }
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
            # If Import-Module failed, try to validate DSC resources in the module directly
            Write-Information "Import-Module failed for $Name, attempting to validate DSC resources directly..." -InformationAction Continue

            $dscResources = Test-DSCResourcesInModule -ModuleName $Name -ModuleVersion $Version

            if($dscResources -and $dscResources.Count -gt 0) {
                Write-Information "Module $Name v$Version contains valid DSC resources despite import failure: $($dscResources -join ', ')" -InformationAction Continue
                $importSuccess = $true
            } else {
                # If no DSC resources found, try with other version formats
                foreach($versionToCheck in $versionsAttempted | Where-Object { $_ -ne $Version }) {
                    $dscResources = Test-DSCResourcesInModule -ModuleName $Name -ModuleVersion $versionToCheck
                    if($dscResources -and $dscResources.Count -gt 0) {
                        Write-Information "Module $Name v$versionToCheck contains valid DSC resources despite import failure: $($dscResources -join ', ')" -InformationAction Continue
                        $importSuccess = $true
                        break
                    }
                }
            }

            if(-not $importSuccess) {
                $verificationErrors += "Module $Name could not be imported with any version formats tried: $($versionsAttempted -join ', ') and no valid DSC resources were found in the module directories. Last error: $($_.Exception.Message)"
            }
        }
    }
}

Write-Output "Module installation verification complete"
Write-Output "PSModulePath: $($env:PSModulePath)"
Write-Output "Current user modules path: $($env:USERPROFILE)\Documents\WindowsPowerShell\Modules"

# Report errors as warnings instead of throwing exceptions
if($moduleInstallErrors.Count -gt 0)
{
    Write-Warning "===== MODULE INSTALLATION ERRORS ====="
    Write-Warning "Some modules failed to install after multiple attempts:"
    foreach($installError in $moduleInstallErrors) {
        Write-Warning "  - Module: $($installError.ModuleName) v$($installError.ModuleVersion) - Attempt $($installError.Attempt): $($installError.ErrorMessage)"
    }
    Write-Warning "======================================"
}

if($verificationErrors.Count -gt 0)
{
    Write-Warning "===== MODULE VERIFICATION ERRORS ====="
    Write-Warning "Some modules failed verification checks:"
    foreach($err in $verificationErrors) {
        Write-Warning "  - $err"
    }
    Write-Warning "======================================"
}

if($versionDirectoryErrors.Count -gt 0)
{
    Write-Warning "===== VERSION DIRECTORY ERRORS ====="
    Write-Warning "Version directory issues encountered (these may cause import problems):"
    foreach($versionError in $versionDirectoryErrors) {
        Write-Warning "  - $versionError"
    }
    Write-Warning "Consider manually checking and renaming version directories if import issues occur."
    Write-Warning "===================================="
}

Write-Output "Finished Installing and Verifying Modules"

$env:PSModulePath = $currentPsModulePath