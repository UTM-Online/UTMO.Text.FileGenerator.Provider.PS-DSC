namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PackageManagement;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Enums;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.BaseDefinitions;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PackageManagement.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Constants.PackageManagementConstants.PackageManagementSource;

public class PackageManagementSourceResource : PackageManagementBase, IPackageManagementSourceResource
{
    public PackageManagementSourceResource(string name) : base(name)
    {
    }

    public string RepositoryName
    {
        get => this.PropertyBag.Get(Constants.Properties.Name);
        
        set => this.PropertyBag.Set(Constants.Properties.Name, value);
    }

    public PSPackageProviders ProviderName
    {
        get => this.PropertyBag.Get<PSPackageProviders>(Constants.Properties.ProviderName);
        
        set => this.PropertyBag.Set(Constants.Properties.ProviderName, value);
    }
    
    public string SourceLocation
    {
        get => this.PropertyBag.Get(Constants.Properties.SourceLocation);
        
        set => this.PropertyBag.Set(Constants.Properties.SourceLocation, value);
    }
    
    public PSInstallPolicy InstallationPolicy
    {
        get => this.PropertyBag.Get<PSInstallPolicy>(Constants.Properties.InstallationPolicy);
        
        set => this.PropertyBag.Set(Constants.Properties.InstallationPolicy, value);
    }
    
    public static PackageManagementSourceResource Create(string name, Action<IPackageManagementSourceResource> configure)
    {
        var resource = new PackageManagementSourceResource(name);
        configure(resource);
        return resource;
    }
    
    public static PackageManagementSourceResource Create(string name, Action<IPackageManagementSourceResource> configure, out PackageManagementSourceResource resource)
    {
        resource = new PackageManagementSourceResource(name);
        configure(resource);
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var errors = this.ValidationBuilder()
                         .ValidateStringNotNullOrEmpty(this.RepositoryName, nameof(this.RepositoryName))
                         .ValidateStringNotNullOrEmpty(this.SourceLocation, nameof(this.SourceLocation)).errors;

        return Task.FromResult(errors);
    }

    public override string ResourceId => Constants.ResourceId;
}