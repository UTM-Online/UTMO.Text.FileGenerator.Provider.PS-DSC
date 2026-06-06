namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Resources;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.SqlServerDscConstants.SqlAGDatabase;

public sealed class SqlAGDatabaseResource : SqlServerDscBase, ISqlAGDatabaseResource
{
    private SqlAGDatabaseResource(string name) : base(name)
    {
    }

    public string[] DatabaseName
    {
        get => this.PropertyBag.Get<string[]>(Constants.Properties.DatabaseName);
        set => this.PropertyBag.Set(Constants.Properties.DatabaseName, value);
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

    public string AvailabilityGroupName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.AvailabilityGroupName);
        set => this.PropertyBag.Set(Constants.Properties.AvailabilityGroupName, value);
    }

    public string BackupPath
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.BackupPath);
        set => this.PropertyBag.Set(Constants.Properties.BackupPath, value);
    }

    public bool? Force
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.Force);
        set => this.PropertyBag.Set(Constants.Properties.Force, value);
    }

    public bool? MatchDatabaseOwner
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.MatchDatabaseOwner);
        set => this.PropertyBag.Set(Constants.Properties.MatchDatabaseOwner, value);
    }

    public bool? ReplaceExisting
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.ReplaceExisting);
        set => this.PropertyBag.Set(Constants.Properties.ReplaceExisting, value);
    }

    public bool? ProcessOnlyOnActiveNode
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.ProcessOnlyOnActiveNode);
        set => this.PropertyBag.Set(Constants.Properties.ProcessOnlyOnActiveNode, value);
    }

    public int? StatementTimeout
    {
        get => this.PropertyBag.Get<int?>(Constants.Properties.StatementTimeout);
        set => this.PropertyBag.Set(Constants.Properties.StatementTimeout, value);
    }

    public static SqlAGDatabaseResource Create(string name, Action<ISqlAGDatabaseResource> configure)
    {
        var resource = new SqlAGDatabaseResource(name);
        configure(resource);
        return resource;
    }

    public static SqlAGDatabaseResource Create(string name, Action<ISqlAGDatabaseResource> configure, out SqlAGDatabaseResource resource)
    {
        resource = new SqlAGDatabaseResource(name);
        configure(resource);
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var validation = this.ValidationBuilder();
        validation.ValidateStringNotNullOrEmpty(this.ServerName, nameof(this.ServerName));
        validation.ValidateStringNotNullOrEmpty(this.InstanceName, nameof(this.InstanceName));
        validation.ValidateStringNotNullOrEmpty(this.AvailabilityGroupName, nameof(this.AvailabilityGroupName));
        validation.ValidateStringNotNullOrEmpty(this.BackupPath, nameof(this.BackupPath));
        return Task.FromResult(validation.errors);
    }

    public override string ResourceId => Constants.ResourceId;

    public override bool HasEnsure => true;
}
