namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Resources;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.SqlServerDscConstants.SqlDatabaseMail;

public sealed class SqlDatabaseMailResource : SqlServerDscBase, ISqlDatabaseMailResource
{
    private SqlDatabaseMailResource(string name) : base(name)
    {
    }

    public string AccountName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.AccountName);
        set => this.PropertyBag.Set(Constants.Properties.AccountName, value);
    }

    public string InstanceName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.InstanceName);
        set => this.PropertyBag.Set(Constants.Properties.InstanceName, value);
    }

    public string EmailAddress
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.EmailAddress);
        set => this.PropertyBag.Set(Constants.Properties.EmailAddress, value);
    }

    public string MailServerName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.MailServerName);
        set => this.PropertyBag.Set(Constants.Properties.MailServerName, value);
    }

    public string ProfileName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.ProfileName);
        set => this.PropertyBag.Set(Constants.Properties.ProfileName, value);
    }

    public string ServerName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.ServerName);
        set => this.PropertyBag.Set(Constants.Properties.ServerName, value);
    }

    public string DisplayName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.DisplayName);
        set => this.PropertyBag.Set(Constants.Properties.DisplayName, value);
    }

    public string ReplyToAddress
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.ReplyToAddress);
        set => this.PropertyBag.Set(Constants.Properties.ReplyToAddress, value);
    }

    public string MailDescription
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.Description);
        set => this.PropertyBag.Set(Constants.Properties.Description, value);
    }

    public string LoggingLevel
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.LoggingLevel);
        set => this.PropertyBag.Set(Constants.Properties.LoggingLevel, value);
    }

    public ushort? TcpPort
    {
        get => this.PropertyBag.Get<ushort?>(Constants.Properties.TcpPort);
        set => this.PropertyBag.Set(Constants.Properties.TcpPort, value);
    }

    public bool? UseDefaultCredentials
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.UseDefaultCredentials);
        set => this.PropertyBag.Set(Constants.Properties.UseDefaultCredentials, value);
    }

    public static SqlDatabaseMailResource Create(string name, Action<ISqlDatabaseMailResource> configure)
    {
        var resource = new SqlDatabaseMailResource(name);
        configure(resource);
        return resource;
    }

    public static SqlDatabaseMailResource Create(string name, Action<ISqlDatabaseMailResource> configure, out SqlDatabaseMailResource resource)
    {
        resource = new SqlDatabaseMailResource(name);
        configure(resource);
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var validation = this.ValidationBuilder();
        validation.ValidateStringNotNullOrEmpty(this.AccountName, nameof(this.AccountName));
        validation.ValidateStringNotNullOrEmpty(this.InstanceName, nameof(this.InstanceName));
        validation.ValidateStringNotNullOrEmpty(this.EmailAddress, nameof(this.EmailAddress));
        validation.ValidateStringNotNullOrEmpty(this.MailServerName, nameof(this.MailServerName));
        validation.ValidateStringNotNullOrEmpty(this.ProfileName, nameof(this.ProfileName));
        return Task.FromResult(validation.errors);
    }

    public override string ResourceId => Constants.ResourceId;

    public override bool HasEnsure => true;
}
