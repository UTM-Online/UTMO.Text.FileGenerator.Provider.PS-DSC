namespace UTMO.Text.FileGenerator.Provider.DSC.UtmoStorage.Resources;

using UTMO.Text.FileGenerator.Abstract;
using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.UtmoStorage.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UtmoStorageDscConstants.XReadOnlyDrive;

/// <summary>
/// Represents the xReadOnlyDrive DSC resource from the UtmoStorageDsc module.
/// This resource enforces a read-only disk state for the disk that owns a given drive letter.
/// Use <see cref="UTMO.Text.FileGenerator.Provider.DSC.Abstract.Enums.DscEnsure.Present"/> to enforce
/// read-only and <see cref="UTMO.Text.FileGenerator.Provider.DSC.Abstract.Enums.DscEnsure.Absent"/> to
/// enforce writable.
/// </summary>
public sealed class ReadOnlyDriveResource : UtmoStorageDscBase, IReadOnlyDriveResource
{
    private ReadOnlyDriveResource(string name) : base(name)
    {
    }

    /// <inheritdoc />
    public string DriveLetter
    {
        get => this.PropertyBag.Get(Constants.Properties.DriveLetter);
        set => this.PropertyBag.Set(Constants.Properties.DriveLetter, NormalizeDriveLetter(value));
    }

    private static string NormalizeDriveLetter(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return value;

        return value.Trim().TrimEnd(':').ToUpperInvariant();
    }

    /// <summary>
    /// Creates a new instance of the xReadOnlyDrive resource.
    /// </summary>
    /// <param name="name">The unique name for this resource instance.</param>
    /// <param name="configure">Action to configure the resource properties.</param>
    /// <returns>The configured ReadOnlyDriveResource instance.</returns>
    public static ReadOnlyDriveResource Create(string name, Action<IReadOnlyDriveResource> configure)
    {
        var resource = new ReadOnlyDriveResource(name);
        configure(resource);
        return resource;
    }

    /// <summary>
    /// Creates a new instance of the xReadOnlyDrive resource, returning it via an out parameter.
    /// </summary>
    /// <param name="name">The unique name for this resource instance.</param>
    /// <param name="configure">Action to configure the resource properties.</param>
    /// <param name="resource">The created ReadOnlyDriveResource instance.</param>
    /// <returns>The configured ReadOnlyDriveResource instance.</returns>
    public static ReadOnlyDriveResource Create(string name, Action<IReadOnlyDriveResource> configure, out ReadOnlyDriveResource resource)
    {
        resource = new ReadOnlyDriveResource(name);
        configure(resource);
        return resource;
    }

    /// <inheritdoc />
    public override Task<List<ValidationFailedException>> Validate()
    {
        var errors = this.ValidationBuilder()
            .ValidateStringNotNullOrEmpty(this.DriveLetter, nameof(this.DriveLetter))
            .errors;

        if (!string.IsNullOrEmpty(this.DriveLetter) &&
            (this.DriveLetter.Length != 1 || !char.IsAsciiLetterUpper(this.DriveLetter[0])))
        {
            errors.Add(new ValidationFailedException(
                nameof(this.DriveLetter),
                nameof(ReadOnlyDriveResource),
                ValidationFailureType.InvalidConfiguration,
                $"{nameof(this.DriveLetter)} must be a single letter A-Z."));
        }

        return Task.FromResult(errors);
    }

    /// <inheritdoc />
    public override string ResourceId => Constants.ResourceId;

    /// <inheritdoc />
    public override bool HasEnsure => true;
}
