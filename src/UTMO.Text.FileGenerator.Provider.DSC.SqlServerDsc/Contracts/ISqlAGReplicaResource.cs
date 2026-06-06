namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Enums;

public interface ISqlAGReplicaResource : IDscResourceConfig
{
    string ReplicaName { get; set; }

    string AvailabilityGroupName { get; set; }

    string ServerName { get; set; }

    string InstanceName { get; set; }

    string PrimaryReplicaServerName { get; set; }

    string PrimaryReplicaInstanceName { get; set; }

    SqlAGAvailabilityMode? AvailabilityMode { get; set; }

    uint? BackupPriority { get; set; }

    SqlAGConnectionModeInPrimaryRole? ConnectionModeInPrimaryRole { get; set; }

    SqlAGConnectionModeInSecondaryRole? ConnectionModeInSecondaryRole { get; set; }

    string EndpointHostName { get; set; }

    SqlAGFailoverMode? FailoverMode { get; set; }

    string ReadOnlyRoutingConnectionUrl { get; set; }

    string[] ReadOnlyRoutingList { get; set; }

    bool? ProcessOnlyOnActiveNode { get; set; }

    SqlAGSeedingMode? SeedingMode { get; set; }
}
