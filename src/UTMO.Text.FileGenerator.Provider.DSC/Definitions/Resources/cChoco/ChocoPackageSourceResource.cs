namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.cChoco;

using UTMO.Text.FileGenerator.Provider.DSC.Constants;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using UTMO.Text.FileGenerator.Provider.DSC.Resources;

public class ChocoPackageSourceResource : cChocoBase
{
    public ChocoPackageSourceResource(string name) : base(name)
    {
        this.PropertyBag[cChocoConstants.ChocoPackageSource.Parameters.Name] = string.Empty;
        this.PropertyBag["Source"] = string.Empty;
    }

    public string SourceName
    {
        get
        {
            return this.PropertyBag[cChocoConstants.ChocoPackageSource.Parameters.Name];
        }
        
        set
        {
            this.PropertyBag[cChocoConstants.ChocoPackageSource.Parameters.Name] = value;
        }
    }
    
    public string SourceUri
    {
        get
        {
            return this.PropertyBag[cChocoConstants.ChocoPackageSource.Parameters.Source];
        }
        
        set
        {
            this.PropertyBag[cChocoConstants.ChocoPackageSource.Parameters.Source] = value;
        }
    }

    public override string ResourceId => cChocoConstants.ChocoPackageSource.ResourceId;
}