namespace UTMO.Text.FileGenerator.Provider.DSC.ChocoModule.Resources;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.ChocoModule.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.ChocoModule.ChocoModuleConstants.ChocolateySoftware;

/// <summary>
/// DSC resource for the ChocolateySoftware class from the Chocolatey module.
/// Installs or removes the Chocolatey software (choco.exe) on a target node.
/// </summary>
public class ChocolateySoftwareResource : ChocoModuleBase, IChocolateySoftwareResource
{
    private ChocolateySoftwareResource(string name) : base(name)
    {
    }

    public string? InstallationDirectory
    {
        get => this.PropertyBag.Get(Constants.Parameters.InstallationDirectory);
        set => this.PropertyBag.Set(Constants.Parameters.InstallationDirectory, value);
    }

    public string? ChocolateyPackageUrl
    {
        get => this.PropertyBag.Get(Constants.Parameters.ChocolateyPackageUrl);
        set => this.PropertyBag.Set(Constants.Parameters.ChocolateyPackageUrl, value);
    }

    public string? PackageFeedUrl
    {
        get => this.PropertyBag.Get(Constants.Parameters.PackageFeedUrl);
        set => this.PropertyBag.Set(Constants.Parameters.PackageFeedUrl, value);
    }

    public string? Version
    {
        get => this.PropertyBag.Get(Constants.Parameters.Version);
        set => this.PropertyBag.Set(Constants.Parameters.Version, value);
    }

    public string? ChocoTempDir
    {
        get => this.PropertyBag.Get(Constants.Parameters.ChocoTempDir);
        set => this.PropertyBag.Set(Constants.Parameters.ChocoTempDir, value);
    }

    public string? ProxyLocation
    {
        get => this.PropertyBag.Get(Constants.Parameters.ProxyLocation);
        set => this.PropertyBag.Set(Constants.Parameters.ProxyLocation, value);
    }

    public bool IgnoreProxy
    {
        get => this.PropertyBag.Get<bool>(Constants.Parameters.IgnoreProxy);
        set => this.PropertyBag.Set(Constants.Parameters.IgnoreProxy, value);
    }

    public static ChocolateySoftwareResource Create(string name, Action<IChocolateySoftwareResource> configure)
    {
        var resource = new ChocolateySoftwareResource(name);
        configure(resource);
        return resource;
    }

    public static ChocolateySoftwareResource Create(string name, Action<IChocolateySoftwareResource> configure,
                                                    out ChocolateySoftwareResource resourceRef)
    {
        var resource = new ChocolateySoftwareResource(name);
        configure(resource);
        resourceRef = resource;
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var errors = this.ValidationBuilder().errors;
        return Task.FromResult(errors);
    }

    public override string ResourceId => Constants.ResourceId;

    /// <summary>
    /// ChocolateySoftware uses Ensure as its Key property; the base template
    /// renders Ensure automatically when HasEnsure is true.
    /// </summary>
    public override bool HasEnsure => true;
}

