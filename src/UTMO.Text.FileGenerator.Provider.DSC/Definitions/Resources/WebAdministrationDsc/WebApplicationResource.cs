namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc;
using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Enums;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Models;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.WebAdministrationDscConstants.WebApplication;
public sealed class WebApplicationResource : WebAdministrationDscBase, IWebApplication
{
    private WebApplicationResource(string name) : base(name) { }
    public string Website
    {
        get => this.PropertyBag.Get(Constants.Properties.Website);
        set => this.PropertyBag.Set(Constants.Properties.Website, value);
    }
    public string ApplicationName
    {
        get => this.PropertyBag.Get(Constants.Properties.Name);
        set => this.PropertyBag.Set(Constants.Properties.Name, value);
    }
    public string WebAppPool
    {
        get => this.PropertyBag.Get(Constants.Properties.WebAppPool);
        set => this.PropertyBag.Set(Constants.Properties.WebAppPool, value);
    }
    public string PhysicalPath
    {
        get => this.PropertyBag.Get(Constants.Properties.PhysicalPath);
        set => this.PropertyBag.Set(Constants.Properties.PhysicalPath, value);
    }
    public SslBinding[]? SslFlags
    {
        get => this.PropertyBag.Get<SslBinding[]?>(Constants.Properties.SslFlags);
        set => this.PropertyBag.Set(Constants.Properties.SslFlags, value);
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
    public string? ApplicationType
    {
        get => this.PropertyBag.Get(Constants.Properties.ApplicationType);
        set => this.PropertyBag.Set(Constants.Properties.ApplicationType, value);
    }
    public EnabledProtocol[]? EnabledProtocols
    {
        get => this.PropertyBag.Get<EnabledProtocol[]?>(Constants.Properties.EnabledProtocols);
        set => this.PropertyBag.Set(Constants.Properties.EnabledProtocols, value);
    }
    public static WebApplicationResource Create(string name, Action<IWebApplication> configure)
    {
        var resource = new WebApplicationResource(name);
        configure(resource);
        return resource;
    }
    public override Task<List<ValidationFailedException>> Validate()
    {
        var errors = this.ValidationBuilder()
            .ValidateStringNotNullOrEmpty(this.Website, nameof(this.Website))
            .ValidateStringNotNullOrEmpty(this.ApplicationName, nameof(this.ApplicationName))
            .ValidateStringNotNullOrEmpty(this.WebAppPool, nameof(this.WebAppPool))
            .ValidateStringNotNullOrEmpty(this.PhysicalPath, nameof(this.PhysicalPath))
            .errors;
        return Task.FromResult(errors);
    }
    public override string ResourceId => Constants.ResourceId;
}
