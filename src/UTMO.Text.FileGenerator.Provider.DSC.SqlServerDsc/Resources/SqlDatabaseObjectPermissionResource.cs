namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Resources;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.SqlServerDscConstants.SqlDatabaseObjectPermission;

public sealed class SqlDatabaseObjectPermissionResource : SqlServerDscBase, ISqlDatabaseObjectPermissionResource
{
    private SqlDatabaseObjectPermissionResource(string name) : base(name)
    {
    }

    public string InstanceName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.InstanceName);
        set => this.PropertyBag.Set(Constants.Properties.InstanceName, value);
    }

    public string DatabaseName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.DatabaseName);
        set => this.PropertyBag.Set(Constants.Properties.DatabaseName, value);
    }

    public string SchemaName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.SchemaName);
        set => this.PropertyBag.Set(Constants.Properties.SchemaName, value);
    }

    public string ObjectName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.ObjectName);
        set => this.PropertyBag.Set(Constants.Properties.ObjectName, value);
    }

    public string ObjectType
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.ObjectType);
        set => this.PropertyBag.Set(Constants.Properties.ObjectType, value);
    }

    public string PrincipalName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.Name);
        set => this.PropertyBag.Set(Constants.Properties.Name, value);
    }

    public string[] Permission
    {
        get => this.PropertyBag.Get<string[]>(Constants.Properties.Permission);
        set => this.PropertyBag.Set(Constants.Properties.Permission, value);
    }

    public string ServerName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.ServerName);
        set => this.PropertyBag.Set(Constants.Properties.ServerName, value);
    }

    public bool? Force
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.Force);
        set => this.PropertyBag.Set(Constants.Properties.Force, value);
    }

    public string State
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.State);
        set => this.PropertyBag.Set(Constants.Properties.State, value);
    }

    public static SqlDatabaseObjectPermissionResource Create(string name, Action<ISqlDatabaseObjectPermissionResource> configure)
    {
        var resource = new SqlDatabaseObjectPermissionResource(name);
        configure(resource);
        return resource;
    }

    public static SqlDatabaseObjectPermissionResource Create(string name, Action<ISqlDatabaseObjectPermissionResource> configure, out SqlDatabaseObjectPermissionResource resource)
    {
        resource = new SqlDatabaseObjectPermissionResource(name);
        configure(resource);
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var validation = this.ValidationBuilder();
        validation.ValidateStringNotNullOrEmpty(this.InstanceName, nameof(this.InstanceName));
        validation.ValidateStringNotNullOrEmpty(this.DatabaseName, nameof(this.DatabaseName));
        validation.ValidateStringNotNullOrEmpty(this.SchemaName, nameof(this.SchemaName));
        validation.ValidateStringNotNullOrEmpty(this.ObjectName, nameof(this.ObjectName));
        validation.ValidateStringNotNullOrEmpty(this.ObjectType, nameof(this.ObjectType));
        validation.ValidateStringNotNullOrEmpty(this.PrincipalName, nameof(this.PrincipalName));
        validation.ValidateStringNotNullOrEmpty(this.State, nameof(this.State));
        return Task.FromResult(validation.errors);
    }

    public override string ResourceId => Constants.ResourceId;

    public override bool HasEnsure => true;
}
