namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration.Enums;

/// <summary>
/// Interface for the PSDesiredStateConfiguration File resource.
/// </summary>
public interface IFileResource : IDscResourceConfig
{
    /// <summary>
    /// Gets or sets the path where you want to ensure the state of a file or directory.
    /// </summary>
    string DestinationPath { get; set; }

    /// <summary>
    /// Gets or sets the desired state of the attributes for the targeted file or directory.
    /// </summary>
    FileAttribute[]? Attributes { get; set; }

    /// <summary>
    /// Gets or sets the checksum type to use when determining whether two files are the same.
    /// Only valid when SourcePath is specified.
    /// </summary>
    FileChecksum? Checksum { get; set; }

    /// <summary>
    /// Gets or sets the contents to ensure the file has. Only valid when Type is File.
    /// </summary>
    string? Contents { get; set; }

    /// <summary>
    /// Gets or sets the credentials required to access resources.
    /// </summary>
    string? Credential { get; set; }

    /// <summary>
    /// Gets or sets whether to override access operations that would result in an error.
    /// </summary>
    bool? Force { get; set; }

    /// <summary>
    /// Gets or sets whether the resource should monitor for new files added to the source directory
    /// after the initial copy. Only valid when SourcePath is specified.
    /// </summary>
    bool? MatchSource { get; set; }

    /// <summary>
    /// Gets or sets whether to perform the operation recursively.
    /// Only valid when Type is Directory.
    /// </summary>
    bool? Recurse { get; set; }

    /// <summary>
    /// Gets or sets the path from which to copy the file or folder resource.
    /// </summary>
    string? SourcePath { get; set; }

    /// <summary>
    /// Gets or sets the type of resource being configured (File or Directory).
    /// </summary>
    FileType? Type { get; set; }
}