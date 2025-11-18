namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Constants;

public static class PSDesiredStateConfigurationConstants
{
    public static class File
    {
        public const string ResourceId = "File";
        
        public static class Properties
        {
            public const string DestinationPath = "DestinationPath";
            public const string Contents = "Contents";
            public const string Type = "Type";
        }
    }

    public static class Registry
    {
        public const string ResourceId = "Registry";

        public static class Properties
        {
            public const string Key       = "Key";
            public const string ValueName = "ValueName";
            public const string ValueData = "ValueData";
            public const string ValueType = "ValueType";
        }
    }
    
    public static class WindowsFeature
    {
        public const string ResourceId = "WindowsFeature";

        public static class Properties
        {
            public const string Name = "Name";
            public const string Source = "Source";
        }
    }

    public static class Group
    {
        public const string ResourceId = "Group";
        
        public static class Properties
        {
            public const string GroupName = "GroupName";
            public const string MembersToInclude = "MembersToInclude";
        }
    }
    
    public static class Package
    {
        public const string ResourceId = "Package";
        
        public static class Properties
        {
            public const string Name = "Name";
            public const string ProductId = "ProductId";
            public const string Path = "Path";
            public const string Arguments = "Arguments";
            public const string LogPath = "LogPath";
            public const string ReturnCode = "ReturnCode";
        }
    }
    
    public static class Service
    {
        public const string ResourceId = "Service";
        
        public static class Properties
        {
            public const string Name = "Name";
            public const string State = "State";
            public const string StartupType = "StartupType";
            public const string BuiltInAccount = "BuiltInAccount";
            public const string Credential = "Credential";
            public const string Dependencies = "Dependencies";
            public const string Description = "Description";
            public const string DisplayName = "DisplayName";
            public const string Path = "Path";
        }
    }
}