namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Enums;
public interface IIisModule : IDscResourceConfig
{
    string ModulePath { get; set; }
    string ModuleName { get; set; }
    string RequestPath { get; set; }
    string[] Verb { get; set; }
    string? SiteName { get; set; }
    IisModuleType? ModuleType { get; set; }
}
