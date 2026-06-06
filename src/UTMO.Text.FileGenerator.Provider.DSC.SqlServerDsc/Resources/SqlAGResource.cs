namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Resources;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Enums;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.SqlServerDscConstants.SqlAG;

public sealed class SqlAGResource : SqlServerDscBase, ISqlAGResource
{
    private SqlAGResource(string name) : base(name)
    {
    }

    public string AgName
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

    public SqlAGAutomatedBackupPreference? AutomatedBackupPreference
    {
        get => this.PropertyBag.Get<SqlAGAutomatedBackupPreference?>(Constants.Properties.AutomatedBackupPreference);
        set => this.PropertyBag.Set(Constants.Properties.AutomatedBackupPreference, value);
    }

    public SqlAGAvailabilityMode? AvailabilityMode
    {
        get => this.PropertyBag.Get<SqlAGAvailabilityMode?>(Constants.Properties.AvailabilityMode);
        set => this.PropertyBag.Set(Constants.Properties.AvailabilityMode, value);
    }

    public uint? BackupPriority
    {
        get => this.PropertyBag.Get<uint?>(Constants.Properties.BackupPriority);
        set => this.PropertyBag.Set(Constants.Properties.BackupPriority, value);
    }

    public bool? BasicAvailabilityGroup
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.BasicAvailabilityGroup);
        set => this.PropertyBag.Set(Constants.Properties.BasicAvailabilityGroup, value);
    }

    public bool? DatabaseHealthTrigger
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.DatabaseHealthTrigger);
        set => this.PropertyBag.Set(Constants.Properties.DatabaseHealthTrigger, value);
    }

    public bool? DtcSupportEnabled
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.DtcSupportEnabled);
        set => this.PropertyBag.Set(Constants.Properties.DtcSupportEnabled, value);
    }

    public SqlAGConnectionModeInPrimaryRole? ConnectionModeInPrimaryRole
    {
        get => this.PropertyBag.Get<SqlAGConnectionModeInPrimaryRole?>(Constants.Properties.ConnectionModeInPrimaryRole);
        set => this.PropertyBag.Set(Constants.Properties.ConnectionModeInPrimaryRole, value);
    }

    public SqlAGConnectionModeInSecondaryRole? ConnectionModeInSecondaryRole
    {
        get => this.PropertyBag.Get<SqlAGConnectionModeInSecondaryRole?>(Constants.Properties.ConnectionModeInSecondaryRole);
        set => this.PropertyBag.Set(Constants.Properties.ConnectionModeInSecondaryRole, value);
    }

    public string EndpointHostName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.EndpointHostName);
        set => this.PropertyBag.Set(Constants.Properties.EndpointHostName, value);
    }

    public SqlAGFailureConditionLevel? FailureConditionLevel
    {
        get => this.PropertyBag.Get<SqlAGFailureConditionLevel?>(Constants.Properties.FailureConditionLevel);
        set => this.PropertyBag.Set(Constants.Properties.FailureConditionLevel, value);
    }

    public SqlAGFailoverMode? FailoverMode
    {
        get => this.PropertyBag.Get<SqlAGFailoverMode?>(Constants.Properties.FailoverMode);
        set => this.PropertyBag.Set(Constants.Properties.FailoverMode, value);
    }

    public SqlAGSeedingMode? SeedingMode
    {
        get => this.PropertyBag.Get<SqlAGSeedingMode?>(Constants.Properties.SeedingMode);
        set => this.PropertyBag.Set(Constants.Properties.SeedingMode, value);
    }

    public uint? HealthCheckTimeout
    {
        get => this.PropertyBag.Get<uint?>(Constants.Properties.HealthCheckTimeout);
        set => this.PropertyBag.Set(Constants.Properties.HealthCheckTimeout, value);
    }

    public bool? ProcessOnlyOnActiveNode
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.ProcessOnlyOnActiveNode);
        set => this.PropertyBag.Set(Constants.Properties.ProcessOnlyOnActiveNode, value);
    }

    public static SqlAGResource Create(string name, Action<ISqlAGResource> configure)
    {
        var resource = new SqlAGResource(name);
        configure(resource);
        return resource;
    }

    public static SqlAGResource Create(string name, Action<ISqlAGResource> configure, out SqlAGResource resource)
    {
        resource = new SqlAGResource(name);
        configure(resource);
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var validation = this.ValidationBuilder();
        validation.ValidateStringNotNullOrEmpty(this.AgName, nameof(this.AgName));
        validation.ValidateStringNotNullOrEmpty(this.ServerName, nameof(this.ServerName));
        validation.ValidateStringNotNullOrEmpty(this.InstanceName, nameof(this.InstanceName));
        return Task.FromResult(validation.errors);
    }

    public override string ResourceId => Constants.ResourceId;

    public override bool HasEnsure => true;
}
