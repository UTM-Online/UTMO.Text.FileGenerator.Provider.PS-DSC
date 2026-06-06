namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Resources;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.SqlServerDscConstants.SqlProtocolTcpIp;

public sealed class SqlProtocolTcpIpResource : SqlServerDscBase, ISqlProtocolTcpIpResource
{
    private SqlProtocolTcpIpResource(string name) : base(name)
    {
    }

    public string InstanceName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.InstanceName);
        set => this.PropertyBag.Set(Constants.Properties.InstanceName, value);
    }

    public string IpAddressGroup
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.IpAddressGroup);
        set => this.PropertyBag.Set(Constants.Properties.IpAddressGroup, value);
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

    public string IpAddress
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.IpAddress);
        set => this.PropertyBag.Set(Constants.Properties.IpAddress, value);
    }

    public bool? UseTcpDynamicPort
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.UseTcpDynamicPort);
        set => this.PropertyBag.Set(Constants.Properties.UseTcpDynamicPort, value);
    }

    public string TcpPort
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.TcpPort);
        set => this.PropertyBag.Set(Constants.Properties.TcpPort, value);
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

    public static SqlProtocolTcpIpResource Create(string name, Action<ISqlProtocolTcpIpResource> configure)
    {
        var resource = new SqlProtocolTcpIpResource(name);
        configure(resource);
        return resource;
    }

    public static SqlProtocolTcpIpResource Create(string name, Action<ISqlProtocolTcpIpResource> configure, out SqlProtocolTcpIpResource resource)
    {
        resource = new SqlProtocolTcpIpResource(name);
        configure(resource);
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var validation = this.ValidationBuilder();
        validation.ValidateStringNotNullOrEmpty(this.InstanceName, nameof(this.InstanceName));
        validation.ValidateStringNotNullOrEmpty(this.IpAddressGroup, nameof(this.IpAddressGroup));
        return Task.FromResult(validation.errors);
    }

    public override string ResourceId => Constants.ResourceId;

    public override bool HasEnsure => false;
}
