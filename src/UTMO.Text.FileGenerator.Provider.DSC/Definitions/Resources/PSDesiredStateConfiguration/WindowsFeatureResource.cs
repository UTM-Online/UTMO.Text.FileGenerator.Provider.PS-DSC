namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.PSDesiredStateConfiguration;

using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using UTMO.Text.FileGenerator.Provider.DSC.Resources;

public class WindowsFeatureResource : PSDesiredStateConfigurationBase
{
    public WindowsFeatureResource(string name) : base(name)
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