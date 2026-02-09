namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;
public interface IIisMimeTypeMapping : IDscResourceConfig
{
    string ConfigurationPath { get; set; }
    string Extension { get; set; }
    string MimeType { get; set; }
}
