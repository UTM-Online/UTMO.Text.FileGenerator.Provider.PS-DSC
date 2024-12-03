namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.BaseDefinitions;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.ModuleDefinitions;

public abstract class PackageManagementBase : DscConfigurationItem
{
    protected PackageManagementBase(string name) : base(name)
    {
    }
    
    public sealed override RequiredModule SourceModule => PackageManagement.Instance;
    
    public sealed override bool HasEnsure => true;
}