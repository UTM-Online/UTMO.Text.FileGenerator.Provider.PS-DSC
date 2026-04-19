namespace UTMO.Text.FileGenerator.Provider.DSC.ChocoModule.Resources;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.ChocoModule.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.ChocoModule.ChocoModuleConstants.ChocolateyFeature;

/// <summary>
/// DSC resource for the ChocolateyFeature class from the Chocolatey module.
/// Enables or disables a named Chocolatey feature flag.
/// </summary>
public class ChocolateyFeatureResource : ChocoModuleBase, IChocolateyFeatureResource
{
    private ChocolateyFeatureResource(string name) : base(name)
    {
    }

    public string FeatureName
    {
        get => this.PropertyBag.Get(Constants.Parameters.Name);
        set => this.PropertyBag.Set(Constants.Parameters.Name, value);
    }

    public static ChocolateyFeatureResource Create(string name, Action<IChocolateyFeatureResource> configure)
    {
        var resource = new ChocolateyFeatureResource(name);
        configure(resource);
        return resource;
    }

    public static ChocolateyFeatureResource Create(string name, Action<IChocolateyFeatureResource> configure,
                                                   out ChocolateyFeatureResource resourceRef)
    {
        var resource = new ChocolateyFeatureResource(name);
        configure(resource);
        resourceRef = resource;
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

    public override bool HasEnsure => true;
}




