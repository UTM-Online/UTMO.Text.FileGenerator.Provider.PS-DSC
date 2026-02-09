namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Constants;

public static class CertificatesDscConstants
{
    public static class CertReq
    {
        public const string ResourceId = "CertReq";

        public static class Properties
        {
            public const string Subject = "Subject";
            public const string CAServerFQDN = "CAServerFQDN";
            public const string CARootName = "CARootName";
            public const string KeyType = "KeyType";
            public const string KeyLength = "KeyLength";
            public const string Exportable = "Exportable";
            public const string ProviderName = "ProviderName";
            public const string OID = "OID";
            public const string KeyUsage = "KeyUsage";
            public const string EnhancedKeyUsage = "EnhancedKeyUsage";
            public const string SubjectAltName = "SubjectAltName";
            public const string Credential = "Credential";
            public const string AutoRenew = "AutoRenew";
            public const string CAType = "CAType";
            public const string CertificateTemplate = "CertificateTemplate";
            public const string SubjectFormat = "SubjectFormat";
            public const string SAN = "SAN";
            public const string KeyAlgorithm = "KeyAlgorithm";
            public const string HashAlgorithm = "HashAlgorithm";
        }
    }
}

