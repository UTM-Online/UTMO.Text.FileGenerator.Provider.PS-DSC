namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Resources;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Enums;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.SqlServerDscConstants.SqlDatabaseUser;

public sealed class SqlDatabaseUserResource : SqlServerDscBase, ISqlDatabaseUserResource
{
    private SqlDatabaseUserResource(string name) : base(name)
    {
    }

    public string UserName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.Name);
        set => this.PropertyBag.Set(Constants.Properties.Name, value);
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

    public string ServerName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.ServerName);
        set => this.PropertyBag.Set(Constants.Properties.ServerName, value);
    }

    public string LoginName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.LoginName);
        set => this.PropertyBag.Set(Constants.Properties.LoginName, value);
    }

    public string AsymmetricKeyName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.AsymmetricKeyName);
        set => this.PropertyBag.Set(Constants.Properties.AsymmetricKeyName, value);
    }

    public string CertificateName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.CertificateName);
        set => this.PropertyBag.Set(Constants.Properties.CertificateName, value);
    }

    public SqlDatabaseUserType? UserType
    {
        get => this.PropertyBag.Get<SqlDatabaseUserType?>(Constants.Properties.UserType);
        set => this.PropertyBag.Set(Constants.Properties.UserType, value);
    }

    public bool? Force
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.Force);
        set => this.PropertyBag.Set(Constants.Properties.Force, value);
    }

    public static SqlDatabaseUserResource Create(string name, Action<ISqlDatabaseUserResource> configure)
    {
        var resource = new SqlDatabaseUserResource(name);
        configure(resource);
        return resource;
    }

    public static SqlDatabaseUserResource Create(string name, Action<ISqlDatabaseUserResource> configure, out SqlDatabaseUserResource resource)
    {
        resource = new SqlDatabaseUserResource(name);
        configure(resource);
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var validation = this.ValidationBuilder();
        validation.ValidateStringNotNullOrEmpty(this.UserName, nameof(this.UserName));
        validation.ValidateStringNotNullOrEmpty(this.InstanceName, nameof(this.InstanceName));
        validation.ValidateStringNotNullOrEmpty(this.DatabaseName, nameof(this.DatabaseName));
        return Task.FromResult(validation.errors);
    }

    public override string ResourceId => Constants.ResourceId;

    public override bool HasEnsure => true;
}
