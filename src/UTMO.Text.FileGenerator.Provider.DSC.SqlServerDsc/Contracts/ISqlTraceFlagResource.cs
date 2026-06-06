namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

public interface ISqlTraceFlagResource : IDscResourceConfig
{
    string ServerName { get; set; }

    string InstanceName { get; set; }

    uint[] TraceFlags { get; set; }

    uint[] TraceFlagsToInclude { get; set; }

    uint[] TraceFlagsToExclude { get; set; }

    bool? ClearAllTraceFlags { get; set; }

    bool? RestartService { get; set; }

    uint? RestartTimeout { get; set; }
}
