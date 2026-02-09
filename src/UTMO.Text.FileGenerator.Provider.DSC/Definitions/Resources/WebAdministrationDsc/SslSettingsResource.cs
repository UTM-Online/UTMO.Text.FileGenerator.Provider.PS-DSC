namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc;
using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Enums;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.WebAdministrationDscConstants.SslSettings;
public sealed class SslSettingsResource : WebAdministrationDscBase, ISslSettings
{
    private SslSettingsResource(string name) : base(name) { }
    public string SiteName
    {
        get => this.PropertyBag.Get(Constants.Properties.Name);
        set => this.PropertyBag.Set(Constants.Properties.Name, value);
    }
    public SslBinding[] Bindings
    {
        get => this.PropertyBag.Get<SslBinding[]>(Constants.Properties.Bindings);
        set => this.PropertyBag.Set(Constants.Properties.Bindings, value);
    }
    public static SslSettingsResource Create(string name, Action<ISslSettings> configure)
    {
        var resource = new SslSettingsResource(name);
        configure(resource);
        return resource;
    }
    public override Task<List<ValidationFailedException>> Validate()
    {
        var errors = this.ValidationBuilder()
            .ValidateStringNotNullOrEmpty(this.SiteName, nameof(this.SiteName))
            .errors;
        return Task.FromResult(errors);
    }
    public override string ResourceId => Constants.ResourceId;
}
