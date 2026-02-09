namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Enums;
public interface ISslSettings : IDscResourceConfig
{
    string SiteName { get; set; }
    SslBinding[] Bindings { get; set; }
}
