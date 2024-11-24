namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;

using System.Diagnostics.CodeAnalysis;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public abstract class xStorageBase : DscConfigurationItem
{
    protected xStorageBase(string name) : base(name)
    {
    }

    public sealed override RequiredModule SourceModule => Modules.xStorage.Instance;
    
    public sealed override bool HasEnsure => true;
}