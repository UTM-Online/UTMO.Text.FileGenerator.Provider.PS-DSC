namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Modules;

using UTMO.Text.FileGenerator.Provider.DSC.SubResources;

public class PSDesiredStateConfiguration : RequiredModule
{
    private PSDesiredStateConfiguration()
    {
    }

    public override string ModuleName => "PSDesiredStateConfiguration";
    public override string ModuleVersion => string.Empty;
    
    public static RequiredModule Instance { get; } = new PSDesiredStateConfiguration();
}