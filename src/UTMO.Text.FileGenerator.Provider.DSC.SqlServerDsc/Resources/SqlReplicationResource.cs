namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Resources;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Enums;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.SqlServerDscConstants.SqlReplication;

public sealed class SqlReplicationResource : SqlServerDscBase, ISqlReplicationResource
{
    private SqlReplicationResource(string name) : base(name)
    {
    }

    public string InstanceName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.InstanceName);
        set => this.PropertyBag.Set(Constants.Properties.InstanceName, value);
    }

    public SqlReplicationDistributorMode DistributorMode
    {
        get => this.PropertyBag.Get<SqlReplicationDistributorMode>(Constants.Properties.DistributorMode);
        set => this.PropertyBag.Set(Constants.Properties.DistributorMode, value);
    }

    public string AdminLinkCredentials
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.AdminLinkCredentials);
        set => this.PropertyBag.Set(Constants.Properties.AdminLinkCredentials, value);
    }

    public string DistributionDBName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.DistributionDBName);
        set => this.PropertyBag.Set(Constants.Properties.DistributionDBName, value);
    }

    public string RemoteDistributor
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.RemoteDistributor);
        set => this.PropertyBag.Set(Constants.Properties.RemoteDistributor, value);
    }

    public string WorkingDirectory
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.WorkingDirectory);
        set => this.PropertyBag.Set(Constants.Properties.WorkingDirectory, value);
    }

    public bool? UseTrustedConnection
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.UseTrustedConnection);
        set => this.PropertyBag.Set(Constants.Properties.UseTrustedConnection, value);
    }

    public bool? UninstallWithForce
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.UninstallWithForce);
        set => this.PropertyBag.Set(Constants.Properties.UninstallWithForce, value);
    }

    public static SqlReplicationResource Create(string name, Action<ISqlReplicationResource> configure)
    {
        var resource = new SqlReplicationResource(name);
        configure(resource);
        return resource;
    }

    public static SqlReplicationResource Create(string name, Action<ISqlReplicationResource> configure, out SqlReplicationResource resource)
    {
        resource = new SqlReplicationResource(name);
        configure(resource);
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var validation = this.ValidationBuilder();
        validation.ValidateStringNotNullOrEmpty(this.InstanceName, nameof(this.InstanceName));
        validation.ValidateStringNotNullOrEmpty(this.AdminLinkCredentials, nameof(this.AdminLinkCredentials));
        validation.ValidateStringNotNullOrEmpty(this.RemoteDistributor, nameof(this.RemoteDistributor));
        validation.ValidateStringNotNullOrEmpty(this.WorkingDirectory, nameof(this.WorkingDirectory));
        return Task.FromResult(validation.errors);
    }

    public override string ResourceId => Constants.ResourceId;

    public override bool HasEnsure => true;
}
