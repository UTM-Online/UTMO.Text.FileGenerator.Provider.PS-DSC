namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Enums;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Models;
public interface IWebApplication : IDscResourceConfig
{
    string Website { get; set; }
    string ApplicationName { get; set; }
    string WebAppPool { get; set; }
    string PhysicalPath { get; set; }
    SslBinding[]? SslFlags { get; set; }
    WebAuthenticationInfo? AuthenticationInfo { get; set; }
    bool? PreloadEnabled { get; set; }
    bool? ServiceAutoStartEnabled { get; set; }
    string? ServiceAutoStartProvider { get; set; }
    string? ApplicationType { get; set; }
    EnabledProtocol[]? EnabledProtocols { get; set; }
}
