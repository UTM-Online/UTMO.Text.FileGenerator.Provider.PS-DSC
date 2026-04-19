namespace UTMO.Text.FileGenerator.Provider.DSC.ChocoModule.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

/// <summary>
/// Contract for the ChocolateySoftware DSC resource.
/// Installs or removes the Chocolatey software (choco.exe) itself.
/// </summary>
public interface IChocolateySoftwareResource : IDscResourceConfig
{
    string? InstallationDirectory { get; set; }

    string? ChocolateyPackageUrl { get; set; }

    string? PackageFeedUrl { get; set; }

    string? Version { get; set; }

    string? ChocoTempDir { get; set; }

    string? ProxyLocation { get; set; }

    bool IgnoreProxy { get; set; }
}

