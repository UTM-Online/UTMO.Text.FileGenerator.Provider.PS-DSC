namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Resources;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.SqlServerDscConstants.SqlAGListener;

public sealed class SqlAGListenerResource : SqlServerDscBase, ISqlAGListenerResource
{
    private SqlAGListenerResource(string name) : base(name)
    {
    }

    public string InstanceName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.InstanceName);
        set => this.PropertyBag.Set(Constants.Properties.InstanceName, value);
    }

    public string ServerName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.ServerName);
        set => this.PropertyBag.Set(Constants.Properties.ServerName, value);
    }

    public string ListenerName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.Name);
        set => this.PropertyBag.Set(Constants.Properties.Name, value);
    }

    public string AvailabilityGroup
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.AvailabilityGroup);
        set => this.PropertyBag.Set(Constants.Properties.AvailabilityGroup, value);
    }

    public string[] IpAddress
    {
        get => this.PropertyBag.Get<string[]>(Constants.Properties.IpAddress);
        set => this.PropertyBag.Set(Constants.Properties.IpAddress, value);
    }

    public ushort? Port
    {
        get => this.PropertyBag.Get<ushort?>(Constants.Properties.Port);
        set => this.PropertyBag.Set(Constants.Properties.Port, value);
    }

    public bool? DHCP
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.DHCP);
        set => this.PropertyBag.Set(Constants.Properties.DHCP, value);
    }

    public bool? ProcessOnlyOnActiveNode
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.ProcessOnlyOnActiveNode);
        set => this.PropertyBag.Set(Constants.Properties.ProcessOnlyOnActiveNode, value);
    }

    public static SqlAGListenerResource Create(string name, Action<ISqlAGListenerResource> configure)
    {
        var resource = new SqlAGListenerResource(name);
        configure(resource);
        return resource;
    }

    public static SqlAGListenerResource Create(string name, Action<ISqlAGListenerResource> configure, out SqlAGListenerResource resource)
    {
        resource = new SqlAGListenerResource(name);
        configure(resource);
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var validation = this.ValidationBuilder();
        validation.ValidateStringNotNullOrEmpty(this.InstanceName, nameof(this.InstanceName));
        validation.ValidateStringNotNullOrEmpty(this.ServerName, nameof(this.ServerName));
        validation.ValidateStringNotNullOrEmpty(this.ListenerName, nameof(this.ListenerName));
        validation.ValidateStringNotNullOrEmpty(this.AvailabilityGroup, nameof(this.AvailabilityGroup));
        return Task.FromResult(validation.errors);
    }

    public override string ResourceId => Constants.ResourceId;

    public override bool HasEnsure => true;
}
