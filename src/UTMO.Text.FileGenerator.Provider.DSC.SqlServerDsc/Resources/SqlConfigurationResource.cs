namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Resources;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.SqlServerDscConstants.SqlConfiguration;

public sealed class SqlConfigurationResource : SqlServerDscBase, ISqlConfigurationResource
{
    private SqlConfigurationResource(string name) : base(name)
    {
    }

    public string InstanceName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.InstanceName);
        set => this.PropertyBag.Set(Constants.Properties.InstanceName, value);
    }

    public string OptionName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.OptionName);
        set => this.PropertyBag.Set(Constants.Properties.OptionName, value);
    }

    public int OptionValue
    {
        get => this.PropertyBag.Get<int>(Constants.Properties.OptionValue);
        set => this.PropertyBag.Set(Constants.Properties.OptionValue, value);
    }

    public string ServerName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.ServerName);
        set => this.PropertyBag.Set(Constants.Properties.ServerName, value);
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

    public static SqlConfigurationResource Create(string name, Action<ISqlConfigurationResource> configure)
    {
        var resource = new SqlConfigurationResource(name);
        configure(resource);
        return resource;
    }

    public static SqlConfigurationResource Create(string name, Action<ISqlConfigurationResource> configure, out SqlConfigurationResource resource)
    {
        resource = new SqlConfigurationResource(name);
        configure(resource);
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var validation = this.ValidationBuilder();
        validation.ValidateStringNotNullOrEmpty(this.InstanceName, nameof(this.InstanceName));
        validation.ValidateStringNotNullOrEmpty(this.OptionName, nameof(this.OptionName));
        return Task.FromResult(validation.errors);
    }

    public override string ResourceId => Constants.ResourceId;

    public override bool HasEnsure => false;
}
