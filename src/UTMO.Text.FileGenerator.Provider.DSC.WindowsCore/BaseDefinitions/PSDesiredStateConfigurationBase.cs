namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.BaseDefinitions;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.ModuleDefinitions;

// ReSharper disable once InconsistentNaming
public abstract class PSDesiredStateConfigurationBase : DscConfigurationItem
{
    protected PSDesiredStateConfigurationBase(string name) : base(name)
    {
    }

    public sealed override RequiredModule SourceModule => PSDesiredStateConfiguration.Instance;
}