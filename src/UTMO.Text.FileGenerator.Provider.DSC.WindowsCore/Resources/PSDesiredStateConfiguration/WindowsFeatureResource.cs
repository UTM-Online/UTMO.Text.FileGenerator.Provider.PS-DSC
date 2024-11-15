namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration;

using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.BaseDefinitions;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Constants.PSDesiredStateConfigurationConstants.WindowsFeature;

public class WindowsFeatureResource : PSDesiredStateConfigurationBase
{
    private WindowsFeatureResource(string name) : base(name)
    {
        this.PropertyBag.Init(Constants.Properties.Name);
    }

    public string FeatureName
    {
        get => this.PropertyBag.Get(Constants.Properties.Name);
        set => this.PropertyBag.Set(Constants.Properties.Name, value);
    }
    
    public static WindowsFeatureResource Create(string name, Action<WindowsFeatureResource> configure)
    {
        var resource = new WindowsFeatureResource(name);
        configure(resource);
        return resource;
    }
    
    public static WindowsFeatureResource Create(string name, Action<WindowsFeatureResource> configure, out WindowsFeatureResource resource)
    {
        resource = new WindowsFeatureResource(name);
        configure(resource);
        return resource;
    }

    public override string ResourceId
    {
        get => Constants.ResourceId;
    }
}