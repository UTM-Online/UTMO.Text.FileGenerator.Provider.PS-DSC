namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.PackageManagement;

using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using UTMO.Text.FileGenerator.Provider.DSC.Enums;

public class PackageManagementResource : PackageManagementBase
{
    public PackageManagementResource(string name) : base(name)
    {
        this.PropertyBag["Name"] = string.Empty;
        this.PropertyBag["ProviderName"] = string.Empty;
        this.PropertyBag["Source"] = string.Empty;
    }

    public override string ResourceId => "PackageManagement";

    public string PackageName
    {
        get
        {
            return this.PropertyBag["Name"];
        }

        set
        {
            this.PropertyBag["Name"] = value;
        }
    }
    
    public PSPackageProviders ProviderName
    {
        get
        {
            return string.IsNullOrWhiteSpace(this.PropertyBag[nameof(this.ProviderName)]) ? PSPackageProviders.PowerShellGet : (PSPackageProviders)Enum.Parse(typeof(PSPackageProviders), this.PropertyBag[nameof(this.ProviderName)]);
        }

        set
        {
            this.PropertyBag["ProviderName"] = value.ToString();
        }
    }
    
    public string Source
    {
        get
        {
            return this.PropertyBag["Source"];
        }

        set
        {
            this.PropertyBag["Source"] = value;
        }
    }
}