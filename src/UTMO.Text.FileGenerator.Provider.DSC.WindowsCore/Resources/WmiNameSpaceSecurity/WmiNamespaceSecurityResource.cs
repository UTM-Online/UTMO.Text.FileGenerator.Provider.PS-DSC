namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.WmiNameSpaceSecurity;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.BaseDefinitions;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.WmiNameSpaceSecurity.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.WmiNameSpaceSecurity.Enums;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Constants.WmiNamespaceSecurityConstants.WmiNamespaceSecurity;

public class WmiNamespaceSecurityResource : WmiNameSpaceSecurityBase, IWmiNamespaceSecurityResource
{
    private WmiNamespaceSecurityResource(string name) : base(name)
    {
    }
    
    public string Path
    {
        get => this.PropertyBag.Get(Constants.Properties.Path);
        set => this.PropertyBag.Set(Constants.Properties.Path, value);
    }
    
    public string Principal
    {
        get => this.PropertyBag.Get(Constants.Properties.Principal);
        set => this.PropertyBag.Set(Constants.Properties.Principal, value);
    }
    
    public NamespacePermissions[] Permission
    {
        get => this.PropertyBag.Get<NamespacePermissions[]>(Constants.Properties.Permission);
        set => this.PropertyBag.Set(Constants.Properties.Permission, value);
    }
    
    /// <summary>
    /// Available Options in <see cref="WmiSecurityAccessType"/>
    /// </summary>
    public string AccessType
    {
        get => this.PropertyBag.Get(Constants.Properties.AccessType);
        set => this.PropertyBag.Set(Constants.Properties.AccessType, value);
    }
    
    public WmiSecurityAppliesTo AppliesTo
    {
        get => this.PropertyBag.Get<WmiSecurityAppliesTo>(Constants.Properties.AppliesTo);
        set => this.PropertyBag.Set(Constants.Properties.AppliesTo, value);
    }
    
    public static WmiNamespaceSecurityResource Create(string name, Action<IWmiNamespaceSecurityResource> configure)
    {
        var resource = new WmiNamespaceSecurityResource(name);
        configure(resource);
        return resource;
    }
    
    public static WmiNamespaceSecurityResource Create(string name, Action<IWmiNamespaceSecurityResource> configure, out WmiNamespaceSecurityResource resource)
    {
        resource = new WmiNamespaceSecurityResource(name);
        configure(resource);
        return resource;
    }
    
    public override Task<List<ValidationFailedException>> Validate()
    {
        var errors = this.ValidationBuilder()
            .ValidateStringNotNullOrEmpty(this.Path, nameof(this.Path))
            .ValidateStringNotNullOrEmpty(this.Principal, nameof(this.Principal))
            .ValidateStringNotNullOrEmpty(this.AccessType, nameof(this.AccessType))
            .errors;

        return Task.FromResult(errors);
    }

    public override string ResourceId => Constants.ResourceId;
}