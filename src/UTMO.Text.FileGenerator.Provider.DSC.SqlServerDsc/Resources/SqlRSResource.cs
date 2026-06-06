namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Resources;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.SqlServerDscConstants.SqlRS;

public sealed class SqlRSResource : SqlServerDscBase, ISqlRSResource
{
    private SqlRSResource(string name) : base(name)
    {
    }

    public string InstanceName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.InstanceName);
        set => this.PropertyBag.Set(Constants.Properties.InstanceName, value);
    }

    public string DatabaseServerName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.DatabaseServerName);
        set => this.PropertyBag.Set(Constants.Properties.DatabaseServerName, value);
    }

    public string DatabaseInstanceName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.DatabaseInstanceName);
        set => this.PropertyBag.Set(Constants.Properties.DatabaseInstanceName, value);
    }

    public string ReportServerVirtualDirectory
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.ReportServerVirtualDirectory);
        set => this.PropertyBag.Set(Constants.Properties.ReportServerVirtualDirectory, value);
    }

    public string ReportsVirtualDirectory
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.ReportsVirtualDirectory);
        set => this.PropertyBag.Set(Constants.Properties.ReportsVirtualDirectory, value);
    }

    public string[] ReportServerReservedUrl
    {
        get => this.PropertyBag.Get<string[]>(Constants.Properties.ReportServerReservedUrl);
        set => this.PropertyBag.Set(Constants.Properties.ReportServerReservedUrl, value);
    }

    public string[] ReportsReservedUrl
    {
        get => this.PropertyBag.Get<string[]>(Constants.Properties.ReportsReservedUrl);
        set => this.PropertyBag.Set(Constants.Properties.ReportsReservedUrl, value);
    }

    public bool? UseSsl
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.UseSsl);
        set => this.PropertyBag.Set(Constants.Properties.UseSsl, value);
    }

    public bool? SuppressRestart
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.SuppressRestart);
        set => this.PropertyBag.Set(Constants.Properties.SuppressRestart, value);
    }

    public uint? RestartTimeout
    {
        get => this.PropertyBag.Get<uint?>(Constants.Properties.RestartTimeout);
        set => this.PropertyBag.Set(Constants.Properties.RestartTimeout, value);
    }

    public string Encrypt
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.Encrypt);
        set => this.PropertyBag.Set(Constants.Properties.Encrypt, value);
    }

    public static SqlRSResource Create(string name, Action<ISqlRSResource> configure)
    {
        var resource = new SqlRSResource(name);
        configure(resource);
        return resource;
    }

    public static SqlRSResource Create(string name, Action<ISqlRSResource> configure, out SqlRSResource resource)
    {
        resource = new SqlRSResource(name);
        configure(resource);
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var validation = this.ValidationBuilder();
        validation.ValidateStringNotNullOrEmpty(this.InstanceName, nameof(this.InstanceName));
        validation.ValidateStringNotNullOrEmpty(this.DatabaseServerName, nameof(this.DatabaseServerName));
        validation.ValidateStringNotNullOrEmpty(this.DatabaseInstanceName, nameof(this.DatabaseInstanceName));
        return Task.FromResult(validation.errors);
    }

    public override string ResourceId => Constants.ResourceId;

    public override bool HasEnsure => false;
}
