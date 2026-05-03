namespace UTMO.Text.FileGenerator.Provider.DSC.UtmoStorage.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

/// <summary>
/// Interface for the xReadOnlyDrive DSC resource from the UtmoStorageDsc module.
/// </summary>
public interface IReadOnlyDriveResource : IDscResourceConfig
{
    /// <summary>
    /// Gets or sets the drive letter to enforce a read-only disk state on.
    /// Accepts a single letter (<c>X</c>) or letter-colon (<c>X:</c>) format.
    /// The value is normalized to an uppercase single letter before storage.
    /// </summary>
    string DriveLetter { get; set; }
}
