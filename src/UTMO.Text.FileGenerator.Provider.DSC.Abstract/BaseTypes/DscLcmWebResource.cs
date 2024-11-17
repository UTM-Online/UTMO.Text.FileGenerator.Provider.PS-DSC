namespace UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

using UTMO.Text.FileGenerator.Attributes;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Enums;

public class DscLcmWebResource : RelatedTemplateResourceBase
{
    internal DscLcmWebResource(DscWebResourceTypes resourceTypes)
    {
        this.LcmResourceType = resourceTypes;
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
    public List<string> ConfigurationNames { get; set; } = new();
}