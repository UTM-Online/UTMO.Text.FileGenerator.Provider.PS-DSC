namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PackageManagement;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Enums;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.BaseDefinitions;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PackageManagement.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Constants.PackageManagementConstants.PackageManagement;

public class PackageManagementResource : PackageManagementBase, IPackageManagementResource
{
    private PackageManagementResource(string name) : base(name)
    {
    }

    public string PackageName
    {
        get => this.PropertyBag.Get(Constants.Properties.Name);
        
        set => this.PropertyBag.Set(Constants.Properties.Name, value);
    }
    
    public PSPackageProviders ProviderName
    {
        get => this.PropertyBag.Get<PSPackageProviders>(Constants.Properties.ProviderName);
        
        set => this.PropertyBag.Set(Constants.Properties.ProviderName, value);
    }
    
    public string Source
    {
        get => this.PropertyBag.Get(Constants.Properties.Source);
        
        set => this.PropertyBag.Set(Constants.Properties.Source, value);
    }
    
    public string RequiredVersion
    {
        get => this.PropertyBag.Get(Constants.Properties.RequiredVersion);
        
        set => this.PropertyBag.Set(Constants.Properties.RequiredVersion, value);
    }
    
    public static PackageManagementResource Create(string name, Action<IPackageManagementResource> configure)
    {
        var resource = new PackageManagementResource(name);
        configure(resource);
        return resource;
    }
    
    public static PackageManagementResource Create(string name, Action<IPackageManagementResource> configure, out PackageManagementResource resource)
    {
        resource = new PackageManagementResource(name);
        configure(resource);
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
}