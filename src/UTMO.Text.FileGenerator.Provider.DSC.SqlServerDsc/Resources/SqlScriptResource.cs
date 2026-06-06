namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Resources;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.SqlServerDscConstants.SqlScript;

public sealed class SqlScriptResource : SqlServerDscBase, ISqlScriptResource
{
    private SqlScriptResource(string name) : base(name)
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

    public string SetFilePath
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.SetFilePath);
        set => this.PropertyBag.Set(Constants.Properties.SetFilePath, value);
    }

    public string GetFilePath
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.GetFilePath);
        set => this.PropertyBag.Set(Constants.Properties.GetFilePath, value);
    }

    public string TestFilePath
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.TestFilePath);
        set => this.PropertyBag.Set(Constants.Properties.TestFilePath, value);
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

    public static SqlScriptResource Create(string name, Action<ISqlScriptResource> configure)
    {
        var resource = new SqlScriptResource(name);
        configure(resource);
        return resource;
    }

    public static SqlScriptResource Create(string name, Action<ISqlScriptResource> configure, out SqlScriptResource resource)
    {
        resource = new SqlScriptResource(name);
        configure(resource);
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var validation = this.ValidationBuilder();
        validation.ValidateStringNotNullOrEmpty(this.Id, nameof(this.Id));
        validation.ValidateStringNotNullOrEmpty(this.InstanceName, nameof(this.InstanceName));
        validation.ValidateStringNotNullOrEmpty(this.SetFilePath, nameof(this.SetFilePath));
        validation.ValidateStringNotNullOrEmpty(this.GetFilePath, nameof(this.GetFilePath));
        validation.ValidateStringNotNullOrEmpty(this.TestFilePath, nameof(this.TestFilePath));
        return Task.FromResult(validation.errors);
    }

    public override string ResourceId => Constants.ResourceId;

    public override bool HasEnsure => false;
}
