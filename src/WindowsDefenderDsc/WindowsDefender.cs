namespace UTMO.Text.FileGenerator.Provider.DSC.WindowsDefender;

using Abstract.BaseTypes;

public class WindowsDefender : RequiredModule
{
    private WindowsDefender()
    {
    }
    
    public override string ModuleName=> "WindowsDefender";

    public override string ModuleVersion => "1.0.0.4";
    
    public static RequiredModule Instance { get; } = new WindowsDefender();
}