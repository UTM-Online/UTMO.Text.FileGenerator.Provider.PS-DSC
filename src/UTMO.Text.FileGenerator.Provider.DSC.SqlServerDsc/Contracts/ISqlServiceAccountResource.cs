namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Enums;

public interface ISqlServiceAccountResource : IDscResourceConfig
{
    string InstanceName { get; set; }

    SqlServiceType ServiceType { get; set; }

    string ServiceAccount { get; set; }

    string ServerName { get; set; }

    bool? RestartService { get; set; }

    bool? Force { get; set; }

    string VersionNumber { get; set; }
}
