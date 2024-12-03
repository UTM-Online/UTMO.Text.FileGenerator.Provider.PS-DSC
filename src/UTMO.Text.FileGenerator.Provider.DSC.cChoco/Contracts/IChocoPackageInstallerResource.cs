namespace UTMO.Text.FileGenerator.Provider.DSC.cChoco.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

public interface IChocoPackageInstallerResource : IDscResourceConfig
{
    string PackageName { get; set; }

    string PackageSource { get; set; }

    bool AutoUpgrade { get; set; }

    string Parameters { get; set; }
}