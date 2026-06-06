namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Resources;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Enums;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.SqlServerDscConstants.SqlSetup;

public sealed class SqlSetupResource : SqlServerDscBase, ISqlSetupResource
{
    private SqlSetupResource(string name) : base(name)
    {
    }

    public SqlSetupAction? Action
    {
        get => this.PropertyBag.Get<SqlSetupAction?>(Constants.Properties.Action);
        set => this.PropertyBag.Set(Constants.Properties.Action, value);
    }

    public string SourcePath
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.SourcePath);
        set => this.PropertyBag.Set(Constants.Properties.SourcePath, value);
    }

    public IPowerShellExpression? SourceCredential
    {
        get => this.PropertyBag.Get<IPowerShellExpression?>(Constants.Properties.SourceCredential);
        set => this.PropertyBag.Set(Constants.Properties.SourceCredential, value);
    }

    public bool? SuppressReboot
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.SuppressReboot);
        set => this.PropertyBag.Set(Constants.Properties.SuppressReboot, value);
    }

    public bool? ForceReboot
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.ForceReboot);
        set => this.PropertyBag.Set(Constants.Properties.ForceReboot, value);
    }

    public string Features
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.Features);
        set => this.PropertyBag.Set(Constants.Properties.Features, value);
    }

    public string InstanceName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.InstanceName);
        set => this.PropertyBag.Set(Constants.Properties.InstanceName, value);
    }

    public string InstanceID
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.InstanceID);
        set => this.PropertyBag.Set(Constants.Properties.InstanceID, value);
    }

    public string ProductKey
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.ProductKey);
        set => this.PropertyBag.Set(Constants.Properties.ProductKey, value);
    }

    public bool? ProductCoveredbySA
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.ProductCoveredbySA);
        set => this.PropertyBag.Set(Constants.Properties.ProductCoveredbySA, value);
    }

    public string UpdateEnabled
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.UpdateEnabled);
        set => this.PropertyBag.Set(Constants.Properties.UpdateEnabled, value);
    }

    public string UpdateSource
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.UpdateSource);
        set => this.PropertyBag.Set(Constants.Properties.UpdateSource, value);
    }

    public string SQMReporting
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.SQMReporting);
        set => this.PropertyBag.Set(Constants.Properties.SQMReporting, value);
    }

    public string ErrorReporting
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.ErrorReporting);
        set => this.PropertyBag.Set(Constants.Properties.ErrorReporting, value);
    }

    public string InstallSharedDir
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.InstallSharedDir);
        set => this.PropertyBag.Set(Constants.Properties.InstallSharedDir, value);
    }

    public string InstallSharedWOWDir
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.InstallSharedWOWDir);
        set => this.PropertyBag.Set(Constants.Properties.InstallSharedWOWDir, value);
    }

    public string InstanceDir
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.InstanceDir);
        set => this.PropertyBag.Set(Constants.Properties.InstanceDir, value);
    }

    public string SQLSvcAccount
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.SQLSvcAccount);
        set => this.PropertyBag.Set(Constants.Properties.SQLSvcAccount, value);
    }

    public string AgtSvcAccount
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.AgtSvcAccount);
        set => this.PropertyBag.Set(Constants.Properties.AgtSvcAccount, value);
    }

    public string SQLCollation
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.SQLCollation);
        set => this.PropertyBag.Set(Constants.Properties.SQLCollation, value);
    }

    public string[] SQLSysAdminAccounts
    {
        get => this.PropertyBag.Get<string[]>(Constants.Properties.SQLSysAdminAccounts);
        set => this.PropertyBag.Set(Constants.Properties.SQLSysAdminAccounts, value);
    }

    public SqlSetupSecurityMode? SecurityMode
    {
        get => this.PropertyBag.Get<SqlSetupSecurityMode?>(Constants.Properties.SecurityMode);
        set => this.PropertyBag.Set(Constants.Properties.SecurityMode, value);
    }

    public string SAPwd
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.SAPwd);
        set => this.PropertyBag.Set(Constants.Properties.SAPwd, value);
    }

    public string InstallSQLDataDir
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.InstallSQLDataDir);
        set => this.PropertyBag.Set(Constants.Properties.InstallSQLDataDir, value);
    }

    public string SQLUserDBDir
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.SQLUserDBDir);
        set => this.PropertyBag.Set(Constants.Properties.SQLUserDBDir, value);
    }

    public string SQLUserDBLogDir
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.SQLUserDBLogDir);
        set => this.PropertyBag.Set(Constants.Properties.SQLUserDBLogDir, value);
    }

    public string SQLTempDBDir
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.SQLTempDBDir);
        set => this.PropertyBag.Set(Constants.Properties.SQLTempDBDir, value);
    }

    public string SQLTempDBLogDir
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.SQLTempDBLogDir);
        set => this.PropertyBag.Set(Constants.Properties.SQLTempDBLogDir, value);
    }

    public string SQLBackupDir
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.SQLBackupDir);
        set => this.PropertyBag.Set(Constants.Properties.SQLBackupDir, value);
    }

    public string FTSvcAccount
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.FTSvcAccount);
        set => this.PropertyBag.Set(Constants.Properties.FTSvcAccount, value);
    }

    public string RSSvcAccount
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.RSSvcAccount);
        set => this.PropertyBag.Set(Constants.Properties.RSSvcAccount, value);
    }

    public SqlSetupRSInstallMode? RSInstallMode
    {
        get => this.PropertyBag.Get<SqlSetupRSInstallMode?>(Constants.Properties.RSInstallMode);
        set => this.PropertyBag.Set(Constants.Properties.RSInstallMode, value);
    }

    public string ASSvcAccount
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.ASSvcAccount);
        set => this.PropertyBag.Set(Constants.Properties.ASSvcAccount, value);
    }

    public string ASCollation
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.ASCollation);
        set => this.PropertyBag.Set(Constants.Properties.ASCollation, value);
    }

    public string[] ASSysAdminAccounts
    {
        get => this.PropertyBag.Get<string[]>(Constants.Properties.ASSysAdminAccounts);
        set => this.PropertyBag.Set(Constants.Properties.ASSysAdminAccounts, value);
    }

    public string ASDataDir
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.ASDataDir);
        set => this.PropertyBag.Set(Constants.Properties.ASDataDir, value);
    }

    public string ASLogDir
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.ASLogDir);
        set => this.PropertyBag.Set(Constants.Properties.ASLogDir, value);
    }

    public string ASBackupDir
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.ASBackupDir);
        set => this.PropertyBag.Set(Constants.Properties.ASBackupDir, value);
    }

    public string ASTempDir
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.ASTempDir);
        set => this.PropertyBag.Set(Constants.Properties.ASTempDir, value);
    }

    public string ASConfigDir
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.ASConfigDir);
        set => this.PropertyBag.Set(Constants.Properties.ASConfigDir, value);
    }

    public SqlSetupASServerMode? ASServerMode
    {
        get => this.PropertyBag.Get<SqlSetupASServerMode?>(Constants.Properties.ASServerMode);
        set => this.PropertyBag.Set(Constants.Properties.ASServerMode, value);
    }

    public string ISSvcAccount
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.ISSvcAccount);
        set => this.PropertyBag.Set(Constants.Properties.ISSvcAccount, value);
    }

    public SqlStartupType? SqlSvcStartupType
    {
        get => this.PropertyBag.Get<SqlStartupType?>(Constants.Properties.SqlSvcStartupType);
        set => this.PropertyBag.Set(Constants.Properties.SqlSvcStartupType, value);
    }

    public SqlStartupType? AgtSvcStartupType
    {
        get => this.PropertyBag.Get<SqlStartupType?>(Constants.Properties.AgtSvcStartupType);
        set => this.PropertyBag.Set(Constants.Properties.AgtSvcStartupType, value);
    }

    public SqlStartupType? IsSvcStartupType
    {
        get => this.PropertyBag.Get<SqlStartupType?>(Constants.Properties.IsSvcStartupType);
        set => this.PropertyBag.Set(Constants.Properties.IsSvcStartupType, value);
    }

    public SqlStartupType? AsSvcStartupType
    {
        get => this.PropertyBag.Get<SqlStartupType?>(Constants.Properties.AsSvcStartupType);
        set => this.PropertyBag.Set(Constants.Properties.AsSvcStartupType, value);
    }

    public SqlStartupType? RSSVCStartupType
    {
        get => this.PropertyBag.Get<SqlStartupType?>(Constants.Properties.RSSVCStartupType);
        set => this.PropertyBag.Set(Constants.Properties.RSSVCStartupType, value);
    }

    public SqlStartupType? BrowserSvcStartupType
    {
        get => this.PropertyBag.Get<SqlStartupType?>(Constants.Properties.BrowserSvcStartupType);
        set => this.PropertyBag.Set(Constants.Properties.BrowserSvcStartupType, value);
    }

    public string FailoverClusterGroupName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.FailoverClusterGroupName);
        set => this.PropertyBag.Set(Constants.Properties.FailoverClusterGroupName, value);
    }

    public string[] FailoverClusterIPAddress
    {
        get => this.PropertyBag.Get<string[]>(Constants.Properties.FailoverClusterIPAddress);
        set => this.PropertyBag.Set(Constants.Properties.FailoverClusterIPAddress, value);
    }

    public string FailoverClusterNetworkName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.FailoverClusterNetworkName);
        set => this.PropertyBag.Set(Constants.Properties.FailoverClusterNetworkName, value);
    }

    public uint? SqlTempdbFileCount
    {
        get => this.PropertyBag.Get<uint?>(Constants.Properties.SqlTempdbFileCount);
        set => this.PropertyBag.Set(Constants.Properties.SqlTempdbFileCount, value);
    }

    public uint? SqlTempdbFileSize
    {
        get => this.PropertyBag.Get<uint?>(Constants.Properties.SqlTempdbFileSize);
        set => this.PropertyBag.Set(Constants.Properties.SqlTempdbFileSize, value);
    }

    public uint? SqlTempdbFileGrowth
    {
        get => this.PropertyBag.Get<uint?>(Constants.Properties.SqlTempdbFileGrowth);
        set => this.PropertyBag.Set(Constants.Properties.SqlTempdbFileGrowth, value);
    }

    public uint? SqlTempdbLogFileSize
    {
        get => this.PropertyBag.Get<uint?>(Constants.Properties.SqlTempdbLogFileSize);
        set => this.PropertyBag.Set(Constants.Properties.SqlTempdbLogFileSize, value);
    }

    public uint? SqlTempdbLogFileGrowth
    {
        get => this.PropertyBag.Get<uint?>(Constants.Properties.SqlTempdbLogFileGrowth);
        set => this.PropertyBag.Set(Constants.Properties.SqlTempdbLogFileGrowth, value);
    }

    public bool? NpEnabled
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.NpEnabled);
        set => this.PropertyBag.Set(Constants.Properties.NpEnabled, value);
    }

    public bool? TcpEnabled
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.TcpEnabled);
        set => this.PropertyBag.Set(Constants.Properties.TcpEnabled, value);
    }

    public uint? SetupProcessTimeout
    {
        get => this.PropertyBag.Get<uint?>(Constants.Properties.SetupProcessTimeout);
        set => this.PropertyBag.Set(Constants.Properties.SetupProcessTimeout, value);
    }

    public string[] FeatureFlag
    {
        get => this.PropertyBag.Get<string[]>(Constants.Properties.FeatureFlag);
        set => this.PropertyBag.Set(Constants.Properties.FeatureFlag, value);
    }

    public bool? UseEnglish
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.UseEnglish);
        set => this.PropertyBag.Set(Constants.Properties.UseEnglish, value);
    }

    public string[] SkipRule
    {
        get => this.PropertyBag.Get<string[]>(Constants.Properties.SkipRule);
        set => this.PropertyBag.Set(Constants.Properties.SkipRule, value);
    }

    public string ServerName
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.ServerName);
        set => this.PropertyBag.Set(Constants.Properties.ServerName, value);
    }

    public string SqlVersion
    {
        get => this.PropertyBag.Get<string>(Constants.Properties.SqlVersion);
        set => this.PropertyBag.Set(Constants.Properties.SqlVersion, value);
    }

    public static SqlSetupResource Create(string name, Action<ISqlSetupResource> configure)
    {
        var resource = new SqlSetupResource(name);
        configure(resource);
        return resource;
    }

    public static SqlSetupResource Create(string name, Action<ISqlSetupResource> configure, out SqlSetupResource resource)
    {
        resource = new SqlSetupResource(name);
        configure(resource);
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var validation = this.ValidationBuilder();
        validation.ValidateStringNotNullOrEmpty(this.SourcePath, nameof(this.SourcePath));
        validation.ValidateStringNotNullOrEmpty(this.InstanceName, nameof(this.InstanceName));
        return Task.FromResult(validation.errors);
    }

    public override string ResourceId => Constants.ResourceId;

    public override bool HasEnsure => false;
}
