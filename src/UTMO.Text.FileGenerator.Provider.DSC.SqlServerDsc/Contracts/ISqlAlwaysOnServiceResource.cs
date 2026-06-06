namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

public interface ISqlAlwaysOnServiceResource : IDscResourceConfig
{
    string InstanceName { get; set; }

    string ServerName { get; set; }

    uint? RestartTimeout { get; set; }
}
