namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;
public interface IWebConfigPropertyCollection : IDscResourceConfig
{
    string WebsitePath { get; set; }
    string Filter { get; set; }
    string CollectionName { get; set; }
    string ItemName { get; set; }
    string ItemKeyName { get; set; }
    string ItemKeyValue { get; set; }
    string ItemPropertyName { get; set; }
    string? ItemPropertyValue { get; set; }
}
