namespace UTMO.Text.FileGenerator.Provider.DSC.Resources.Windows;

using UTMO.Text.FileGenerator.Attributes;

public class RegistryDsc : DscConfigurationItem
{
    public RegistryDsc(string name) : base(name)
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