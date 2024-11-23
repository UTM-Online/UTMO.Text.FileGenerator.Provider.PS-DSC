namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.xWebAdministration.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

public interface IxWebAppPool : IDscResourceConfig
{
    string AppPoolName { get; set; }

    xWebAppPoolIdentityType IdentityType { get; set; }
}