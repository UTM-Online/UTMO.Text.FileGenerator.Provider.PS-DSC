namespace UTMO.Text.FileGenerator.Provider.DSC.Resources.Windows;

public class WindowsFeatureDsc : DscConfigurationItem
{
    public WindowsFeatureDsc(string name) : base(name)
    {
        this.PropertyBag.Add("Name", string.Empty);
    }

    public string FeatureName
    {
        get
        {
            return this.PropertyBag["Name"];
        }
        
        set
        {
            this.PropertyBag["Name"] = value;
        }
    }

    public override string ResourceId => "WindowsFeature";
}