namespace UTMO.Text.FileGenerator.Provider.DSC.Constants;

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
}