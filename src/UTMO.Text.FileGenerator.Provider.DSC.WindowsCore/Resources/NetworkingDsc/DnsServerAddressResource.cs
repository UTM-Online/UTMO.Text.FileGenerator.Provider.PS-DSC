namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.NetworkingDsc;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Attributes;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.BaseDefinitions;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.NetworkingDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.NetworkingDsc.Enums;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Constants.NetworkingDscConstants.DnsServerAddress;

/// <summary>
/// Represents a DnsServerAddress DSC resource from the NetworkingDsc module.
/// This resource is used to configure DNS server addresses for a network interface.
/// </summary>
public sealed class DnsServerAddressResource : NetworkingDscBase, IDnsServerAddressResource
{
    private DnsServerAddressResource(string name) : base(name)
    {
    }

    /// <summary>
    /// Gets or sets the alias of the network interface for which the DNS server address is being set.
    /// </summary>
    public string InterfaceAlias
    {
        get => this.PropertyBag.Get(Constants.Properties.InterfaceAlias);
        set => this.PropertyBag.Set(Constants.Properties.InterfaceAlias, value);
    }

    /// <summary>
    /// Gets or sets the IP address family (IPv4 or IPv6).
    /// </summary>
    [UnquotedEnum]
    public AddressFamily AddressFamily
    {
        get => this.PropertyBag.Get<AddressFamily>(Constants.Properties.AddressFamily);
        set => this.PropertyBag.Set(Constants.Properties.AddressFamily, value);
    }

    /// <summary>
    /// Gets or sets the desired DNS Server address(es). Exclude this value to set the DNS server to DHCP.
    /// </summary>
    public string[] Address
    {
        get => this.PropertyBag.Get<string[]>(Constants.Properties.Address);
        set => this.PropertyBag.Set(Constants.Properties.Address, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether to validate the DNS servers after they have been set.
    /// </summary>
    public bool? ValidateDns
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.Validate);
        set => this.PropertyBag.Set(Constants.Properties.Validate, value);
    }

    /// <summary>
    /// Creates a new instance of the DnsServerAddressResource.
    /// </summary>
    /// <param name="name">The name of the resource instance.</param>
    /// <param name="configure">An action to configure the resource properties.</param>
    /// <returns>The configured resource instance.</returns>
    public static DnsServerAddressResource Create(string name, Action<IDnsServerAddressResource> configure)
    {
        var resource = new DnsServerAddressResource(name);
        configure(resource);
        return resource;
    }

    /// <summary>
    /// Creates a new instance of the DnsServerAddressResource.
    /// </summary>
    /// <param name="name">The name of the resource instance.</param>
    /// <param name="configure">An action to configure the resource properties.</param>
    /// <param name="resource">The configured resource instance output parameter.</param>
    /// <returns>The configured resource instance.</returns>
    public static DnsServerAddressResource Create(string name, Action<IDnsServerAddressResource> configure, out DnsServerAddressResource resource)
    {
        resource = new DnsServerAddressResource(name);
        configure(resource);
        return resource;
    }

    /// <inheritdoc />
    public override Task<List<ValidationFailedException>> Validate()
    {
        var errors = this.ValidationBuilder()
            .ValidateStringNotNullOrEmpty(this.InterfaceAlias, nameof(this.InterfaceAlias))
            .errors;

        return Task.FromResult(errors);
    }

    /// <inheritdoc />
    public override string ResourceId => Constants.ResourceId;

    public override bool HasEnsure => false;
}
