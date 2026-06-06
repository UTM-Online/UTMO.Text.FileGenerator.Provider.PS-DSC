namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Resources;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.SqlServerDscConstants.SqlWaitForAG;

public sealed class SqlWaitForAGResource : SqlServerDscBase, ISqlWaitForAGResource
{
    private SqlWaitForAGResource(string name) : base(name)
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

    public string GroupName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.Name);
        set => this.PropertyBag.Set(Constants.Properties.Name, value);
    }

    public ulong? RetryIntervalSec
    {
        get => this.PropertyBag.Get<ulong?>(Constants.Properties.RetryIntervalSec);
        set => this.PropertyBag.Set(Constants.Properties.RetryIntervalSec, value);
    }

    public uint? RetryCount
    {
        get => this.PropertyBag.Get<uint?>(Constants.Properties.RetryCount);
        set => this.PropertyBag.Set(Constants.Properties.RetryCount, value);
    }

    public static SqlWaitForAGResource Create(string name, Action<ISqlWaitForAGResource> configure)
    {
        var resource = new SqlWaitForAGResource(name);
        configure(resource);
        return resource;
    }

    public static SqlWaitForAGResource Create(string name, Action<ISqlWaitForAGResource> configure, out SqlWaitForAGResource resource)
    {
        resource = new SqlWaitForAGResource(name);
        configure(resource);
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var validation = this.ValidationBuilder();
        validation.ValidateStringNotNullOrEmpty(this.InstanceName, nameof(this.InstanceName));
        validation.ValidateStringNotNullOrEmpty(this.GroupName, nameof(this.GroupName));
        return Task.FromResult(validation.errors);
    }

    public override string ResourceId => Constants.ResourceId;

    public override bool HasEnsure => false;
}
