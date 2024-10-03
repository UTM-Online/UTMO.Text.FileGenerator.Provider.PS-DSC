namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.cChoco;

using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.cChocoConstants.ChocoPackageInstaller;

public class ChocoPackageInstallerResource : cChocoBase
{
    public ChocoPackageInstallerResource(string name) : base(name)
    {
        this.PropertyBag.Add(Constants.Parameters.PackageName, string.Empty);
        this.PropertyBag.Add(Constants.Parameters.PackageSource, string.Empty);
        this.PropertyBag.Add(Constants.Parameters.AutoUpgrade, string.Empty);
        this.PropertyBag.Add(Constants.Parameters.Params, string.Empty);
    }
    
    public string PackageName
    {
        get => this.PropertyBag[Constants.Parameters.PackageName].ToString() ?? string.Empty;

        set => this.PropertyBag[Constants.Parameters.PackageName] = value;
    }
    
    public string PackageSource
    {
        get => this.PropertyBag[Constants.Parameters.PackageSource].ToString() ?? string.Empty;

        set => this.PropertyBag[Constants.Parameters.PackageSource] = value;
    }
    
    public bool AutoUpgrade
    {
        get => bool.TryParse(this.PropertyBag[Constants.Parameters.AutoUpgrade].ToString(), out var result) && result;

        set => this.PropertyBag[Constants.Parameters.AutoUpgrade] = value.ToString();
    }

    public string Parameters
    {
        get => this.PropertyBag[Constants.Parameters.Params].ToString() ?? string.Empty;

        set => this.PropertyBag[Constants.Parameters.Params] = value;
    }
    
    public override string ResourceId => Constants.ResourceId;
}