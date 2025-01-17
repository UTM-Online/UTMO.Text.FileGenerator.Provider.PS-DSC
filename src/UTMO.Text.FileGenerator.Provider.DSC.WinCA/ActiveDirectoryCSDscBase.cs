namespace UTMO.Text.FileGenerator.Provider.DSC.WinCA;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

public abstract class ActiveDirectoryCSDscBase : DscConfigurationItem
{
    protected ActiveDirectoryCSDscBase(string name) : base(name)
    {
    }
    
    public sealed override RequiredModule SourceModule => ActiveDirectoryCSDsc.Instance;
}