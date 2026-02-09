namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Enums;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Models;
public interface IWebSite : IDscResourceConfig
{
    string SiteName { get; set; }
    uint? SiteId { get; set; }
    string? PhysicalPath { get; set; }
    WebSiteState? State { get; set; }
    string? ApplicationPool { get; set; }
    WebBindingInfo[]? BindingInfo { get; set; }
    string[]? DefaultPage { get; set; }
    string? EnabledProtocols { get; set; }
    bool? ServerAutoStart { get; set; }
    WebAuthenticationInfo? AuthenticationInfo { get; set; }
    bool? PreloadEnabled { get; set; }
    bool? ServiceAutoStartEnabled { get; set; }
    string? ServiceAutoStartProvider { get; set; }
    string? LogPath { get; set; }
    LogFlags[]? LogFlags { get; set; }
    LogPeriod? LogPeriod { get; set; }
    LogFormat? LogFormat { get; set; }
}
