namespace UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

using System.Diagnostics.CodeAnalysis;
using Models;
using UTMO.Text.FileGenerator.Attributes;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Enums;

[SuppressMessage("ReSharper", "MemberCanBeProtected.Global", Justification = "API Surface, must remain public for consumers")]
public abstract class DscConfigurationItem : SubTemplateResourceBase
{
    // ReSharper disable once PublicConstructorInAbstractClass
    public DscConfigurationItem(string name)
    {
        this.Name        = name;
        this.Description = string.Empty;
        // Register this instance's type as the owner of the property bag for attribute lookups
        this.PropertyBag.SetOwner(this.GetType());
    }

    [TemplateProperty]
    [MemberName("resource_id")]
    public abstract string ResourceId { get; }

    [TemplateProperty]
    [MemberName("name")]
    public string Name { get; set; }

    [TemplateProperty]
    [MemberName("description")]
    public string Description { get; set; }

    [TemplateProperty]
    [MemberName("ensure")]
    public DscEnsure Ensure { get; set; }

    [TemplateProperty]
    [MemberName("depends_on")]
    // ReSharper disable once CollectionNeverQueried.Global
    public List<string> DependsOn { get; set; } = new();

    [TemplateProperty]
    [MemberName("property_bag")]
    public virtual DscConfigurationPropertyBag PropertyBag { get; } = new();

    [IgnoreMember]
    public virtual bool RequiresPlainTextPassword => this.PropertyBag.ContainsValue<IRequiresPlainTextPassword>();
    
    [TemplateProperty]
    [MemberName("has_ensure")]
    public abstract bool HasEnsure { get; }
    
    public override bool GenerateManifest => false;

    public string DependencyName => $"[{this.ResourceId}]{this.Name}";

    public abstract RequiredModule SourceModule { get; }

    public DscConfigurationItem AddDependency<T>(T resource) where T : DscConfigurationItem
    {
        this.DependsOn.Add(resource.DependencyName);
        return this;
    }
}