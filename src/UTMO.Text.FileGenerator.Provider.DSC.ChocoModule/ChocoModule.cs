namespace UTMO.Text.FileGenerator.Provider.DSC.ChocoModule;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

public class ChocoModule : RequiredModule
{
    private ChocoModule()
    {
    }

    public override string ModuleName => "Chocolatey";
    public override string ModuleVersion => "0.10.5";

    public static RequiredModule Instance { get; } = new ChocoModule();
}

