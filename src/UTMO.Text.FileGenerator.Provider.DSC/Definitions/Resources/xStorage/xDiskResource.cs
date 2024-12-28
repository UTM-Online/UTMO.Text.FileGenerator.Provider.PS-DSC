namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.xStorage;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.xStorage.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.xStorageConstants.xDisk;

// ReSharper disable once InconsistentNaming
public class xDiskResource : xStorageBase, IxDiskResource
{
    private xDiskResource(string name) : base(name)
    {
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
    
    public static xDiskResource Create(string name, Action<IxDiskResource> configure)
    {
        var resource = new xDiskResource(name);
        configure(resource);
        return resource;
    }
    
    public static xDiskResource Create(string name, Action<IxDiskResource> configure, out xDiskResource resource)
    {
        resource = new xDiskResource(name);
        configure(resource);
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var validation = this.ValidationBuilder()
                         .ValidateStringNotNullOrEmpty(this.DiskId.ToString(), nameof(this.DiskId))
                         .ValidateStringNotNullOrEmpty(this.DriveLetter.ToString(), nameof(this.DriveLetter));
        
        return Task.FromResult(validation.errors);
    }

    public override string ResourceId => Constants.ResourceId;

    public sealed override bool HasEnsure => false;
}