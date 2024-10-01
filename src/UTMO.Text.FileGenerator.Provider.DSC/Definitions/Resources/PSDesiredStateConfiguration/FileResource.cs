namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.PSDesiredStateConfiguration;

using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using UTMO.Text.FileGenerator.Provider.DSC.Resources;

public class FileResource : PSDesiredStateConfigurationBase
{
    public FileResource(string name) : base(name)
    {
        this.PropertyBag["DestinationPath"] = string.Empty;
        this.PropertyBag["Contents"] = string.Empty;
    }

    public string DestinationPath
    {
        get
        {
            return this.PropertyBag["DestinationPath"];
        }
        
        set
        {
            this.PropertyBag["DestinationPath"] = value;
        }
    }
    
    public string Contents
    {
        get
        {
            return this.PropertyBag["Contents"];
        }
        
        set
        {
            this.PropertyBag["Contents"] = value;
        }
    }

    public override string ResourceId => "File";
}