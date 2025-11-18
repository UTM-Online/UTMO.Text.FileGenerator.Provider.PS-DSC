namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration.Enums;

public interface IServiceResource : IDscResourceConfig
{
    string ServiceName { get; set; }
    
    ServiceState? State { get; set; }
    
    ServiceStartupType? StartupType { get; set; }
    
    string BuiltInAccount { get; set; }
    
    string Credential { get; set; }
    
    string[] Dependencies { get; set; }
    
    string ServiceDescription { get; set; }
    
    string DisplayName { get; set; }
    
    string Path { get; set; }
}

