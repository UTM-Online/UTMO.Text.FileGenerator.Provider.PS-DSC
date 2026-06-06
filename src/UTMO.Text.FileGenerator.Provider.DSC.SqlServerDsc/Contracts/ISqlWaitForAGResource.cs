namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

public interface ISqlWaitForAGResource : IDscResourceConfig
{
    string InstanceName { get; set; }

    string ServerName { get; set; }

    string GroupName { get; set; }

    ulong? RetryIntervalSec { get; set; }

    uint? RetryCount { get; set; }
}
