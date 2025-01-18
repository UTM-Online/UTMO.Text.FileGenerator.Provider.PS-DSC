namespace UTMO.Text.FileGenerator.Provider.DSC.AD_DSC;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

public class ActiveDirectoryDsc : RequiredModule
{
    private ActiveDirectoryDsc()
    {
    }
    
    public override string ModuleName => "ActiveDirectoryDsc";

    public override string ModuleVersion => "6.6.0";
    
    public static RequiredModule Instance { get; } = new ActiveDirectoryDsc();
}