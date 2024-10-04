namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.cChoco;

using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.cChocoConstants.ChocoPackageInstaller;

public class ChocoPackageInstallerResource : cChocoBase
{
    public ChocoPackageInstallerResource(string name) : base(name)
    {
        this.PropertyBag.Init(Constants.Parameters.PackageName);
        this.PropertyBag.Init(Constants.Parameters.PackageSource);
        this.PropertyBag.Init(Constants.Parameters.AutoUpgrade);
        this.PropertyBag.Init(Constants.Parameters.Params);
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
    
    public override string ResourceId => Constants.ResourceId;
}