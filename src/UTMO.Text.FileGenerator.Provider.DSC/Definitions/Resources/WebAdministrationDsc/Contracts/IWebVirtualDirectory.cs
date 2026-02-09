namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;
public interface IWebVirtualDirectory : IDscResourceConfig
{
    string Website { get; set; }
    string WebApplication { get; set; }
    string VirtualDirectoryName { get; set; }
    string PhysicalPath { get; set; }
    string? Credential { get; set; }
}
