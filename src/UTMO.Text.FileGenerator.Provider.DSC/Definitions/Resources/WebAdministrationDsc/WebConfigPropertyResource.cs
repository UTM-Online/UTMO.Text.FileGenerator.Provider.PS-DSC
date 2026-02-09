namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc;
using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.WebAdministrationDscConstants.WebConfigProperty;
public sealed class WebConfigPropertyResource : WebAdministrationDscBase, IWebConfigProperty
{
    private WebConfigPropertyResource(string name) : base(name) { }
    public string WebsitePath
    {
        get => this.PropertyBag.Get(Constants.Properties.WebsitePath);
        set => this.PropertyBag.Set(Constants.Properties.WebsitePath, value);
    }
    public string Filter
    {
        get => this.PropertyBag.Get(Constants.Properties.Filter);
        set => this.PropertyBag.Set(Constants.Properties.Filter, value);
    }
    public string PropertyName
    {
        get => this.PropertyBag.Get(Constants.Properties.PropertyName);
        set => this.PropertyBag.Set(Constants.Properties.PropertyName, value);
    }
    public string? Value
    {
        get => this.PropertyBag.Get(Constants.Properties.Value);
        set => this.PropertyBag.Set(Constants.Properties.Value, value);
    }
    public static WebConfigPropertyResource Create(string name, Action<IWebConfigProperty> configure)
    {
        var resource = new WebConfigPropertyResource(name);
        configure(resource);
        return resource;
    }
    public override Task<List<ValidationFailedException>> Validate()
    {
        var errors = this.ValidationBuilder()
            .ValidateStringNotNullOrEmpty(this.WebsitePath, nameof(this.WebsitePath))
            .ValidateStringNotNullOrEmpty(this.Filter, nameof(this.Filter))
            .ValidateStringNotNullOrEmpty(this.PropertyName, nameof(this.PropertyName))
            .errors;
        return Task.FromResult(errors);
    }
    public override string ResourceId => Constants.ResourceId;
}
