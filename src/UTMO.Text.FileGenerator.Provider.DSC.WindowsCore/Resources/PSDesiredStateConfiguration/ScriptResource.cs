namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Attributes;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.BaseDefinitions;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Constants.PSDesiredStateConfigurationConstants.Script;

/// <summary>
/// Represents the Script DSC resource from the PSDesiredStateConfiguration module.
/// This resource allows you to run PowerShell scripts to get, test, and set the desired state.
/// </summary>
public sealed class ScriptResource : PSDesiredStateConfigurationBase, IScriptResource
{
    private ScriptResource(string name) : base(name)
    {
    }

    /// <summary>
    /// Gets or sets a script block that returns the current state of the resource.
    /// The script must return a hashtable with at least a Result key.
    /// </summary>
    [ScriptBlock]
    public string GetScript
    {
        get => this.PropertyBag.Get(Constants.Properties.GetScript);
        set => this.PropertyBag.Set(Constants.Properties.GetScript, value);
    }
    
    /// <summary>
    /// Gets or sets a script block that sets the resource to the desired state.
    /// </summary>
    [ScriptBlock]
    public string SetScript
    {
        get => this.PropertyBag.Get(Constants.Properties.SetScript);
        set => this.PropertyBag.Set(Constants.Properties.SetScript, value);
    }
    
    /// <summary>
    /// Gets or sets a script block that tests if the resource is in the desired state.
    /// The script must return $true or $false.
    /// </summary>
    [ScriptBlock]
    public string TestScript
    {
        get => this.PropertyBag.Get(Constants.Properties.TestScript);
        set => this.PropertyBag.Set(Constants.Properties.TestScript, value);
    }
    
    /// <summary>
    /// Gets or sets the credential to use for running the scripts.
    /// </summary>
    public string Credential
    {
        get => this.PropertyBag.Get(Constants.Properties.Credential);
        set => this.PropertyBag.Set(Constants.Properties.Credential, value);
    }

    /// <summary>
    /// Creates a new Script resource with the specified name and configuration.
    /// </summary>
    /// <param name="name">The unique name for this resource instance.</param>
    /// <param name="configure">An action to configure the resource properties.</param>
    /// <returns>A configured ScriptResource instance.</returns>
    public static ScriptResource Create(string name, Action<IScriptResource> configure)
    {
        var resource = new ScriptResource(name);
        configure(resource);
        return resource;
    }
    
    /// <summary>
    /// Creates a new Script resource with the specified name and configuration, returning the resource via an out parameter.
    /// </summary>
    /// <param name="name">The unique name for this resource instance.</param>
    /// <param name="configure">An action to configure the resource properties.</param>
    /// <param name="resource">The created ScriptResource instance.</param>
    /// <returns>The configured ScriptResource instance.</returns>
    public static ScriptResource Create(string name, Action<IScriptResource> configure, out ScriptResource resource)
    {
        resource = new ScriptResource(name);
        configure(resource);
        return resource;
    }

    /// <inheritdoc />
    public override Task<List<ValidationFailedException>> Validate()
    {
        var errors = this.ValidationBuilder()
            .ValidateStringNotNullOrEmpty(this.GetScript, nameof(this.GetScript))
            .ValidateStringNotNullOrEmpty(this.SetScript, nameof(this.SetScript))
            .ValidateStringNotNullOrEmpty(this.TestScript, nameof(this.TestScript))
            .errors;

        return Task.FromResult(errors);
    }

    /// <inheritdoc />
    public override string ResourceId => Constants.ResourceId;
}

