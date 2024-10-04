namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.PSDesiredStateConfiguration;

using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.PSDesiredStateConfigurationConstants.Registry;

public class RegistryResource : PSDesiredStateConfigurationBase
{
    public RegistryResource(string name) : base(name)
    {
        this.PropertyBag.Init(Constants.Properties.Key);
        this.PropertyBag.Init(Constants.Properties.ValueName);
        this.PropertyBag.Init(Constants.Properties.ValueData);
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
    
    public override string ResourceId
    {
        get => Constants.ResourceId;
    }
}