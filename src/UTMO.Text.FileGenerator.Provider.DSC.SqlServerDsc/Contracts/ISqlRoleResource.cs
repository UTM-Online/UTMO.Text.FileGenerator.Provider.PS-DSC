namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

public interface ISqlRoleResource : IDscResourceConfig
{
    string ServerRoleName { get; set; }

    string InstanceName { get; set; }

    string ServerName { get; set; }

    string[] Members { get; set; }

    string[] MembersToInclude { get; set; }

    string[] MembersToExclude { get; set; }
}
