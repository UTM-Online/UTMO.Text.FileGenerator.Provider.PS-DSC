namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.PackageManagement;

using UTMO.Text.FileGenerator.Provider.DSC.Constants;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using UTMO.Text.FileGenerator.Provider.DSC.Enums;

public class PackageManagementSourceResource : PackageManagementBase
{
    public PackageManagementSourceResource(string name) : base(name)
    {
        this.PropertyBag.Add(PackageManagementConstants.PackageManagementSource.Properties.Name, string.Empty);
        this.PropertyBag.Add(PackageManagementConstants.PackageManagementSource.Properties.ProviderName, string.Empty);
        this.PropertyBag.Add(PackageManagementConstants.PackageManagementSource.Properties.SourceLocation, string.Empty);
        this.PropertyBag.Add(PackageManagementConstants.PackageManagementSource.Properties.InstallationPolicy, string.Empty);
    }

    public override string ResourceId => PackageManagementConstants.PackageManagementSource.ResourceId;

    public string RepositoryName
    {
        get
        {
            return this.PropertyBag[PackageManagementConstants.PackageManagementSource.Properties.Name];
        }
        
        set
        {
            this.PropertyBag[PackageManagementConstants.PackageManagementSource.Properties.Name] = value;
        }
    }

    public PSPackageProviders ProviderName
    {
        get
        {
            return this.PropertyBag[PackageManagementConstants.PackageManagementSource.Properties.ProviderName] != string.Empty ? (PSPackageProviders)Enum.Parse(typeof(PSPackageProviders), this.PropertyBag[PackageManagementConstants.PackageManagementSource.Properties.ProviderName]) : PSPackageProviders.PowerShellGet;
        }
        
        set
        {
            this.PropertyBag[PackageManagementConstants.PackageManagementSource.Properties.ProviderName] = value.ToString();
        }
    }
    
    public string SourceLocation
    {
        get
        {
            return this.PropertyBag[PackageManagementConstants.PackageManagementSource.Properties.SourceLocation];
        }
        
        set
        {
            this.PropertyBag[PackageManagementConstants.PackageManagementSource.Properties.SourceLocation] = value;
        }
    }
    
    public PSInstallPolicy InstallationPolicy
    {
        get
        {
            return this.PropertyBag[PackageManagementConstants.PackageManagementSource.Properties.InstallationPolicy] != string.Empty ? (PSInstallPolicy)Enum.Parse(typeof(PSInstallPolicy), this.PropertyBag[PackageManagementConstants.PackageManagementSource.Properties.InstallationPolicy]) : PSInstallPolicy.Untrusted;
        }
        
        set
        {
            this.PropertyBag[PackageManagementConstants.PackageManagementSource.Properties.InstallationPolicy] = value.ToString();
        }
    }
}