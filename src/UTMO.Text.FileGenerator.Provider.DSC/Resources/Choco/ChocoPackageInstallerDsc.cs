namespace UTMO.Text.FileGenerator.Provider.DSC.Resources.Choco;

public class ChocoPackageInstallerDsc : DscConfigurationItem
{
    public ChocoPackageInstallerDsc(string name) : base(name)
    {
        this.PropertyBag.Add(nameof(this.PackageName), string.Empty);
        this.PropertyBag.Add(nameof(this.PackageSource), string.Empty);
        this.PropertyBag.Add(nameof(this.AutoUpgrade), string.Empty);
        this.PropertyBag.Add("Params", string.Empty);
    }
    
    public string PackageName
    {
        get
        {
            return this.PropertyBag[nameof(this.PackageName)];
        }
        
        set
        {
            this.PropertyBag[nameof(this.PackageName)] = value;
        }
    }
    
    public string PackageSource
    {
        get
        {
            return this.PropertyBag[nameof(this.PackageSource)];
        }
        
        set
        {
            this.PropertyBag[nameof(this.PackageSource)] = value;
        }
    }
    
    public bool AutoUpgrade
    {
        get
        {
            return this.PropertyBag[nameof(this.AutoUpgrade)] != string.Empty && bool.Parse(this.PropertyBag[nameof(this.AutoUpgrade)]);
        }
        
        set
        {
            this.PropertyBag[nameof(this.AutoUpgrade)] = value.ToString();
        }
    }

    public string Parameters
    {
        get
        {
            return this.PropertyBag["Params"];
        }
        
        set
        {
            this.PropertyBag["Params"] = value;
        }
    }
    
    public override string ResourceId => "cChocoPackageInstaller";
}