namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Resources;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.SqlServerDscConstants.SqlEndpointPermission;

public sealed class SqlEndpointPermissionResource : SqlServerDscBase, ISqlEndpointPermissionResource
{
    private SqlEndpointPermissionResource(string name) : base(name)
    {
    }

    public string InstanceName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.InstanceName);
        set => this.PropertyBag.Set(Constants.Properties.InstanceName, value);
    }

    public string Principal
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.Principal);
        set => this.PropertyBag.Set(Constants.Properties.Principal, value);
    }

    public string EndpointName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.Name);
        set => this.PropertyBag.Set(Constants.Properties.Name, value);
    }

    public string ServerName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.ServerName);
        set => this.PropertyBag.Set(Constants.Properties.ServerName, value);
    }

    public string Permission
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.Permission);
        set => this.PropertyBag.Set(Constants.Properties.Permission, value);
    }

    public static SqlEndpointPermissionResource Create(string name, Action<ISqlEndpointPermissionResource> configure)
    {
        var resource = new SqlEndpointPermissionResource(name);
        configure(resource);
        return resource;
    }

    public static SqlEndpointPermissionResource Create(string name, Action<ISqlEndpointPermissionResource> configure, out SqlEndpointPermissionResource resource)
    {
        resource = new SqlEndpointPermissionResource(name);
        configure(resource);
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var validation = this.ValidationBuilder();
        validation.ValidateStringNotNullOrEmpty(this.InstanceName, nameof(this.InstanceName));
        validation.ValidateStringNotNullOrEmpty(this.Principal, nameof(this.Principal));
        validation.ValidateStringNotNullOrEmpty(this.EndpointName, nameof(this.EndpointName));
        return Task.FromResult(validation.errors);
    }

    public override string ResourceId => Constants.ResourceId;

    public override bool HasEnsure => true;
}
