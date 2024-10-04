namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;

using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Modules;
using UTMO.Text.FileGenerator.Provider.DSC.Models;
using UTMO.Text.FileGenerator.Provider.DSC.SubResources;

// ReSharper disable once InconsistentNaming
public abstract class PSDesiredStateConfigurationBase : DscConfigurationItem
{
    protected PSDesiredStateConfigurationBase(string name) : base(name)
    {
    }

    public sealed override RequiredModule SourceModule => PSDesiredStateConfiguration.Instance;
}