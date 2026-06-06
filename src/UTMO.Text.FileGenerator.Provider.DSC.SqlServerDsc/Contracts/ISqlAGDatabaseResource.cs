namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

public interface ISqlAGDatabaseResource : IDscResourceConfig
{
    string[] DatabaseName { get; set; }

    string ServerName { get; set; }

    string InstanceName { get; set; }

    string AvailabilityGroupName { get; set; }

    string BackupPath { get; set; }

    bool? Force { get; set; }

    bool? MatchDatabaseOwner { get; set; }

    bool? ReplaceExisting { get; set; }

    bool? ProcessOnlyOnActiveNode { get; set; }

    int? StatementTimeout { get; set; }
}
