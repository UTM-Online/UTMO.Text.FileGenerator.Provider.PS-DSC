namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;

using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Modules;
using UTMO.Text.FileGenerator.Provider.DSC.Models;
using UTMO.Text.FileGenerator.Provider.DSC.SubResources;

// ReSharper disable once InconsistentNaming
public abstract class cChocoBase : DscConfigurationItem
{
    protected cChocoBase(string name) : base(name)
    {
    }

    public sealed override RequiredModule SourceModule => cChoco.Instance;
}