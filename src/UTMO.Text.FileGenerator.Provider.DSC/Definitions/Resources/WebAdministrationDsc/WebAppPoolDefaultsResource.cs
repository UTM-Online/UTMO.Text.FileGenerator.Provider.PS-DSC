﻿namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc;
using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Enums;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.WebAdministrationDscConstants.WebAppPoolDefaults;
public sealed class WebAppPoolDefaultsResource : WebAdministrationDscBase, IWebAppPoolDefaults
{
    private WebAppPoolDefaultsResource(string name) : base(name) { }
    public string IsSingleInstance
    {
        get => this.PropertyBag.Get(Constants.Properties.IsSingleInstance);
        set => this.PropertyBag.Set(Constants.Properties.IsSingleInstance, value);
    }
    public string? ManagedRuntimeVersion
    {
        get => this.PropertyBag.Get(Constants.Properties.ManagedRuntimeVersion);
        set => this.PropertyBag.Set(Constants.Properties.ManagedRuntimeVersion, value);
    }
    public AppPoolIdentityType? IdentityType
    {
        get => this.PropertyBag.Get<AppPoolIdentityType?>(Constants.Properties.IdentityType);
        set => this.PropertyBag.Set(Constants.Properties.IdentityType, value);
    }
    public static WebAppPoolDefaultsResource Create(string name, Action<IWebAppPoolDefaults> configure)
    {
        var resource = new WebAppPoolDefaultsResource(name);
        configure(resource);
        return resource;
    }
    
    public static WebAppPoolDefaultsResource Create(string name, Action<IWebAppPoolDefaults> configure, out WebAppPoolDefaultsResource resource)
    {
        resource = new WebAppPoolDefaultsResource(name);
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
