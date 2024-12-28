namespace UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

using System.Diagnostics.CodeAnalysis;
using Models;
using UTMO.Text.FileGenerator.Attributes;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Enums;

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class DscLcmWebResource : SubTemplateResourceBase
{
    public DscLcmWebResource(DscLcmConfiguration parent)
    {
        this.Parent = parent;
    }

    public sealed override bool GenerateManifest => false;
    
    [MemberName("lcm_resource_type")]
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public required DscWebResourceTypes LcmResourceType { get; init; }

    [MemberName("lcm_resource_name")]
    public required string LcmResourceName { get; init; }
    
    [MemberName("server_url")]
    public required string ServerUrl { get; init; }

    [MemberName("configuration_names")]
    public List<string> ConfigurationNames =>
        this.LcmResourceType == DscWebResourceTypes.ConfigurationRepositoryWeb ? this.Parent.DscConfiguration.Select(a => a.FullName).ToList() : [];
    
    [IgnoreMember]
    private DscLcmConfiguration Parent { get; }
}