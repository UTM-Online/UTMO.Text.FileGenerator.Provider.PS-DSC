namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.BaseDefinitions;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration.Enums;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Constants.PSDesiredStateConfigurationConstants.File;

/// <summary>
/// DSC resource for managing files and directories.
/// </summary>
public sealed class FileResource : PSDesiredStateConfigurationBase, IFileResource
{
    private FileResource(string name) : base(name)
    {
    }

    /// <inheritdoc />
    public string DestinationPath
    {
        get => this.PropertyBag.Get(Constants.Properties.DestinationPath);
        set => this.PropertyBag.Set(Constants.Properties.DestinationPath, value);
    }

    /// <inheritdoc />
    public FileAttribute[]? Attributes
    {
        get => this.PropertyBag.Get<FileAttribute[]?>(Constants.Properties.Attributes);
        set => this.PropertyBag.Set(Constants.Properties.Attributes, value);
    }

    /// <inheritdoc />
    public FileChecksum? Checksum
    {
        get => this.PropertyBag.Get<FileChecksum?>(Constants.Properties.Checksum);
        set => this.PropertyBag.Set(Constants.Properties.Checksum, value);
    }

    /// <inheritdoc />
    public string? Contents
    {
        get => this.PropertyBag.Get(Constants.Properties.Contents);
        set => this.PropertyBag.Set(Constants.Properties.Contents, value);
    }

    /// <inheritdoc />
    public string? Credential
    {
        get => this.PropertyBag.Get(Constants.Properties.Credential);
        set => this.PropertyBag.Set(Constants.Properties.Credential, value);
    }

    /// <inheritdoc />
    public bool? Force
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.Force);
        set => this.PropertyBag.Set(Constants.Properties.Force, value);
    }

    /// <inheritdoc />
    public bool? MatchSource
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.MatchSource);
        set => this.PropertyBag.Set(Constants.Properties.MatchSource, value);
    }

    /// <inheritdoc />
    public bool? Recurse
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.Recurse);
        set => this.PropertyBag.Set(Constants.Properties.Recurse, value);
    }

    /// <inheritdoc />
    public string? SourcePath
    {
        get => this.PropertyBag.Get(Constants.Properties.SourcePath);
        set => this.PropertyBag.Set(Constants.Properties.SourcePath, value);
    }

    /// <inheritdoc />
    public FileType? Type
    {
        get => this.PropertyBag.Get<FileType?>(Constants.Properties.Type);
        set => this.PropertyBag.Set(Constants.Properties.Type, value);
    }

    /// <summary>
    /// Creates a new instance of the File resource.
    /// </summary>
    /// <param name="name">The unique name for this resource instance.</param>
    /// <param name="configure">Action to configure the resource properties.</param>
    /// <returns>The configured FileResource instance.</returns>
    public static FileResource Create(string name, Action<IFileResource> configure)
    {
        var resource = new FileResource(name);
        configure(resource);
        return resource;
    }

    /// <summary>
    /// Creates a new instance of the File resource.
    /// </summary>
    /// <param name="name">The unique name for this resource instance.</param>
    /// <param name="configure">Action to configure the resource properties.</param>
    /// <param name="resource">The created resource instance.</param>
    /// <returns>The configured FileResource instance.</returns>
    public static FileResource Create(string name, Action<IFileResource> configure, out FileResource resource)
    {
        resource = new FileResource(name);
        configure(resource);
        return resource;
    }

    /// <inheritdoc />
    public override Task<List<ValidationFailedException>> Validate()
    {
        var errors = this.ValidationBuilder()
            .ValidateStringNotNullOrEmpty(this.DestinationPath, nameof(this.DestinationPath))
            .errors;

        return Task.FromResult(errors);
    }

    /// <inheritdoc />
    public override string ResourceId => Constants.ResourceId;
}