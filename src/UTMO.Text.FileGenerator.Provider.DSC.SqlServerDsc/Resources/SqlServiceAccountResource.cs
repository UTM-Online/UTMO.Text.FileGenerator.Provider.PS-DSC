namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Resources;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Attributes;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Enums;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.SqlServerDscConstants.SqlServiceAccount;

public sealed class SqlServiceAccountResource : SqlServerDscBase, ISqlServiceAccountResource
{
    private SqlServiceAccountResource(string name) : base(name)
    {
    }

    public string InstanceName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.InstanceName);
        set => this.PropertyBag.Set(Constants.Properties.InstanceName, value);
    }

    [UnquotedEnum]
    public SqlServiceType ServiceType
    {
        get => this.PropertyBag.Get<SqlServiceType>(Constants.Properties.ServiceType);
        set => this.PropertyBag.Set(Constants.Properties.ServiceType, value);
    }

    public string ServiceAccount
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.ServiceAccount);
        set => this.PropertyBag.Set(Constants.Properties.ServiceAccount, value);
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

    public bool? Force
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.Force);
        set => this.PropertyBag.Set(Constants.Properties.Force, value);
    }

    public string VersionNumber
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.VersionNumber);
        set => this.PropertyBag.Set(Constants.Properties.VersionNumber, value);
    }

    public static SqlServiceAccountResource Create(string name, Action<ISqlServiceAccountResource> configure)
    {
        var resource = new SqlServiceAccountResource(name);
        configure(resource);
        return resource;
    }

    public static SqlServiceAccountResource Create(string name, Action<ISqlServiceAccountResource> configure, out SqlServiceAccountResource resource)
    {
        resource = new SqlServiceAccountResource(name);
        configure(resource);
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var validation = this.ValidationBuilder();
        validation.ValidateStringNotNullOrEmpty(this.InstanceName, nameof(this.InstanceName));
        validation.ValidateStringNotNullOrEmpty(this.ServiceAccount, nameof(this.ServiceAccount));
        return Task.FromResult(validation.errors);
    }

    public override string ResourceId => Constants.ResourceId;

    public override bool HasEnsure => false;
}
