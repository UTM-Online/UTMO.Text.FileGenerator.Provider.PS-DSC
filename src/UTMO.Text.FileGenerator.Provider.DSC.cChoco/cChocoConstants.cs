namespace UTMO.Text.FileGenerator.Provider.DSC.cChoco;

public static class cChocoConstants
{
    public static class ChocoPackageInstaller
    {
        public const string ResourceId = "cChocoPackageInstaller";
        
        public static class Parameters
        {
            public const string PackageName = "Name";
            
            public const string PackageSource = "Source";
            
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
    
    public static class ChocoInstaller
    {
        public const string ResourceId = "cChocoInstaller";
        
        public static class Parameters
        {
            public const string InstallDirectory = "InstallDir";
            
            public const string ChocoInstallScriptUrl = "ChocoInstallScriptUrl";
        }
    }
}