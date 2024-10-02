namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.NetworkingDsc;

using UTMO.Text.FileGenerator.Provider.DSC.Constants;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;

public class PredefinedFirewallResource : NetworkingDscBase
{
    public PredefinedFirewallResource(string name) : base(name)
    {
        this.PropertyBag.Add(NetworkingDscConstants.Firewall.Parameters.Enabled, string.Empty);
        this.PropertyBag.Add(NetworkingDscConstants.Firewall.Parameters.Name, string.Empty);
    }
    
    public bool Enabled
    {
        get
        {
            return this.PropertyBag[NetworkingDscConstants.Firewall.Parameters.Enabled] != string.Empty && bool.Parse(this.PropertyBag[NetworkingDscConstants.Firewall.Parameters.Enabled]);
        }
        
        set
        {
            this.PropertyBag[NetworkingDscConstants.Firewall.Parameters.Enabled] = value.ToString();
        }
    }
    
    public string Name
    {
        get
        {
            return this.PropertyBag[NetworkingDscConstants.Firewall.Parameters.Name];
        }
        
        set
        {
            this.PropertyBag[NetworkingDscConstants.Firewall.Parameters.Name] = value;
        }
    }

    public override string ResourceId => NetworkingDscConstants.Firewall.ResourceId;
}