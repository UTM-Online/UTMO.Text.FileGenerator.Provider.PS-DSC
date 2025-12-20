namespace UTMO.Text.FileGenerator.Provider.DSC.WindowsDefender;

/// <summary>
/// Submit samples consent type for Windows Defender.
/// Values match the official WindowsDefender DSC resource.
/// </summary>
public enum SubmitSamplesConsentType
{
    AlwaysPrompt = 0,
    SendSafeSamples = 1,
    NeverSend = 2,
    SendAllSamples = 3,
}

