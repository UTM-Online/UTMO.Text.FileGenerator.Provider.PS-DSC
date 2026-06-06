namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Resources;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.SqlServerDscConstants.SqlMemory;

public sealed class SqlMemoryResource : SqlServerDscBase, ISqlMemoryResource
{
    private SqlMemoryResource(string name) : base(name)
    {
    }

    public string InstanceName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.InstanceName);
        set => this.PropertyBag.Set(Constants.Properties.InstanceName, value);
    }

    public string ServerName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.ServerName);
        set => this.PropertyBag.Set(Constants.Properties.ServerName, value);
    }

    public bool? DynamicAlloc
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.DynamicAlloc);
        set => this.PropertyBag.Set(Constants.Properties.DynamicAlloc, value);
    }

    public int? MinMemory
    {
        get => this.PropertyBag.Get<int?>(Constants.Properties.MinMemory);
        set => this.PropertyBag.Set(Constants.Properties.MinMemory, value);
    }

    public int? MaxMemory
    {
        get => this.PropertyBag.Get<int?>(Constants.Properties.MaxMemory);
        set => this.PropertyBag.Set(Constants.Properties.MaxMemory, value);
    }

    public int? MinMemoryPercent
    {
        get => this.PropertyBag.Get<int?>(Constants.Properties.MinMemoryPercent);
        set => this.PropertyBag.Set(Constants.Properties.MinMemoryPercent, value);
    }

    public int? MaxMemoryPercent
    {
        get => this.PropertyBag.Get<int?>(Constants.Properties.MaxMemoryPercent);
        set => this.PropertyBag.Set(Constants.Properties.MaxMemoryPercent, value);
    }

    public bool? ProcessOnlyOnActiveNode
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.ProcessOnlyOnActiveNode);
        set => this.PropertyBag.Set(Constants.Properties.ProcessOnlyOnActiveNode, value);
    }

    public static SqlMemoryResource Create(string name, Action<ISqlMemoryResource> configure)
    {
        var resource = new SqlMemoryResource(name);
        configure(resource);
        return resource;
    }

    public static SqlMemoryResource Create(string name, Action<ISqlMemoryResource> configure, out SqlMemoryResource resource)
    {
        resource = new SqlMemoryResource(name);
        configure(resource);
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var validation = this.ValidationBuilder();
        validation.ValidateStringNotNullOrEmpty(this.InstanceName, nameof(this.InstanceName));
        return Task.FromResult(validation.errors);
    }

    public override string ResourceId => Constants.ResourceId;

    public override bool HasEnsure => true;
}
