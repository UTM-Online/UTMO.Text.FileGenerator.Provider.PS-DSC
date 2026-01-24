namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.NetworkingDsc;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.BaseDefinitions;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.NetworkingDsc.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Constants.NetworkingDscConstants.DnsClientGlobalSetting;

/// <summary>
/// Represents a DnsClientGlobalSetting DSC resource from the NetworkingDsc module.
/// This resource is used to configure DNS client global settings.
/// </summary>
public sealed class DnsClientGlobalSettingResource : NetworkingDscBase, IDnsClientGlobalSettingResource
{
    private DnsClientGlobalSettingResource(string name) : base(name)
    {
    }

    /// <summary>
    /// Gets or sets the single instance identifier. This must always be 'Yes'.
    /// </summary>
    public string IsSingleInstance
    {
        get => this.PropertyBag.Get(Constants.Properties.IsSingleInstance);
        set => this.PropertyBag.Set(Constants.Properties.IsSingleInstance, value);
    }

    /// <summary>
    /// Gets or sets the list of DNS suffixes to use for DNS name resolution.
    /// </summary>
    public string[] SuffixSearchList
    {
        get => this.PropertyBag.Get<string[]>(Constants.Properties.SuffixSearchList);
        set => this.PropertyBag.Set(Constants.Properties.SuffixSearchList, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether DNS devolution is enabled.
    /// </summary>
    public bool? UseDevolution
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.UseDevolution);
        set => this.PropertyBag.Set(Constants.Properties.UseDevolution, value);
    }

    /// <summary>
    /// Gets or sets the devolution level. This determines the level at which devolution stops.
    /// </summary>
    public uint? DevolutionLevel
    {
        get => this.PropertyBag.Get<uint?>(Constants.Properties.DevolutionLevel);
        set => this.PropertyBag.Set(Constants.Properties.DevolutionLevel, value);
    }

    /// <summary>
    /// Creates a new instance of the DnsClientGlobalSettingResource.
    /// </summary>
    /// <param name="name">The name of the resource instance.</param>
    /// <param name="configure">An action to configure the resource properties.</param>
    /// <returns>The configured resource instance.</returns>
    public static DnsClientGlobalSettingResource Create(string name, Action<IDnsClientGlobalSettingResource> configure)
    {
        var resource = new DnsClientGlobalSettingResource(name);
        configure(resource);
        return resource;
    }

    /// <summary>
    /// Creates a new instance of the DnsClientGlobalSettingResource.
    /// </summary>
    /// <param name="name">The name of the resource instance.</param>
    /// <param name="configure">An action to configure the resource properties.</param>
    /// <param name="resource">The configured resource instance output parameter.</param>
    /// <returns>The configured resource instance.</returns>
    public static DnsClientGlobalSettingResource Create(string name, Action<IDnsClientGlobalSettingResource> configure, out DnsClientGlobalSettingResource resource)
    {
        resource = new DnsClientGlobalSettingResource(name);
        configure(resource);
        return resource;
    }

    /// <inheritdoc />
    public override Task<List<ValidationFailedException>> Validate()
    {
        var errors = this.ValidationBuilder()
            .ValidateStringNotNullOrEmpty(this.IsSingleInstance, nameof(this.IsSingleInstance))
            .errors;

        return Task.FromResult(errors);
    }

    /// <inheritdoc />
    public override string ResourceId => Constants.ResourceId;
}
