namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Enums;

public interface ISqlDatabaseDefaultLocationResource : IDscResourceConfig
{
    string InstanceName { get; set; }

    SqlDatabaseDefaultLocationType Type { get; set; }

    string Path { get; set; }

    string ServerName { get; set; }

    bool? RestartService { get; set; }

    bool? ProcessOnlyOnActiveNode { get; set; }
}
