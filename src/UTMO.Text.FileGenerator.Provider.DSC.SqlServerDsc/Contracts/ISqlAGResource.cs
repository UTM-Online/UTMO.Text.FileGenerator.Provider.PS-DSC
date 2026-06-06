namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Enums;

public interface ISqlAGResource : IDscResourceConfig
{
    string AgName { get; set; }

    string ServerName { get; set; }

    string InstanceName { get; set; }

    SqlAGAutomatedBackupPreference? AutomatedBackupPreference { get; set; }

    SqlAGAvailabilityMode? AvailabilityMode { get; set; }

    uint? BackupPriority { get; set; }

    bool? BasicAvailabilityGroup { get; set; }

    bool? DatabaseHealthTrigger { get; set; }

    bool? DtcSupportEnabled { get; set; }

    SqlAGConnectionModeInPrimaryRole? ConnectionModeInPrimaryRole { get; set; }

    SqlAGConnectionModeInSecondaryRole? ConnectionModeInSecondaryRole { get; set; }

    string EndpointHostName { get; set; }

    SqlAGFailureConditionLevel? FailureConditionLevel { get; set; }

    SqlAGFailoverMode? FailoverMode { get; set; }

    SqlAGSeedingMode? SeedingMode { get; set; }

    uint? HealthCheckTimeout { get; set; }

    bool? ProcessOnlyOnActiveNode { get; set; }
}
