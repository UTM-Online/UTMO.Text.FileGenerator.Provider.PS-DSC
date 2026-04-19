namespace UTMO.Text.FileGenerator.Provider.DSC.ChocoModule.Resources;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.ChocoModule.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.ChocoModule.ChocoModuleConstants.ChocolateyPin;

/// <summary>
/// DSC resource for the ChocolateyPin class from the Chocolatey module.
/// Pins or unpins a Chocolatey package to/from a specific version to prevent unintended upgrades.
/// </summary>
public class ChocolateyPinResource : ChocoModuleBase, IChocolateyPinResource
{
    private ChocolateyPinResource(string name) : base(name)
    {
    }

    public string PackageName
    {
        get => this.PropertyBag.Get(Constants.Parameters.Name);
        set => this.PropertyBag.Set(Constants.Parameters.Name, value);
    }

    public string? Version
    {
        get => this.PropertyBag.Get(Constants.Parameters.Version);
        set => this.PropertyBag.Set(Constants.Parameters.Version, value);
    }

    public static ChocolateyPinResource Create(string name, Action<IChocolateyPinResource> configure)
    {
        var resource = new ChocolateyPinResource(name);
        configure(resource);
        return resource;
    }

    public static ChocolateyPinResource Create(string name, Action<IChocolateyPinResource> configure,
                                               out ChocolateyPinResource resourceRef)
    {
        var resource = new ChocolateyPinResource(name);
        configure(resource);
        resourceRef = resource;
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var errors = this.ValidationBuilder()
                         .ValidateStringNotNullOrEmpty(this.PackageName, nameof(this.PackageName))
                         .errors;

        return Task.FromResult(errors);
    }

    public override string ResourceId => Constants.ResourceId;

    public override bool HasEnsure => true;
}




