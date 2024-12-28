namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.xPSDesiredStateConfiguration;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.xPSDesiredStateConfiguration.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.xPSDesiredStateConfigurationConstants.xDSCWebService;

// ReSharper disable once InconsistentNaming
public class xDscWebServiceResource : xPSDesiredStateConfigurationBase, IxDscWebServiceResource
{
    private xDscWebServiceResource(string name) : base(name)
    {
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
    
    public bool SqlProvider
    {
        get => this.PropertyBag.Get<bool>(Constants.Properties.SqlProvider);
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
    
    public string AppPoolName
    {
        get => this.PropertyBag.Get(Constants.Properties.AppPoolName);
        set => this.PropertyBag.Set(Constants.Properties.AppPoolName, value);
    }
    
    public static xDscWebServiceResource Create(string name, Action<IxDscWebServiceResource> configure)
    {
        var resource = new xDscWebServiceResource(name);
        configure(resource);
        return resource;
    }
    
    public static xDscWebServiceResource Create(string name, Action<IxDscWebServiceResource> configure, out xDscWebServiceResource resource)
    {
        resource = new xDscWebServiceResource(name);
        configure(resource);
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var validations = this.ValidationBuilder()
                  .ValidateStringNotNullOrEmpty(this.EndpointName, nameof(this.EndpointName));
        
        return Task.FromResult(validations.errors);
    }

    public override string ResourceId => Constants.ResourceId;
}