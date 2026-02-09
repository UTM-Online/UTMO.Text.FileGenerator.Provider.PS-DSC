namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration.Enums;

/// <summary>
/// Specifies the checksum type to use when determining whether two files are the same.
/// </summary>
public enum FileChecksum
{
    /// <summary>
    /// Uses the creation date.
    /// </summary>
    CreatedDate,
    
    /// <summary>
    /// Uses the modified date.
    /// </summary>
    ModifiedDate,
    
    /// <summary>
    /// Uses SHA-1 hash.
    /// </summary>
    Sha1,
    
    /// <summary>
    /// Uses SHA-256 hash.
    /// </summary>
    Sha256,
    
    /// <summary>
    /// Uses SHA-512 hash.
    /// </summary>
    Sha512
}


