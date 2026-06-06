namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

public interface ISqlProtocolTcpIpResource : IDscResourceConfig
{
    string InstanceName { get; set; }

    string IpAddressGroup { get; set; }

    string ServerName { get; set; }

    bool? Enabled { get; set; }

    string IpAddress { get; set; }

    bool? UseTcpDynamicPort { get; set; }

    string TcpPort { get; set; }

    bool? SuppressRestart { get; set; }

    ushort? RestartTimeout { get; set; }
}
