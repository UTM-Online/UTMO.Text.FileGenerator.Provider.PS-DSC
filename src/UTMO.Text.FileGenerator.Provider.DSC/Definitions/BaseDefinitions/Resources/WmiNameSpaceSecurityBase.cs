namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;

using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Modules;
using UTMO.Text.FileGenerator.Provider.DSC.Models;
using UTMO.Text.FileGenerator.Provider.DSC.SubResources;

public abstract class WmiNameSpaceSecurityBase : DscConfigurationItem
{
    protected WmiNameSpaceSecurityBase(string name) : base(name)
    {
    }

    public sealed override RequiredModule SourceModule
    {
        get => WmiNamespaceSecurity.Instance;
    }
}