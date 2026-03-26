namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

/// <summary>
/// Interface for the PSDesiredStateConfiguration Environment resource.
/// </summary>
public interface IEnvironmentResource : IDscResourceConfig
{
    /// <summary>
    /// Gets or sets the name of the environment variable for which you want to ensure a specific state.
    /// </summary>
    string EnvironmentName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the environment variable is a path variable.
    /// When <see langword="true"/>, the <see cref="Value"/> is appended to the existing value rather
    /// than replacing it.
    /// </summary>
    bool? Path { get; set; }

    /// <summary>
    /// Gets or sets the value to assign to the environment variable.
    /// </summary>
    string? Value { get; set; }
}
