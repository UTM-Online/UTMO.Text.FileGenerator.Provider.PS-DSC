namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.BaseDefinitions;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration.Enums;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Constants.PSDesiredStateConfigurationConstants.Service;

public sealed class ServiceResource : PSDesiredStateConfigurationBase, IServiceResource
{
    private ServiceResource(string name) : base(name)
    {
    }

    public string ServiceName
    {
        get => this.PropertyBag.Get(Constants.Properties.Name);
        set => this.PropertyBag.Set(Constants.Properties.Name, value);
    }
    
    public ServiceState? State
    {
        get => this.PropertyBag.Get<ServiceState?>(Constants.Properties.State);
        set => this.PropertyBag.Set(Constants.Properties.State, value);
    }
    
    public ServiceStartupType? StartupType
    {
        get => this.PropertyBag.Get<ServiceStartupType?>(Constants.Properties.StartupType);
        set => this.PropertyBag.Set(Constants.Properties.StartupType, value);
    }
    
    public string BuiltInAccount
    {
        get => this.PropertyBag.Get(Constants.Properties.BuiltInAccount);
        set => this.PropertyBag.Set(Constants.Properties.BuiltInAccount, value);
    }
    
    public string Credential
    {
        get => this.PropertyBag.Get(Constants.Properties.Credential);
        set => this.PropertyBag.Set(Constants.Properties.Credential, value);
    }
    
    public string[] Dependencies
    {
        get => this.PropertyBag.Get<string[]>(Constants.Properties.Dependencies);
        set => this.PropertyBag.Set(Constants.Properties.Dependencies, value);
    }
    
    public string ServiceDescription
    {
        get => this.PropertyBag.Get(Constants.Properties.Description);
        set => this.PropertyBag.Set(Constants.Properties.Description, value);
    }
    
    public string DisplayName
    {
        get => this.PropertyBag.Get(Constants.Properties.DisplayName);
        set => this.PropertyBag.Set(Constants.Properties.DisplayName, value);
    }
    
    public string Path
    {
        get => this.PropertyBag.Get(Constants.Properties.Path);
        set => this.PropertyBag.Set(Constants.Properties.Path, value);
    }
    
    public static ServiceResource Create(string name, Action<IServiceResource> configure)
    {
        var resource = new ServiceResource(name);
        configure(resource);
        return resource;
    }
    
    public static ServiceResource Create(string name, Action<IServiceResource> configure, out ServiceResource resource)
    {
        resource = new ServiceResource(name);
        configure(resource);
        return resource;
    }
    
    public override Task<List<ValidationFailedException>> Validate()
    {
        var errors = this.ValidationBuilder()
            .ValidateStringNotNullOrEmpty(this.ServiceName, nameof(this.ServiceName))
            .errors;

        return Task.FromResult(errors);
    }

    public override string ResourceId => Constants.ResourceId;
}
