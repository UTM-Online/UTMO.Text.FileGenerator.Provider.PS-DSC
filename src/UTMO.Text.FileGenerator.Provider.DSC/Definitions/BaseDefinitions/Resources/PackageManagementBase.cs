namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;

using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Modules;
using UTMO.Text.FileGenerator.Provider.DSC.Models;
using UTMO.Text.FileGenerator.Provider.DSC.SubResources;

public abstract class PackageManagementBase : DscConfigurationItem
{
    protected PackageManagementBase(string name) : base(name)
    {
    }
    
    public sealed override RequiredModule SourceModule => PackageManagement.Instance;
}