namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Resources;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.SqlServerDscConstants.SqlWindowsFirewall;

public sealed class SqlWindowsFirewallResource : SqlServerDscBase, ISqlWindowsFirewallResource
{
    private SqlWindowsFirewallResource(string name) : base(name)
    {
    }

    public string SourcePath
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.SourcePath);
        set => this.PropertyBag.Set(Constants.Properties.SourcePath, value);
    }

    public string Features
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.Features);
        set => this.PropertyBag.Set(Constants.Properties.Features, value);
    }

    public string InstanceName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.InstanceName);
        set => this.PropertyBag.Set(Constants.Properties.InstanceName, value);
    }

    public string SourceCredential
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.SourceCredential);
        set => this.PropertyBag.Set(Constants.Properties.SourceCredential, value);
    }

    public static SqlWindowsFirewallResource Create(string name, Action<ISqlWindowsFirewallResource> configure)
    {
        var resource = new SqlWindowsFirewallResource(name);
        configure(resource);
        return resource;
    }

    public static SqlWindowsFirewallResource Create(string name, Action<ISqlWindowsFirewallResource> configure, out SqlWindowsFirewallResource resource)
    {
        resource = new SqlWindowsFirewallResource(name);
        configure(resource);
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var validation = this.ValidationBuilder();
        validation.ValidateStringNotNullOrEmpty(this.Features, nameof(this.Features));
        validation.ValidateStringNotNullOrEmpty(this.InstanceName, nameof(this.InstanceName));
        return Task.FromResult(validation.errors);
    }

    public override string ResourceId => Constants.ResourceId;

    public override bool HasEnsure => true;
}
