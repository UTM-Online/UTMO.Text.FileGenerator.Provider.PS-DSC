namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;

using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Modules;
using UTMO.Text.FileGenerator.Provider.DSC.Resources;
using UTMO.Text.FileGenerator.Provider.DSC.SubResources;

public abstract class NetworkingDscBase : DscConfigurationItem
{
    protected NetworkingDscBase(string name) : base(name)
    {
    }

    public sealed override RequiredModule SourceModule => NetworkingDsc.Instance;
}