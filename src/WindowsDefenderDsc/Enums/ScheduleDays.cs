﻿namespace UTMO.Text.FileGenerator.Provider.DSC.WindowsDefender;

/// <summary>
/// Schedule days for Windows Defender scans and remediation.
/// Values match the official WindowsDefender DSC resource.
/// </summary>
public enum ScheduleDays
{
    Everyday = 0,
    Sunday = 1,
    Monday = 2,
    Tuesday = 3,
    Wednesday = 4,
    Thursday = 5,
    Friday = 6,
    Saturday = 7,
    Never = 8,
}