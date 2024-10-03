namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.PSDesiredStateConfiguration;

using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.PSDesiredStateConfigurationConstants.File;

public class FileResource : PSDesiredStateConfigurationBase
{
    public FileResource(string name) : base(name)
    {
        this.PropertyBag[Constants.Properties.DestinationPath] = string.Empty;
        this.PropertyBag[Constants.Properties.Contents] = string.Empty;
    }

    public string DestinationPath
    {
        get => this.PropertyBag[Constants.Properties.DestinationPath].ToString() ?? string.Empty;

        set => this.PropertyBag[Constants.Properties.DestinationPath] = value;
    }
    
    public string Contents
    {
        get => this.PropertyBag[Constants.Properties.Contents].ToString() ?? string.Empty;

        set => this.PropertyBag[Constants.Properties.Contents] = value;
    }

    public override string ResourceId => Constants.ResourceId;
}