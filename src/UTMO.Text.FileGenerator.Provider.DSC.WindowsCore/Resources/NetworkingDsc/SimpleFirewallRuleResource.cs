namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.NetworkingDsc;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.BaseDefinitions;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.NetworkingDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.NetworkingDsc.Enums;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Constants.NetworkingDscConstants.Firewall;

public class SimpleFirewallRuleResource : NetworkingDscBase, ISimpleFirewallRuleResource
{
    private SimpleFirewallRuleResource(string name) : base(name)
    {
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
    
    public static SimpleFirewallRuleResource Create(string name, Action<ISimpleFirewallRuleResource> configure)
    {
        var resource = new SimpleFirewallRuleResource(name);
        configure(resource);
        return resource;
    }
    
    public static SimpleFirewallRuleResource Create(string name, Action<ISimpleFirewallRuleResource> configure, out SimpleFirewallRuleResource resource)
    {
        resource = new SimpleFirewallRuleResource(name);
        configure(resource);
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var errors = this.ValidationBuilder()
                         .ValidateStringNotNullOrEmpty(this.RuleName, Constants.Parameters.Name)
                         .errors;

        return Task.FromResult(errors);
    }

    public override string ResourceId => Constants.ResourceId;
}