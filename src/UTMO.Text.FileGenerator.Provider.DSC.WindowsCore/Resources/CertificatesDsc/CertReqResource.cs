namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.CertificatesDsc;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Attributes;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.BaseDefinitions;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.CertificatesDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.CertificatesDsc.Enums;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Constants.CertificatesDscConstants.CertReq;

/// <summary>
/// Represents a CertReq resource from the CertificatesDsc module.
/// This resource manages certificate requests and certificates.
/// </summary>
public sealed class CertReqResource : CertificatesDscBase, ICertReqResource
{
    private CertReqResource(string name) : base(name)
    {
    }

    /// <summary>
    /// Gets or sets the subject of the certificate request.
    /// </summary>
    public string Subject
    {
        get => this.PropertyBag.Get(Constants.Properties.Subject);
        set => this.PropertyBag.Set(Constants.Properties.Subject, value);
    }

    /// <summary>
    /// Gets or sets the fully qualified domain name of the certificate authority server.
    /// </summary>
    public string CAServerFQDN
    {
        get => this.PropertyBag.Get(Constants.Properties.CAServerFQDN);
        set => this.PropertyBag.Set(Constants.Properties.CAServerFQDN, value);
    }

    /// <summary>
    /// Gets or sets the name of the root certificate authority.
    /// </summary>
    public string CARootName
    {
        get => this.PropertyBag.Get(Constants.Properties.CARootName);
        set => this.PropertyBag.Set(Constants.Properties.CARootName, value);
    }

    /// <summary>
    /// Gets or sets the type of key to use (RSA or ECDH).
    /// </summary>
    [UnquotedEnum]
    public CertificateRequestKeyType? KeyType
    {
        get => this.PropertyBag.Get<CertificateRequestKeyType?>(Constants.Properties.KeyType);
        set => this.PropertyBag.Set(Constants.Properties.KeyType, value);
    }

    /// <summary>
    /// Gets or sets the length of the key in bits (1024, 2048, 4096).
    /// </summary>
    public int? KeyLength
    {
        get => this.PropertyBag.Get<int?>(Constants.Properties.KeyLength);
        set => this.PropertyBag.Set(Constants.Properties.KeyLength, value);
    }

    /// <summary>
    /// Gets or sets whether the certificate private key should be exportable.
    /// </summary>
    public bool? Exportable
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.Exportable);
        set => this.PropertyBag.Set(Constants.Properties.Exportable, value);
    }

    /// <summary>
    /// Gets or sets the name of the key storage provider.
    /// </summary>
    public string ProviderName
    {
        get => this.PropertyBag.Get(Constants.Properties.ProviderName);
        set => this.PropertyBag.Set(Constants.Properties.ProviderName, value);
    }

    /// <summary>
    /// Gets or sets the object identifier (OID) for the certificate.
    /// </summary>
    public string OID
    {
        get => this.PropertyBag.Get(Constants.Properties.OID);
        set => this.PropertyBag.Set(Constants.Properties.OID, value);
    }

    /// <summary>
    /// Gets or sets the key usage extensions for the certificate.
    /// </summary>
    public string[] KeyUsage
    {
        get => this.PropertyBag.Get<string[]>(Constants.Properties.KeyUsage);
        set => this.PropertyBag.Set(Constants.Properties.KeyUsage, value);
    }

    /// <summary>
    /// Gets or sets the enhanced key usage extensions for the certificate.
    /// </summary>
    public string[] EnhancedKeyUsage
    {
        get => this.PropertyBag.Get<string[]>(Constants.Properties.EnhancedKeyUsage);
        set => this.PropertyBag.Set(Constants.Properties.EnhancedKeyUsage, value);
    }

    /// <summary>
    /// Gets or sets the subject alternative names for the certificate.
    /// </summary>
    public string[] SubjectAltName
    {
        get => this.PropertyBag.Get<string[]>(Constants.Properties.SubjectAltName);
        set => this.PropertyBag.Set(Constants.Properties.SubjectAltName, value);
    }

    /// <summary>
    /// Gets or sets the credential to use for the certificate request.
    /// </summary>
    public string Credential
    {
        get => this.PropertyBag.Get(Constants.Properties.Credential);
        set => this.PropertyBag.Set(Constants.Properties.Credential, value);
    }

    /// <summary>
    /// Gets or sets whether the certificate should be automatically renewed.
    /// </summary>
    public bool? AutoRenew
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.AutoRenew);
        set => this.PropertyBag.Set(Constants.Properties.AutoRenew, value);
    }

    /// <summary>
    /// Gets or sets the type of certificate authority (Enterprise or Standalone).
    /// </summary>
    public string CAType
    {
        get => this.PropertyBag.Get(Constants.Properties.CAType);
        set => this.PropertyBag.Set(Constants.Properties.CAType, value);
    }

    /// <summary>
    /// Gets or sets the certificate template to use for the request.
    /// </summary>
    public string CertificateTemplate
    {
        get => this.PropertyBag.Get(Constants.Properties.CertificateTemplate);
        set => this.PropertyBag.Set(Constants.Properties.CertificateTemplate, value);
    }

    /// <summary>
    /// Gets or sets the format for the subject name.
    /// </summary>
    public string SubjectFormat
    {
        get => this.PropertyBag.Get(Constants.Properties.SubjectFormat);
        set => this.PropertyBag.Set(Constants.Properties.SubjectFormat, value);
    }

    /// <summary>
    /// Gets or sets the subject alternative names (alias for SubjectAltName).
    /// </summary>
    public string[] SAN
    {
        get => this.PropertyBag.Get<string[]>(Constants.Properties.SAN);
        set => this.PropertyBag.Set(Constants.Properties.SAN, value);
    }

    /// <summary>
    /// Gets or sets the key algorithm to use.
    /// </summary>
    public string KeyAlgorithm
    {
        get => this.PropertyBag.Get(Constants.Properties.KeyAlgorithm);
        set => this.PropertyBag.Set(Constants.Properties.KeyAlgorithm, value);
    }

    /// <summary>
    /// Gets or sets the hash algorithm to use.
    /// </summary>
    [UnquotedEnum]
    public CertificateRequestHashAlgorithm? HashAlgorithm
    {
        get => this.PropertyBag.Get<CertificateRequestHashAlgorithm?>(Constants.Properties.HashAlgorithm);
        set => this.PropertyBag.Set(Constants.Properties.HashAlgorithm, value);
    }

    /// <summary>
    /// Creates a new instance of the CertReqResource with the specified name.
    /// </summary>
    /// <param name="name">The name of the resource instance.</param>
    /// <param name="configure">The action to configure the resource properties.</param>
    /// <returns>The configured CertReqResource instance.</returns>
    public static CertReqResource Create(string name, Action<ICertReqResource> configure)
    {
        var resource = new CertReqResource(name);
        configure(resource);
        return resource;
    }

    /// <summary>
    /// Creates a new instance of the CertReqResource with the specified name and returns it via an out parameter.
    /// </summary>
    /// <param name="name">The name of the resource instance.</param>
    /// <param name="configure">The action to configure the resource properties.</param>
    /// <param name="resource">The out parameter that receives the created resource.</param>
    /// <returns>The configured CertReqResource instance.</returns>
    public static CertReqResource Create(string name, Action<ICertReqResource> configure, out CertReqResource resource)
    {
        resource = new CertReqResource(name);
        configure(resource);
        return resource;
    }

    /// <summary>
    /// Validates the resource configuration.
    /// </summary>
    /// <returns>A list of validation errors, if any.</returns>
    public override Task<List<ValidationFailedException>> Validate()
    {
        var errors = this.ValidationBuilder()
            .ValidateStringNotNullOrEmpty(this.Subject, nameof(this.Subject))
            .ValidateStringNotNullOrEmpty(this.CAServerFQDN, nameof(this.CAServerFQDN))
            .ValidateStringNotNullOrEmpty(this.CARootName, nameof(this.CARootName))
            .errors;

        return Task.FromResult(errors);
    }

    /// <summary>
    /// Gets the resource identifier.
    /// </summary>
    public override string ResourceId => Constants.ResourceId;
}

