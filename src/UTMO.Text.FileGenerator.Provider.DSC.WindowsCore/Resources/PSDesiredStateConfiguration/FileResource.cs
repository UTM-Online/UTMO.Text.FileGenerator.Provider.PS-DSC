namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration;

using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.BaseDefinitions;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Constants.PSDesiredStateConfigurationConstants.File;

public class FileResource : PSDesiredStateConfigurationBase
{
    private FileResource(string name) : base(name)
    {
        this.PropertyBag.Init(Constants.Properties.DestinationPath);
        this.PropertyBag.Init(Constants.Properties.Contents);
        this.PropertyBag.Set(Constants.Properties.Type, "File");
    }

    public string DestinationPath
    {
        get => this.PropertyBag.Get(Constants.Properties.DestinationPath);
        set => this.PropertyBag.Set(Constants.Properties.DestinationPath, value);
    }
    
    public string Contents
    {
        get => this.PropertyBag.Get(Constants.Properties.Contents);
        set => this.PropertyBag.Set(Constants.Properties.Contents, value);
    }
    
    public static FileResource Create(string name, Action<FileResource> configure)
    {
        var resource = new FileResource(name);
        configure(resource);
        return resource;
    }
    
    public static FileResource Create(string name, Action<FileResource> configure, out FileResource resource)
    {
        resource = new FileResource(name);
        configure(resource);
        return resource;
    }

    public override string ResourceId
    {
        get => Constants.ResourceId;
    }
}