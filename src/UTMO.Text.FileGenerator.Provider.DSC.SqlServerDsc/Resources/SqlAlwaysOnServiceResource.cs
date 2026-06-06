namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Resources;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.SqlServerDscConstants.SqlAlwaysOnService;

public sealed class SqlAlwaysOnServiceResource : SqlServerDscBase, ISqlAlwaysOnServiceResource
{
    private SqlAlwaysOnServiceResource(string name) : base(name)
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

    public uint? RestartTimeout
    {
        get => this.PropertyBag.Get<uint?>(Constants.Properties.RestartTimeout);
        set => this.PropertyBag.Set(Constants.Properties.RestartTimeout, value);
    }

    public static SqlAlwaysOnServiceResource Create(string name, Action<ISqlAlwaysOnServiceResource> configure)
    {
        var resource = new SqlAlwaysOnServiceResource(name);
        configure(resource);
        return resource;
    }

    public static SqlAlwaysOnServiceResource Create(string name, Action<ISqlAlwaysOnServiceResource> configure, out SqlAlwaysOnServiceResource resource)
    {
        resource = new SqlAlwaysOnServiceResource(name);
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
