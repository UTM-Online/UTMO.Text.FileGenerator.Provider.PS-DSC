namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PackageManagement.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Enums;

public interface IPackageManagementSourceResource : IDscResourceConfig
{
    string RepositoryName { get; set; }

    PSPackageProviders ProviderName { get; set; }

    string SourceLocation { get; set; }

    PSInstallPolicy InstallationPolicy { get; set; }
}