namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Resources;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Enums;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.SqlServerDscConstants.SqlProtocol;

public sealed class SqlProtocolResource : SqlServerDscBase, ISqlProtocolResource
{
    private SqlProtocolResource(string name) : base(name)
    {
    }

    public string InstanceName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.InstanceName);
        set => this.PropertyBag.Set(Constants.Properties.InstanceName, value);
    }

    public SqlProtocolName ProtocolName
    {
        get => this.PropertyBag.Get<SqlProtocolName>(Constants.Properties.ProtocolName);
        set => this.PropertyBag.Set(Constants.Properties.ProtocolName, value);
    }

    public string ServerName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.ServerName);
        set => this.PropertyBag.Set(Constants.Properties.ServerName, value);
    }

    public bool? Enabled
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.Enabled);
        set => this.PropertyBag.Set(Constants.Properties.Enabled, value);
    }

    public bool? ListenOnAllIpAddresses
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.ListenOnAllIpAddresses);
        set => this.PropertyBag.Set(Constants.Properties.ListenOnAllIpAddresses, value);
    }

    public int? KeepAlive
    {
        get => this.PropertyBag.Get<int?>(Constants.Properties.KeepAlive);
        set => this.PropertyBag.Set(Constants.Properties.KeepAlive, value);
    }

    public string PipeName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.PipeName);
        set => this.PropertyBag.Set(Constants.Properties.PipeName, value);
    }

    public bool? SuppressRestart
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.SuppressRestart);
        set => this.PropertyBag.Set(Constants.Properties.SuppressRestart, value);
    }

    public ushort? RestartTimeout
    {
        get => this.PropertyBag.Get<ushort?>(Constants.Properties.RestartTimeout);
        set => this.PropertyBag.Set(Constants.Properties.RestartTimeout, value);
    }

    public static SqlProtocolResource Create(string name, Action<ISqlProtocolResource> configure)
    {
        var resource = new SqlProtocolResource(name);
        configure(resource);
        return resource;
    }

    public static SqlProtocolResource Create(string name, Action<ISqlProtocolResource> configure, out SqlProtocolResource resource)
    {
        resource = new SqlProtocolResource(name);
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
