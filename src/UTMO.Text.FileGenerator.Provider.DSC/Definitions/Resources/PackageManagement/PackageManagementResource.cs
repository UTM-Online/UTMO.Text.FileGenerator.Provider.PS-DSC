namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.PackageManagement;

using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using UTMO.Text.FileGenerator.Provider.DSC.Enums;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.PackageManagementConstants.PackageManagement;

public class PackageManagementResource : PackageManagementBase
{
    public PackageManagementResource(string name) : base(name)
    {
        this.PropertyBag.Init(Constants.Properties.Name);
        this.PropertyBag.Init<PSPackageProviders>(Constants.Properties.ProviderName);
        this.PropertyBag.Init(Constants.Properties.Source);
    }

    public override string ResourceId => Constants.ResourceId;

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
}