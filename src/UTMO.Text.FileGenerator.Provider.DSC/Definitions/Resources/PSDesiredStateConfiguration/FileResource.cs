namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.PSDesiredStateConfiguration;

using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using ResourceConstants = UTMO.Text.FileGenerator.Provider.DSC.Constants.PSDesiredStateConfigurationConstants.File;

public class FileResource : PSDesiredStateConfigurationBase
{
    public FileResource(string name) : base(name)
    {
        this.PropertyBag[ResourceConstants.Properties.DestinationPath] = string.Empty;
        this.PropertyBag[ResourceConstants.Properties.Contents] = string.Empty;
    }

    public string DestinationPath
    {
        get
        {
            return this.PropertyBag[ResourceConstants.Properties.DestinationPath];
        }
        
        set
        {
            this.PropertyBag[ResourceConstants.Properties.DestinationPath] = value;
        }
    }
    
    public string Contents
    {
        get
        {
            return this.PropertyBag[ResourceConstants.Properties.Contents];
        }
        
        set
        {
            this.PropertyBag[ResourceConstants.Properties.Contents] = value;
        }
    }

    public override string ResourceId => ResourceConstants.ResourceId;
}