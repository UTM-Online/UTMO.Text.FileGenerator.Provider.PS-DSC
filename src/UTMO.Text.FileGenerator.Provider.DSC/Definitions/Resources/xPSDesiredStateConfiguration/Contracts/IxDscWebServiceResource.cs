namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.xPSDesiredStateConfiguration.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

public interface IxDscWebServiceResource : IDscResourceConfig
{
    string EndpointName { get; set; }

    int Port { get; set; }

    string PhysicalPath { get; set; }

    string CertificateThumbPrint { get; set; }

    string ModulePath { get; set; }

    string ConfigurationPath { get; set; }

    string State { get; set; }

    string RegistrationKeyPath { get; set; }

    bool SqlProvider { get; set; }

    string SqlConnectionString { get; set; }

    bool AcceptSelfSignedCertificates { get; set; }

    bool UseSecurityBestPractices { get; set; }

    string AppPoolName { get; set; }
}