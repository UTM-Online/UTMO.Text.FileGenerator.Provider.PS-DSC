namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

public interface ISqlDatabaseObjectPermissionResource : IDscResourceConfig
{
    string InstanceName { get; set; }

    string DatabaseName { get; set; }

    string SchemaName { get; set; }

    string ObjectName { get; set; }

    string ObjectType { get; set; }

    string PrincipalName { get; set; }

    string[] Permission { get; set; }

    string ServerName { get; set; }

    bool? Force { get; set; }

    string State { get; set; }
}
