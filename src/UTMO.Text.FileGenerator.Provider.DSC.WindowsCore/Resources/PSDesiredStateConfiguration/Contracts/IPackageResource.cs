namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

public interface IPackageResource : IDscResourceConfig
{
    string PackageName { get; set; }
    
    string ProductId { get; set; }
    
    string Path { get; set; }
    
    string Arguments { get; set; }
    
    string LogPath { get; set; }
    
    int[] ReturnCode { get; set; }
}
