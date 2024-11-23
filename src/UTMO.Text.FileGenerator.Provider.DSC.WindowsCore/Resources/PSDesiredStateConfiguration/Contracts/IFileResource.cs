namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

public interface IFileResource : IDscResourceConfig
{
    string DestinationPath { get; set; }

    string Contents { get; set; }
}