﻿namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc;
using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Enums;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.WebAdministrationDscConstants.WebApplicationHandler;
public sealed class WebApplicationHandlerResource : WebAdministrationDscBase, IWebApplicationHandler
{
    private WebApplicationHandlerResource(string name) : base(name) { }
    public string HandlerName
    {
        get => this.PropertyBag.Get(Constants.Properties.Name);
        set => this.PropertyBag.Set(Constants.Properties.Name, value);
    }
    public string? PhysicalHandlerPath
    {
        get => this.PropertyBag.Get(Constants.Properties.PhysicalHandlerPath);
        set => this.PropertyBag.Set(Constants.Properties.PhysicalHandlerPath, value);
    }
    public string? Verb
    {
        get => this.PropertyBag.Get(Constants.Properties.Verb);
        set => this.PropertyBag.Set(Constants.Properties.Verb, value);
    }
    public string[] Path
    {
        get => this.PropertyBag.Get<string[]>(Constants.Properties.Path);
        set => this.PropertyBag.Set(Constants.Properties.Path, value);
    }
    public string? Type
    {
        get => this.PropertyBag.Get(Constants.Properties.Type);
        set => this.PropertyBag.Set(Constants.Properties.Type, value);
    }
    public string? Modules
    {
        get => this.PropertyBag.Get(Constants.Properties.Modules);
        set => this.PropertyBag.Set(Constants.Properties.Modules, value);
    }
    public string? ScriptProcessor
    {
        get => this.PropertyBag.Get(Constants.Properties.ScriptProcessor);
        set => this.PropertyBag.Set(Constants.Properties.ScriptProcessor, value);
    }
    public string? PreCondition
    {
        get => this.PropertyBag.Get(Constants.Properties.PreCondition);
        set => this.PropertyBag.Set(Constants.Properties.PreCondition, value);
    }
    public RequireAccess? RequireAccess
    {
        get => this.PropertyBag.Get<RequireAccess?>(Constants.Properties.RequireAccess);
        set => this.PropertyBag.Set(Constants.Properties.RequireAccess, value);
    }
    public string? ResourceType
    {
        get => this.PropertyBag.Get(Constants.Properties.ResourceType);
        set => this.PropertyBag.Set(Constants.Properties.ResourceType, value);
    }
    public bool? AllowPathInfo
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.AllowPathInfo);
        set => this.PropertyBag.Set(Constants.Properties.AllowPathInfo, value);
    }
    public uint? ResponseBufferLimit
    {
        get => this.PropertyBag.Get<uint?>(Constants.Properties.ResponseBufferLimit);
        set => this.PropertyBag.Set(Constants.Properties.ResponseBufferLimit, value);
    }
    public string? Location
    {
        get => this.PropertyBag.Get(Constants.Properties.Location);
        set => this.PropertyBag.Set(Constants.Properties.Location, value);
    }
    public static WebApplicationHandlerResource Create(string name, Action<IWebApplicationHandler> configure)
    {
        var resource = new WebApplicationHandlerResource(name);
        configure(resource);
        return resource;
    }
    
    public static WebApplicationHandlerResource Create(string name, Action<IWebApplicationHandler> configure, out WebApplicationHandlerResource resource)
    {
        resource = new WebApplicationHandlerResource(name);
        configure(resource);
        return resource;
    }
    public override Task<List<ValidationFailedException>> Validate()
    {
        var errors = this.ValidationBuilder()
            .ValidateStringNotNullOrEmpty(this.HandlerName, nameof(this.HandlerName))
            .errors;
        return Task.FromResult(errors);
    }
    public override string ResourceId => Constants.ResourceId;
}
