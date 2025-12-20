namespace UTMO.Text.FileGenerator.Provider.DSC.WindowsDefender;

/// <summary>
/// Real-time scan direction for Windows Defender.
/// Values match the official WindowsDefender DSC resource.
/// </summary>
public enum ScanDirection
{
    Both = 0,
    Incoming = 1,
    Outgoing = 2,
}

