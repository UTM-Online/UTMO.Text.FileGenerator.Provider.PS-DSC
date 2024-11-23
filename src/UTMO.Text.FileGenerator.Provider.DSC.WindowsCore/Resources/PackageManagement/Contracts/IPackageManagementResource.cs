namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PackageManagement.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Enums;

public interface IPackageManagementResource : IDscResourceConfig
{
    string PackageName { get; set; }

    PSPackageProviders ProviderName { get; set; }

    string Source { get; set; }

    string RequiredVersion { get; set; }
}