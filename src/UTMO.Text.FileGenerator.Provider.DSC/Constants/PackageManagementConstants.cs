namespace UTMO.Text.FileGenerator.Provider.DSC.Constants;

public static class PackageManagementConstants
{
    public static class PackageManagement
    {
        public const string ResourceId = "PackageManagement";
        
        public static class Properties
        {
            public const string Name = "Name";
            public const string ProviderName = "ProviderName";
            public const string Source = "Source";
        }
    }
    
    public static class PackageManagementSource
    {
        public const string ResourceId = "PackageManagementSource";
        
        public static class Properties
        {
            public const string Name = "Name";
            public const string ProviderName = "ProviderName";
            public const string SourceLocation = "SourceLocation";
            public const string InstallationPolicy = "InstallationPolicy";
        }
    }
}