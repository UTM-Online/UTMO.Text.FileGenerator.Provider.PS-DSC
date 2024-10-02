namespace UTMO.Text.FileGenerator.Provider.DSC.Constants;

using System.Diagnostics.CodeAnalysis;

[SuppressMessage("ReSharper", "InconsistentNaming", Justification = "This is a DSC module, class confirms to module naming conventions")]
public static class xPSDesiredStateConfigurationConstants
{
    public static class xDSCWebService
    {
        public const string ResourceId = "xDSCWebService";
        
        public static class Properties
        {
            public const string EndpointName = "EndpointName";
            
            public const string Port = "Port";
            
            public const string PhysicalPath = "PhysicalPath";
            
            public const string CertificateThumbPrint = "CertificateThumbPrint";
            
            public const string ModulePath = "ModulePath";
            
            public const string ConfigurationPath = "ConfigurationPath";
            
            public const string State = "State";
            
            public const string RegistrationKeyPath = "RegistrationKeyPath";
            
            public const string SqlProvider = "SqlProvider";
            
            public const string SqlConnectionString = "SqlConnectionString";
            
            public const string AcceptSelfSignedCertificates = "AcceptSelfSignedCertificates";
            
            public const string UseSecurityBestPractices = "UseSecurityBestPractices";
        }
    }
}