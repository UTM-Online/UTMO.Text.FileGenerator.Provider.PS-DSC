namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Resources;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Enums;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.SqlServerDscConstants.SqlAgentFailsafe;

public sealed class SqlAgentFailsafeResource : SqlServerDscBase, ISqlAgentFailsafeResource
{
    private SqlAgentFailsafeResource(string name) : base(name)
    {
    }

    public string OperatorName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.Name);
        set => this.PropertyBag.Set(Constants.Properties.Name, value);
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

    public SqlAgentNotificationMethod? NotificationMethod
    {
        get => this.PropertyBag.Get<SqlAgentNotificationMethod?>(Constants.Properties.NotificationMethod);
        set => this.PropertyBag.Set(Constants.Properties.NotificationMethod, value);
    }

    public static SqlAgentFailsafeResource Create(string name, Action<ISqlAgentFailsafeResource> configure)
    {
        var resource = new SqlAgentFailsafeResource(name);
        configure(resource);
        return resource;
    }

    public static SqlAgentFailsafeResource Create(string name, Action<ISqlAgentFailsafeResource> configure, out SqlAgentFailsafeResource resource)
    {
        resource = new SqlAgentFailsafeResource(name);
        configure(resource);
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var validation = this.ValidationBuilder();
        validation.ValidateStringNotNullOrEmpty(this.OperatorName, nameof(this.OperatorName));
        validation.ValidateStringNotNullOrEmpty(this.InstanceName, nameof(this.InstanceName));
        return Task.FromResult(validation.errors);
    }

    public override string ResourceId => Constants.ResourceId;

    public override bool HasEnsure => true;
}
