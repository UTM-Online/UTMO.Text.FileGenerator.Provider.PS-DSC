namespace UTMO.Text.FileGenerator.Provider.DSC.ChocoModule.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

/// <summary>
/// Contract for the ChocolateyPackage DSC resource.
/// Installs, upgrades, or removes Chocolatey packages.
/// </summary>
public interface IChocolateyPackageResource : IDscResourceConfig
{
    string PackageName { get; set; }

    string? Version { get; set; }

    string? Source { get; set; }

    bool UpdateOnly { get; set; }

    /// <summary>
    /// Adds a key/value pair to the <c>ChocolateyOptions</c> hashtable that is passed to the DSC resource.
    /// The hashtable is only emitted when at least one option has been added.
    /// </summary>
    /// <param name="key">The option name (e.g. <c>source</c>).</param>
    /// <param name="value">The option value.</param>
    /// <returns>The current resource instance to allow method chaining.</returns>
    IChocolateyPackageResource AddOption(string key, string value);
}


