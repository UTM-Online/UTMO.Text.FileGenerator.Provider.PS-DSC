namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Resources;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.SqlServerDscConstants.SqlRole;

public sealed class SqlRoleResource : SqlServerDscBase, ISqlRoleResource
{
    private SqlRoleResource(string name) : base(name)
    {
    }

    public string ServerRoleName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.ServerRoleName);
        set => this.PropertyBag.Set(Constants.Properties.ServerRoleName, value);
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

    public static SqlRoleResource Create(string name, Action<ISqlRoleResource> configure)
    {
        var resource = new SqlRoleResource(name);
        configure(resource);
        return resource;
    }

    public static SqlRoleResource Create(string name, Action<ISqlRoleResource> configure, out SqlRoleResource resource)
    {
        resource = new SqlRoleResource(name);
        configure(resource);
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var validation = this.ValidationBuilder();
        validation.ValidateStringNotNullOrEmpty(this.ServerRoleName, nameof(this.ServerRoleName));
        validation.ValidateStringNotNullOrEmpty(this.InstanceName, nameof(this.InstanceName));
        return Task.FromResult(validation.errors);
    }

    public override string ResourceId => Constants.ResourceId;

    public override bool HasEnsure => true;
}
