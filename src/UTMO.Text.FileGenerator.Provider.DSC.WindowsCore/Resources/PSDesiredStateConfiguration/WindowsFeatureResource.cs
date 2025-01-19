namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.BaseDefinitions;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Constants.PSDesiredStateConfigurationConstants.WindowsFeature;

public class WindowsFeatureResource : PSDesiredStateConfigurationBase, IWindowsFeatureResource
{
    private WindowsFeatureResource(string name) : base(name)
    {
    }

    public string FeatureName
    {
        get => this.PropertyBag.Get(Constants.Properties.Name);
        set => this.PropertyBag.Set(Constants.Properties.Name, value);
    }
    
    public string Source
    {
        get => this.PropertyBag.Get(Constants.Properties.Source);
        set => this.PropertyBag.Set(Constants.Properties.Source, value);
    }
    
    public static WindowsFeatureResource Create(string name, Action<IWindowsFeatureResource> configure)
    {
        var resource = new WindowsFeatureResource(name);
        configure(resource);
        return resource;
    }
    
    public static WindowsFeatureResource Create(string name, Action<IWindowsFeatureResource> configure, out WindowsFeatureResource resource)
    {
        resource = new WindowsFeatureResource(name);
        configure(resource);
        return resource;
    }
    
    public override Task<List<ValidationFailedException>> Validate()
    {
        var errors = this.ValidationBuilder()
            .ValidateStringNotNullOrEmpty(this.FeatureName, nameof(this.FeatureName))
            .errors;

        return Task.FromResult(errors);
    }

    public override string ResourceId => Constants.ResourceId;
}