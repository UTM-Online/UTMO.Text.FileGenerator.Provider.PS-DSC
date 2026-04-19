namespace UTMO.Text.FileGenerator.Provider.DSC.ChocoModule.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

/// <summary>
/// Contract for the ChocolateyPackage DSC resource.
/// Installs, upgrades, or removes Chocolatey packages.
/// </summary>
public interface IChocolateyPackageResource : IDscResourceConfig
{
    string PackageName { get; set; }

    string? Version { get; set; }

    string? Source { get; set; }

    bool UpdateOnly { get; set; }
}


