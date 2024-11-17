namespace UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

using UTMO.Text.FileGenerator.Attributes;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Enums;

public class DscLcmWebResource : RelatedTemplateResourceBase
{
    public sealed override bool GenerateManifest => false;
    
    [MemberName("resource_name")]
    public string ResourceName { get; set; }
    
    [MemberName("server_url")]
    public string ServerUrl { get; set; }
    
    [MemberName("registration_key")]
    public string RegistrationKey { get; set; }
    
    [MemberName("configuration_names")]
    public List<string> ConfigurationNames { get; set; } = new();
}