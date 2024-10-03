namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.NetworkingDsc;

using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.NetworkingDscConstants.Firewall;

public class PredefinedFirewallResource : NetworkingDscBase
{
    public PredefinedFirewallResource(string name) : base(name)
    {
        this.PropertyBag.Add(Constants.Parameters.Enabled, string.Empty);
        this.PropertyBag.Add(Constants.Parameters.Name, string.Empty);
    }
    
    public bool Enabled
    {
        get => bool.Parse(this.PropertyBag[Constants.Parameters.Enabled].ToString() ?? throw new InvalidOperationException($"Property {Constants.Parameters.Enabled} is not set"));

        set => this.PropertyBag[Constants.Parameters.Enabled] = value.ToString();
    }
    
    public string Name
    {
        get => this.PropertyBag[Constants.Parameters.Name].ToString() ?? throw new InvalidOperationException($"Property {Constants.Parameters.Name} is not set");

        set => this.PropertyBag[Constants.Parameters.Name] = value;
    }

    public override string ResourceId => Constants.ResourceId;
}