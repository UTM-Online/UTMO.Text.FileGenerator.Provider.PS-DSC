namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.PackageManagement;

using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using UTMO.Text.FileGenerator.Provider.DSC.Enums;

public class PackageManagementSourceResource : PackageManagementBase
{
    public PackageManagementSourceResource(string name) : base(name)
    {
        this.PropertyBag.Add("Name", string.Empty);
        this.PropertyBag.Add("ProviderName", string.Empty);
        this.PropertyBag.Add("SourceLocation", string.Empty);
    }

    public override string ResourceId => "PackageManagementSource";

    public string RepositoryName
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
            return this.PropertyBag["ProviderName"] != string.Empty ? (PSPackageProviders)Enum.Parse(typeof(PSPackageProviders), this.PropertyBag["ProviderName"]) : PSPackageProviders.PowerShellGet;
        }
        
        set
        {
            this.PropertyBag["ProviderName"] = value.ToString();
        }
    }
    
    public string SourceLocation
    {
        get
        {
            return this.PropertyBag["SourceLocation"];
        }
        
        set
        {
            this.PropertyBag["SourceLocation"] = value;
        }
    }
    
    public PSInstallPolicy InstallationPolicy
    {
        get
        {
            return this.PropertyBag["InstallationPolicy"] != string.Empty ? (PSInstallPolicy)Enum.Parse(typeof(PSInstallPolicy), this.PropertyBag["InstallationPolicy"]) : PSInstallPolicy.Untrusted;
        }
        
        set
        {
            this.PropertyBag["InstallationPolicy"] = value.ToString();
        }
    }
}