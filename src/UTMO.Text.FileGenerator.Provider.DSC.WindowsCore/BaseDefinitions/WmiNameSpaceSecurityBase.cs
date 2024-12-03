namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.BaseDefinitions;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.ModuleDefinitions;

public abstract class WmiNameSpaceSecurityBase : DscConfigurationItem
{
    protected WmiNameSpaceSecurityBase(string name) : base(name)
    {
    }

    public sealed override RequiredModule SourceModule => WmiNamespaceSecurity.Instance;
    
    public sealed override bool HasEnsure => true;
}