namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Resources;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.SqlServerDscConstants.SqlScriptQuery;

public sealed class SqlScriptQueryResource : SqlServerDscBase, ISqlScriptQueryResource
{
    private SqlScriptQueryResource(string name) : base(name)
    {
    }

    public string Id
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.Id);
        set => this.PropertyBag.Set(Constants.Properties.Id, value);
    }

    public string InstanceName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.InstanceName);
        set => this.PropertyBag.Set(Constants.Properties.InstanceName, value);
    }

    public string GetQuery
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.GetQuery);
        set => this.PropertyBag.Set(Constants.Properties.GetQuery, value);
    }

    public string TestQuery
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.TestQuery);
        set => this.PropertyBag.Set(Constants.Properties.TestQuery, value);
    }

    public string SetQuery
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.SetQuery);
        set => this.PropertyBag.Set(Constants.Properties.SetQuery, value);
    }

    public string ServerName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.ServerName);
        set => this.PropertyBag.Set(Constants.Properties.ServerName, value);
    }

    public string Credential
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.Credential);
        set => this.PropertyBag.Set(Constants.Properties.Credential, value);
    }

    public string[] Variable
    {
        get => this.PropertyBag.Get<string[]>(Constants.Properties.Variable);
        set => this.PropertyBag.Set(Constants.Properties.Variable, value);
    }

    public bool? DisableVariables
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.DisableVariables);
        set => this.PropertyBag.Set(Constants.Properties.DisableVariables, value);
    }

    public uint? QueryTimeout
    {
        get => this.PropertyBag.Get<uint?>(Constants.Properties.QueryTimeout);
        set => this.PropertyBag.Set(Constants.Properties.QueryTimeout, value);
    }

    public string Encrypt
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.Encrypt);
        set => this.PropertyBag.Set(Constants.Properties.Encrypt, value);
    }

    public static SqlScriptQueryResource Create(string name, Action<ISqlScriptQueryResource> configure)
    {
        var resource = new SqlScriptQueryResource(name);
        configure(resource);
        return resource;
    }

    public static SqlScriptQueryResource Create(string name, Action<ISqlScriptQueryResource> configure, out SqlScriptQueryResource resource)
    {
        resource = new SqlScriptQueryResource(name);
        configure(resource);
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var validation = this.ValidationBuilder();
        validation.ValidateStringNotNullOrEmpty(this.Id, nameof(this.Id));
        validation.ValidateStringNotNullOrEmpty(this.InstanceName, nameof(this.InstanceName));
        validation.ValidateStringNotNullOrEmpty(this.GetQuery, nameof(this.GetQuery));
        validation.ValidateStringNotNullOrEmpty(this.TestQuery, nameof(this.TestQuery));
        validation.ValidateStringNotNullOrEmpty(this.SetQuery, nameof(this.SetQuery));
        return Task.FromResult(validation.errors);
    }

    public override string ResourceId => Constants.ResourceId;

    public override bool HasEnsure => false;
}
