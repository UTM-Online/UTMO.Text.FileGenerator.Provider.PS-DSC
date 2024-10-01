namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.cChoco;

using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using UTMO.Text.FileGenerator.Provider.DSC.Resources;

public class ChocoPackageSourceResource : cChocoBase
{
    public ChocoPackageSourceResource(string name) : base(name)
    {
        this.PropertyBag["Name"] = string.Empty;
        this.PropertyBag["Source"] = string.Empty;
    }

    public string SourceName
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
    
    public string SourceUri
    {
        get
        {
            return this.PropertyBag["Source"];
        }
        
        set
        {
            this.PropertyBag["Source"] = value;
        }
    }

    public override string ResourceId => "cChocoSource";
}