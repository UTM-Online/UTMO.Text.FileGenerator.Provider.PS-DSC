namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

public interface ISqlDatabaseRoleResource : IDscResourceConfig
{
    string InstanceName { get; set; }

    string DatabaseName { get; set; }

    string RoleName { get; set; }

    string ServerName { get; set; }

    string[] Members { get; set; }

    string[] MembersToInclude { get; set; }

    string[] MembersToExclude { get; set; }
}
