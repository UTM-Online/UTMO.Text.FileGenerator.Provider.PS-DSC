namespace UTMO.Text.FileGenerator.Provider.DSC.WindowsDefender;

using Abstract.BaseTypes;

public abstract class WindowsDefenderBase(string name) : DscConfigurationItem(name)
{
    public sealed override RequiredModule SourceModule => WindowsDefender.Instance;
}