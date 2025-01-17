namespace UTMO.Text.FileGenerator.Provider.DSC.WinCA;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

public class ActiveDirectoryCSDsc : RequiredModule
{
    private ActiveDirectoryCSDsc()
    {
    }
    
    public override string ModuleName => "ActiveDirectoryCSDsc";

    public override string ModuleVersion => "5.0.0";
    
    public static RequiredModule Instance { get; } = new ActiveDirectoryCSDsc();
}