namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.NetworkingDsc.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

/// <summary>
/// Interface for DnsClientGlobalSetting DSC resource configuration.
/// This resource is used to configure DNS client global settings.
/// </summary>
public interface IDnsClientGlobalSettingResource : IDscResourceConfig
{
    /// <summary>
    /// Gets or sets the single instance identifier. This must always be 'Yes'.
    /// </summary>
    string IsSingleInstance { get; set; }
    
    /// <summary>
    /// Gets or sets the list of DNS suffixes to use for DNS name resolution.
    /// </summary>
    string[] SuffixSearchList { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether DNS devolution is enabled.
    /// </summary>
    bool? UseDevolution { get; set; }
    
    /// <summary>
    /// Gets or sets the devolution level. This determines the level at which devolution stops.
    /// </summary>
    uint? DevolutionLevel { get; set; }
}
