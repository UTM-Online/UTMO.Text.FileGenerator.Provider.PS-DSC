namespace UTMO.Text.FileGenerator.Provider.DSC.ChocoModule.Resources;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.ChocoModule.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.ChocoModule.ChocoModuleConstants.ChocolateyPackage;

/// <summary>
/// DSC resource for the ChocolateyPackage class from the Chocolatey module.
/// Installs, upgrades, or removes a Chocolatey package on a target node.
/// </summary>
public class ChocolateyPackageResource : ChocoModuleBase, IChocolateyPackageResource
{
    private ChocolateyPackageResource(string name) : base(name)
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

    public string? Source
    {
        get => this.PropertyBag.Get(Constants.Parameters.Source);
        set => this.PropertyBag.Set(Constants.Parameters.Source, value);
    }

    public bool UpdateOnly
    {
        get => this.PropertyBag.Get<bool>(Constants.Parameters.UpdateOnly);
        set => this.PropertyBag.Set(Constants.Parameters.UpdateOnly, value);
    }

    public static ChocolateyPackageResource Create(string name, Action<IChocolateyPackageResource> configure)
    {
        var resource = new ChocolateyPackageResource(name);
        configure(resource);
        return resource;
    }

    public static ChocolateyPackageResource Create(string name, Action<IChocolateyPackageResource> configure,
                                                   out ChocolateyPackageResource resourceRef)
    {
        var resource = new ChocolateyPackageResource(name);
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




