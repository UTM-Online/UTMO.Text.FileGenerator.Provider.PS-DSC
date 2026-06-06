namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

public interface ISqlConfigurationResource : IDscResourceConfig
{
    string InstanceName { get; set; }

    string OptionName { get; set; }

    int OptionValue { get; set; }

    string ServerName { get; set; }

    bool? RestartService { get; set; }

    uint? RestartTimeout { get; set; }
}
