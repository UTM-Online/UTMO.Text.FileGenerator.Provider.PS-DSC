namespace UTMO.Text.FileGenerator.Provider.DSC.Constants;

using System.Diagnostics.CodeAnalysis;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public static class xStorageConstants
{
    public static class xDisk
    {
        public const string ResourceId = "xDisk";
        
        public static class Properties
        {
            public const string DiskId = "DiskId";
            
            public const string DriveLetter = "DriveLetter";
            
            public const string FSFormat = "FSFormat";
        }
    }
}