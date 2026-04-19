namespace UTMO.Text.FileGenerator.Provider.DSC.ChocoModule;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

public abstract class ChocoModuleBase : DscConfigurationItem
{
    protected ChocoModuleBase(string name) : base(name)
    {
    }

    public sealed override RequiredModule SourceModule => ChocoModule.Instance;
}

