namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.PSDesiredStateConfiguration;

using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.PSDesiredStateConfigurationConstants.WindowsFeature;

public class WindowsFeatureResource : PSDesiredStateConfigurationBase
{
    public WindowsFeatureResource(string name) : base(name)
    {
        this.PropertyBag.Add(Constants.Properties.Name, string.Empty);
    }

    public string FeatureName
    {
        get => this.PropertyBag[Constants.Properties.Name].ToString() ?? string.Empty;

        set => this.PropertyBag[Constants.Properties.Name] = value;
    }

    public override string ResourceId => Constants.ResourceId;
}