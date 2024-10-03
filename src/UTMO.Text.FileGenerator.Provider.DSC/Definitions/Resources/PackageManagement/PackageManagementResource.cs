namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.PackageManagement;

using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using UTMO.Text.FileGenerator.Provider.DSC.Enums;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.PackageManagementConstants.PackageManagement;

public class PackageManagementResource : PackageManagementBase
{
    public PackageManagementResource(string name) : base(name)
    {
        this.PropertyBag[Constants.Properties.Name]         = string.Empty;
        this.PropertyBag[Constants.Properties.ProviderName] = string.Empty;
        this.PropertyBag[Constants.Properties.Source]              = string.Empty;
    }

    public override string ResourceId => Constants.ResourceId;

    public string PackageName
    {
        get
        {
            return this.PropertyBag[Constants.Properties.Name].ToString() ?? throw new InvalidOperationException($"Property {Constants.Properties.Name} is not set.");
        }

        set
        {
            this.PropertyBag[Constants.Properties.Name] = value;
        }
    }
    
    public PSPackageProviders ProviderName
    {
        get
        {
            return string.IsNullOrWhiteSpace(this.PropertyBag[Constants.Properties.ProviderName].ToString()) ? PSPackageProviders.PowerShellGet : Enum.Parse<PSPackageProviders>(this.PropertyBag[Constants.Properties.ProviderName].ToString()!);
        }

        set
        {
            this.PropertyBag[Constants.Properties.ProviderName] = value.ToString();
        }
    }
    
    public string Source
    {
        get
        {
            return this.PropertyBag[Constants.Properties.Source].ToString() ?? throw new InvalidOperationException($"Property {Constants.Properties.Source} is not set.");
        }

        set
        {
            this.PropertyBag[Constants.Properties.Source] = value;
        }
    }
}