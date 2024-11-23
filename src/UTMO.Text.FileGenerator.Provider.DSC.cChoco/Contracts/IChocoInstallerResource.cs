namespace UTMO.Text.FileGenerator.Provider.DSC.cChoco.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

public interface IChocoInstallerResource : IDscResourceConfig
{
    string InstallDirectory { get; set; }

    string ChocoInstallScriptUrl { get; set; }
}