namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.NetworkingDsc;

using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.BaseDefinitions;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.NetworkingDsc.Enums;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Constants.NetworkingDscConstants.Firewall;

public class SimpleFirewallRuleResource : NetworkingDscBase
{
    private SimpleFirewallRuleResource(string name) : base(name)
    {
        this.PropertyBag.Init(Constants.Parameters.Name);
        this.PropertyBag.Init(Constants.Parameters.DisplayName);
        this.PropertyBag.Init<FirewallRuleActions>(Constants.Parameters.Action);
        this.PropertyBag.Init<FirewallRuleDirection>(Constants.Parameters.Direction);
        this.PropertyBag.Init<int[]>(Constants.Parameters.LocalPort);
        this.PropertyBag.Init<int[]>(Constants.Parameters.RemotePort);
        this.PropertyBag.Init(Constants.Parameters.LocalAddress);
        this.PropertyBag.Init<string[]>(Constants.Parameters.RemoteAddress);
        this.PropertyBag.Init<FirewallRuleProtocols>(Constants.Parameters.Protocol);
        this.PropertyBag.Init(Constants.Parameters.Group);
        this.PropertyBag.Init<bool>(Constants.Parameters.Enabled);
    }

    public string RuleName
    {
        get => this.PropertyBag.Get(Constants.Parameters.Name);
        set => this.PropertyBag.Set(Constants.Parameters.Name, value);
    }
    
    public string DisplayName
    {
        get => this.PropertyBag.Get(Constants.Parameters.DisplayName);
        set => this.PropertyBag.Set(Constants.Parameters.DisplayName, value);
    }
    
    public FirewallRuleActions Action
    {
        get => this.PropertyBag.Get<FirewallRuleActions>(Constants.Parameters.Action);
        set => this.PropertyBag.Set(Constants.Parameters.Action, value);
    }
    
    public FirewallRuleDirection Direction
    {
        get => this.PropertyBag.Get<FirewallRuleDirection>(Constants.Parameters.Direction);
        set => this.PropertyBag.Set(Constants.Parameters.Direction, value);
    }
    
    public string[] LocalPort
    {
        get => this.PropertyBag.Get<string[]>(Constants.Parameters.LocalPort);
        set => this.PropertyBag.Set(Constants.Parameters.LocalPort, value);
    }
    
    public string[] RemotePort
    {
        get => this.PropertyBag.Get<string[]>(Constants.Parameters.RemotePort);
        set => this.PropertyBag.Set(Constants.Parameters.RemotePort, value);
    }
    
    public string LocalAddress
    {
        get => this.PropertyBag.Get(Constants.Parameters.LocalAddress);
        set => this.PropertyBag.Set(Constants.Parameters.LocalAddress, value);
    }
    
    public string[] RemoteAddress
    {
        get => this.PropertyBag.Get<string[]>(Constants.Parameters.RemoteAddress);
        set => this.PropertyBag.Set(Constants.Parameters.RemoteAddress, value);
    }
    
    public FirewallRuleProtocols Protocol
    {
        get => this.PropertyBag.Get<FirewallRuleProtocols>(Constants.Parameters.Protocol);
        set => this.PropertyBag.Set(Constants.Parameters.Protocol, value);
    }
    
    public string Group
    {
        get => this.PropertyBag.Get(Constants.Parameters.Group);
        set => this.PropertyBag.Set(Constants.Parameters.Group, value);
    }
    
    public bool Enabled
    {
        get => this.PropertyBag.Get<bool>(Constants.Parameters.Enabled);
        set => this.PropertyBag.Set(Constants.Parameters.Enabled, value);
    }
    
    public static SimpleFirewallRuleResource Create(string name, Action<SimpleFirewallRuleResource> configure)
    {
        var resource = new SimpleFirewallRuleResource(name);
        configure(resource);
        return resource;
    }
    
    public static SimpleFirewallRuleResource Create(string name, Action<SimpleFirewallRuleResource> configure, out SimpleFirewallRuleResource resource)
    {
        resource = new SimpleFirewallRuleResource(name);
        configure(resource);
        return resource;
    }

    public override string ResourceId
    {
        get => Constants.ResourceId;
    }
}