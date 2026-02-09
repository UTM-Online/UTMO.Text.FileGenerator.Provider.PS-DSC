namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;
public interface IWebConfigProperty : IDscResourceConfig
{
    string WebsitePath { get; set; }
    string Filter { get; set; }
    string PropertyName { get; set; }
    string? Value { get; set; }
}
