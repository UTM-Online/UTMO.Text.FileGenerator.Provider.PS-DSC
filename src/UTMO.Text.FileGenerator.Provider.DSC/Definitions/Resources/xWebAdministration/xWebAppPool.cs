namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.xWebAdministration;

using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.xWebAdministrationConstants.xWebAppPool;

// ReSharper disable once InconsistentNaming
public class xWebAppPool : xWebAdministrationBase
{
    public xWebAppPool(string name) : base(name)
    {
        this.PropertyBag.Add(Constants.Properties.Name, string.Empty);
        this.PropertyBag.Add(Constants.Properties.IdentityType, string.Empty);
    }
    
    public string Name
    {
        get => this.PropertyBag[Constants.Properties.Name].ToString() ?? string.Empty;
        set => this.PropertyBag[Constants.Properties.Name] = value;
    }
    
    public xWebAppPoolIdentityType IdentityType
    {
        get => string.IsNullOrWhiteSpace(this.PropertyBag[Constants.Properties.IdentityType].ToString()) ? xWebAppPoolIdentityType.ApplicationPoolIdentity : Enum.Parse<xWebAppPoolIdentityType>(this.PropertyBag[Constants.Properties.IdentityType].ToString()!);
        set => this.PropertyBag[Constants.Properties.IdentityType] = value.ToString();
    }

    public override string ResourceId => Constants.ResourceId;
}