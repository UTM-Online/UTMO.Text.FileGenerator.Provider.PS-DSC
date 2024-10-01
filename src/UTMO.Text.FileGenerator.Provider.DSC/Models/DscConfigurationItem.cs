namespace UTMO.Text.FileGenerator.Provider.DSC.Resources;

using UTMO.Text.FileGenerator.Attributes;
using UTMO.Text.FileGenerator.Provider.DSC.Enums;
using UTMO.Text.FileGenerator.Provider.DSC.SubResources;

public abstract class DscConfigurationItem : RelatedTemplateResourceBase
{
    public DscConfigurationItem(string name)
    {
        this.Name = name;
    }
    
    [MemberName("resource_id")]
    public abstract string ResourceId  { get; }
    
    [MemberName("name")]
    public string Name        { get; set; }
    
    [MemberName("description")]
    public string Description { get; set; }
    
    [MemberName("ensure")]
    public DscEnsure Ensure   { get; set; }
    
    [MemberName("depends_on")]
    public List<string> DependsOn { get; set; } = new();

    [MemberName("property_bag")]
    public Dictionary<string, string> PropertyBag { get; } = new();

    public sealed override bool GenerateManifest => false;
    
    public string DependencyName => $"[{this.ResourceId}]{this.Name}";
    
    public abstract RequiredModule SourceModule { get; }
    
    public DscConfigurationItem AddDependency<T>(T resource) where T : DscConfigurationItem
    {
        this.DependsOn.Add(resource.DependencyName);
        return this;
    }
}