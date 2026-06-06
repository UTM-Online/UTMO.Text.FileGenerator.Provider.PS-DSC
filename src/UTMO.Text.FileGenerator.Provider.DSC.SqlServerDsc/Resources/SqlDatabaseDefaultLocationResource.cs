namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Resources;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Enums;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.SqlServerDscConstants.SqlDatabaseDefaultLocation;

public sealed class SqlDatabaseDefaultLocationResource : SqlServerDscBase, ISqlDatabaseDefaultLocationResource
{
    private SqlDatabaseDefaultLocationResource(string name) : base(name)
    {
    }

    public string InstanceName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.InstanceName);
        set => this.PropertyBag.Set(Constants.Properties.InstanceName, value);
    }

    public SqlDatabaseDefaultLocationType Type
    {
        get => this.PropertyBag.Get<SqlDatabaseDefaultLocationType>(Constants.Properties.Type);
        set => this.PropertyBag.Set(Constants.Properties.Type, value);
    }

    public string Path
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.Path);
        set => this.PropertyBag.Set(Constants.Properties.Path, value);
    }

    public string ServerName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.ServerName);
        set => this.PropertyBag.Set(Constants.Properties.ServerName, value);
    }

    public bool? RestartService
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.RestartService);
        set => this.PropertyBag.Set(Constants.Properties.RestartService, value);
    }

    public bool? ProcessOnlyOnActiveNode
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.ProcessOnlyOnActiveNode);
        set => this.PropertyBag.Set(Constants.Properties.ProcessOnlyOnActiveNode, value);
    }

    public static SqlDatabaseDefaultLocationResource Create(string name, Action<ISqlDatabaseDefaultLocationResource> configure)
    {
        var resource = new SqlDatabaseDefaultLocationResource(name);
        configure(resource);
        return resource;
    }

    public static SqlDatabaseDefaultLocationResource Create(string name, Action<ISqlDatabaseDefaultLocationResource> configure, out SqlDatabaseDefaultLocationResource resource)
    {
        resource = new SqlDatabaseDefaultLocationResource(name);
        configure(resource);
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var validation = this.ValidationBuilder();
        validation.ValidateStringNotNullOrEmpty(this.InstanceName, nameof(this.InstanceName));
        validation.ValidateStringNotNullOrEmpty(this.Path, nameof(this.Path));
        return Task.FromResult(validation.errors);
    }

    public override string ResourceId => Constants.ResourceId;

    public override bool HasEnsure => false;
}
