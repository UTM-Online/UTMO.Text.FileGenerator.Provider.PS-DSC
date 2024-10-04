namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.PSDesiredStateConfiguration;

using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.PSDesiredStateConfigurationConstants.WindowsFeature;

public class WindowsFeatureResource : PSDesiredStateConfigurationBase
{
    public WindowsFeatureResource(string name) : base(name)
    {
        this.PropertyBag.Init(Constants.Properties.Name);
    }

    public string FeatureName
    {
        get => this.PropertyBag.Get(Constants.Properties.Name);
        set => this.PropertyBag.Set(Constants.Properties.Name, value);
    }

    public override string ResourceId
    {
        get => Constants.ResourceId;
    }
}