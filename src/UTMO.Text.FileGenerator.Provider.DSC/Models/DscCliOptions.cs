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
}