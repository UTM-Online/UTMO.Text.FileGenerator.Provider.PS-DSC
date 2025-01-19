namespace UTMO.Text.FileGenerator.Provider.DSC.Constants;

using System.Diagnostics.CodeAnalysis;

[SuppressMessage("ReSharper", "InconsistentNaming",  Justification = "This is a DSC module, class confirms to module naming conventions")]
public static class xWebAdministrationConstants
{
    public static class xWebAppPool
    {
        public const string ResourceId = "xWebAppPool";

        public static class Properties
        {
            public const string Name = "Name";
            
            public const string IdentityType = "IdentityType";
        }
    }

    public static class xWebConfigProperty
    {
        public const string ResourceId = "xWebConfigProperty";
        
        public static class Properties
        {
            public const string WebsitePath = "WebsitePath";
            
            public const string Filter = "Filter";
            
            public const string PropertyName = "PropertyName";
            
            public const string Value = "Value";
        }
    }
}