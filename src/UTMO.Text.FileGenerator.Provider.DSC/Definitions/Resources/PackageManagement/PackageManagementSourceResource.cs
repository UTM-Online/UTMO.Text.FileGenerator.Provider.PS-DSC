namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.PackageManagement;

using UTMO.Text.FileGenerator.Provider.DSC.Constants;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using UTMO.Text.FileGenerator.Provider.DSC.Enums;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.PackageManagementConstants.PackageManagementSource;

public class PackageManagementSourceResource : PackageManagementBase
{
    public PackageManagementSourceResource(string name) : base(name)
    {
        this.PropertyBag.Add(Constants.Properties.Name, string.Empty);
        this.PropertyBag.Add(Constants.Properties.ProviderName, string.Empty);
        this.PropertyBag.Add(Constants.Properties.SourceLocation, string.Empty);
        this.PropertyBag.Add(Constants.Properties.InstallationPolicy, string.Empty);
    }

    public override string ResourceId => Constants.ResourceId;

    public string RepositoryName
    {
        get => this.PropertyBag[Constants.Properties.Name].ToString() ?? string.Empty;

        set => this.PropertyBag[Constants.Properties.Name] = value;
    }

    public PSPackageProviders ProviderName
    {
        get => !string.IsNullOrWhiteSpace(this.PropertyBag[Constants.Properties.ProviderName].ToString()) ? Enum.Parse<PSPackageProviders>(this.PropertyBag[Constants.Properties.ProviderName].ToString()!) : PSPackageProviders.PowerShellGet;

        set => this.PropertyBag[Constants.Properties.ProviderName] = value.ToString();
    }
    
    public string SourceLocation
    {
        get => this.PropertyBag[Constants.Properties.SourceLocation].ToString() ?? string.Empty;

        set => this.PropertyBag[Constants.Properties.SourceLocation] = value;
    }
    
    public PSInstallPolicy InstallationPolicy
    {
        get => this.PropertyBag[Constants.Properties.InstallationPolicy] != string.Empty ? Enum.Parse<PSInstallPolicy>(this.PropertyBag[Constants.Properties.InstallationPolicy].ToString()!) : PSInstallPolicy.Untrusted;

        set => this.PropertyBag[Constants.Properties.InstallationPolicy] = value.ToString();
    }
}