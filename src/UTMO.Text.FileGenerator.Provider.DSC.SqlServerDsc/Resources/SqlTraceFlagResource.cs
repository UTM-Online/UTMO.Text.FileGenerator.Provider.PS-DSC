namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Resources;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.SqlServerDscConstants.SqlTraceFlag;

public sealed class SqlTraceFlagResource : SqlServerDscBase, ISqlTraceFlagResource
{
    private SqlTraceFlagResource(string name) : base(name)
    {
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

    public uint[] TraceFlags
    {
        get => this.PropertyBag.Get<uint[]>(Constants.Properties.TraceFlags);
        set => this.PropertyBag.Set(Constants.Properties.TraceFlags, value);
    }

    public uint[] TraceFlagsToInclude
    {
        get => this.PropertyBag.Get<uint[]>(Constants.Properties.TraceFlagsToInclude);
        set => this.PropertyBag.Set(Constants.Properties.TraceFlagsToInclude, value);
    }

    public uint[] TraceFlagsToExclude
    {
        get => this.PropertyBag.Get<uint[]>(Constants.Properties.TraceFlagsToExclude);
        set => this.PropertyBag.Set(Constants.Properties.TraceFlagsToExclude, value);
    }

    public bool? ClearAllTraceFlags
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.ClearAllTraceFlags);
        set => this.PropertyBag.Set(Constants.Properties.ClearAllTraceFlags, value);
    }

    public bool? RestartService
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.RestartService);
        set => this.PropertyBag.Set(Constants.Properties.RestartService, value);
    }

    public uint? RestartTimeout
    {
        get => this.PropertyBag.Get<uint?>(Constants.Properties.RestartTimeout);
        set => this.PropertyBag.Set(Constants.Properties.RestartTimeout, value);
    }

    public static SqlTraceFlagResource Create(string name, Action<ISqlTraceFlagResource> configure)
    {
        var resource = new SqlTraceFlagResource(name);
        configure(resource);
        return resource;
    }

    public static SqlTraceFlagResource Create(string name, Action<ISqlTraceFlagResource> configure, out SqlTraceFlagResource resource)
    {
        resource = new SqlTraceFlagResource(name);
        configure(resource);
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var validation = this.ValidationBuilder();
        validation.ValidateStringNotNullOrEmpty(this.InstanceName, nameof(this.InstanceName));
        return Task.FromResult(validation.errors);
    }

    public override string ResourceId => Constants.ResourceId;

    public override bool HasEnsure => false;
}
