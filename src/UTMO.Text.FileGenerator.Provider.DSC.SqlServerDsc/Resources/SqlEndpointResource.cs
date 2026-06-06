namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Resources;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Enums;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.SqlServerDscConstants.SqlEndpoint;

public sealed class SqlEndpointResource : SqlServerDscBase, ISqlEndpointResource
{
    private SqlEndpointResource(string name) : base(name)
    {
    }

    public string EndpointName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.EndpointName);
        set => this.PropertyBag.Set(Constants.Properties.EndpointName, value);
    }

    public string InstanceName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.InstanceName);
        set => this.PropertyBag.Set(Constants.Properties.InstanceName, value);
    }

    public SqlEndpointType EndpointType
    {
        get => this.PropertyBag.Get<SqlEndpointType>(Constants.Properties.EndpointType);
        set => this.PropertyBag.Set(Constants.Properties.EndpointType, value);
    }

    public ushort? Port
    {
        get => this.PropertyBag.Get<ushort?>(Constants.Properties.Port);
        set => this.PropertyBag.Set(Constants.Properties.Port, value);
    }

    public string ServerName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.ServerName);
        set => this.PropertyBag.Set(Constants.Properties.ServerName, value);
    }

    public string IpAddress
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.IpAddress);
        set => this.PropertyBag.Set(Constants.Properties.IpAddress, value);
    }

    public string Owner
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.Owner);
        set => this.PropertyBag.Set(Constants.Properties.Owner, value);
    }

    public bool? IsMessageForwardingEnabled
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.IsMessageForwardingEnabled);
        set => this.PropertyBag.Set(Constants.Properties.IsMessageForwardingEnabled, value);
    }

    public uint? MessageForwardingSize
    {
        get => this.PropertyBag.Get<uint?>(Constants.Properties.MessageForwardingSize);
        set => this.PropertyBag.Set(Constants.Properties.MessageForwardingSize, value);
    }

    public SqlEndpointState? State
    {
        get => this.PropertyBag.Get<SqlEndpointState?>(Constants.Properties.State);
        set => this.PropertyBag.Set(Constants.Properties.State, value);
    }

    public static SqlEndpointResource Create(string name, Action<ISqlEndpointResource> configure)
    {
        var resource = new SqlEndpointResource(name);
        configure(resource);
        return resource;
    }

    public static SqlEndpointResource Create(string name, Action<ISqlEndpointResource> configure, out SqlEndpointResource resource)
    {
        resource = new SqlEndpointResource(name);
        configure(resource);
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var validation = this.ValidationBuilder();
        validation.ValidateStringNotNullOrEmpty(this.EndpointName, nameof(this.EndpointName));
        validation.ValidateStringNotNullOrEmpty(this.InstanceName, nameof(this.InstanceName));
        return Task.FromResult(validation.errors);
    }

    public override string ResourceId => Constants.ResourceId;

    public override bool HasEnsure => true;
}
