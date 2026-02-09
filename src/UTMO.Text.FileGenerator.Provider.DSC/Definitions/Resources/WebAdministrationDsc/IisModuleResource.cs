namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc;
using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Enums;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.WebAdministrationDscConstants.IisModule;
public sealed class IisModuleResource : WebAdministrationDscBase, IIisModule
{
    private IisModuleResource(string name) : base(name) { }
    public string ModulePath
    {
        get => this.PropertyBag.Get(Constants.Properties.Path);
        set => this.PropertyBag.Set(Constants.Properties.Path, value);
    }
    public string ModuleName
    {
        get => this.PropertyBag.Get(Constants.Properties.Name);
        set => this.PropertyBag.Set(Constants.Properties.Name, value);
    }
    public string RequestPath
    {
        get => this.PropertyBag.Get(Constants.Properties.RequestPath);
        set => this.PropertyBag.Set(Constants.Properties.RequestPath, value);
    }
    public string[] Verb
    {
        get => this.PropertyBag.Get<string[]>(Constants.Properties.Verb);
        set => this.PropertyBag.Set(Constants.Properties.Verb, value);
    }
    public string? SiteName
    {
        get => this.PropertyBag.Get(Constants.Properties.SiteName);
        set => this.PropertyBag.Set(Constants.Properties.SiteName, value);
    }
    public IisModuleType? ModuleType
    {
        get => this.PropertyBag.Get<IisModuleType?>(Constants.Properties.ModuleType);
        set => this.PropertyBag.Set(Constants.Properties.ModuleType, value);
    }
    public static IisModuleResource Create(string name, Action<IIisModule> configure)
    {
        var resource = new IisModuleResource(name);
        configure(resource);
        return resource;
    }
    public override Task<List<ValidationFailedException>> Validate()
    {
        var errors = this.ValidationBuilder()
            .ValidateStringNotNullOrEmpty(this.ModulePath, nameof(this.ModulePath))
            .ValidateStringNotNullOrEmpty(this.ModuleName, nameof(this.ModuleName))
            .ValidateStringNotNullOrEmpty(this.RequestPath, nameof(this.RequestPath))
            .errors;
        return Task.FromResult(errors);
    }
    public override string ResourceId => Constants.ResourceId;
}
