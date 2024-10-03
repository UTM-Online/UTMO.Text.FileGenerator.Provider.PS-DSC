namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.xPSDesiredStateConfiguration;

using UTMO.Text.FileGenerator.Provider.DSC.Constants;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;

// ReSharper disable once InconsistentNaming
public class xDscWebService : xPSDesiredStateConfigurationBase
{
    public xDscWebService(string name) : base(name)
    {
        this.PropertyBag.Add(xPSDesiredStateConfigurationConstants.xDSCWebService.Properties.EndpointName, string.Empty);
        this.PropertyBag.Add(xPSDesiredStateConfigurationConstants.xDSCWebService.Properties.Port, string.Empty);
        this.PropertyBag.Add(xPSDesiredStateConfigurationConstants.xDSCWebService.Properties.PhysicalPath, string.Empty);
        this.PropertyBag.Add(xPSDesiredStateConfigurationConstants.xDSCWebService.Properties.CertificateThumbPrint, string.Empty);
        this.PropertyBag.Add(xPSDesiredStateConfigurationConstants.xDSCWebService.Properties.ModulePath, string.Empty);
        this.PropertyBag.Add(xPSDesiredStateConfigurationConstants.xDSCWebService.Properties.ConfigurationPath, string.Empty);
        this.PropertyBag.Add(xPSDesiredStateConfigurationConstants.xDSCWebService.Properties.State, string.Empty);
        this.PropertyBag.Add(xPSDesiredStateConfigurationConstants.xDSCWebService.Properties.RegistrationKeyPath, string.Empty);
        this.PropertyBag.Add(xPSDesiredStateConfigurationConstants.xDSCWebService.Properties.SqlProvider, string.Empty);
        this.PropertyBag.Add(xPSDesiredStateConfigurationConstants.xDSCWebService.Properties.SqlConnectionString, string.Empty);
        this.PropertyBag.Add(xPSDesiredStateConfigurationConstants.xDSCWebService.Properties.AcceptSelfSignedCertificates, false);
        this.PropertyBag.Add(xPSDesiredStateConfigurationConstants.xDSCWebService.Properties.UseSecurityBestPractices, false);
    }
    
    public string EndpointName
    {
        get => this.PropertyBag[xPSDesiredStateConfigurationConstants.xDSCWebService.Properties.EndpointName].ToString() ?? string.Empty;
        set => this.PropertyBag[xPSDesiredStateConfigurationConstants.xDSCWebService.Properties.EndpointName] = value;
    }
    
    public int Port
    {
        get => (int)this.PropertyBag[xPSDesiredStateConfigurationConstants.xDSCWebService.Properties.Port];
        set => this.PropertyBag[xPSDesiredStateConfigurationConstants.xDSCWebService.Properties.Port] = value;
    }
    
    public string PhysicalPath
    {
        get => this.PropertyBag[xPSDesiredStateConfigurationConstants.xDSCWebService.Properties.PhysicalPath].ToString() ?? string.Empty;
        set => this.PropertyBag[xPSDesiredStateConfigurationConstants.xDSCWebService.Properties.PhysicalPath] = value;
    }
    
    public string CertificateThumbPrint
    {
        get => this.PropertyBag[xPSDesiredStateConfigurationConstants.xDSCWebService.Properties.CertificateThumbPrint].ToString() ?? string.Empty;
        set => this.PropertyBag[xPSDesiredStateConfigurationConstants.xDSCWebService.Properties.CertificateThumbPrint] = value;
    }
    
    public string ModulePath
    {
        get => this.PropertyBag[xPSDesiredStateConfigurationConstants.xDSCWebService.Properties.ModulePath].ToString() ?? string.Empty;
        set => this.PropertyBag[xPSDesiredStateConfigurationConstants.xDSCWebService.Properties.ModulePath] = value;
    }
    
    public string ConfigurationPath
    {
        get => this.PropertyBag[xPSDesiredStateConfigurationConstants.xDSCWebService.Properties.ConfigurationPath].ToString() ?? string.Empty;
        set => this.PropertyBag[xPSDesiredStateConfigurationConstants.xDSCWebService.Properties.ConfigurationPath] = value;
    }
    
    public string State
    {
        get => this.PropertyBag[xPSDesiredStateConfigurationConstants.xDSCWebService.Properties.State].ToString() ?? string.Empty;
        set => this.PropertyBag[xPSDesiredStateConfigurationConstants.xDSCWebService.Properties.State] = value;
    }
    
    public string RegistrationKeyPath
    {
        get => this.PropertyBag[xPSDesiredStateConfigurationConstants.xDSCWebService.Properties.RegistrationKeyPath].ToString() ?? string.Empty;
        set => this.PropertyBag[xPSDesiredStateConfigurationConstants.xDSCWebService.Properties.RegistrationKeyPath] = value;
    }
    
    public string SqlProvider
    {
        get => this.PropertyBag[xPSDesiredStateConfigurationConstants.xDSCWebService.Properties.SqlProvider].ToString() ?? string.Empty;
        set => this.PropertyBag[xPSDesiredStateConfigurationConstants.xDSCWebService.Properties.SqlProvider] = value;
    }
    
    public string SqlConnectionString
    {
        get => this.PropertyBag[xPSDesiredStateConfigurationConstants.xDSCWebService.Properties.SqlConnectionString].ToString() ?? string.Empty;
        set => this.PropertyBag[xPSDesiredStateConfigurationConstants.xDSCWebService.Properties.SqlConnectionString] = value;
    }
    
    public bool AcceptSelfSignedCertificates
    {
        get => (bool)this.PropertyBag[xPSDesiredStateConfigurationConstants.xDSCWebService.Properties.AcceptSelfSignedCertificates];
        set => this.PropertyBag[xPSDesiredStateConfigurationConstants.xDSCWebService.Properties.AcceptSelfSignedCertificates] = value;
    }
    
    public bool UseSecurityBestPractices
    {
        get => (bool)this.PropertyBag[xPSDesiredStateConfigurationConstants.xDSCWebService.Properties.UseSecurityBestPractices];
        set => this.PropertyBag[xPSDesiredStateConfigurationConstants.xDSCWebService.Properties.UseSecurityBestPractices] = value;
    }

    public override string ResourceId => xPSDesiredStateConfigurationConstants.xDSCWebService.ResourceId;
}