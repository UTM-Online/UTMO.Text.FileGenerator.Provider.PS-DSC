namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc;

public static class SqlServerDscConstants
{
    public static class SqlAG
    {
        public const string ResourceId = "SqlAG";

        public static class Properties
        {
            public const string Name = "Name";
            public const string ServerName = "ServerName";
            public const string InstanceName = "InstanceName";
            public const string Ensure = "Ensure";
            public const string AutomatedBackupPreference = "AutomatedBackupPreference";
            public const string AvailabilityMode = "AvailabilityMode";
            public const string BackupPriority = "BackupPriority";
            public const string BasicAvailabilityGroup = "BasicAvailabilityGroup";
            public const string DatabaseHealthTrigger = "DatabaseHealthTrigger";
            public const string DtcSupportEnabled = "DtcSupportEnabled";
            public const string ConnectionModeInPrimaryRole = "ConnectionModeInPrimaryRole";
            public const string ConnectionModeInSecondaryRole = "ConnectionModeInSecondaryRole";
            public const string EndpointHostName = "EndpointHostName";
            public const string FailureConditionLevel = "FailureConditionLevel";
            public const string FailoverMode = "FailoverMode";
            public const string SeedingMode = "SeedingMode";
            public const string HealthCheckTimeout = "HealthCheckTimeout";
            public const string ProcessOnlyOnActiveNode = "ProcessOnlyOnActiveNode";
        }
    }

    public static class SqlAGDatabase
    {
        public const string ResourceId = "SqlAGDatabase";

        public static class Properties
        {
            public const string DatabaseName = "DatabaseName";
            public const string ServerName = "ServerName";
            public const string InstanceName = "InstanceName";
            public const string AvailabilityGroupName = "AvailabilityGroupName";
            public const string BackupPath = "BackupPath";
            public const string Ensure = "Ensure";
            public const string Force = "Force";
            public const string MatchDatabaseOwner = "MatchDatabaseOwner";
            public const string ReplaceExisting = "ReplaceExisting";
            public const string ProcessOnlyOnActiveNode = "ProcessOnlyOnActiveNode";
            public const string StatementTimeout = "StatementTimeout";
        }
    }

    public static class SqlAGListener
    {
        public const string ResourceId = "SqlAGListener";

        public static class Properties
        {
            public const string InstanceName = "InstanceName";
            public const string ServerName = "ServerName";
            public const string Name = "Name";
            public const string Ensure = "Ensure";
            public const string AvailabilityGroup = "AvailabilityGroup";
            public const string IpAddress = "IpAddress";
            public const string Port = "Port";
            public const string DHCP = "DHCP";
            public const string ProcessOnlyOnActiveNode = "ProcessOnlyOnActiveNode";
        }
    }

    public static class SqlAGReplica
    {
        public const string ResourceId = "SqlAGReplica";

        public static class Properties
        {
            public const string Name = "Name";
            public const string AvailabilityGroupName = "AvailabilityGroupName";
            public const string ServerName = "ServerName";
            public const string InstanceName = "InstanceName";
            public const string PrimaryReplicaServerName = "PrimaryReplicaServerName";
            public const string PrimaryReplicaInstanceName = "PrimaryReplicaInstanceName";
            public const string Ensure = "Ensure";
            public const string AvailabilityMode = "AvailabilityMode";
            public const string BackupPriority = "BackupPriority";
            public const string ConnectionModeInPrimaryRole = "ConnectionModeInPrimaryRole";
            public const string ConnectionModeInSecondaryRole = "ConnectionModeInSecondaryRole";
            public const string EndpointHostName = "EndpointHostName";
            public const string FailoverMode = "FailoverMode";
            public const string ReadOnlyRoutingConnectionUrl = "ReadOnlyRoutingConnectionUrl";
            public const string ReadOnlyRoutingList = "ReadOnlyRoutingList";
            public const string ProcessOnlyOnActiveNode = "ProcessOnlyOnActiveNode";
            public const string SeedingMode = "SeedingMode";
        }
    }

    public static class SqlAgentFailsafe
    {
        public const string ResourceId = "SqlAgentFailsafe";

        public static class Properties
        {
            public const string Name = "Name";
            public const string Ensure = "Ensure";
            public const string ServerName = "ServerName";
            public const string InstanceName = "InstanceName";
            public const string NotificationMethod = "NotificationMethod";
        }
    }

    public static class SqlAgentOperator
    {
        public const string ResourceId = "SqlAgentOperator";

        public static class Properties
        {
            public const string Name = "Name";
            public const string Ensure = "Ensure";
            public const string ServerName = "ServerName";
            public const string InstanceName = "InstanceName";
            public const string EmailAddress = "EmailAddress";
        }
    }

    public static class SqlAlias
    {
        public const string ResourceId = "SqlAlias";

        public static class Properties
        {
            public const string Name = "Name";
            public const string Protocol = "Protocol";
            public const string ServerName = "ServerName";
            public const string TcpPort = "TcpPort";
            public const string UseDynamicTcpPort = "UseDynamicTcpPort";
            public const string Ensure = "Ensure";
        }
    }

    public static class SqlAlwaysOnService
    {
        public const string ResourceId = "SqlAlwaysOnService";

        public static class Properties
        {
            public const string InstanceName = "InstanceName";
            public const string Ensure = "Ensure";
            public const string ServerName = "ServerName";
            public const string RestartTimeout = "RestartTimeout";
        }
    }

    public static class SqlConfiguration
    {
        public const string ResourceId = "SqlConfiguration";

        public static class Properties
        {
            public const string InstanceName = "InstanceName";
            public const string OptionName = "OptionName";
            public const string OptionValue = "OptionValue";
            public const string ServerName = "ServerName";
            public const string RestartService = "RestartService";
            public const string RestartTimeout = "RestartTimeout";
        }
    }

    public static class SqlDatabaseDefaultLocation
    {
        public const string ResourceId = "SqlDatabaseDefaultLocation";

        public static class Properties
        {
            public const string InstanceName = "InstanceName";
            public const string Type = "Type";
            public const string Path = "Path";
            public const string ServerName = "ServerName";
            public const string RestartService = "RestartService";
            public const string ProcessOnlyOnActiveNode = "ProcessOnlyOnActiveNode";
        }
    }

    public static class SqlDatabaseMail
    {
        public const string ResourceId = "SqlDatabaseMail";

        public static class Properties
        {
            public const string AccountName = "AccountName";
            public const string InstanceName = "InstanceName";
            public const string EmailAddress = "EmailAddress";
            public const string MailServerName = "MailServerName";
            public const string ProfileName = "ProfileName";
            public const string Ensure = "Ensure";
            public const string ServerName = "ServerName";
            public const string DisplayName = "DisplayName";
            public const string ReplyToAddress = "ReplyToAddress";
            public const string Description = "Description";
            public const string LoggingLevel = "LoggingLevel";
            public const string TcpPort = "TcpPort";
            public const string UseDefaultCredentials = "UseDefaultCredentials";
        }
    }

    public static class SqlDatabaseObjectPermission
    {
        public const string ResourceId = "SqlDatabaseObjectPermission";

        public static class Properties
        {
            public const string InstanceName = "InstanceName";
            public const string DatabaseName = "DatabaseName";
            public const string SchemaName = "SchemaName";
            public const string ObjectName = "ObjectName";
            public const string ObjectType = "ObjectType";
            public const string Name = "Name";
            public const string Permission = "Permission";
            public const string ServerName = "ServerName";
            public const string Force = "Force";
            public const string State = "State";
            public const string Ensure = "Ensure";
        }
    }

    public static class SqlDatabaseRole
    {
        public const string ResourceId = "SqlDatabaseRole";

        public static class Properties
        {
            public const string InstanceName = "InstanceName";
            public const string DatabaseName = "DatabaseName";
            public const string Name = "Name";
            public const string ServerName = "ServerName";
            public const string Members = "Members";
            public const string MembersToInclude = "MembersToInclude";
            public const string MembersToExclude = "MembersToExclude";
            public const string Ensure = "Ensure";
        }
    }

    public static class SqlDatabaseUser
    {
        public const string ResourceId = "SqlDatabaseUser";

        public static class Properties
        {
            public const string Name = "Name";
            public const string InstanceName = "InstanceName";
            public const string DatabaseName = "DatabaseName";
            public const string ServerName = "ServerName";
            public const string LoginName = "LoginName";
            public const string AsymmetricKeyName = "AsymmetricKeyName";
            public const string CertificateName = "CertificateName";
            public const string UserType = "UserType";
            public const string Ensure = "Ensure";
            public const string Force = "Force";
        }
    }

    public static class SqlEndpoint
    {
        public const string ResourceId = "SqlEndpoint";

        public static class Properties
        {
            public const string EndpointName = "EndpointName";
            public const string InstanceName = "InstanceName";
            public const string EndpointType = "EndpointType";
            public const string Ensure = "Ensure";
            public const string Port = "Port";
            public const string ServerName = "ServerName";
            public const string IpAddress = "IpAddress";
            public const string Owner = "Owner";
            public const string IsMessageForwardingEnabled = "IsMessageForwardingEnabled";
            public const string MessageForwardingSize = "MessageForwardingSize";
            public const string State = "State";
        }
    }

    public static class SqlEndpointPermission
    {
        public const string ResourceId = "SqlEndpointPermission";

        public static class Properties
        {
            public const string InstanceName = "InstanceName";
            public const string Principal = "Principal";
            public const string Name = "Name";
            public const string ServerName = "ServerName";
            public const string Ensure = "Ensure";
            public const string Permission = "Permission";
        }
    }

    public static class SqlLogin
    {
        public const string ResourceId = "SqlLogin";

        public static class Properties
        {
            public const string Name = "Name";
            public const string InstanceName = "InstanceName";
            public const string Ensure = "Ensure";
            public const string ServerName = "ServerName";
            public const string LoginCredential = "LoginCredential";
            public const string LoginMustChangePassword = "LoginMustChangePassword";
            public const string LoginPasswordExpirationEnabled = "LoginPasswordExpirationEnabled";
            public const string LoginPasswordPolicyEnforced = "LoginPasswordPolicyEnforced";
            public const string Disabled = "Disabled";
            public const string DefaultDatabase = "DefaultDatabase";
            public const string Language = "Language";
            public const string Sid = "Sid";
            public const string LoginType = "LoginType";
        }
    }

    public static class SqlMaxDop
    {
        public const string ResourceId = "SqlMaxDop";

        public static class Properties
        {
            public const string Ensure = "Ensure";
            public const string DynamicAlloc = "DynamicAlloc";
            public const string MaxDop = "MaxDop";
            public const string ServerName = "ServerName";
            public const string InstanceName = "InstanceName";
            public const string ProcessOnlyOnActiveNode = "ProcessOnlyOnActiveNode";
        }
    }

    public static class SqlMemory
    {
        public const string ResourceId = "SqlMemory";

        public static class Properties
        {
            public const string InstanceName = "InstanceName";
            public const string ServerName = "ServerName";
            public const string Ensure = "Ensure";
            public const string DynamicAlloc = "DynamicAlloc";
            public const string MinMemory = "MinMemory";
            public const string MaxMemory = "MaxMemory";
            public const string MinMemoryPercent = "MinMemoryPercent";
            public const string MaxMemoryPercent = "MaxMemoryPercent";
            public const string ProcessOnlyOnActiveNode = "ProcessOnlyOnActiveNode";
        }
    }

    public static class SqlProtocol
    {
        public const string ResourceId = "SqlProtocol";

        public static class Properties
        {
            public const string InstanceName = "InstanceName";
            public const string ProtocolName = "ProtocolName";
            public const string ServerName = "ServerName";
            public const string Enabled = "Enabled";
            public const string ListenOnAllIpAddresses = "ListenOnAllIpAddresses";
            public const string KeepAlive = "KeepAlive";
            public const string PipeName = "PipeName";
            public const string SuppressRestart = "SuppressRestart";
            public const string RestartTimeout = "RestartTimeout";
        }
    }

    public static class SqlProtocolTcpIp
    {
        public const string ResourceId = "SqlProtocolTcpIp";

        public static class Properties
        {
            public const string InstanceName = "InstanceName";
            public const string IpAddressGroup = "IpAddressGroup";
            public const string ServerName = "ServerName";
            public const string Enabled = "Enabled";
            public const string IpAddress = "IpAddress";
            public const string UseTcpDynamicPort = "UseTcpDynamicPort";
            public const string TcpPort = "TcpPort";
            public const string SuppressRestart = "SuppressRestart";
            public const string RestartTimeout = "RestartTimeout";
        }
    }

    public static class SqlRS
    {
        public const string ResourceId = "SqlRS";

        public static class Properties
        {
            public const string InstanceName = "InstanceName";
            public const string DatabaseServerName = "DatabaseServerName";
            public const string DatabaseInstanceName = "DatabaseInstanceName";
            public const string ReportServerVirtualDirectory = "ReportServerVirtualDirectory";
            public const string ReportsVirtualDirectory = "ReportsVirtualDirectory";
            public const string ReportServerReservedUrl = "ReportServerReservedUrl";
            public const string ReportsReservedUrl = "ReportsReservedUrl";
            public const string UseSsl = "UseSsl";
            public const string SuppressRestart = "SuppressRestart";
            public const string RestartTimeout = "RestartTimeout";
            public const string Encrypt = "Encrypt";
        }
    }

    public static class SqlReplication
    {
        public const string ResourceId = "SqlReplication";

        public static class Properties
        {
            public const string InstanceName = "InstanceName";
            public const string Ensure = "Ensure";
            public const string DistributorMode = "DistributorMode";
            public const string AdminLinkCredentials = "AdminLinkCredentials";
            public const string DistributionDBName = "DistributionDBName";
            public const string RemoteDistributor = "RemoteDistributor";
            public const string WorkingDirectory = "WorkingDirectory";
            public const string UseTrustedConnection = "UseTrustedConnection";
            public const string UninstallWithForce = "UninstallWithForce";
        }
    }

    public static class SqlRole
    {
        public const string ResourceId = "SqlRole";

        public static class Properties
        {
            public const string ServerRoleName = "ServerRoleName";
            public const string InstanceName = "InstanceName";
            public const string ServerName = "ServerName";
            public const string Ensure = "Ensure";
            public const string Members = "Members";
            public const string MembersToInclude = "MembersToInclude";
            public const string MembersToExclude = "MembersToExclude";
        }
    }

    public static class SqlScript
    {
        public const string ResourceId = "SqlScript";

        public static class Properties
        {
            public const string Id = "Id";
            public const string InstanceName = "InstanceName";
            public const string SetFilePath = "SetFilePath";
            public const string GetFilePath = "GetFilePath";
            public const string TestFilePath = "TestFilePath";
            public const string ServerName = "ServerName";
            public const string Credential = "Credential";
            public const string Variable = "Variable";
            public const string DisableVariables = "DisableVariables";
            public const string QueryTimeout = "QueryTimeout";
            public const string Encrypt = "Encrypt";
        }
    }

    public static class SqlScriptQuery
    {
        public const string ResourceId = "SqlScriptQuery";

        public static class Properties
        {
            public const string Id = "Id";
            public const string InstanceName = "InstanceName";
            public const string GetQuery = "GetQuery";
            public const string TestQuery = "TestQuery";
            public const string SetQuery = "SetQuery";
            public const string ServerName = "ServerName";
            public const string Credential = "Credential";
            public const string Variable = "Variable";
            public const string DisableVariables = "DisableVariables";
            public const string QueryTimeout = "QueryTimeout";
            public const string Encrypt = "Encrypt";
        }
    }

    public static class SqlSecureConnection
    {
        public const string ResourceId = "SqlSecureConnection";

        public static class Properties
        {
            public const string InstanceName = "InstanceName";
            public const string Thumbprint = "Thumbprint";
            public const string ForceEncryption = "ForceEncryption";
            public const string ServiceAccount = "ServiceAccount";
            public const string SuppressRestart = "SuppressRestart";
            public const string Ensure = "Ensure";
            public const string ServerName = "ServerName";
        }
    }

    public static class SqlServiceAccount
    {
        public const string ResourceId = "SqlServiceAccount";

        public static class Properties
        {
            public const string InstanceName = "InstanceName";
            public const string ServiceType = "ServiceType";
            public const string ServiceAccount = "ServiceAccount";
            public const string ServerName = "ServerName";
            public const string RestartService = "RestartService";
            public const string Force = "Force";
            public const string VersionNumber = "VersionNumber";
        }
    }

    public static class SqlSetup
    {
        public const string ResourceId = "SqlSetup";

        public static class Properties
        {
            public const string Action = "Action";
            public const string SourcePath = "SourcePath";
            public const string SourceCredential = "SourceCredential";
            public const string SuppressReboot = "SuppressReboot";
            public const string ForceReboot = "ForceReboot";
            public const string Features = "Features";
            public const string InstanceName = "InstanceName";
            public const string InstanceID = "InstanceID";
            public const string ProductKey = "ProductKey";
            public const string ProductCoveredbySA = "ProductCoveredbySA";
            public const string UpdateEnabled = "UpdateEnabled";
            public const string UpdateSource = "UpdateSource";
            public const string SQMReporting = "SQMReporting";
            public const string ErrorReporting = "ErrorReporting";
            public const string InstallSharedDir = "InstallSharedDir";
            public const string InstallSharedWOWDir = "InstallSharedWOWDir";
            public const string InstanceDir = "InstanceDir";
            public const string SQLSvcAccount = "SQLSvcAccount";
            public const string AgtSvcAccount = "AgtSvcAccount";
            public const string SQLCollation = "SQLCollation";
            public const string SQLSysAdminAccounts = "SQLSysAdminAccounts";
            public const string SecurityMode = "SecurityMode";
            public const string SAPwd = "SAPwd";
            public const string InstallSQLDataDir = "InstallSQLDataDir";
            public const string SQLUserDBDir = "SQLUserDBDir";
            public const string SQLUserDBLogDir = "SQLUserDBLogDir";
            public const string SQLTempDBDir = "SQLTempDBDir";
            public const string SQLTempDBLogDir = "SQLTempDBLogDir";
            public const string SQLBackupDir = "SQLBackupDir";
            public const string FTSvcAccount = "FTSvcAccount";
            public const string RSSvcAccount = "RSSvcAccount";
            public const string RSInstallMode = "RSInstallMode";
            public const string ASSvcAccount = "ASSvcAccount";
            public const string ASCollation = "ASCollation";
            public const string ASSysAdminAccounts = "ASSysAdminAccounts";
            public const string ASDataDir = "ASDataDir";
            public const string ASLogDir = "ASLogDir";
            public const string ASBackupDir = "ASBackupDir";
            public const string ASTempDir = "ASTempDir";
            public const string ASConfigDir = "ASConfigDir";
            public const string ASServerMode = "ASServerMode";
            public const string ISSvcAccount = "ISSvcAccount";
            public const string SqlSvcStartupType = "SqlSvcStartupType";
            public const string AgtSvcStartupType = "AgtSvcStartupType";
            public const string IsSvcStartupType = "IsSvcStartupType";
            public const string AsSvcStartupType = "AsSvcStartupType";
            public const string RSSVCStartupType = "RSSVCStartupType";
            public const string BrowserSvcStartupType = "BrowserSvcStartupType";
            public const string FailoverClusterGroupName = "FailoverClusterGroupName";
            public const string FailoverClusterIPAddress = "FailoverClusterIPAddress";
            public const string FailoverClusterNetworkName = "FailoverClusterNetworkName";
            public const string SqlTempdbFileCount = "SqlTempdbFileCount";
            public const string SqlTempdbFileSize = "SqlTempdbFileSize";
            public const string SqlTempdbFileGrowth = "SqlTempdbFileGrowth";
            public const string SqlTempdbLogFileSize = "SqlTempdbLogFileSize";
            public const string SqlTempdbLogFileGrowth = "SqlTempdbLogFileGrowth";
            public const string NpEnabled = "NpEnabled";
            public const string TcpEnabled = "TcpEnabled";
            public const string SetupProcessTimeout = "SetupProcessTimeout";
            public const string FeatureFlag = "FeatureFlag";
            public const string UseEnglish = "UseEnglish";
            public const string SkipRule = "SkipRule";
            public const string ServerName = "ServerName";
            public const string SqlVersion = "SqlVersion";
        }
    }

    public static class SqlTraceFlag
    {
        public const string ResourceId = "SqlTraceFlag";

        public static class Properties
        {
            public const string ServerName = "ServerName";
            public const string InstanceName = "InstanceName";
            public const string TraceFlags = "TraceFlags";
            public const string TraceFlagsToInclude = "TraceFlagsToInclude";
            public const string TraceFlagsToExclude = "TraceFlagsToExclude";
            public const string ClearAllTraceFlags = "ClearAllTraceFlags";
            public const string RestartService = "RestartService";
            public const string RestartTimeout = "RestartTimeout";
        }
    }

    public static class SqlWaitForAG
    {
        public const string ResourceId = "SqlWaitForAG";

        public static class Properties
        {
            public const string InstanceName = "InstanceName";
            public const string ServerName = "ServerName";
            public const string Name = "Name";
            public const string RetryIntervalSec = "RetryIntervalSec";
            public const string RetryCount = "RetryCount";
        }
    }

    public static class SqlWindowsFirewall
    {
        public const string ResourceId = "SqlWindowsFirewall";

        public static class Properties
        {
            public const string Ensure = "Ensure";
            public const string SourcePath = "SourcePath";
            public const string Features = "Features";
            public const string InstanceName = "InstanceName";
            public const string SourceCredential = "SourceCredential";
        }
    }

}
