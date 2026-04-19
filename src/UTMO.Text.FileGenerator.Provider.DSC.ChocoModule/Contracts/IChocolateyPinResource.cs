namespace UTMO.Text.FileGenerator.Provider.DSC.ChocoModule.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

/// <summary>
/// Contract for the ChocolateyPin DSC resource.
/// Pins or unpins Chocolatey packages to/from a specific version.
/// </summary>
public interface IChocolateyPinResource : IDscResourceConfig
{
    string PackageName { get; set; }

    string? Version { get; set; }
}


