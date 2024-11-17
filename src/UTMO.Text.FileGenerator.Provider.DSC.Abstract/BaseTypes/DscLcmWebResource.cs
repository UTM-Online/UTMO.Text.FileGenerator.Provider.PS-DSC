namespace UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

using System.Diagnostics.CodeAnalysis;
using UTMO.Text.FileGenerator.Attributes;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Enums;

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
public class DscLcmWebResource : RelatedTemplateResourceBase
{
    internal DscLcmWebResource(DscWebResourceTypes resourceTypes, DscLcmConfiguration parent)
    {
        this.LcmResourceType = resourceTypes;
        this.Parent = parent;
    }
    
    public sealed override bool GenerateManifest => false;
    
    [MemberName("lcm_resource_type")]
    public DscWebResourceTypes LcmResourceType { get; set; }

    [MemberName("lcm_resource_name")]
    public string LcmResourceName { get; set; } = null!;
    
    [MemberName("server_url")]
    public string ServerUrl { get; set; } = null!;
    
    [MemberName("registration_key")]
    public string RegistrationKey { get; set; } = null!;

    [MemberName("configuration_names")]
    public List<string> ConfigurationNames =>
        this.LcmResourceType == DscWebResourceTypes.ConfigurationRepositoryWeb ? this.Parent.DscConfiguration.Select(a => a.FullName).ToList() : new List<string>();
    
    [IgnoreMember]
    private DscLcmConfiguration Parent { get; }
}