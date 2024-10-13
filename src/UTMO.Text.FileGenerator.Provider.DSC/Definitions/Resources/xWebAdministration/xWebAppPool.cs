namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.xWebAdministration;

using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.xWebAdministrationConstants.xWebAppPool;

// ReSharper disable once InconsistentNaming
public class xWebAppPool : xWebAdministrationBase
{
    private xWebAppPool(string name) : base(name)
    {
        this.PropertyBag.Init(Constants.Properties.Name);
        this.PropertyBag.Init(Constants.Properties.IdentityType);
    }
    
    public string AppPoolName
    {
        get => this.PropertyBag.Get(Constants.Properties.Name);
        set => this.PropertyBag.Set(Constants.Properties.Name, value);
    }
    
    public xWebAppPoolIdentityType IdentityType
    {
        get => this.PropertyBag.Get<xWebAppPoolIdentityType>(Constants.Properties.IdentityType);
        set => this.PropertyBag.Set(Constants.Properties.IdentityType, value);
    }
    
    public static xWebAppPool Create(string name, Action<xWebAppPool> configure)
    {
        var resource = new xWebAppPool(name);
        configure(resource);
        return resource;
    }
    
    public static xWebAppPool Create(string name, Action<xWebAppPool> configure, out xWebAppPool resource)
    {
        resource = new xWebAppPool(name);
        configure(resource);
        return resource;
    }

    public override string ResourceId
    {
        get => Constants.ResourceId;
    }
}