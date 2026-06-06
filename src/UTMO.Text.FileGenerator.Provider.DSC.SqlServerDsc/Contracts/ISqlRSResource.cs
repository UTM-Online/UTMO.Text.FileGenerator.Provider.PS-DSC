namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

public interface ISqlRSResource : IDscResourceConfig
{
    string InstanceName { get; set; }

    string DatabaseServerName { get; set; }

    string DatabaseInstanceName { get; set; }

    string ReportServerVirtualDirectory { get; set; }

    string ReportsVirtualDirectory { get; set; }

    string[] ReportServerReservedUrl { get; set; }

    string[] ReportsReservedUrl { get; set; }

    bool? UseSsl { get; set; }

    bool? SuppressRestart { get; set; }

    uint? RestartTimeout { get; set; }

    string Encrypt { get; set; }
}
