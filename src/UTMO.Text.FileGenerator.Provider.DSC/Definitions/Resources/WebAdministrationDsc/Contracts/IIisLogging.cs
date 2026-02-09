namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Enums;
public interface IIisLogging : IDscResourceConfig
{
    string LogPath { get; set; }
    LogFlags[]? LogFlags { get; set; }
    LogPeriod? LogPeriod { get; set; }
    string? LogTruncateSize { get; set; }
    bool? LoglocalTimeRollover { get; set; }
    LogFormat? LogFormat { get; set; }
    string? LogTargetW3C { get; set; }
}
