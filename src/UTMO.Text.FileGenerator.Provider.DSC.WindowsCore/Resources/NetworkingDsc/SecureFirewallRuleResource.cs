namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.NetworkingDsc;

using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.BaseDefinitions;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.NetworkingDsc.Enums;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Constants.NetworkingDscConstants.Firewall;

public class SecureFirewallRuleResource : NetworkingDscBase
{
    private SecureFirewallRuleResource(string name) : base(name)
    {
        this.PropertyBag.Init<bool>(Constants.Parameters.Enabled);
        this.PropertyBag.Init(Constants.Parameters.Name);
        this.PropertyBag.Init(Constants.Parameters.Group);
        this.PropertyBag.Init(Constants.Parameters.DisplayName);
        this.PropertyBag.Init<FirewallRuleActions>(Constants.Parameters.Action);
        this.PropertyBag.Init<FirewallRuleDirection>(Constants.Parameters.Direction);
        this.PropertyBag.Init<FirewallRuleProtocols>(Constants.Parameters.Protocol);
        this.PropertyBag.Init(Constants.Parameters.RemoteMachine);
        this.PropertyBag.Init<FirewallRuleAuthentication>(Constants.Parameters.Authentication);
        this.PropertyBag.Init<FirewallRuleEncryption>(Constants.Parameters.Encryption);
        this.PropertyBag.Init<int[]>(Constants.Parameters.LocalPort);
    }
    
    public bool Enabled
    {
        get => this.PropertyBag.Get<bool>(Constants.Parameters.Enabled);
        
        set => this.PropertyBag.Set(Constants.Parameters.Enabled, value);
    }
    
    public string RuleName
    {
        get => this.PropertyBag.Get(Constants.Parameters.Name);
        
        set => this.PropertyBag.Set(Constants.Parameters.Name, value);
    }
    
    public string Group
    {
        get => this.PropertyBag.Get(Constants.Parameters.Group);
        
        set => this.PropertyBag.Set(Constants.Parameters.Group, value);
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
    
    public FirewallRuleProtocols Protocol
    {
        get => this.PropertyBag.Get<FirewallRuleProtocols>(Constants.Parameters.Protocol);
        
        set => this.PropertyBag.Set(Constants.Parameters.Protocol, value);
    }
    
    public string RemoteMachine
    {
        get => this.PropertyBag.Get(Constants.Parameters.RemoteMachine);
        
        set => this.PropertyBag.Set(Constants.Parameters.RemoteMachine, value);
    }
    
    public FirewallRuleAuthentication Authentication
    {
        get => this.PropertyBag.Get<FirewallRuleAuthentication>(Constants.Parameters.Authentication);
        
        set => this.PropertyBag.Set(Constants.Parameters.Authentication, value);
    }
    
    public FirewallRuleEncryption Encryption
    {
        get => this.PropertyBag.Get<FirewallRuleEncryption>(Constants.Parameters.Encryption);
        
        set => this.PropertyBag.Set(Constants.Parameters.Encryption, value);
    }
    
    public string[] LocalPort
    {
        get => this.PropertyBag.Get<string[]>(Constants.Parameters.LocalPort);
        
        set => this.PropertyBag.Set(Constants.Parameters.LocalPort, value);
    }
    
    public static SecureFirewallRuleResource Create(string name, Action<SecureFirewallRuleResource> configure)
    {
        var resource = new SecureFirewallRuleResource(name);
        configure(resource);
        return resource;
    }
    
    public static SecureFirewallRuleResource Create(string name, Action<SecureFirewallRuleResource> configure, out SecureFirewallRuleResource resource)
    {
        resource = new SecureFirewallRuleResource(name);
        configure(resource);
        return resource;
    }

    public override string ResourceId => Constants.ResourceId;
}