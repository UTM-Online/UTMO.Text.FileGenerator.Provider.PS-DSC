namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration.Enums;

using System;

/// <summary>
/// Specifies the desired state of the attributes for the targeted file or directory.
/// </summary>
[Flags]
public enum FileAttribute
{
    /// <summary>
    /// The file is read-only.
    /// </summary>
    ReadOnly = 1,
    
    /// <summary>
    /// The file is hidden.
    /// </summary>
    Hidden = 2,
    
    /// <summary>
    /// The file is a system file.
    /// </summary>
    System = 4,
    
    /// <summary>
    /// The file is archived.
    /// </summary>
    Archive = 8
}

