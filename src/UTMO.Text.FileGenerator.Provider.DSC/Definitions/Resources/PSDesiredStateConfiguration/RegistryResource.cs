namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.PSDesiredStateConfiguration;

using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using UTMO.Text.FileGenerator.Provider.DSC.Resources;

public class RegistryResource : PSDesiredStateConfigurationBase
{
    public RegistryResource(string name) : base(name)
    {
        this.PropertyBag.Add(nameof(this.Key), string.Empty);
        this.PropertyBag.Add(nameof(this.ValueName), string.Empty);
        this.PropertyBag.Add(nameof(this.ValueData), string.Empty);
    }
    
    public string Key
    {
        get
        {
            return this.PropertyBag[nameof(this.Key)];
        }

        set
        {
            this.PropertyBag[nameof(this.Key)] = value;
        }
    }

    public string ValueName
    {
        get
        {
            return this.PropertyBag[nameof(this.ValueName)];
        }
        
        set
        {
            this.PropertyBag[nameof(this.ValueName)] = value;
        }
    }

    public string ValueData
    {
        get
        {
            return this.PropertyBag[nameof(this.ValueData)];
        }
        
        set
        {
            this.PropertyBag[nameof(this.ValueData)] = value;
        }
    }
    
    public override string ResourceId => "Registry";
}