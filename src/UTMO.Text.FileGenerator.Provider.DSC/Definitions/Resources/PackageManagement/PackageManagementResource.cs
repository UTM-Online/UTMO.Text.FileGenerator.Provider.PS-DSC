namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.PackageManagement;

using UTMO.Text.FileGenerator.Provider.DSC.Constants;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using UTMO.Text.FileGenerator.Provider.DSC.Enums;

public class PackageManagementResource : PackageManagementBase
{
    public PackageManagementResource(string name) : base(name)
    {
        this.PropertyBag[PackageManagementConstants.PackageManagement.Properties.Name]         = string.Empty;
        this.PropertyBag[PackageManagementConstants.PackageManagement.Properties.ProviderName] = string.Empty;
        this.PropertyBag[PackageManagementConstants.PackageManagement.Properties.Source]              = string.Empty;
    }

    public override string ResourceId => PackageManagementConstants.PackageManagement.ResourceId;

    public string PackageName
    {
        get
        {
            return this.PropertyBag[PackageManagementConstants.PackageManagement.Properties.Name];
        }

        set
        {
            this.PropertyBag[PackageManagementConstants.PackageManagement.Properties.Name] = value;
        }
    }
    
    public PSPackageProviders ProviderName
    {
        get
        {
            return string.IsNullOrWhiteSpace(this.PropertyBag[PackageManagementConstants.PackageManagement.Properties.ProviderName]) ? PSPackageProviders.PowerShellGet : (PSPackageProviders)Enum.Parse(typeof(PSPackageProviders), this.PropertyBag[PackageManagementConstants.PackageManagement.Properties.ProviderName]);
        }

        set
        {
            this.PropertyBag[PackageManagementConstants.PackageManagement.Properties.ProviderName] = value.ToString();
        }
    }
    
    public string Source
    {
        get
        {
            return this.PropertyBag[PackageManagementConstants.PackageManagement.Properties.Source];
        }

        set
        {
            this.PropertyBag[PackageManagementConstants.PackageManagement.Properties.Source] = value;
        }
    }
}