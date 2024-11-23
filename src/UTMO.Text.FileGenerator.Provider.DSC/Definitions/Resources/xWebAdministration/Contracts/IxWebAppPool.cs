namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.xWebAdministration.Contracts;

public interface IxWebAppPool
{
    string AppPoolName { get; set; }

    xWebAppPoolIdentityType IdentityType { get; set; }
}