namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Enums;

public interface ISqlAgentFailsafeResource : IDscResourceConfig
{
    string OperatorName { get; set; }

    string ServerName { get; set; }

    string InstanceName { get; set; }

    SqlAgentNotificationMethod? NotificationMethod { get; set; }
}
