namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.xWebAdministration;

using UTMO.Text.FileGenerator.Provider.DSC.Constants;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.xWebAdministration.Contracts;
using Constants = Constants.xWebAdministrationConstants.xWebConfigProperty;

public class xWebConfigProperty : xWebAdministrationBase, IxWebConfigProperty
{
    private xWebConfigProperty(string name) : base(name)
    {
    }
    
    public string WebsitePath
    {
        get => this.PropertyBag.Get(Constants.Properties.WebsitePath);
        set => this.PropertyBag.Set(Constants.Properties.WebsitePath, value);
    }
    
    public string Filter
    {
        get => this.PropertyBag.Get(Constants.Properties.Filter);
        set => this.PropertyBag.Set(Constants.Properties.Filter, value);
    }
    
    public string PropertyName
    {
        get => this.PropertyBag.Get(Constants.Properties.PropertyName);
        set => this.PropertyBag.Set(Constants.Properties.PropertyName, value);
    }
    
    public string Value
    {
        get => this.PropertyBag.Get(Constants.Properties.Value);
        set => this.PropertyBag.Set(Constants.Properties.Value, value);
    }
    
    public static xWebConfigProperty Create(string name, Action<IxWebConfigProperty> config)
    {
        var resource = new xWebConfigProperty(name);
        config(resource);
        return resource;
    }
    
    public static xWebConfigProperty Create(string name, Action<IxWebConfigProperty> config, out xWebConfigProperty resource)
    {
        resource = new xWebConfigProperty(name);
        config(resource);
        return resource;
    }
 
    public override string ResourceId => Constants.ResourceId;
}