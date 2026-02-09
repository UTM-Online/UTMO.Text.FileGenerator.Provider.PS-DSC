namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Enums;
public interface IWebSiteDefaults : IDscResourceConfig
{
    string IsSingleInstance { get; set; }
    LogFormat? LogFormat { get; set; }
    string? LogDirectory { get; set; }
    string? TraceLogDirectory { get; set; }
    string? DefaultApplicationPool { get; set; }
    string? AllowSubDirConfig { get; set; }
}
