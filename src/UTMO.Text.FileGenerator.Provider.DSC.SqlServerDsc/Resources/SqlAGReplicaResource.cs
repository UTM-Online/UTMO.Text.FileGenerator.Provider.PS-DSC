namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Resources;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Enums;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.SqlServerDscConstants.SqlAGReplica;

public sealed class SqlAGReplicaResource : SqlServerDscBase, ISqlAGReplicaResource
{
    private SqlAGReplicaResource(string name) : base(name)
    {
    }

    public string ReplicaName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.Name);
        set => this.PropertyBag.Set(Constants.Properties.Name, value);
    }

    public string AvailabilityGroupName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.AvailabilityGroupName);
        set => this.PropertyBag.Set(Constants.Properties.AvailabilityGroupName, value);
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

    public string PrimaryReplicaServerName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.PrimaryReplicaServerName);
        set => this.PropertyBag.Set(Constants.Properties.PrimaryReplicaServerName, value);
    }

    public string PrimaryReplicaInstanceName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.PrimaryReplicaInstanceName);
        set => this.PropertyBag.Set(Constants.Properties.PrimaryReplicaInstanceName, value);
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

    public SqlAGFailoverMode? FailoverMode
    {
        get => this.PropertyBag.Get<SqlAGFailoverMode?>(Constants.Properties.FailoverMode);
        set => this.PropertyBag.Set(Constants.Properties.FailoverMode, value);
    }

    public string ReadOnlyRoutingConnectionUrl
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.ReadOnlyRoutingConnectionUrl);
        set => this.PropertyBag.Set(Constants.Properties.ReadOnlyRoutingConnectionUrl, value);
    }

    public string[] ReadOnlyRoutingList
    {
        get => this.PropertyBag.Get<string[]>(Constants.Properties.ReadOnlyRoutingList);
        set => this.PropertyBag.Set(Constants.Properties.ReadOnlyRoutingList, value);
    }

    public bool? ProcessOnlyOnActiveNode
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.ProcessOnlyOnActiveNode);
        set => this.PropertyBag.Set(Constants.Properties.ProcessOnlyOnActiveNode, value);
    }

    public SqlAGSeedingMode? SeedingMode
    {
        get => this.PropertyBag.Get<SqlAGSeedingMode?>(Constants.Properties.SeedingMode);
        set => this.PropertyBag.Set(Constants.Properties.SeedingMode, value);
    }

    public static SqlAGReplicaResource Create(string name, Action<ISqlAGReplicaResource> configure)
    {
        var resource = new SqlAGReplicaResource(name);
        configure(resource);
        return resource;
    }

    public static SqlAGReplicaResource Create(string name, Action<ISqlAGReplicaResource> configure, out SqlAGReplicaResource resource)
    {
        resource = new SqlAGReplicaResource(name);
        configure(resource);
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var validation = this.ValidationBuilder();
        validation.ValidateStringNotNullOrEmpty(this.ReplicaName, nameof(this.ReplicaName));
        validation.ValidateStringNotNullOrEmpty(this.AvailabilityGroupName, nameof(this.AvailabilityGroupName));
        validation.ValidateStringNotNullOrEmpty(this.ServerName, nameof(this.ServerName));
        validation.ValidateStringNotNullOrEmpty(this.InstanceName, nameof(this.InstanceName));
        return Task.FromResult(validation.errors);
    }

    public override string ResourceId => Constants.ResourceId;

    public override bool HasEnsure => true;
}
