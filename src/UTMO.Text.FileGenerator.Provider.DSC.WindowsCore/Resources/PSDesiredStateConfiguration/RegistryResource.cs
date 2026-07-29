namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.BaseDefinitions;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration.Enums;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Constants.PSDesiredStateConfigurationConstants.Registry;

public class RegistryResource : PSDesiredStateConfigurationBase, IRegistryResource
{
    private RegistryResource(string name) : base(name)
    {
    }
    
    public string Key
    {
        get => this.PropertyBag.Get(Constants.Properties.Key);
        
        set => this.PropertyBag.Set(Constants.Properties.Key, value);
    }

    public string ValueName
    {
        get => this.PropertyBag.Get(Constants.Properties.ValueName);
        
        set => this.PropertyBag.Set(Constants.Properties.ValueName, value);
    }

    public string ValueData
    {
        get => this.PropertyBag.Get(Constants.Properties.ValueData);
        
        set => this.PropertyBag.Set(Constants.Properties.ValueData, value);
    }
    
    public RegistryValueType ValueType
    {
        get => this.PropertyBag.Get<RegistryValueType>(Constants.Properties.ValueType);
        
        set => this.PropertyBag.Set(Constants.Properties.ValueType, value);
    }
    
    public static RegistryResource Create(string name, Action<IRegistryResource> configure)
    {
        var resource = new RegistryResource(name);
        configure(resource);
        return resource;
    }
    
    public static RegistryResource Create(string name, Action<IRegistryResource> configure, out RegistryResource resource)
    {
        resource = new RegistryResource(name);
        configure(resource);
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var errors = this.ValidationBuilder()
            .ValidateStringNotNullOrEmpty(this.Key, nameof(this.Key))
            .ValidateStringNotNullOrEmpty(this.ValueName, nameof(this.ValueName))
            .errors;

        return Task.FromResult(errors);
    }

    public override string ResourceId => Constants.ResourceId;
}