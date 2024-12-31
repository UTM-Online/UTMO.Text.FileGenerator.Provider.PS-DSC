namespace UTMO.Text.FileGenerator.Provider.DSC.cChoco.Resources;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.cChoco.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.cChoco.cChocoConstants.ChocoPackageInstaller;

public class ChocoPackageInstallerResource : cChocoBase, IChocoPackageInstallerResource
{
    private ChocoPackageInstallerResource(string name) : base(name)
    {
    }
    
    public string PackageName
    {
        get => this.PropertyBag.Get(Constants.Parameters.PackageName);

        set => this.PropertyBag.Set(Constants.Parameters.PackageName, value);
    }
    
    public string PackageSource
    {
        get => this.PropertyBag.Get(Constants.Parameters.PackageSource);

        set => this.PropertyBag.Set(Constants.Parameters.PackageSource, value);
    }
    
    public bool AutoUpgrade
    {
        get => this.PropertyBag.Get<bool>(Constants.Parameters.AutoUpgrade);
        
        set => this.PropertyBag.Set(Constants.Parameters.AutoUpgrade, value);
    }

    public string Parameters
    {
        get => this.PropertyBag.Get(Constants.Parameters.Params);

        set => this.PropertyBag.Set(Constants.Parameters.Params, value);
    }

    public static ChocoPackageInstallerResource Create(string name, Action<IChocoPackageInstallerResource> configure)
    {
        var resource = new ChocoPackageInstallerResource(name);
        configure(resource);
        return resource;
    }

    public static ChocoPackageInstallerResource Create(string name, Action<IChocoPackageInstallerResource> configure,
                                                       out ChocoPackageInstallerResource resourceRef)
    {
        var resource = new ChocoPackageInstallerResource(name);
        configure(resource);
        resourceRef = resource;
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var validators = this.ValidationBuilder()
                             .ValidateStringNotNullOrEmpty(this.PackageName, nameof(this.PackageName));
        
        return Task.FromResult(validators.errors);
    }

    public override string ResourceId => Constants.ResourceId;

    public override bool HasEnsure => true;
}