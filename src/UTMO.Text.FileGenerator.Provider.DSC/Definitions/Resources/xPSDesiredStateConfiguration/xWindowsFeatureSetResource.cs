namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.xPSDesiredStateConfiguration;

using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.xPSDesiredStateConfigurationConstants.xWindowsFeatureSet;

// ReSharper disable once InconsistentNaming
public class xWindowsFeatureSetResource : xPSDesiredStateConfigurationBase
{
    private xWindowsFeatureSetResource(string name) : base(name)
    {
        this.PropertyBag.Init<string[]>(Constants.Properties.Name);
        this.PropertyBag.Init(Constants.Properties.Source);
        this.PropertyBag.Init<bool>(Constants.Properties.IncludeAllSubFeature);
        this.PropertyBag.Init(Constants.Properties.LogPath);
    }
    
    public string[] FeatureName
    {
        get => this.PropertyBag.Get<string[]>(Constants.Properties.Name);
        set => this.PropertyBag.Set(Constants.Properties.Name, value);
    }
    
    public string Source
    {
        get => this.PropertyBag.Get(Constants.Properties.Source);
        set => this.PropertyBag.Set(Constants.Properties.Source, value);
    }
    
    public bool IncludeAllSubFeature
    {
        get => this.PropertyBag.Get<bool>(Constants.Properties.IncludeAllSubFeature);
        set => this.PropertyBag.Set(Constants.Properties.IncludeAllSubFeature, value);
    }
    
    public string LogPath
    {
        get => this.PropertyBag.Get(Constants.Properties.LogPath);
        set => this.PropertyBag.Set(Constants.Properties.LogPath, value);
    }
    
    public static xWindowsFeatureSetResource Create(string name, Action<xWindowsFeatureSetResource> configure)
    {
        var resource = new xWindowsFeatureSetResource(name);
        configure(resource);
        return resource;
    }
    
    public static xWindowsFeatureSetResource Create(string name, Action<xWindowsFeatureSetResource> configure, out xWindowsFeatureSetResource resource)
    {
        resource = new xWindowsFeatureSetResource(name);
        configure(resource);
        return resource;
    }

    public override string ResourceId => Constants.ResourceId;
}