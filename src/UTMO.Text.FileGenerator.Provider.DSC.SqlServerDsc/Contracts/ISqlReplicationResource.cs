namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Enums;

public interface ISqlReplicationResource : IDscResourceConfig
{
    string InstanceName { get; set; }

    SqlReplicationDistributorMode DistributorMode { get; set; }

    string AdminLinkCredentials { get; set; }

    string DistributionDBName { get; set; }

    string RemoteDistributor { get; set; }

    string WorkingDirectory { get; set; }

    bool? UseTrustedConnection { get; set; }

    bool? UninstallWithForce { get; set; }
}
