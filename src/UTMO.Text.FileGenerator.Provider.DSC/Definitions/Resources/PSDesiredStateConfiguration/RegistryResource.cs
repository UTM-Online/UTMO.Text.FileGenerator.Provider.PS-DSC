namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.PSDesiredStateConfiguration;

using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.PSDesiredStateConfigurationConstants.Registry;

public class RegistryResource : PSDesiredStateConfigurationBase
{
    private RegistryResource(string name) : base(name)
    {
        this.PropertyBag.Init(Constants.Properties.Key);
        this.PropertyBag.Init(Constants.Properties.ValueName);
        this.PropertyBag.Init(Constants.Properties.ValueData);
        this.PropertyBag.Init(Constants.Properties.ValueType);
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
    
    public static RegistryResource Create(string name, Action<RegistryResource> configure)
    {
        var resource = new RegistryResource(name);
        configure(resource);
        return resource;
    }
    
    public static RegistryResource Create(string name, Action<RegistryResource> configure, out RegistryResource resource)
    {
        resource = new RegistryResource(name);
        configure(resource);
        return resource;
    }
    
    public override string ResourceId => Constants.ResourceId;
}