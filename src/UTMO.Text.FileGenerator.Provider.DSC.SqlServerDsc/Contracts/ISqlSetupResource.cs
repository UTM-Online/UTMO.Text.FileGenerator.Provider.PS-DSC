namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Enums;

public interface ISqlSetupResource : IDscResourceConfig
{
    SqlSetupAction? Action { get; set; }

    string SourcePath { get; set; }

    IPowerShellExpression? SourceCredential { get; set; }

    bool? SuppressReboot { get; set; }

    bool? ForceReboot { get; set; }

    string Features { get; set; }

    string InstanceName { get; set; }

    string InstanceID { get; set; }

    string ProductKey { get; set; }

    bool? ProductCoveredbySA { get; set; }

    string UpdateEnabled { get; set; }

    string UpdateSource { get; set; }

    string SQMReporting { get; set; }

    string ErrorReporting { get; set; }

    string InstallSharedDir { get; set; }

    string InstallSharedWOWDir { get; set; }

    string InstanceDir { get; set; }

    string SQLSvcAccount { get; set; }

    string AgtSvcAccount { get; set; }

    string SQLCollation { get; set; }

    string[] SQLSysAdminAccounts { get; set; }

    SqlSetupSecurityMode? SecurityMode { get; set; }

    string SAPwd { get; set; }

    string InstallSQLDataDir { get; set; }

    string SQLUserDBDir { get; set; }

    string SQLUserDBLogDir { get; set; }

    string SQLTempDBDir { get; set; }

    string SQLTempDBLogDir { get; set; }

    string SQLBackupDir { get; set; }

    string FTSvcAccount { get; set; }

    string RSSvcAccount { get; set; }

    SqlSetupRSInstallMode? RSInstallMode { get; set; }

    string ASSvcAccount { get; set; }

    string ASCollation { get; set; }

    string[] ASSysAdminAccounts { get; set; }

    string ASDataDir { get; set; }

    string ASLogDir { get; set; }

    string ASBackupDir { get; set; }

    string ASTempDir { get; set; }

    string ASConfigDir { get; set; }

    SqlSetupASServerMode? ASServerMode { get; set; }

    string ISSvcAccount { get; set; }

    SqlStartupType? SqlSvcStartupType { get; set; }

    SqlStartupType? AgtSvcStartupType { get; set; }

    SqlStartupType? IsSvcStartupType { get; set; }

    SqlStartupType? AsSvcStartupType { get; set; }

    SqlStartupType? RSSVCStartupType { get; set; }

    SqlStartupType? BrowserSvcStartupType { get; set; }

    string FailoverClusterGroupName { get; set; }

    string[] FailoverClusterIPAddress { get; set; }

    string FailoverClusterNetworkName { get; set; }

    uint? SqlTempdbFileCount { get; set; }

    uint? SqlTempdbFileSize { get; set; }

    uint? SqlTempdbFileGrowth { get; set; }

    uint? SqlTempdbLogFileSize { get; set; }

    uint? SqlTempdbLogFileGrowth { get; set; }

    bool? NpEnabled { get; set; }

    bool? TcpEnabled { get; set; }

    uint? SetupProcessTimeout { get; set; }

    string[] FeatureFlag { get; set; }

    bool? UseEnglish { get; set; }

    string[] SkipRule { get; set; }

    string ServerName { get; set; }

    string SqlVersion { get; set; }
}
