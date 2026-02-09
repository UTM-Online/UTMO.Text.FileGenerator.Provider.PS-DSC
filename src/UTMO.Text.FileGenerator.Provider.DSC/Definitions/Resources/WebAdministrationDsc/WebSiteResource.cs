namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc;
using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Enums;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Models;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.WebAdministrationDscConstants.WebSite;
public sealed class WebSiteResource : WebAdministrationDscBase, IWebSite
{
    private WebSiteResource(string name) : base(name) { }
    public string SiteName
    {
        get => this.PropertyBag.Get(Constants.Properties.Name);
        set => this.PropertyBag.Set(Constants.Properties.Name, value);
    }
    public uint? SiteId
    {
        get => this.PropertyBag.Get<uint?>(Constants.Properties.SiteId);
        set => this.PropertyBag.Set(Constants.Properties.SiteId, value);
    }
    public string? PhysicalPath
    {
        get => this.PropertyBag.Get(Constants.Properties.PhysicalPath);
        set => this.PropertyBag.Set(Constants.Properties.PhysicalPath, value);
    }
    public WebSiteState? State
    {
        get => this.PropertyBag.Get<WebSiteState?>(Constants.Properties.State);
        set => this.PropertyBag.Set(Constants.Properties.State, value);
    }
    public string? ApplicationPool
    {
        get => this.PropertyBag.Get(Constants.Properties.ApplicationPool);
        set => this.PropertyBag.Set(Constants.Properties.ApplicationPool, value);
    }
    public WebBindingInfo[]? BindingInfo
    {
        get => this.PropertyBag.Get<WebBindingInfo[]?>(Constants.Properties.BindingInfo);
        set => this.PropertyBag.Set(Constants.Properties.BindingInfo, value);
    }
    public string[]? DefaultPage
    {
        get => this.PropertyBag.Get<string[]?>(Constants.Properties.DefaultPage);
        set => this.PropertyBag.Set(Constants.Properties.DefaultPage, value);
    }
    public string? EnabledProtocols
    {
        get => this.PropertyBag.Get(Constants.Properties.EnabledProtocols);
        set => this.PropertyBag.Set(Constants.Properties.EnabledProtocols, value);
    }
    public bool? ServerAutoStart
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.ServerAutoStart);
        set => this.PropertyBag.Set(Constants.Properties.ServerAutoStart, value);
    }
    public WebAuthenticationInfo? AuthenticationInfo
    {
        get => this.PropertyBag.Get<WebAuthenticationInfo?>(Constants.Properties.AuthenticationInfo);
        set => this.PropertyBag.Set(Constants.Properties.AuthenticationInfo, value);
    }
    public bool? PreloadEnabled
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.PreloadEnabled);
        set => this.PropertyBag.Set(Constants.Properties.PreloadEnabled, value);
    }
    public bool? ServiceAutoStartEnabled
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.ServiceAutoStartEnabled);
        set => this.PropertyBag.Set(Constants.Properties.ServiceAutoStartEnabled, value);
    }
    public string? ServiceAutoStartProvider
    {
        get => this.PropertyBag.Get(Constants.Properties.ServiceAutoStartProvider);
        set => this.PropertyBag.Set(Constants.Properties.ServiceAutoStartProvider, value);
    }
    public string? LogPath
    {
        get => this.PropertyBag.Get(Constants.Properties.LogPath);
        set => this.PropertyBag.Set(Constants.Properties.LogPath, value);
    }
    public LogFlags[]? LogFlags
    {
        get => this.PropertyBag.Get<LogFlags[]?>(Constants.Properties.LogFlags);
        set => this.PropertyBag.Set(Constants.Properties.LogFlags, value);
    }
    public LogPeriod? LogPeriod
    {
        get => this.PropertyBag.Get<LogPeriod?>(Constants.Properties.LogPeriod);
        set => this.PropertyBag.Set(Constants.Properties.LogPeriod, value);
    }
    public LogFormat? LogFormat
    {
        get => this.PropertyBag.Get<LogFormat?>(Constants.Properties.LogFormat);
        set => this.PropertyBag.Set(Constants.Properties.LogFormat, value);
    }
    public static WebSiteResource Create(string name, Action<IWebSite> configure)
    {
        var resource = new WebSiteResource(name);
        configure(resource);
        return resource;
    }
    public override Task<List<ValidationFailedException>> Validate()
    {
        var errors = this.ValidationBuilder()
            .ValidateStringNotNullOrEmpty(this.SiteName, nameof(this.SiteName))
            .errors;
        return Task.FromResult(errors);
    }
    public override string ResourceId => Constants.ResourceId;
}
