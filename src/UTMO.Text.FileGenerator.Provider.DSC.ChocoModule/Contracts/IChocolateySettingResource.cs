namespace UTMO.Text.FileGenerator.Provider.DSC.ChocoModule.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

/// <summary>
/// Contract for the ChocolateySetting DSC resource.
/// Sets or unsets Chocolatey configuration settings.
/// </summary>
public interface IChocolateySettingResource : IDscResourceConfig
{
    string SettingName { get; set; }

    string? Value { get; set; }
}


