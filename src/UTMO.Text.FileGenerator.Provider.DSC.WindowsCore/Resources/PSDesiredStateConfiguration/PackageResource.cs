namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.BaseDefinitions;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Constants.PSDesiredStateConfigurationConstants.Package;

public sealed class PackageResource : PSDesiredStateConfigurationBase, IPackageResource
{
    private PackageResource(string name) : base(name)
    {
    }

    public string PackageName
    {
        get => this.PropertyBag.Get(Constants.Properties.Name);
        set => this.PropertyBag.Set(Constants.Properties.Name, value);
    }
    
    public string ProductId
    {
        get => this.PropertyBag.Get(Constants.Properties.ProductId);
        set => this.PropertyBag.Set(Constants.Properties.ProductId, value);
    }
    
    public string Path
    {
        get => this.PropertyBag.Get(Constants.Properties.Path);
        set => this.PropertyBag.Set(Constants.Properties.Path, value);
    }
    
    public string Arguments
    {
        get => this.PropertyBag.Get(Constants.Properties.Arguments);
        set => this.PropertyBag.Set(Constants.Properties.Arguments, value);
    }
    
    public string LogPath
    {
        get => this.PropertyBag.Get(Constants.Properties.LogPath);
        set => this.PropertyBag.Set(Constants.Properties.LogPath, value);
    }
    
    public int[] ReturnCode
    {
        get => this.PropertyBag.Get<int[]>(Constants.Properties.ReturnCode);
        set => this.PropertyBag.Set(Constants.Properties.ReturnCode, value);
    }
    
    public static PackageResource Create(string name, Action<IPackageResource> configure)
    {
        var resource = new PackageResource(name);
        configure(resource);
        return resource;
    }
    
    public static PackageResource Create(string name, Action<IPackageResource> configure, out PackageResource resource)
    {
        resource = new PackageResource(name);
        configure(resource);
        return resource;
    }
    
    public override Task<List<ValidationFailedException>> Validate()
    {
        var errors = this.ValidationBuilder()
            .ValidateStringNotNullOrEmpty(this.PackageName, nameof(this.PackageName))
            .ValidateStringNotNullOrEmpty(this.ProductId, nameof(this.ProductId))
            .ValidateStringNotNullOrEmpty(this.Path, nameof(this.Path))
            .errors;

        return Task.FromResult(errors);
    }

    public override string ResourceId => Constants.ResourceId;
}
