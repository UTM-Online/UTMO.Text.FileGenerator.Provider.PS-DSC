param(
    [string]$SourceFile,
    [string]$OutputPath,
    [ValidateSet("LCM","CFG")]
    [string]$Mode
)

if($SourceFile -notlike "*.ps1")
{
    write-warning "Source file: `"$SourceFile`" must be a PowerShell script"
    return
}

# Parse the file name from the $sourceFile with out the file extension
$FileName = [System.IO.Path]::GetFileNameWithoutExtension($SourceFile)

switch ($Mode)
{
    "LCM"
    {
        Write-Output "Generating LCM Configuration: $FileName"
        & $SourceFile -OutputPath $OutputPath | Out-Null
    }
    "CFG"
    {
        Write-Output "Generating DSC Configuration: $FileName"
        & $SourceFile -OutputPath $OutputPath | Out-Null
        $itemFullPath = Join-Path -Path $OutputPath -ChildPath "localhost.mof"
        $itemNewFullPath = Join-Path -Path $OutputPath -ChildPath "$($FileName).mof"
        Move-Item -Path $itemFullPath -Destination $itemNewFullPath -Force
    }
}
