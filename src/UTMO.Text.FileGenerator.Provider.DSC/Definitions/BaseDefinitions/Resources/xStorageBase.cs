namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;

using System.Diagnostics.CodeAnalysis;
using UTMO.Text.FileGenerator.Provider.DSC.Models;
using UTMO.Text.FileGenerator.Provider.DSC.SubResources;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public abstract class xStorageBase : DscConfigurationItem
{
    protected xStorageBase(string name) : base(name)
    {
    }

    public sealed override RequiredModule SourceModule
    {
        get => Modules.xStorage.Instance;
    }
}