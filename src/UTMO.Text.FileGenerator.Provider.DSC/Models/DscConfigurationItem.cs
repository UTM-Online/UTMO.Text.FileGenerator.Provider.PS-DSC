namespace UTMO.Text.FileGenerator.Provider.DSC.Models;

using System.Diagnostics.CodeAnalysis;
using UTMO.Text.FileGenerator.Attributes;
using UTMO.Text.FileGenerator.Provider.DSC.Enums;
using UTMO.Text.FileGenerator.Provider.DSC.SubResources;

[SuppressMessage("ReSharper", "MemberCanBeProtected.Global", Justification = "API Surface, must remain public for consumers")]
public abstract class DscConfigurationItem : RelatedTemplateResourceBase
{
    // ReSharper disable once PublicConstructorInAbstractClass
    public DscConfigurationItem(string name)
    {
        this.Name        = name;
        this.Description = string.Empty;
    }

    [MemberName("resource_id")]
    public abstract string ResourceId { get; }

    [MemberName("name")]
    public string Name { get; set; }

    [MemberName("description")]
    public string Description { get; set; }

    [MemberName("ensure")]
    public DscEnsure Ensure { get; set; }

    [MemberName("depends_on")]
    // ReSharper disable once CollectionNeverQueried.Global
    public List<string> DependsOn { get; set; } = new();

    [MemberName("property_bag")]
    public DscConfigurationPropertyBag PropertyBag { get; } = new();

    public sealed override bool GenerateManifest => false;

    public string DependencyName => $"[{this.ResourceId}]{this.Name}";

    public abstract RequiredModule SourceModule { get; }

    public DscConfigurationItem AddDependency<T>(T resource) where T : DscConfigurationItem
    {
        this.DependsOn.Add(resource.DependencyName);
        return this;
    }
}