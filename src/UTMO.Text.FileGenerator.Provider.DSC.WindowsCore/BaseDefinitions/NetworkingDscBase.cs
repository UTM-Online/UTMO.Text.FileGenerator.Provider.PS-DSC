namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.BaseDefinitions;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.ModuleDefinitions;

public abstract class NetworkingDscBase : DscConfigurationItem
{
    protected NetworkingDscBase(string name) : base(name)
    {
    }

    public sealed override RequiredModule SourceModule => NetworkingDsc.Instance;
}