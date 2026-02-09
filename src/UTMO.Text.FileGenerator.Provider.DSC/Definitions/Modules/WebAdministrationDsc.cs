namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Modules;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

/// <summary>
/// WebAdministrationDsc module definition for IIS configuration resources.
/// </summary>
public sealed class WebAdministrationDsc : RequiredModule
{
    private WebAdministrationDsc()
    {
    }

    public override string ModuleName => "WebAdministrationDsc";

    public override string ModuleVersion => "4.2.1";

    public override string? RewriteModuleVersion => "4.2.1.0";

    public override bool UseAlternateFormat => true;

    public static RequiredModule Instance { get; } = new WebAdministrationDsc();
}

