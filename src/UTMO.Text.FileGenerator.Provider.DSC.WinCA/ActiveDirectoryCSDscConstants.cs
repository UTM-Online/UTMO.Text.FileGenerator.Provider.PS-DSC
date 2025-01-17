namespace UTMO.Text.FileGenerator.Provider.DSC.WinCA;

public static class ActiveDirectoryCSDscConstants
{
    public static class AuthorityInformationAccess
    {
        public const string ResourceId = "AdcsAuthorityInformationAccess";
        
        public static class Parameters
        {
            public const string IsSingleInstance = "IsSingleInstance";
            
            public const string AiaUri = "AiaUri";
            
            public const string OcspUri = "OcspUri";
            
            public const string AllowRestartService = "AllowRestartService";
        }
    }
    
    public static class CertificationAuthority
    {
        public const string ResourceId = "AdcsCertificationAuthority";
        
        public static class Parameters
        {
            public const string IsSingleInstance = "IsSingleInstance";
            
            public const string CAType = "CAType";
            
            
        }
    }
}