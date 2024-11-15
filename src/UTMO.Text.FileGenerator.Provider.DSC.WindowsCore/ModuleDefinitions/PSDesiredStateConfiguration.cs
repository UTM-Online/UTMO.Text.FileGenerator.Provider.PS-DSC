namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.ModuleDefinitions;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

public class PSDesiredStateConfiguration : RequiredModule
{
    private PSDesiredStateConfiguration()
    {
    }

    public override string ModuleName => "PSDesiredStateConfiguration";
    public override string ModuleVersion => string.Empty;
    
    public static RequiredModule Instance { get; } = new PSDesiredStateConfiguration();
}