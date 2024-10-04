namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.NetworkingDsc;

using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.NetworkingDscConstants.Firewall;

public class PredefinedFirewallResource : NetworkingDscBase
{
    public PredefinedFirewallResource(string name) : base(name)
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

    public override string ResourceId
    {
        get => Constants.ResourceId;
    }
}