namespace UTMO.Text.FileGenerator.Provider.DSC.Constants;

public static class cChocoConstants
{
    public static class ChocoPackageInstaller
    {
        public const string ResourceId = "cChocoPackageInstaller";
        
        public static class Parameters
        {
            public const string PackageName = "PackageName";
            
            public const string PackageSource = "PackageSource";
            
            public const string AutoUpgrade = "AutoUpgrade";
            
            public const string Params = "Params"; 
        }
    }
    
    public static class ChocoPackageSource
    {
        public const string ResourceId = "cChocoSource";
        
        public static class Parameters
        {
            public const string Name = "Name";
            
            public const string Source = "Source";
        }
    }
}