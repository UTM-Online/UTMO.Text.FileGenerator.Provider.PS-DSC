namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc;
using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.WebAdministrationDscConstants.WebConfigPropertyCollection;
public sealed class WebConfigPropertyCollectionResource : WebAdministrationDscBase, IWebConfigPropertyCollection
{
    private WebConfigPropertyCollectionResource(string name) : base(name) { }
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
    public string CollectionName
    {
        get => this.PropertyBag.Get(Constants.Properties.CollectionName);
        set => this.PropertyBag.Set(Constants.Properties.CollectionName, value);
    }
    public string ItemName
    {
        get => this.PropertyBag.Get(Constants.Properties.ItemName);
        set => this.PropertyBag.Set(Constants.Properties.ItemName, value);
    }
    public string ItemKeyName
    {
        get => this.PropertyBag.Get(Constants.Properties.ItemKeyName);
        set => this.PropertyBag.Set(Constants.Properties.ItemKeyName, value);
    }
    public string ItemKeyValue
    {
        get => this.PropertyBag.Get(Constants.Properties.ItemKeyValue);
        set => this.PropertyBag.Set(Constants.Properties.ItemKeyValue, value);
    }
    public string ItemPropertyName
    {
        get => this.PropertyBag.Get(Constants.Properties.ItemPropertyName);
        set => this.PropertyBag.Set(Constants.Properties.ItemPropertyName, value);
    }
    public string? ItemPropertyValue
    {
        get => this.PropertyBag.Get(Constants.Properties.ItemPropertyValue);
        set => this.PropertyBag.Set(Constants.Properties.ItemPropertyValue, value);
    }
    public static WebConfigPropertyCollectionResource Create(string name, Action<IWebConfigPropertyCollection> configure)
    {
        var resource = new WebConfigPropertyCollectionResource(name);
        configure(resource);
        return resource;
    }
    public override Task<List<ValidationFailedException>> Validate()
    {
        var errors = this.ValidationBuilder()
            .ValidateStringNotNullOrEmpty(this.WebsitePath, nameof(this.WebsitePath))
            .ValidateStringNotNullOrEmpty(this.Filter, nameof(this.Filter))
            .ValidateStringNotNullOrEmpty(this.CollectionName, nameof(this.CollectionName))
            .ValidateStringNotNullOrEmpty(this.ItemName, nameof(this.ItemName))
            .ValidateStringNotNullOrEmpty(this.ItemKeyName, nameof(this.ItemKeyName))
            .ValidateStringNotNullOrEmpty(this.ItemKeyValue, nameof(this.ItemKeyValue))
            .ValidateStringNotNullOrEmpty(this.ItemPropertyName, nameof(this.ItemPropertyName))
            .errors;
        return Task.FromResult(errors);
    }
    public override string ResourceId => Constants.ResourceId;
}
