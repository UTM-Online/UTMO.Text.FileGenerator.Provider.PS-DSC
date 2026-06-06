namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Resources;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.SqlServerDscConstants.SqlAlias;

public sealed class SqlAliasResource : SqlServerDscBase, ISqlAliasResource
{
    private SqlAliasResource(string name) : base(name)
    {
    }

    public string AliasName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.Name);
        set => this.PropertyBag.Set(Constants.Properties.Name, value);
    }

    public string Protocol
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.Protocol);
        set => this.PropertyBag.Set(Constants.Properties.Protocol, value);
    }

    public string ServerName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.ServerName);
        set => this.PropertyBag.Set(Constants.Properties.ServerName, value);
    }

    public ushort? TcpPort
    {
        get => this.PropertyBag.Get<ushort?>(Constants.Properties.TcpPort);
        set => this.PropertyBag.Set(Constants.Properties.TcpPort, value);
    }

    public bool? UseDynamicTcpPort
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.UseDynamicTcpPort);
        set => this.PropertyBag.Set(Constants.Properties.UseDynamicTcpPort, value);
    }

    public static SqlAliasResource Create(string name, Action<ISqlAliasResource> configure)
    {
        var resource = new SqlAliasResource(name);
        configure(resource);
        return resource;
    }

    public static SqlAliasResource Create(string name, Action<ISqlAliasResource> configure, out SqlAliasResource resource)
    {
        resource = new SqlAliasResource(name);
        configure(resource);
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var validation = this.ValidationBuilder();
        validation.ValidateStringNotNullOrEmpty(this.AliasName, nameof(this.AliasName));
        return Task.FromResult(validation.errors);
    }

    public override string ResourceId => Constants.ResourceId;

    public override bool HasEnsure => true;
}
