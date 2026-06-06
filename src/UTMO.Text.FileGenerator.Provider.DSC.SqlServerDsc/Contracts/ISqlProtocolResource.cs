namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Enums;

public interface ISqlProtocolResource : IDscResourceConfig
{
    string InstanceName { get; set; }

    SqlProtocolName ProtocolName { get; set; }

    string ServerName { get; set; }

    bool? Enabled { get; set; }

    bool? ListenOnAllIpAddresses { get; set; }

    int? KeepAlive { get; set; }

    string PipeName { get; set; }

    bool? SuppressRestart { get; set; }

    ushort? RestartTimeout { get; set; }
}
