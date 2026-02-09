namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Modules;

/// <summary>
/// Base class for all WebAdministrationDsc resources.
/// </summary>
public abstract class WebAdministrationDscBase : DscConfigurationItem
{
    protected WebAdministrationDscBase(string name) : base(name)
    {
    }

    public sealed override RequiredModule SourceModule => WebAdministrationDsc.Instance;

    public sealed override bool HasEnsure => true;
}

