namespace UTMO.Text.FileGenerator.Provider.DSC.cChoco;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

// ReSharper disable once InconsistentNaming
public abstract class cChocoBase : DscConfigurationItem
{
    protected cChocoBase(string name) : base(name)
    {
    }

    public sealed override RequiredModule SourceModule => cChoco.Instance;
}