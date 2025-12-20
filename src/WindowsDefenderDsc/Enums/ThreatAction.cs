namespace UTMO.Text.FileGenerator.Provider.DSC.WindowsDefender;

/// <summary>
/// Default action for threat levels in Windows Defender.
/// Values match the official WindowsDefender DSC resource.
/// </summary>
public enum ThreatAction
{
    Clean = 1,
    Quarantine = 2,
    Remove = 3,
    Allow = 6,
    UserDefined = 8,
    NoAction = 9,
    Block = 10,
}

