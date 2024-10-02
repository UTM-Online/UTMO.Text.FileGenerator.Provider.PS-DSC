namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.PSDesiredStateConfiguration;

using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.PSDesiredStateConfigurationConstants.Registry;

public class RegistryResource : PSDesiredStateConfigurationBase
{
    public RegistryResource(string name) : base(name)
    {
        this.PropertyBag.Add(Constants.Properties.Key, string.Empty);
        this.PropertyBag.Add(Constants.Properties.ValueName, string.Empty);
        this.PropertyBag.Add(Constants.Properties.ValueData, string.Empty);
    }
    
    public string Key
    {
        get
        {
            return this.PropertyBag[Constants.Properties.Key];
        }

        set
        {
            this.PropertyBag[Constants.Properties.Key] = value;
        }
    }

    public string ValueName
    {
        get
        {
            return this.PropertyBag[Constants.Properties.ValueName];
        }
        
        set
        {
            this.PropertyBag[Constants.Properties.ValueName] = value;
        }
    }

    public string ValueData
    {
        get
        {
            return this.PropertyBag[Constants.Properties.ValueData];
        }
        
        set
        {
            this.PropertyBag[Constants.Properties.ValueData] = value;
        }
    }
    
    public override string ResourceId => Constants.ResourceId;
}