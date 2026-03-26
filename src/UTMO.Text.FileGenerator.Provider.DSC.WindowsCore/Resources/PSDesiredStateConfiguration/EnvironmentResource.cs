namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.BaseDefinitions;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Constants.PSDesiredStateConfigurationConstants.Environment;

/// <summary>
/// DSC resource for managing environment variables.
/// </summary>
public sealed class EnvironmentResource : PSDesiredStateConfigurationBase, IEnvironmentResource
{
    private EnvironmentResource(string name) : base(name)
    {
    }

    /// <inheritdoc />
    public string EnvironmentName
    {
        get => this.PropertyBag.Get(Constants.Properties.Name);
        set => this.PropertyBag.Set(Constants.Properties.Name, value);
    }

    /// <inheritdoc />
    public bool? Path
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.Path);
        set => this.PropertyBag.Set(Constants.Properties.Path, value);
    }

    /// <inheritdoc />
    public string? Value
    {
        get => this.PropertyBag.Get(Constants.Properties.Value);
        set => this.PropertyBag.Set(Constants.Properties.Value, value);
    }

    /// <summary>
    /// Creates a new instance of the Environment resource.
    /// </summary>
    /// <param name="name">The unique name for this resource instance.</param>
    /// <param name="configure">Action to configure the resource properties.</param>
    /// <returns>The configured EnvironmentResource instance.</returns>
    public static EnvironmentResource Create(string name, Action<IEnvironmentResource> configure)
    {
        var resource = new EnvironmentResource(name);
        configure(resource);
        return resource;
    }

    /// <summary>
    /// Creates a new instance of the Environment resource.
    /// </summary>
    /// <param name="name">The unique name for this resource instance.</param>
    /// <param name="configure">Action to configure the resource properties.</param>
    /// <param name="resource">The created resource instance.</param>
    /// <returns>The configured EnvironmentResource instance.</returns>
    public static EnvironmentResource Create(string name, Action<IEnvironmentResource> configure, out EnvironmentResource resource)
    {
        resource = new EnvironmentResource(name);
        configure(resource);
        return resource;
    }

    /// <inheritdoc />
    public override Task<List<ValidationFailedException>> Validate()
    {
        var errors = this.ValidationBuilder()
            .ValidateStringNotNullOrEmpty(this.EnvironmentName, nameof(this.EnvironmentName))
            .errors;

        return Task.FromResult(errors);
    }

    /// <inheritdoc />
    public override string ResourceId => Constants.ResourceId;
}
