namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.cChoco;

using UTMO.Text.FileGenerator.Provider.DSC.Constants;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using UTMO.Text.FileGenerator.Provider.DSC.Resources;

public class ChocoPackageInstallerResource : cChocoBase
{
    public ChocoPackageInstallerResource(string name) : base(name)
    {
        this.PropertyBag.Add(cChocoConstants.ChocoPackageInstaller.Parameters.PackageName, string.Empty);
        this.PropertyBag.Add(cChocoConstants.ChocoPackageInstaller.Parameters.PackageSource, string.Empty);
        this.PropertyBag.Add(cChocoConstants.ChocoPackageInstaller.Parameters.AutoUpgrade, string.Empty);
        this.PropertyBag.Add(cChocoConstants.ChocoPackageInstaller.Parameters.Params, string.Empty);
    }
    
    public string PackageName
    {
        get
        {
            return this.PropertyBag[cChocoConstants.ChocoPackageInstaller.Parameters.PackageName];
        }
        
        set
        {
            this.PropertyBag[cChocoConstants.ChocoPackageInstaller.Parameters.PackageName] = value;
        }
    }
    
    public string PackageSource
    {
        get
        {
            return this.PropertyBag[cChocoConstants.ChocoPackageInstaller.Parameters.PackageSource];
        }
        
        set
        {
            this.PropertyBag[cChocoConstants.ChocoPackageInstaller.Parameters.PackageSource] = value;
        }
    }
    
    public bool AutoUpgrade
    {
        get
        {
            return this.PropertyBag[cChocoConstants.ChocoPackageInstaller.Parameters.AutoUpgrade] != string.Empty && bool.Parse(this.PropertyBag[cChocoConstants.ChocoPackageInstaller.Parameters.AutoUpgrade]);
        }
        
        set
        {
            this.PropertyBag[cChocoConstants.ChocoPackageInstaller.Parameters.AutoUpgrade] = value.ToString();
        }
    }

    public string Parameters
    {
        get
        {
            return this.PropertyBag[cChocoConstants.ChocoPackageInstaller.Parameters.Params];
        }
        
        set
        {
            this.PropertyBag[cChocoConstants.ChocoPackageInstaller.Parameters.Params] = value;
        }
    }
    
    public override string ResourceId => cChocoConstants.ChocoPackageInstaller.ResourceId;
}