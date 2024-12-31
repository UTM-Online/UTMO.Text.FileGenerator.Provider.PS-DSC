namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.NetworkingDsc;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.BaseDefinitions;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.NetworkingDsc.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Constants.NetworkingDscConstants.Firewall;

public class PredefinedFirewallRuleResource : NetworkingDscBase, IPredefinedFirewallRuleResource
{
    private PredefinedFirewallRuleResource(string name) : base(name)
    {
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
    
    public static PredefinedFirewallRuleResource Create(string name, Action<IPredefinedFirewallRuleResource> configure)
    {
        var resource = new PredefinedFirewallRuleResource(name);
        configure(resource);
        return resource;
    }
    
    public static PredefinedFirewallRuleResource Create(string name, Action<IPredefinedFirewallRuleResource> configure, out PredefinedFirewallRuleResource resourceRef)
    {
        var resource = new PredefinedFirewallRuleResource(name);
        configure(resource);
        resourceRef = resource;
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var errors = this.ValidationBuilder()
            .ValidateStringNotNullOrEmpty(this.RuleName, nameof(this.RuleName))
            .errors;

        return Task.FromResult(errors);
    }

    public override string ResourceId => Constants.ResourceId;
}