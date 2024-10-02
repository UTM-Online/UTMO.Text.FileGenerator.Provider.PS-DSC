namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;

using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Modules;
using UTMO.Text.FileGenerator.Provider.DSC.Resources;
using UTMO.Text.FileGenerator.Provider.DSC.SubResources;

// ReSharper disable once InconsistentNaming
public abstract class xPSDesiredStateConfigurationBase : DscConfigurationItem
{
    protected xPSDesiredStateConfigurationBase(string name) : base(name)
    {
    }

    public sealed override RequiredModule SourceModule => xPSDesiredStateConfiguration.Instance;
}