namespace UTMO.Text.FileGenerator.Provider.DSC.WindowsDefender;

/// <summary>
/// Scan type for Windows Defender scheduled scans.
/// Values match the official WindowsDefender DSC resource.
/// </summary>
public enum ScanType
{
    QuickScan = 1,
    FullScan = 2,
}
