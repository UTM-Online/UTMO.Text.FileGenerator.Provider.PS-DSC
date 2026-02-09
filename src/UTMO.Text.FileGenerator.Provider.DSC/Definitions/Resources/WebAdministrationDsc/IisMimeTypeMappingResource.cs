﻿namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc;
using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.WebAdministrationDscConstants.IisMimeTypeMapping;
public sealed class IisMimeTypeMappingResource : WebAdministrationDscBase, IIisMimeTypeMapping
{
    private IisMimeTypeMappingResource(string name) : base(name) { }
    public string ConfigurationPath
    {
        get => this.PropertyBag.Get(Constants.Properties.ConfigurationPath);
        set => this.PropertyBag.Set(Constants.Properties.ConfigurationPath, value);
    }
    public string Extension
    {
        get => this.PropertyBag.Get(Constants.Properties.Extension);
        set => this.PropertyBag.Set(Constants.Properties.Extension, value);
    }
    public string MimeType
    {
        get => this.PropertyBag.Get(Constants.Properties.MimeType);
        set => this.PropertyBag.Set(Constants.Properties.MimeType, value);
    }
    public static IisMimeTypeMappingResource Create(string name, Action<IIisMimeTypeMapping> configure)
    {
        var resource = new IisMimeTypeMappingResource(name);
        configure(resource);
        return resource;
    }
    
    public static IisMimeTypeMappingResource Create(string name, Action<IIisMimeTypeMapping> configure, out IisMimeTypeMappingResource resource)
    {
        resource = new IisMimeTypeMappingResource(name);
        configure(resource);
        return resource;
    }
    public override Task<List<ValidationFailedException>> Validate()
    {
        var errors = this.ValidationBuilder()
            .ValidateStringNotNullOrEmpty(this.ConfigurationPath, nameof(this.ConfigurationPath))
            .ValidateStringNotNullOrEmpty(this.Extension, nameof(this.Extension))
            .ValidateStringNotNullOrEmpty(this.MimeType, nameof(this.MimeType))
            .errors;
        return Task.FromResult(errors);
    }
    public override string ResourceId => Constants.ResourceId;
}
