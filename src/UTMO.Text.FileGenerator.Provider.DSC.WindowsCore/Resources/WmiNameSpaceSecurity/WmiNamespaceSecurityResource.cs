namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.WmiNameSpaceSecurity;

using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.BaseDefinitions;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.WmiNameSpaceSecurity.Enums;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Constants.WmiNamespaceSecurityConstants.WmiNamespaceSecurity;

public class WmiNamespaceSecurityResource : WmiNameSpaceSecurityBase
{
    private WmiNamespaceSecurityResource(string name) : base(name)
    {
        this.PropertyBag.Init(Constants.Properties.Path);
        this.PropertyBag.Init(Constants.Properties.Principal);
        this.PropertyBag.Init<NamespacePermissions[]>(Constants.Properties.Permission);
        this.PropertyBag.Init(Constants.Properties.AccessType);
        this.PropertyBag.Init<WmiSecurityAppliesTo>(Constants.Properties.AppliesTo);
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
    
    public static WmiNamespaceSecurityResource Create(string name, Action<WmiNamespaceSecurityResource> configure)
    {
        var resource = new WmiNamespaceSecurityResource(name);
        configure(resource);
        return resource;
    }
    
    public static WmiNamespaceSecurityResource Create(string name, Action<WmiNamespaceSecurityResource> configure, out WmiNamespaceSecurityResource resource)
    {
        resource = new WmiNamespaceSecurityResource(name);
        configure(resource);
        return resource;
    }

    public override string ResourceId
    {
        get => Constants.ResourceId;
    }
}