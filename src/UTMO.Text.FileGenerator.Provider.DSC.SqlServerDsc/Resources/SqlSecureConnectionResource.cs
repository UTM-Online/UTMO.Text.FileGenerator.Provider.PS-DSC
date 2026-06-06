namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Resources;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.SqlServerDscConstants.SqlSecureConnection;

public sealed class SqlSecureConnectionResource : SqlServerDscBase, ISqlSecureConnectionResource
{
    private SqlSecureConnectionResource(string name) : base(name)
    {
    }

    public string InstanceName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.InstanceName);
        set => this.PropertyBag.Set(Constants.Properties.InstanceName, value);
    }

    public string Thumbprint
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.Thumbprint);
        set => this.PropertyBag.Set(Constants.Properties.Thumbprint, value);
    }

    public bool? ForceEncryption
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.ForceEncryption);
        set => this.PropertyBag.Set(Constants.Properties.ForceEncryption, value);
    }

    public string ServiceAccount
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.ServiceAccount);
        set => this.PropertyBag.Set(Constants.Properties.ServiceAccount, value);
    }

    public bool? SuppressRestart
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.SuppressRestart);
        set => this.PropertyBag.Set(Constants.Properties.SuppressRestart, value);
    }

    public string ServerName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.ServerName);
        set => this.PropertyBag.Set(Constants.Properties.ServerName, value);
    }

    public static SqlSecureConnectionResource Create(string name, Action<ISqlSecureConnectionResource> configure)
    {
        var resource = new SqlSecureConnectionResource(name);
        configure(resource);
        return resource;
    }

    public static SqlSecureConnectionResource Create(string name, Action<ISqlSecureConnectionResource> configure, out SqlSecureConnectionResource resource)
    {
        resource = new SqlSecureConnectionResource(name);
        configure(resource);
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var validation = this.ValidationBuilder();
        validation.ValidateStringNotNullOrEmpty(this.InstanceName, nameof(this.InstanceName));
        validation.ValidateStringNotNullOrEmpty(this.Thumbprint, nameof(this.Thumbprint));
        validation.ValidateStringNotNullOrEmpty(this.ServiceAccount, nameof(this.ServiceAccount));
        return Task.FromResult(validation.errors);
    }

    public override string ResourceId => Constants.ResourceId;

    public override bool HasEnsure => true;
}
