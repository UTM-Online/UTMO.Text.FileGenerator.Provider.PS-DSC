﻿namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc;
using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Enums;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.WebAdministrationDscConstants.WebSiteDefaults;
public sealed class WebSiteDefaultsResource : WebAdministrationDscBase, IWebSiteDefaults
{
    private WebSiteDefaultsResource(string name) : base(name) { }
    public string IsSingleInstance
    {
        get => this.PropertyBag.Get(Constants.Properties.IsSingleInstance);
        set => this.PropertyBag.Set(Constants.Properties.IsSingleInstance, value);
    }
    public LogFormat? LogFormat
    {
        get => this.PropertyBag.Get<LogFormat?>(Constants.Properties.LogFormat);
        set => this.PropertyBag.Set(Constants.Properties.LogFormat, value);
    }
    public string? LogDirectory
    {
        get => this.PropertyBag.Get(Constants.Properties.LogDirectory);
        set => this.PropertyBag.Set(Constants.Properties.LogDirectory, value);
    }
    public string? TraceLogDirectory
    {
        get => this.PropertyBag.Get(Constants.Properties.TraceLogDirectory);
        set => this.PropertyBag.Set(Constants.Properties.TraceLogDirectory, value);
    }
    public string? DefaultApplicationPool
    {
        get => this.PropertyBag.Get(Constants.Properties.DefaultApplicationPool);
        set => this.PropertyBag.Set(Constants.Properties.DefaultApplicationPool, value);
    }
    public string? AllowSubDirConfig
    {
        get => this.PropertyBag.Get(Constants.Properties.AllowSubDirConfig);
        set => this.PropertyBag.Set(Constants.Properties.AllowSubDirConfig, value);
    }
    public static WebSiteDefaultsResource Create(string name, Action<IWebSiteDefaults> configure)
    {
        var resource = new WebSiteDefaultsResource(name);
        configure(resource);
        return resource;
    }
    
    public static WebSiteDefaultsResource Create(string name, Action<IWebSiteDefaults> configure, out WebSiteDefaultsResource resource)
    {
        resource = new WebSiteDefaultsResource(name);
        configure(resource);
        return resource;
    }
    public override Task<List<ValidationFailedException>> Validate()
    {
        var errors = this.ValidationBuilder()
            .ValidateStringNotNullOrEmpty(this.IsSingleInstance, nameof(this.IsSingleInstance))
            .errors;
        return Task.FromResult(errors);
    }
    public override string ResourceId => Constants.ResourceId;
}
