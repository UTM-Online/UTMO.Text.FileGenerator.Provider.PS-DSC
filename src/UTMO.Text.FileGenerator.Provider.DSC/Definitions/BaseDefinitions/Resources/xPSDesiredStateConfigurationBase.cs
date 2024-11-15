namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Modules;

// ReSharper disable once InconsistentNaming
public abstract class xPSDesiredStateConfigurationBase : DscConfigurationItem
{
    protected xPSDesiredStateConfigurationBase(string name) : base(name)
    {
    }

    public sealed override RequiredModule SourceModule => xPSDesiredStateConfiguration.Instance;
}