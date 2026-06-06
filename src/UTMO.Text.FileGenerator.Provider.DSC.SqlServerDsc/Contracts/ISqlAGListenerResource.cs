namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

public interface ISqlAGListenerResource : IDscResourceConfig
{
    string InstanceName { get; set; }

    string ServerName { get; set; }

    string ListenerName { get; set; }

    string AvailabilityGroup { get; set; }

    string[] IpAddress { get; set; }

    ushort? Port { get; set; }

    bool? DHCP { get; set; }

    bool? ProcessOnlyOnActiveNode { get; set; }
}
