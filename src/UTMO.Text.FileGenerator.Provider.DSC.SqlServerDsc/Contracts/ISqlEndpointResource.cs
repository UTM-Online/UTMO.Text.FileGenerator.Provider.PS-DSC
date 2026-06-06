namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Enums;

public interface ISqlEndpointResource : IDscResourceConfig
{
    string EndpointName { get; set; }

    string InstanceName { get; set; }

    SqlEndpointType EndpointType { get; set; }

    ushort? Port { get; set; }

    string ServerName { get; set; }

    string IpAddress { get; set; }

    string Owner { get; set; }

    bool? IsMessageForwardingEnabled { get; set; }

    uint? MessageForwardingSize { get; set; }

    SqlEndpointState? State { get; set; }
}
