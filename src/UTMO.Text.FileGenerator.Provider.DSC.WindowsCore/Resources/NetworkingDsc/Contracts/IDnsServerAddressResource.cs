namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.NetworkingDsc.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.NetworkingDsc.Enums;

/// <summary>
/// Represents the configuration interface for a DnsServerAddress DSC resource.
/// </summary>
public interface IDnsServerAddressResource : IDscResourceConfig
{
    /// <summary>
    /// Gets or sets the alias of the network interface for which the DNS server address is being set.
    /// </summary>
    string InterfaceAlias { get; set; }
    
    /// <summary>
    /// Gets or sets the IP address family (IPv4 or IPv6).
    /// </summary>
    AddressFamily AddressFamily { get; set; }
    
    /// <summary>
    /// Gets or sets the desired DNS Server address(es). Exclude this value to set the DNS server to DHCP.
    /// </summary>
    string[] Address { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether to validate the DNS servers after they have been set.
    /// </summary>
    bool? ValidateDns { get; set; }
}
