namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Enums;
public interface IWebAppPoolDefaults : IDscResourceConfig
{
    string IsSingleInstance { get; set; }
    string? ManagedRuntimeVersion { get; set; }
    AppPoolIdentityType? IdentityType { get; set; }
}
