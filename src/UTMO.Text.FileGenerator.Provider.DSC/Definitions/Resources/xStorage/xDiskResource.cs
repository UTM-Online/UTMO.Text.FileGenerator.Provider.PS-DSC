namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.xStorage;

using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.xStorageConstants.xDisk;

// ReSharper disable once InconsistentNaming
public class xDiskResource : xStorageBase
{
    private xDiskResource(string name) : base(name)
    {
        this.PropertyBag.Init<char>(Constants.Properties.DriveLetter);
        this.PropertyBag.Init<int>(Constants.Properties.DiskId);
        this.PropertyBag.Init(Constants.Properties.FSFormat);
    }
    
    public char DriveLetter
    {
        get => this.PropertyBag.Get<char>(Constants.Properties.DriveLetter);
        set => this.PropertyBag.Set(Constants.Properties.DriveLetter, value);
    }
    
    public int DiskId
    {
        get => this.PropertyBag.Get<int>(Constants.Properties.DiskId);
        set => this.PropertyBag.Set(Constants.Properties.DiskId, value);
    }
    
    public string FileSystemFormat
    {
        get => this.PropertyBag.Get(Constants.Properties.FSFormat);
        set => this.PropertyBag.Set(Constants.Properties.FSFormat, value);
    }
    
    public static xDiskResource Create(string name, Action<xDiskResource> configure)
    {
        var resource = new xDiskResource(name);
        configure(resource);
        return resource;
    }
    
    public static xDiskResource Create(string name, Action<xDiskResource> configure, out xDiskResource resource)
    {
        resource = new xDiskResource(name);
        configure(resource);
        return resource;
    }

    public override string ResourceId => Constants.ResourceId;
}