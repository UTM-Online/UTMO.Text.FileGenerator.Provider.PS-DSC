namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.NetworkingDsc;

using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;

public class PredefinedFirewallResource : NetworkingDscBase
{
    public PredefinedFirewallResource(string name) : base(name)
    {
        this.PropertyBag.Add("Enabled", string.Empty);
        this.PropertyBag.Add("Name", string.Empty);
    }
    
    public bool Enabled
    {
        get
        {
            return this.PropertyBag["Enabled"] != string.Empty && bool.Parse(this.PropertyBag["Enabled"]);
        }
        
        set
        {
            this.PropertyBag["Enabled"] = value.ToString();
        }
    }
    
    public string Name
    {
        get
        {
            return this.PropertyBag["Name"];
        }
        
        set
        {
            this.PropertyBag["Name"] = value;
        }
    }

    public override string ResourceId => "Firewall";
}