namespace UTMO.Text.FileGenerator.Provider.DSC.Constants;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Constants for WebAdministrationDsc module resources.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming", Justification = "This is a DSC module, class conforms to module naming conventions")]
public static class WebAdministrationDscConstants
{
    public static class IisFeatureDelegation
    {
        public const string ResourceId = "IisFeatureDelegation";

        public static class Properties
        {
            public const string Path = "Path";
            public const string Filter = "Filter";
            public const string OverrideMode = "OverrideMode";
        }
    }

    public static class IisLogging
    {
        public const string ResourceId = "IisLogging";

        public static class Properties
        {
            public const string LogPath = "LogPath";
            public const string LogFlags = "LogFlags";
            public const string LogPeriod = "LogPeriod";
            public const string LogTruncateSize = "LogTruncateSize";
            public const string LoglocalTimeRollover = "LoglocalTimeRollover";
            public const string LogFormat = "LogFormat";
            public const string LogTargetW3C = "LogTargetW3C";
            public const string LogCustomFields = "LogCustomFields";
        }
    }

    public static class IisMimeTypeMapping
    {
        public const string ResourceId = "IisMimeTypeMapping";

        public static class Properties
        {
            public const string ConfigurationPath = "ConfigurationPath";
            public const string Extension = "Extension";
            public const string MimeType = "MimeType";
        }
    }

    public static class IisModule
    {
        public const string ResourceId = "IisModule";

        public static class Properties
        {
            public const string Path = "Path";
            public const string Name = "Name";
            public const string RequestPath = "RequestPath";
            public const string Verb = "Verb";
            public const string SiteName = "SiteName";
            public const string ModuleType = "ModuleType";
        }
    }

    public static class SslSettings
    {
        public const string ResourceId = "SslSettings";

        public static class Properties
        {
            public const string Name = "Name";
            public const string Bindings = "Bindings";
        }
    }

    public static class WebAppPool
    {
        public const string ResourceId = "WebAppPool";

        public static class Properties
        {
            public const string Name = "Name";
            public const string State = "State";
            public const string AutoStart = "autoStart";
            public const string CLRConfigFile = "CLRConfigFile";
            public const string Enable32BitAppOnWin64 = "enable32BitAppOnWin64";
            public const string EnableConfigurationOverride = "enableConfigurationOverride";
            public const string ManagedPipelineMode = "managedPipelineMode";
            public const string ManagedRuntimeLoader = "managedRuntimeLoader";
            public const string ManagedRuntimeVersion = "managedRuntimeVersion";
            public const string PassAnonymousToken = "passAnonymousToken";
            public const string StartMode = "startMode";
            public const string QueueLength = "queueLength";
            public const string CpuAction = "cpuAction";
            public const string CpuLimit = "cpuLimit";
            public const string CpuResetInterval = "cpuResetInterval";
            public const string CpuSmpAffinitized = "cpuSmpAffinitized";
            public const string CpuSmpProcessorAffinityMask = "cpuSmpProcessorAffinityMask";
            public const string CpuSmpProcessorAffinityMask2 = "cpuSmpProcessorAffinityMask2";
            public const string IdentityType = "identityType";
            public const string Credential = "Credential";
            public const string IdleTimeout = "idleTimeout";
            public const string IdleTimeoutAction = "idleTimeoutAction";
            public const string LoadUserProfile = "loadUserProfile";
            public const string LogEventOnProcessModel = "logEventOnProcessModel";
            public const string LogonType = "logonType";
            public const string ManualGroupMembership = "manualGroupMembership";
            public const string MaxProcesses = "maxProcesses";
            public const string PingingEnabled = "pingingEnabled";
            public const string PingInterval = "pingInterval";
            public const string PingResponseTime = "pingResponseTime";
            public const string SetProfileEnvironment = "setProfileEnvironment";
            public const string ShutdownTimeLimit = "shutdownTimeLimit";
            public const string StartupTimeLimit = "startupTimeLimit";
            public const string OrphanActionExe = "orphanActionExe";
            public const string OrphanActionParams = "orphanActionParams";
            public const string OrphanWorkerProcess = "orphanWorkerProcess";
            public const string LoadBalancerCapabilities = "loadBalancerCapabilities";
            public const string RapidFailProtection = "rapidFailProtection";
            public const string RapidFailProtectionInterval = "rapidFailProtectionInterval";
            public const string RapidFailProtectionMaxCrashes = "rapidFailProtectionMaxCrashes";
            public const string AutoShutdownExe = "autoShutdownExe";
            public const string AutoShutdownParams = "autoShutdownParams";
            public const string DisallowOverlappingRotation = "disallowOverlappingRotation";
            public const string DisallowRotationOnConfigChange = "disallowRotationOnConfigChange";
            public const string LogEventOnRecycle = "logEventOnRecycle";
            public const string RestartMemoryLimit = "restartMemoryLimit";
            public const string RestartPrivateMemoryLimit = "restartPrivateMemoryLimit";
            public const string RestartRequestsLimit = "restartRequestsLimit";
            public const string RestartTimeLimit = "restartTimeLimit";
            public const string RestartSchedule = "restartSchedule";
        }
    }

    public static class WebAppPoolDefaults
    {
        public const string ResourceId = "WebAppPoolDefaults";

        public static class Properties
        {
            public const string IsSingleInstance = "IsSingleInstance";
            public const string ManagedRuntimeVersion = "ManagedRuntimeVersion";
            public const string IdentityType = "IdentityType";
        }
    }

    public static class WebApplication
    {
        public const string ResourceId = "WebApplication";

        public static class Properties
        {
            public const string Website = "Website";
            public const string Name = "Name";
            public const string WebAppPool = "WebAppPool";
            public const string PhysicalPath = "PhysicalPath";
            public const string SslFlags = "SslFlags";
            public const string AuthenticationInfo = "AuthenticationInfo";
            public const string PreloadEnabled = "PreloadEnabled";
            public const string ServiceAutoStartEnabled = "ServiceAutoStartEnabled";
            public const string ServiceAutoStartProvider = "ServiceAutoStartProvider";
            public const string ApplicationType = "ApplicationType";
            public const string EnabledProtocols = "EnabledProtocols";
        }
    }

    public static class WebApplicationHandler
    {
        public const string ResourceId = "WebApplicationHandler";

        public static class Properties
        {
            public const string Name = "Name";
            public const string PhysicalHandlerPath = "physicalHandlerPath";
            public const string Verb = "Verb";
            public const string Path = "Path";
            public const string Type = "Type";
            public const string Modules = "Modules";
            public const string ScriptProcessor = "ScriptProcessor";
            public const string PreCondition = "PreCondition";
            public const string RequireAccess = "RequireAccess";
            public const string ResourceType = "ResourceType";
            public const string AllowPathInfo = "AllowPathInfo";
            public const string ResponseBufferLimit = "ResponseBufferLimit";
            public const string Location = "Location";
        }
    }

    public static class WebConfigProperty
    {
        public const string ResourceId = "WebConfigProperty";

        public static class Properties
        {
            public const string WebsitePath = "WebsitePath";
            public const string Filter = "Filter";
            public const string PropertyName = "PropertyName";
            public const string Value = "Value";
        }
    }

    public static class WebConfigPropertyCollection
    {
        public const string ResourceId = "WebConfigPropertyCollection";

        public static class Properties
        {
            public const string WebsitePath = "WebsitePath";
            public const string Filter = "Filter";
            public const string CollectionName = "CollectionName";
            public const string ItemName = "ItemName";
            public const string ItemKeyName = "ItemKeyName";
            public const string ItemKeyValue = "ItemKeyValue";
            public const string ItemPropertyName = "ItemPropertyName";
            public const string ItemPropertyValue = "ItemPropertyValue";
        }
    }

    public static class WebSite
    {
        public const string ResourceId = "WebSite";

        public static class Properties
        {
            public const string Name = "Name";
            public const string SiteId = "SiteId";
            public const string PhysicalPath = "PhysicalPath";
            public const string State = "State";
            public const string ApplicationPool = "ApplicationPool";
            public const string BindingInfo = "BindingInfo";
            public const string DefaultPage = "DefaultPage";
            public const string EnabledProtocols = "EnabledProtocols";
            public const string ServerAutoStart = "ServerAutoStart";
            public const string AuthenticationInfo = "AuthenticationInfo";
            public const string PreloadEnabled = "PreloadEnabled";
            public const string ServiceAutoStartEnabled = "ServiceAutoStartEnabled";
            public const string ServiceAutoStartProvider = "ServiceAutoStartProvider";
            public const string ApplicationType = "ApplicationType";
            public const string LogPath = "LogPath";
            public const string LogFlags = "LogFlags";
            public const string LogPeriod = "LogPeriod";
            public const string LogTruncateSize = "LogTruncateSize";
            public const string LoglocalTimeRollover = "LoglocalTimeRollover";
            public const string LogFormat = "LogFormat";
            public const string LogTargetW3C = "LogTargetW3C";
            public const string LogCustomFields = "LogCustomFields";
        }
    }

    public static class WebSiteDefaults
    {
        public const string ResourceId = "WebSiteDefaults";

        public static class Properties
        {
            public const string IsSingleInstance = "IsSingleInstance";
            public const string LogFormat = "LogFormat";
            public const string LogDirectory = "LogDirectory";
            public const string TraceLogDirectory = "TraceLogDirectory";
            public const string DefaultApplicationPool = "DefaultApplicationPool";
            public const string AllowSubDirConfig = "AllowSubDirConfig";
        }
    }

    public static class WebVirtualDirectory
    {
        public const string ResourceId = "WebVirtualDirectory";

        public static class Properties
        {
            public const string Website = "Website";
            public const string WebApplication = "WebApplication";
            public const string Name = "Name";
            public const string PhysicalPath = "PhysicalPath";
            public const string Credential = "Credential";
        }
    }
}

