namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.xPSDesiredStateConfiguration;

using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.xPSDesiredStateConfigurationConstants.xDSCWebService;

// ReSharper disable once InconsistentNaming
public class xDscWebServiceResource : xPSDesiredStateConfigurationBase
{
    public xDscWebServiceResource(string name) : base(name)
    {
        this.PropertyBag.Init(Constants.Properties.EndpointName);
        this.PropertyBag.Init<int>(Constants.Properties.Port);
        this.PropertyBag.Init(Constants.Properties.PhysicalPath);
        this.PropertyBag.Init(Constants.Properties.CertificateThumbPrint);
        this.PropertyBag.Init(Constants.Properties.ModulePath);
        this.PropertyBag.Init(Constants.Properties.ConfigurationPath);
        this.PropertyBag.Init(Constants.Properties.State);
        this.PropertyBag.Init(Constants.Properties.RegistrationKeyPath);
        this.PropertyBag.Init(Constants.Properties.SqlProvider);
        this.PropertyBag.Init(Constants.Properties.SqlConnectionString);
        this.PropertyBag.Init<bool>(Constants.Properties.AcceptSelfSignedCertificates);
        this.PropertyBag.Init<bool>(Constants.Properties.UseSecurityBestPractices);
    }
    
    public string EndpointName
    {
        get => this.PropertyBag.Get(Constants.Properties.EndpointName);
        set => this.PropertyBag.Set(Constants.Properties.EndpointName, value);
    }
    
    public int Port
    {
        get => this.PropertyBag.Get<int>(Constants.Properties.Port);
        set => this.PropertyBag.Set(Constants.Properties.Port, value);
    }
    
    public string PhysicalPath
    {
        get => this.PropertyBag.Get(Constants.Properties.PhysicalPath);
        set => this.PropertyBag.Set(Constants.Properties.PhysicalPath, value);
    }
    
    public string CertificateThumbPrint
    {
        get => this.PropertyBag.Get(Constants.Properties.CertificateThumbPrint);
        set => this.PropertyBag.Set(Constants.Properties.CertificateThumbPrint, value);
    }
    
    public string ModulePath
    {
        get => this.PropertyBag.Get(Constants.Properties.ModulePath);
        set => this.PropertyBag.Set(Constants.Properties.ModulePath, value);
    }
    
    public string ConfigurationPath
    {
        get => this.PropertyBag.Get(Constants.Properties.ConfigurationPath);
        set => this.PropertyBag.Set(Constants.Properties.ConfigurationPath, value);
    }
    
    public string State
    {
        get => this.PropertyBag.Get(Constants.Properties.State);
        set => this.PropertyBag.Set(Constants.Properties.State, value);
    }
    
    public string RegistrationKeyPath
    {
        get => this.PropertyBag.Get(Constants.Properties.RegistrationKeyPath);
        set => this.PropertyBag.Set(Constants.Properties.RegistrationKeyPath, value);
    }
    
    public string SqlProvider
    {
        get => this.PropertyBag.Get(Constants.Properties.SqlProvider);
        set => this.PropertyBag.Set(Constants.Properties.SqlProvider, value);
    }
    
    public string SqlConnectionString
    {
        get => this.PropertyBag.Get(Constants.Properties.SqlConnectionString);
        set => this.PropertyBag.Set(Constants.Properties.SqlConnectionString, value);
    }
    
    public bool AcceptSelfSignedCertificates
    {
        get => this.PropertyBag.Get<bool>(Constants.Properties.AcceptSelfSignedCertificates);
        set => this.PropertyBag.Set(Constants.Properties.AcceptSelfSignedCertificates, value);
    }
    
    public bool UseSecurityBestPractices
    {
        get => this.PropertyBag.Get<bool>(Constants.Properties.UseSecurityBestPractices);
        set => this.PropertyBag.Set(Constants.Properties.UseSecurityBestPractices, value);
    }

    public override string ResourceId
    {
        get => Constants.ResourceId;
    }
}