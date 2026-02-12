using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.CertificateDsc.Enums;

namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.CertificateDsc.Contracts;

public interface ICertReqResource : IDscResourceConfig
{
    /// <summary>
    /// The subject of the certificate request.
    /// </summary>
    string Subject { get; set; }

    /// <summary>
    /// The fully qualified domain name of the certificate authority server.
    /// </summary>
    string CAServerFQDN { get; set; }

    /// <summary>
    /// The name of the root certificate authority.
    /// </summary>
    string CARootName { get; set; }

    /// <summary>
    /// The type of key to use (RSA or ECDH).
    /// </summary>
    CertificateRequestKeyType? KeyType { get; set; }

    /// <summary>
    /// The length of the key in bits (1024, 2048, 4096).
    /// </summary>
    int? KeyLength { get; set; }

    /// <summary>
    /// Whether the certificate private key should be exportable.
    /// </summary>
    bool? Exportable { get; set; }

    /// <summary>
    /// The name of the key storage provider.
    /// </summary>
    string ProviderName { get; set; }

    /// <summary>
    /// The object identifier (OID) for the certificate.
    /// </summary>
    string OID { get; set; }

    /// <summary>
    /// The key usage extensions for the certificate.
    /// </summary>
    string[] KeyUsage { get; set; }

    /// <summary>
    /// The enhanced key usage extensions for the certificate.
    /// </summary>
    string[] EnhancedKeyUsage { get; set; }

    /// <summary>
    /// The subject alternative names for the certificate.
    /// </summary>
    string[] SubjectAltName { get; set; }

    /// <summary>
    /// The credential to use for the certificate request.
    /// </summary>
    string Credential { get; set; }

    /// <summary>
    /// Whether the certificate should be automatically renewed.
    /// </summary>
    bool? AutoRenew { get; set; }

    /// <summary>
    /// The type of certificate authority (Enterprise or Standalone).
    /// </summary>
    string CAType { get; set; }

    /// <summary>
    /// The certificate template to use for the request.
    /// </summary>
    string CertificateTemplate { get; set; }

    /// <summary>
    /// The format for the subject name.
    /// </summary>
    string SubjectFormat { get; set; }

    /// <summary>
    /// The subject alternative names (alias for SubjectAltName).
    /// </summary>
    string[] SAN { get; set; }

    /// <summary>
    /// The key algorithm to use.
    /// </summary>
    string KeyAlgorithm { get; set; }

    /// <summary>
    /// The hash algorithm to use.
    /// </summary>
    CertificateRequestHashAlgorithm? HashAlgorithm { get; set; }
}

