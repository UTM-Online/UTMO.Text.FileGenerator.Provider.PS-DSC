namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Resources;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.SqlServerDscConstants.SqlMaxDop;

public sealed class SqlMaxDopResource : SqlServerDscBase, ISqlMaxDopResource
{
    private SqlMaxDopResource(string name) : base(name)
    {
    }

    public bool? DynamicAlloc
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.DynamicAlloc);
        set => this.PropertyBag.Set(Constants.Properties.DynamicAlloc, value);
    }

    public int? MaxDop
    {
        get => this.PropertyBag.Get<int?>(Constants.Properties.MaxDop);
        set => this.PropertyBag.Set(Constants.Properties.MaxDop, value);
    }

    public string ServerName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.ServerName);
        set => this.PropertyBag.Set(Constants.Properties.ServerName, value);
    }

    public string InstanceName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.InstanceName);
        set => this.PropertyBag.Set(Constants.Properties.InstanceName, value);
    }

    public bool? ProcessOnlyOnActiveNode
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.ProcessOnlyOnActiveNode);
        set => this.PropertyBag.Set(Constants.Properties.ProcessOnlyOnActiveNode, value);
    }

    public static SqlMaxDopResource Create(string name, Action<ISqlMaxDopResource> configure)
    {
        var resource = new SqlMaxDopResource(name);
        configure(resource);
        return resource;
    }

    public static SqlMaxDopResource Create(string name, Action<ISqlMaxDopResource> configure, out SqlMaxDopResource resource)
    {
        resource = new SqlMaxDopResource(name);
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
