namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.cChoco;

using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.cChocoConstants.ChocoPackageSource;

public class ChocoPackageSourceResource : cChocoBase
{
    public ChocoPackageSourceResource(string name) : base(name)
    {
        this.PropertyBag[Constants.Parameters.Name] = string.Empty;
        this.PropertyBag[Constants.Parameters.Source] = string.Empty;
    }

    public string SourceName
    {
        get => this.PropertyBag[Constants.Parameters.Name].ToString() ?? string.Empty;

        set => this.PropertyBag[Constants.Parameters.Name] = value;
    }
    
    public string SourceUri
    {
        get => this.PropertyBag[Constants.Parameters.Source].ToString() ?? string.Empty;

        set => this.PropertyBag[Constants.Parameters.Source] = value;
    }

    public override string ResourceId => Constants.ResourceId;
}