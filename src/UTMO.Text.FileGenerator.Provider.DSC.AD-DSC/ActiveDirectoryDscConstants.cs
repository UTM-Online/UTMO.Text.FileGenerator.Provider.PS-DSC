namespace UTMO.Text.FileGenerator.Provider.DSC.AD_DSC;

public static class ActiveDirectoryDscConstants
{
    public static class ADManagedServiceAccount
    {
        public const string ResourceId = "ADManagedServiceAccount";
        
        public static class Parameters
        {
            public const string ServiceAccountName = "ServiceAccountName";
            
            public const string AccountType = "AccountType";
            
            public const string CommonName = "CommonName";
            
            public const string Description = "Description";
            
            public const string DisplayName = "DisplayName";
            
            public const string KerberosEncryptionType = "KerberosEncryptionType";
            
            public const string TrustedForDelegation = "TrustedForDelegation";
            
            public const string ManagedPasswordPrincipals = "ManagedPasswordPrincipals";
            
            public const string MembershipAttribute = "MembershipAttribute";
            
            public const string Path = "Path";
        }
    }
}