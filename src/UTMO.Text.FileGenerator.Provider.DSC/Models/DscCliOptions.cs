namespace UTMO.Text.FileGenerator.Provider.DSC.Models;
using CommandLine;
using UTMO.Text.FileGenerator.Models;

// ReSharper disable once ClassNeverInstantiated.Global
public class DscCliOptions : GeneratorCliOptions
{
    [Option('k', "registrationKey", Required = true, HelpText = "The registration key for the pull server.")]
    public string RegistrationKey { get; set; } = null!;
    
    [Option('c', "IsCiCd", Required = false, HelpText = "Indicates if the DSC configuration is being generated for a CI/CD pipeline.", Default = false)]
    public bool IsCiCd { get; set; }
    
    [Option('r', "restoreRequiredModules", Required = false, HelpText = "Indicates whether to restore required DSC modules before generating MOF files.", Default = false)]
    public bool RestoreRequiredModules { get; set; }
    
    [Option('p', "repackageRequiredModules", Required = false, HelpText = "Indicates whether to repackage required DSC modules after restoring them.", Default = false)]
    public bool RepackageRequiredModules  { get; set; }
}