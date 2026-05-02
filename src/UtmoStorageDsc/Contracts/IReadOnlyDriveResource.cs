namespace UTMO.Text.FileGenerator.Provider.DSC.UtmoStorage.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

/// <summary>
/// Interface for the xReadOnlyDrive DSC resource from the UtmoStorageDsc module.
/// </summary>
public interface IReadOnlyDriveResource : IDscResourceConfig
{
    /// <summary>
    /// Gets or sets the drive letter to enforce a read-only disk state on.
    /// Supports <c>X</c> or <c>X:</c> formats. Drive letters are normalized
    /// internally to uppercase single-letter format.
    /// </summary>
    string DriveLetter { get; set; }
}
