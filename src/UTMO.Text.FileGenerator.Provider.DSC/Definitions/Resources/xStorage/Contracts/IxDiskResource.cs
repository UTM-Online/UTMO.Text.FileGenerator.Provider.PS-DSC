namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.xStorage.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

public interface IxDiskResource : IDscResourceConfig
{
    char DriveLetter { get; set; }

    int DiskId { get; set; }

    string FileSystemFormat { get; set; }
}