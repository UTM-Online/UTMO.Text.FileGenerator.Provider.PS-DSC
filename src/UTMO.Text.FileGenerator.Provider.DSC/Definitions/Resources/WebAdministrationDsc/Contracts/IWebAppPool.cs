namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Enums;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Types;
public interface IWebAppPool : IDscResourceConfig
{
    string PoolName { get; set; }
    AppPoolState? State { get; set; }
    bool? AutoStart { get; set; }
    bool? Enable32BitAppOnWin64 { get; set; }
    ManagedPipelineMode? ManagedPipelineMode { get; set; }
    string? ManagedRuntimeVersion { get; set; }
    AppPoolStartMode? StartMode { get; set; }
    AppPoolIdentityType? IdentityType { get; set; }
    GmsaCredential? Credential { get; set; }
    string? IdleTimeout { get; set; }
    IdleTimeoutAction? IdleTimeoutAction { get; set; }
    uint? MaxProcesses { get; set; }
    bool? RapidFailProtection { get; set; }
    string? RestartTimeLimit { get; set; }
    string[]? RestartSchedule { get; set; }
}
