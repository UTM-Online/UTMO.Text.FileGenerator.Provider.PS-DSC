﻿namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc;
using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.WebAdministrationDscConstants.WebVirtualDirectory;
public sealed class WebVirtualDirectoryResource : WebAdministrationDscBase, IWebVirtualDirectory
{
    private WebVirtualDirectoryResource(string name) : base(name) { }
    public string Website
    {
        get => this.PropertyBag.Get(Constants.Properties.Website);
        set => this.PropertyBag.Set(Constants.Properties.Website, value);
    }
    public string WebApplication
    {
        get => this.PropertyBag.Get(Constants.Properties.WebApplication);
        set => this.PropertyBag.Set(Constants.Properties.WebApplication, value);
    }
    public string VirtualDirectoryName
    {
        get => this.PropertyBag.Get(Constants.Properties.Name);
        set => this.PropertyBag.Set(Constants.Properties.Name, value);
    }
    public string PhysicalPath
    {
        get => this.PropertyBag.Get(Constants.Properties.PhysicalPath);
        set => this.PropertyBag.Set(Constants.Properties.PhysicalPath, value);
    }
    public string? Credential
    {
        get => this.PropertyBag.Get(Constants.Properties.Credential);
        set => this.PropertyBag.Set(Constants.Properties.Credential, value);
    }
    public static WebVirtualDirectoryResource Create(string name, Action<IWebVirtualDirectory> configure)
    {
        var resource = new WebVirtualDirectoryResource(name);
        configure(resource);
        return resource;
    }
    
    public static WebVirtualDirectoryResource Create(string name, Action<IWebVirtualDirectory> configure, out WebVirtualDirectoryResource resource)
    {
        resource = new WebVirtualDirectoryResource(name);
        configure(resource);
        return resource;
    }
    public override Task<List<ValidationFailedException>> Validate()
    {
        var errors = this.ValidationBuilder()
            .ValidateStringNotNullOrEmpty(this.Website, nameof(this.Website))
            .ValidateStringNotNullOrEmpty(this.WebApplication, nameof(this.WebApplication))
            .ValidateStringNotNullOrEmpty(this.VirtualDirectoryName, nameof(this.VirtualDirectoryName))
            .ValidateStringNotNullOrEmpty(this.PhysicalPath, nameof(this.PhysicalPath))
            .errors;
        return Task.FromResult(errors);
    }
    public override string ResourceId => Constants.ResourceId;
}
