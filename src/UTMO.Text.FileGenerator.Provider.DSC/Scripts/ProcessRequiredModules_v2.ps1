param(
    [Parameter(Mandatory = $true)]
    [string]$ManifestPath,
    
    [Parameter(Mandatory = $true)]
    [string]$OutputPath,
    
    [switch]$NoArchive
)

$ErrorActionPreference = 'Stop'
$InformationPreference = 'Continue'

#region Functions

function Write-ScriptLog {
    param(
        [Parameter(Mandatory = $true)]
        [string]$Message,
     
        [ValidateSet('Information', 'Warning', 'Error')]
    [string]$Level = 'Information'
    )
    
    $timestamp = Get-Date -Format 'yyyy-MM-dd HH:mm:ss'
    $logMessage = "[$timestamp] [$Level] $Message"
    
    switch ($Level) {
        'Information' { Write-Host $logMessage }
      'Warning' { Write-Warning $logMessage }
        'Error' { Write-Error $logMessage }
    }
}

function Initialize-UserModulePath {
    <#
    .SYNOPSIS
 Ensures PowerShell module paths are correctly set for current user
    #>
    
    $userModulesPath = Join-Path -Path $env:USERPROFILE -ChildPath 'Documents\WindowsPowerShell\Modules'
    
    if (-not (Test-Path -Path $userModulesPath)) {
        Write-ScriptLog -Message "Creating user modules directory: $userModulesPath"
   New-Item -Path $userModulesPath -ItemType Directory -Force | Out-Null
    }
    
    $currentPSModulePath = $env:PSModulePath
    if (-not $currentPSModulePath.Contains($userModulesPath)) {
   $env:PSModulePath = "$userModulesPath;$currentPSModulePath"
        Write-ScriptLog -Message "Updated PSModulePath to include: $userModulesPath"
    }
    
    return $userModulesPath
}

function Test-ManifestFile {
    param(
        [Parameter(Mandatory = $true)]
        [string]$Path
    )
    
    if (-not (Test-Path -Path $Path)) {
        throw "Manifest not found at location: $Path"
    }
    
    try {
        $content = Get-Content -Path $Path -Raw -ErrorAction Stop
        $json = ConvertFrom-Json -InputObject $content -ErrorAction Stop
        return $json
    }
    catch {
 throw "Failed to read or parse manifest file: $_"
    }
}

function Test-RequiredModules {
    param(
        [Parameter(Mandatory = $true)]
   [array]$Packages
    )
    
    Write-ScriptLog -Message 'Verifying required modules are available...'
    
    foreach ($package in $Packages) {
        $moduleName = $package.Name
    $moduleVersion = $package.Version
        
        $installedModule = Get-InstalledModule -Name $moduleName -RequiredVersion $moduleVersion -ErrorAction SilentlyContinue
      
        if (-not $installedModule) {
       Write-ScriptLog -Message "Module $moduleName v$moduleVersion is not installed. This may cause Save-Module to fail." -Level Warning
    
            $allVersions = Get-InstalledModule -Name $moduleName -AllVersions -ErrorAction SilentlyContinue
       if ($allVersions) {
     $versionList = ($allVersions | ForEach-Object { $_.Version.ToString() }) -join ', '
        Write-ScriptLog -Message "Available versions of $moduleName : $versionList"
            }
        }
        else {
            Write-ScriptLog -Message "Module $moduleName v$moduleVersion is available"
 }
    }
}

function New-TempDirectory {
    <#
    .SYNOPSIS
    Creates a temporary directory with a random name
    #>
    
    $randomId = Get-Random
    $tempPath = Join-Path -Path $env:TEMP -ChildPath $randomId
    
    Write-ScriptLog -Message "Creating temporary directory: $tempPath"
    New-Item -Path $tempPath -ItemType Directory -Force | Out-Null
    
    return $tempPath
}

function Remove-GitDirectories {
    param(
        [Parameter(Mandatory = $true)]
        [string]$Path
    )
    
    $gitDirs = Get-ChildItem -Path $Path -Recurse -Directory -Filter '.git' -ErrorAction SilentlyContinue
    
    foreach ($gitDir in $gitDirs) {
        Write-ScriptLog -Message "Removing .git directory: $($gitDir.FullName)"
        Remove-Item -Path $gitDir.FullName -Recurse -Force -Confirm:$false -ErrorAction SilentlyContinue
    }
}

function Save-PackageModule {
    param(
    [Parameter(Mandatory = $true)]
        [psobject]$Package,
   
      [Parameter(Mandatory = $true)]
      [string]$DestinationPath,
        
[Parameter(Mandatory = $true)]
        [string]$OutputPath,
        
        [switch]$NoArchive
    )
    
    $moduleName = $Package.Name
    $moduleVersion = $Package.Version
    
    Write-ScriptLog -Message "Processing package: $moduleName"
    
    # Save module to destination path
    $saveParams = @{
        Name    = $moduleName
        RequiredVersion = $moduleVersion
        Repository= 'DSCResources'
        Path            = $DestinationPath
    }
    
    Save-Module @saveParams | Out-Null
    Write-ScriptLog -Message "Saved module $moduleName v$moduleVersion to: $DestinationPath"
    
    # Remove .git directories
    Remove-GitDirectories -Path $DestinationPath
    
    if (-not $NoArchive) {
        # Create archive
        $modulePath = Join-Path -Path $DestinationPath -ChildPath $moduleName
     $versionPath = Join-Path -Path $modulePath -ChildPath $moduleVersion
        $sourcePattern = Join-Path -Path $versionPath -ChildPath '*'
        
      # Determine archive filename
        if ($Package.UseAlternateFormat -and $Package.AlternateVersion) {
    $archiveFileName = "$moduleName`_$($Package.AlternateVersion).zip"
        }
        else {
            $archiveFileName = "$moduleName`_$moduleVersion.zip"
   }
        
        $archivePath = Join-Path -Path $OutputPath -ChildPath $archiveFileName
        
  Write-ScriptLog -Message "Creating archive: $archiveFileName"
        Compress-Archive -Path $sourcePattern -DestinationPath $archivePath -Force | Out-Null
        Write-ScriptLog -Message "Archive created successfully: $archivePath"
    }
}

#endregion

#region Main Script

try {
Write-ScriptLog -Message '========================================='
    Write-ScriptLog -Message 'ProcessRequiredModules Script Starting'
    Write-ScriptLog -Message '========================================='
    
    # Display environment information
    Write-ScriptLog -Message "PSModulePath: $($env:PSModulePath)"
    Write-ScriptLog -Message "User Profile: $env:USERPROFILE"
    Write-ScriptLog -Message "Working Directory: $(Get-Location)"
    Write-ScriptLog -Message "Manifest Path: $ManifestPath"
    Write-ScriptLog -Message "Output Path: $OutputPath"
    Write-ScriptLog -Message "NoArchive Mode: $NoArchive"
    
    # Initialize user module path
    $userModulePath = Initialize-UserModulePath
    
    # Validate and load manifest
    Write-ScriptLog -Message 'Loading manifest file...'
    $packages = Test-ManifestFile -Path $ManifestPath
    Write-ScriptLog -Message "Loaded $($packages.Count) package(s) from manifest"
    
    # Verify required modules
    Test-RequiredModules -Packages $packages
    
    # Create output directory
  $modulesOutputPath = Join-Path -Path $OutputPath -ChildPath 'Modules'
    if (-not (Test-Path -Path $modulesOutputPath)) {
  Write-ScriptLog -Message "Creating output directory: $modulesOutputPath"
        New-Item -Path $modulesOutputPath -ItemType Directory -Force | Out-Null
    }
    
    # Determine working path
    if ($NoArchive) {
      $workingPath = $modulesOutputPath
        Write-ScriptLog -Message 'NoArchive mode: Saving modules directly to output path'
    }
    else {
        $workingPath = New-TempDirectory
        Write-ScriptLog -Message "Archive mode: Using temporary directory: $workingPath"
    }
    
    # Process each package
    $failedPackages = @()
    $successCount = 0
    
    foreach ($package in $packages) {
     try {
    Save-PackageModule -Package $package -DestinationPath $workingPath -OutputPath $modulesOutputPath -NoArchive:$NoArchive
            $successCount++
        }
   catch {
    $errorInfo = @{
      Package   = $package.Name
    Version   = $package.Version
            Exception = $_
 }
    $failedPackages += $errorInfo
            
          Write-ScriptLog -Message "Failed to process module $($package.Name): $($_.Exception.Message)" -Level Warning
        }
    }
    
    # Summary
    Write-ScriptLog -Message '========================================='
    Write-ScriptLog -Message 'Processing Complete'
    Write-ScriptLog -Message "Successfully processed: $successCount/$($packages.Count) packages"
    
    if ($failedPackages.Count -gt 0) {
   Write-ScriptLog -Message "Failed packages: $($failedPackages.Count)" -Level Warning
     foreach ($failed in $failedPackages) {
            Write-ScriptLog -Message "  - $($failed.Package) v$($failed.Version): $($failed.Exception.Message)" -Level Warning
     }
    }
    
    Write-ScriptLog -Message '========================================='
    
    # Cleanup
    if ($env:SkipCleanup -ne '1' -and -not $NoArchive -and (Test-Path -Path $workingPath)) {
        Write-ScriptLog -Message 'Cleaning up temporary directory...'
        Remove-Item -Path $workingPath -Recurse -Force -Confirm:$false -ErrorAction SilentlyContinue
     Write-ScriptLog -Message 'Cleanup complete'
    }
    elseif ($env:SkipCleanup -eq '1') {
     Write-ScriptLog -Message "Skipping cleanup (SkipCleanup flag set). Temp directory: $workingPath"
  }
    
    # Exit with error if any packages failed
    if ($failedPackages.Count -gt 0) {
        $errorMessage = "Failed to process $($failedPackages.Count) package(s)"
   Write-ScriptLog -Message $errorMessage -Level Error
        
        # Create aggregate exception message
        $exceptionMessages = $failedPackages | ForEach-Object {
      "$($_.Package) v$($_.Version): $($_.Exception.Message)"
        }
        $aggregateMessage = $exceptionMessages -join "`n"
      
        throw "Package processing failed:`n$aggregateMessage"
    }
    
    Write-ScriptLog -Message 'All packages processed successfully'
    exit 0
}
catch {
    Write-ScriptLog -Message "Critical error: $($_.Exception.Message)" -Level Error
    Write-ScriptLog -Message "Stack trace: $($_.ScriptStackTrace)" -Level Error
    
  # Attempt cleanup even on failure
    if ($workingPath -and (Test-Path -Path $workingPath) -and -not $NoArchive) {
        Write-ScriptLog -Message 'Attempting cleanup after error...'
    Remove-Item -Path $workingPath -Recurse -Force -Confirm:$false -ErrorAction SilentlyContinue
    }
    
    exit 1
}

#endregion
