namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.PackageManagement;

using UTMO.Text.FileGenerator.Provider.DSC.Constants;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using UTMO.Text.FileGenerator.Provider.DSC.Enums;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.PackageManagementConstants.PackageManagementSource;

public class PackageManagementSourceResource : PackageManagementBase
{
    public PackageManagementSourceResource(string name) : base(name)
    {
        this.PropertyBag.Set(Constants.Properties.Name, string.Empty);
        this.PropertyBag.Set(Constants.Properties.ProviderName, string.Empty);
        this.PropertyBag.Set(Constants.Properties.SourceLocation, string.Empty);
        this.PropertyBag.Set(Constants.Properties.InstallationPolicy, string.Empty);
    }

    public override string ResourceId => Constants.ResourceId;

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
    
    public static PackageManagementSourceResource Create(string name, Action<PackageManagementSourceResource> configure)
    {
        var resource = new PackageManagementSourceResource(name);
        configure(resource);
        return resource;
    }
    
    public static PackageManagementSourceResource Create(string name, Action<PackageManagementSourceResource> configure, out PackageManagementSourceResource resource)
    {
        resource = new PackageManagementSourceResource(name);
        configure(resource);
        return resource;
    }
}