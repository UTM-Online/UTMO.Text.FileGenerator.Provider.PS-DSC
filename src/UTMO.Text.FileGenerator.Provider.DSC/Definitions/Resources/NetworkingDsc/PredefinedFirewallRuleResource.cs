namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.NetworkingDsc;

using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.NetworkingDscConstants.Firewall;

public class PredefinedFirewallRuleResource : NetworkingDscBase
{
    private PredefinedFirewallRuleResource(string name) : base(name)
    {
        this.PropertyBag.Init<bool>(Constants.Parameters.Enabled);
        this.PropertyBag.Init(Constants.Parameters.Name);
    }
    
    public bool EnableRule
    {
        get => this.PropertyBag.Get<bool>(Constants.Parameters.Enabled);

        set => this.PropertyBag.Set(Constants.Parameters.Enabled, value);
    }
    
    public string RuleName
    {
        get => this.PropertyBag.Get(Constants.Parameters.Name);

        set => this.PropertyBag.Set(Constants.Parameters.Name, value);
    }
    
    public static PredefinedFirewallRuleResource Create(string name, Action<PredefinedFirewallRuleResource> configure)
    {
        var resource = new PredefinedFirewallRuleResource(name);
        configure(resource);
        return resource;
    }
    
    public static PredefinedFirewallRuleResource Create(string name, Action<PredefinedFirewallRuleResource> configure, out PredefinedFirewallRuleResource resourceRef)
    {
        var resource = new PredefinedFirewallRuleResource(name);
        configure(resource);
        resourceRef = resource;
        return resource;
    }

    public override string ResourceId => Constants.ResourceId;
}