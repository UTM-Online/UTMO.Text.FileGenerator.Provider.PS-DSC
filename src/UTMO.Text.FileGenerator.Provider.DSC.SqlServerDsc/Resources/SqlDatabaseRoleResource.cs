namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Resources;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.SqlServerDscConstants.SqlDatabaseRole;

public sealed class SqlDatabaseRoleResource : SqlServerDscBase, ISqlDatabaseRoleResource
{
    private SqlDatabaseRoleResource(string name) : base(name)
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

    public string RoleName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.Name);
        set => this.PropertyBag.Set(Constants.Properties.Name, value);
    }

    public string ServerName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.ServerName);
        set => this.PropertyBag.Set(Constants.Properties.ServerName, value);
    }

    public string[] Members
    {
        get => this.PropertyBag.Get<string[]>(Constants.Properties.Members);
        set => this.PropertyBag.Set(Constants.Properties.Members, value);
    }

    public string[] MembersToInclude
    {
        get => this.PropertyBag.Get<string[]>(Constants.Properties.MembersToInclude);
        set => this.PropertyBag.Set(Constants.Properties.MembersToInclude, value);
    }

    public string[] MembersToExclude
    {
        get => this.PropertyBag.Get<string[]>(Constants.Properties.MembersToExclude);
        set => this.PropertyBag.Set(Constants.Properties.MembersToExclude, value);
    }

    public static SqlDatabaseRoleResource Create(string name, Action<ISqlDatabaseRoleResource> configure)
    {
        var resource = new SqlDatabaseRoleResource(name);
        configure(resource);
        return resource;
    }

    public static SqlDatabaseRoleResource Create(string name, Action<ISqlDatabaseRoleResource> configure, out SqlDatabaseRoleResource resource)
    {
        resource = new SqlDatabaseRoleResource(name);
        configure(resource);
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var validation = this.ValidationBuilder();
        validation.ValidateStringNotNullOrEmpty(this.InstanceName, nameof(this.InstanceName));
        validation.ValidateStringNotNullOrEmpty(this.DatabaseName, nameof(this.DatabaseName));
        validation.ValidateStringNotNullOrEmpty(this.RoleName, nameof(this.RoleName));
        return Task.FromResult(validation.errors);
    }

    public override string ResourceId => Constants.ResourceId;

    public override bool HasEnsure => true;
}
