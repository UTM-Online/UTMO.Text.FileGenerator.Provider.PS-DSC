namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc;
using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Enums;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.WebAdministrationDscConstants.IisFeatureDelegation;
public sealed class IisFeatureDelegationResource : WebAdministrationDscBase, IIisFeatureDelegation
{
    private IisFeatureDelegationResource(string name) : base(name) { }
    public string Path
    {
        get => this.PropertyBag.Get(Constants.Properties.Path);
        set => this.PropertyBag.Set(Constants.Properties.Path, value);
    }
    public string Filter
    {
        get => this.PropertyBag.Get(Constants.Properties.Filter);
        set => this.PropertyBag.Set(Constants.Properties.Filter, value);
    }
    public OverrideMode OverrideMode
    {
        get => this.PropertyBag.Get<OverrideMode>(Constants.Properties.OverrideMode);
        set => this.PropertyBag.Set(Constants.Properties.OverrideMode, value);
    }
    public static IisFeatureDelegationResource Create(string name, Action<IIisFeatureDelegation> configure)
    {
        var resource = new IisFeatureDelegationResource(name);
        configure(resource);
        return resource;
    }
    public override Task<List<ValidationFailedException>> Validate()
    {
        var errors = this.ValidationBuilder()
            .ValidateStringNotNullOrEmpty(this.Path, nameof(this.Path))
            .ValidateStringNotNullOrEmpty(this.Filter, nameof(this.Filter))
            .errors;
        return Task.FromResult(errors);
    }
    public override string ResourceId => Constants.ResourceId;
}
