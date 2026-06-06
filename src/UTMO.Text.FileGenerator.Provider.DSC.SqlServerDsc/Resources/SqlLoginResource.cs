namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Resources;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Enums;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.SqlServerDscConstants.SqlLogin;

public sealed class SqlLoginResource : SqlServerDscBase, ISqlLoginResource
{
    private SqlLoginResource(string name) : base(name)
    {
    }

    public string LoginName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.Name);
        set => this.PropertyBag.Set(Constants.Properties.Name, value);
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

    public IPowerShellExpression? LoginCredential
    {
        get => this.PropertyBag.Get<IPowerShellExpression?>(Constants.Properties.LoginCredential);
        set => this.PropertyBag.Set(Constants.Properties.LoginCredential, value);
    }

    public bool? LoginMustChangePassword
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.LoginMustChangePassword);
        set => this.PropertyBag.Set(Constants.Properties.LoginMustChangePassword, value);
    }

    public bool? LoginPasswordExpirationEnabled
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.LoginPasswordExpirationEnabled);
        set => this.PropertyBag.Set(Constants.Properties.LoginPasswordExpirationEnabled, value);
    }

    public bool? LoginPasswordPolicyEnforced
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.LoginPasswordPolicyEnforced);
        set => this.PropertyBag.Set(Constants.Properties.LoginPasswordPolicyEnforced, value);
    }

    public bool? Disabled
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.Disabled);
        set => this.PropertyBag.Set(Constants.Properties.Disabled, value);
    }

    public string DefaultDatabase
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.DefaultDatabase);
        set => this.PropertyBag.Set(Constants.Properties.DefaultDatabase, value);
    }

    public string Language
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.Language);
        set => this.PropertyBag.Set(Constants.Properties.Language, value);
    }

    public string Sid
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.Sid);
        set => this.PropertyBag.Set(Constants.Properties.Sid, value);
    }

    public SqlLoginType? LoginType
    {
        get => this.PropertyBag.Get<SqlLoginType?>(Constants.Properties.LoginType);
        set => this.PropertyBag.Set(Constants.Properties.LoginType, value);
    }

    public static SqlLoginResource Create(string name, Action<ISqlLoginResource> configure)
    {
        var resource = new SqlLoginResource(name);
        configure(resource);
        return resource;
    }

    public static SqlLoginResource Create(string name, Action<ISqlLoginResource> configure, out SqlLoginResource resource)
    {
        resource = new SqlLoginResource(name);
        configure(resource);
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var validation = this.ValidationBuilder();
        validation.ValidateStringNotNullOrEmpty(this.LoginName, nameof(this.LoginName));
        validation.ValidateStringNotNullOrEmpty(this.InstanceName, nameof(this.InstanceName));
        return Task.FromResult(validation.errors);
    }

    public override string ResourceId => Constants.ResourceId;

    public override bool HasEnsure => true;
}
