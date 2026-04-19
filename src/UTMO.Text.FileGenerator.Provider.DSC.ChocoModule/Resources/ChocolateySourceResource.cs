namespace UTMO.Text.FileGenerator.Provider.DSC.ChocoModule.Resources;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.ChocoModule.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.ChocoModule.ChocoModuleConstants.ChocolateySource;

/// <summary>
/// DSC resource for the ChocolateySource class from the Chocolatey module.
/// Registers, updates, enables, disables, or removes a Chocolatey package source/feed.
/// </summary>
public class ChocolateySourceResource : ChocoModuleBase, IChocolateySourceResource
{
    private ChocolateySourceResource(string name) : base(name)
    {
    }

    public string SourceName
    {
        get => this.PropertyBag.Get(Constants.Parameters.Name);
        set => this.PropertyBag.Set(Constants.Parameters.Name, value);
    }

    public string? Source
    {
        get => this.PropertyBag.Get(Constants.Parameters.Source);
        set => this.PropertyBag.Set(Constants.Parameters.Source, value);
    }

    public bool? Disabled
    {
        get => this.PropertyBag.Get<bool?>(Constants.Parameters.Disabled);
        set => this.PropertyBag.Set(Constants.Parameters.Disabled, value);
    }

    public bool? ByPassProxy
    {
        get => this.PropertyBag.Get<bool?>(Constants.Parameters.ByPassProxy);
        set => this.PropertyBag.Set(Constants.Parameters.ByPassProxy, value);
    }

    public bool? SelfService
    {
        get => this.PropertyBag.Get<bool?>(Constants.Parameters.SelfService);
        set => this.PropertyBag.Set(Constants.Parameters.SelfService, value);
    }

    public int? Priority
    {
        get => this.PropertyBag.Get<int?>(Constants.Parameters.Priority);
        set => this.PropertyBag.Set(Constants.Parameters.Priority, value);
    }

    public string? Username
    {
        get => this.PropertyBag.Get(Constants.Parameters.Username);
        set => this.PropertyBag.Set(Constants.Parameters.Username, value);
    }

    public string? Password
    {
        get => this.PropertyBag.Get(Constants.Parameters.Password);
        set => this.PropertyBag.Set(Constants.Parameters.Password, value);
    }

    public static ChocolateySourceResource Create(string name, Action<IChocolateySourceResource> configure)
    {
        var resource = new ChocolateySourceResource(name);
        configure(resource);
        return resource;
    }

    public static ChocolateySourceResource Create(string name, Action<IChocolateySourceResource> configure,
                                                  out ChocolateySourceResource resourceRef)
    {
        var resource = new ChocolateySourceResource(name);
        configure(resource);
        resourceRef = resource;
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var errors = this.ValidationBuilder()
                         .ValidateStringNotNullOrEmpty(this.SourceName, nameof(this.SourceName))
                         .errors;

        return Task.FromResult(errors);
    }

    public override string ResourceId => Constants.ResourceId;

    public override bool HasEnsure => true;
}




