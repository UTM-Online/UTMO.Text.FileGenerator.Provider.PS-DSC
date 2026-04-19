namespace UTMO.Text.FileGenerator.Provider.DSC.ChocoModule;

public static class ChocoModuleConstants
{
    public static class ChocolateySoftware
    {
        public const string ResourceId = "ChocolateySoftware";

        public static class Parameters
        {
            public const string Ensure = "Ensure";

            public const string InstallationDirectory = "InstallationDirectory";

            public const string ChocolateyPackageUrl = "ChocolateyPackageUrl";

            public const string PackageFeedUrl = "PackageFeedUrl";

            public const string Version = "Version";

            public const string ChocoTempDir = "ChocoTempDir";

            public const string ProxyLocation = "ProxyLocation";

            public const string IgnoreProxy = "IgnoreProxy";
        }
    }

    public static class ChocolateyPackage
    {
        public const string ResourceId = "ChocolateyPackage";

        public static class Parameters
        {
            public const string Name = "Name";

            public const string Version = "Version";

            public const string Source = "Source";

            public const string UpdateOnly = "UpdateOnly";
        }
    }

    public static class ChocolateySource
    {
        public const string ResourceId = "ChocolateySource";

        public static class Parameters
        {
            public const string Name = "Name";

            public const string Source = "Source";

            public const string Disabled = "Disabled";

            public const string ByPassProxy = "ByPassProxy";

            public const string SelfService = "SelfService";

            public const string Priority = "Priority";

            public const string Username = "Username";

            public const string Password = "Password";
        }
    }

    public static class ChocolateyFeature
    {
        public const string ResourceId = "ChocolateyFeature";

        public static class Parameters
        {
            public const string Name = "Name";
        }
    }

    public static class ChocolateySetting
    {
        public const string ResourceId = "ChocolateySetting";

        public static class Parameters
        {
            public const string Name = "Name";

            public const string Value = "Value";
        }
    }

    public static class ChocolateyPin
    {
        public const string ResourceId = "ChocolateyPin";

        public static class Parameters
        {
            public const string Name = "Name";

            public const string Version = "Version";
        }
    }
}

