namespace UTMO.Text.FileGenerator.Provider.DSC.ChocoModule.Resources;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.ChocoModule.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.ChocoModule.ChocoModuleConstants.ChocolateySetting;

/// <summary>
/// DSC resource for the ChocolateySetting class from the Chocolatey module.
/// Sets or clears a named Chocolatey configuration setting.
/// </summary>
public class ChocolateySettingResource : ChocoModuleBase, IChocolateySettingResource
{
    private ChocolateySettingResource(string name) : base(name)
    {
    }

    public string SettingName
    {
        get => this.PropertyBag.Get(Constants.Parameters.Name);
        set => this.PropertyBag.Set(Constants.Parameters.Name, value);
    }

    public string? Value
    {
        get => this.PropertyBag.Get(Constants.Parameters.Value);
        set => this.PropertyBag.Set(Constants.Parameters.Value, value);
    }

    public static ChocolateySettingResource Create(string name, Action<IChocolateySettingResource> configure)
    {
        var resource = new ChocolateySettingResource(name);
        configure(resource);
        return resource;
    }

    public static ChocolateySettingResource Create(string name, Action<IChocolateySettingResource> configure,
                                                   out ChocolateySettingResource resourceRef)
    {
        var resource = new ChocolateySettingResource(name);
        configure(resource);
        resourceRef = resource;
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var errors = this.ValidationBuilder()
                         .ValidateStringNotNullOrEmpty(this.SettingName, nameof(this.SettingName))
                         .errors;

        return Task.FromResult(errors);
    }

    public override string ResourceId => Constants.ResourceId;

    public override bool HasEnsure => true;
}




