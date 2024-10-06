namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WmiNameSpaceSecurity;

using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WmiNameSpaceSecurity.Enums;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.WmiNamespaceSecurityConstants.WmiNamespaceSecurity;

public class WmiNamespaceSecurityResource : WmiNameSpaceSecurityBase
{
    public WmiNamespaceSecurityResource(string name) : base(name)
    {
        this.PropertyBag.Init(Constants.Properties.Path);
        this.PropertyBag.Init(Constants.Properties.Principal);
        this.PropertyBag.Init<WmiSecurityAppliesTo[]>(Constants.Properties.Permission);
        this.PropertyBag.Init(Constants.Properties.AccessType);
        this.PropertyBag.Init(Constants.Properties.AppliesTo);
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
    
    public WmiSecurityAppliesTo[] Permission
    {
        get => this.PropertyBag.Get<WmiSecurityAppliesTo[]>(Constants.Properties.Permission);
        set => this.PropertyBag.Set(Constants.Properties.Permission, value);
    }
    
    public string AccessType
    {
        get => this.PropertyBag.Get(Constants.Properties.AccessType);
        set => this.PropertyBag.Set(Constants.Properties.AccessType, value);
    }
    
    public string AppliesTo
    {
        get => this.PropertyBag.Get(Constants.Properties.AppliesTo);
        set => this.PropertyBag.Set(Constants.Properties.AppliesTo, value);
    }

    public override string ResourceId
    {
        get => Constants.ResourceId;
    }
}