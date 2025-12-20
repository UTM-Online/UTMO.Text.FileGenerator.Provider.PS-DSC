namespace UTMO.Text.FileGenerator.Provider.DSC.WindowsDefender.Resources;

using System.Diagnostics.CodeAnalysis;
using UTMO.Text.FileGenerator.Provider.DSC.WindowsDefender.Contracts;
using Constants = WindowsDefenderConstants.WindowsDefender;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public sealed class WindowsDefenderResource : WindowsDefenderBase, IWindowsDefenderResource
{
    private WindowsDefenderResource(string name) : base(name)
    {
        this.PropertyBag.Set(Constants.Parameters.IsSingleInstance, "yes");
    }
    
    #region Exclusions
    
    public string[] ExclusionPath
    {
        get => this.PropertyBag.Get<string[]>(Constants.Parameters.ExclusionPath);
        set => this.PropertyBag.Set(Constants.Parameters.ExclusionPath, value);
    }
    
    public string[] ExclusionExtension
    {
        get => this.PropertyBag.Get<string[]>(Constants.Parameters.ExclusionExtension);
        set => this.PropertyBag.Set(Constants.Parameters.ExclusionExtension, value);
    }
    
    public string[] ExclusionProcess
    {
        get => this.PropertyBag.Get<string[]>(Constants.Parameters.ExclusionProcess);
        set => this.PropertyBag.Set(Constants.Parameters.ExclusionProcess, value);
    }
    
    #endregion
    
    #region Threat ID Default Actions
    
    public ulong[] ThreatIDDefaultAction_Ids
    {
        get => this.PropertyBag.Get<ulong[]>(Constants.Parameters.ThreatIDDefaultAction_Ids);
        set => this.PropertyBag.Set(Constants.Parameters.ThreatIDDefaultAction_Ids, value);
    }
    
    public ThreatAction[] ThreatIDDefaultAction_Actions
    {
        get => this.PropertyBag.Get<ThreatAction[]>(Constants.Parameters.ThreatIDDefaultAction_Actions);
        set => this.PropertyBag.Set(Constants.Parameters.ThreatIDDefaultAction_Actions, value);
    }
    
    #endregion
    
    #region Quarantine
    
    public int QuarantinePurgeItemsAfterDelay
    {
        get => this.PropertyBag.Get<int>(Constants.Parameters.QuarantinePurgeItemsAfterDelay);
        set => this.PropertyBag.Set(Constants.Parameters.QuarantinePurgeItemsAfterDelay, value);
    }
    
    #endregion
    
    #region Real-time Protection
    
    public ScanDirection RealTimeScanDirection
    {
        get => this.PropertyBag.Get<ScanDirection>(Constants.Parameters.RealTimeScanDirection);
        set => this.PropertyBag.Set(Constants.Parameters.RealTimeScanDirection, value);
    }
    
    #endregion
    
    #region Remediation Schedule
    
    public ScheduleDays RemediationScheduleDay
    {
        get => this.PropertyBag.Get<ScheduleDays>(Constants.Parameters.RemediationScheduleDay);
        set => this.PropertyBag.Set(Constants.Parameters.RemediationScheduleDay, value);
    }
    
    public DateTime RemediationScheduleTime
    {
        get => this.PropertyBag.Get<DateTime>(Constants.Parameters.RemediationScheduleTime);
        set => this.PropertyBag.Set(Constants.Parameters.RemediationScheduleTime, value);
    }
    
    #endregion
    
    #region Reporting Timeouts
    
    public int ReportingAdditionalActionTimeOut
    {
        get => this.PropertyBag.Get<int>(Constants.Parameters.ReportingAdditionalActionTimeOut);
        set => this.PropertyBag.Set(Constants.Parameters.ReportingAdditionalActionTimeOut, value);
    }
    
    public int ReportingNonCriticalTimeOut
    {
        get => this.PropertyBag.Get<int>(Constants.Parameters.ReportingNonCriticalTimeOut);
        set => this.PropertyBag.Set(Constants.Parameters.ReportingNonCriticalTimeOut, value);
    }
    
    public int ReportingCriticalFailureTimeOut
    {
        get => this.PropertyBag.Get<int>(Constants.Parameters.ReportingCriticalFailureTimeOut);
        set => this.PropertyBag.Set(Constants.Parameters.ReportingCriticalFailureTimeOut, value);
    }
    
    #endregion
    
    #region Scan Settings
    
    public int ScanAvgCPULoadFactor
    {
        get => this.PropertyBag.Get<int>(Constants.Parameters.ScanAvgCPULoadFactor);
        set => this.PropertyBag.Set(Constants.Parameters.ScanAvgCPULoadFactor, value);
    }
    
    public bool CheckForSignaturesBeforeRunningScan
    {
        get => this.PropertyBag.Get<bool>(Constants.Parameters.CheckForSignaturesBeforeRunningScan);
        set => this.PropertyBag.Set(Constants.Parameters.CheckForSignaturesBeforeRunningScan, value);
    }
    
    public int ScanPurgeItemsAfterDelay
    {
        get => this.PropertyBag.Get<int>(Constants.Parameters.ScanPurgeItemsAfterDelay);
        set => this.PropertyBag.Set(Constants.Parameters.ScanPurgeItemsAfterDelay, value);
    }
    
    public bool ScanOnlyIfIdleEnabled
    {
        get => this.PropertyBag.Get<bool>(Constants.Parameters.ScanOnlyIfIdleEnabled);
        set => this.PropertyBag.Set(Constants.Parameters.ScanOnlyIfIdleEnabled, value);
    }
    
    public ScanType ScanParameters
    {
        get => this.PropertyBag.Get<ScanType>(Constants.Parameters.ScanParameters);
        set => this.PropertyBag.Set(Constants.Parameters.ScanParameters, value);
    }
    
    public ScheduleDays ScanScheduleDay
    {
        get => this.PropertyBag.Get<ScheduleDays>(Constants.Parameters.ScanScheduleDay);
        set => this.PropertyBag.Set(Constants.Parameters.ScanScheduleDay, value);
    }
    
    public DateTime ScanScheduleTime
    {
        get => this.PropertyBag.Get<DateTime>(Constants.Parameters.ScanScheduleTime);
        set => this.PropertyBag.Set(Constants.Parameters.ScanScheduleTime, value);
    }
    
    public DateTime ScanScheduleQuickScanTime
    {
        get => this.PropertyBag.Get<DateTime>(Constants.Parameters.ScanScheduleQuickScanTime);
        set => this.PropertyBag.Set(Constants.Parameters.ScanScheduleQuickScanTime, value);
    }
    
    public bool RandomizeScheduleTaskTimes
    {
        get => this.PropertyBag.Get<bool>(Constants.Parameters.RandomizeScheduleTaskTimes);
        set => this.PropertyBag.Set(Constants.Parameters.RandomizeScheduleTaskTimes, value);
    }
    
    #endregion
    
    #region Signature Updates
    
    public int SignatureFirstAuGracePeriod
    {
        get => this.PropertyBag.Get<int>(Constants.Parameters.SignatureFirstAuGracePeriod);
        set => this.PropertyBag.Set(Constants.Parameters.SignatureFirstAuGracePeriod, value);
    }
    
    public int SignatureAuGracePeriod
    {
        get => this.PropertyBag.Get<int>(Constants.Parameters.SignatureAuGracePeriod);
        set => this.PropertyBag.Set(Constants.Parameters.SignatureAuGracePeriod, value);
    }
    
    public string SignatureDefinitionUpdateFileSharesSources
    {
        get => this.PropertyBag.Get<string>(Constants.Parameters.SignatureDefinitionUpdateFileSharesSources);
        set => this.PropertyBag.Set(Constants.Parameters.SignatureDefinitionUpdateFileSharesSources, value);
    }
    
    public bool SignatureDisableUpdateOnStartupWithoutEngine
    {
        get => this.PropertyBag.Get<bool>(Constants.Parameters.SignatureDisableUpdateOnStartupWithoutEngine);
        set => this.PropertyBag.Set(Constants.Parameters.SignatureDisableUpdateOnStartupWithoutEngine, value);
    }
    
    public string SignatureFallbackOrder
    {
        get => this.PropertyBag.Get<string>(Constants.Parameters.SignatureFallbackOrder);
        set => this.PropertyBag.Set(Constants.Parameters.SignatureFallbackOrder, value);
    }
    
    public ScheduleDays SignatureScheduleDay
    {
        get => this.PropertyBag.Get<ScheduleDays>(Constants.Parameters.SignatureScheduleDay);
        set => this.PropertyBag.Set(Constants.Parameters.SignatureScheduleDay, value);
    }
    
    public DateTime SignatureScheduleTime
    {
        get => this.PropertyBag.Get<DateTime>(Constants.Parameters.SignatureScheduleTime);
        set => this.PropertyBag.Set(Constants.Parameters.SignatureScheduleTime, value);
    }
    
    public int SignatureUpdateCatchupInterval
    {
        get => this.PropertyBag.Get<int>(Constants.Parameters.SignatureUpdateCatchupInterval);
        set => this.PropertyBag.Set(Constants.Parameters.SignatureUpdateCatchupInterval, value);
    }
    
    public int SignatureUpdateInterval
    {
        get => this.PropertyBag.Get<int>(Constants.Parameters.SignatureUpdateInterval);
        set => this.PropertyBag.Set(Constants.Parameters.SignatureUpdateInterval, value);
    }
    
    #endregion
    
    #region Disable Features
    
    public bool DisableRealtimeMonitoring
    {
        get => this.PropertyBag.Get<bool>(Constants.Parameters.DisableRealtimeMonitoring);
        set => this.PropertyBag.Set(Constants.Parameters.DisableRealtimeMonitoring, value);
    }
    
    public bool DisableBehaviorMonitoring
    {
        get => this.PropertyBag.Get<bool>(Constants.Parameters.DisableBehaviorMonitoring);
        set => this.PropertyBag.Set(Constants.Parameters.DisableBehaviorMonitoring, value);
    }
    
    public bool DisableBlockAtFirstSeen
    {
        get => this.PropertyBag.Get<bool>(Constants.Parameters.DisableBlockAtFirstSeen);
        set => this.PropertyBag.Set(Constants.Parameters.DisableBlockAtFirstSeen, value);
    }
    
    public bool DisableIOAVProtection
    {
        get => this.PropertyBag.Get<bool>(Constants.Parameters.DisableIOAVProtection);
        set => this.PropertyBag.Set(Constants.Parameters.DisableIOAVProtection, value);
    }
    
    public bool DisablePrivacyMode
    {
        get => this.PropertyBag.Get<bool>(Constants.Parameters.DisablePrivacyMode);
        set => this.PropertyBag.Set(Constants.Parameters.DisablePrivacyMode, value);
    }
    
    public bool DisableScriptScanning
    {
        get => this.PropertyBag.Get<bool>(Constants.Parameters.DisableScriptScanning);
        set => this.PropertyBag.Set(Constants.Parameters.DisableScriptScanning, value);
    }
    
    public bool DisableArchiveScanning
    {
        get => this.PropertyBag.Get<bool>(Constants.Parameters.DisableArchiveScanning);
        set => this.PropertyBag.Set(Constants.Parameters.DisableArchiveScanning, value);
    }
    
    public bool DisableCatchupFullScan
    {
        get => this.PropertyBag.Get<bool>(Constants.Parameters.DisableCatchupFullScan);
        set => this.PropertyBag.Set(Constants.Parameters.DisableCatchupFullScan, value);
    }
    
    public bool DisableCatchupQuickScan
    {
        get => this.PropertyBag.Get<bool>(Constants.Parameters.DisableCatchupQuickScan);
        set => this.PropertyBag.Set(Constants.Parameters.DisableCatchupQuickScan, value);
    }
    
    public bool DisableEmailScanning
    {
        get => this.PropertyBag.Get<bool>(Constants.Parameters.DisableEmailScanning);
        set => this.PropertyBag.Set(Constants.Parameters.DisableEmailScanning, value);
    }
    
    public bool DisableRemovableDriveScanning
    {
        get => this.PropertyBag.Get<bool>(Constants.Parameters.DisableRemovableDriveScanning);
        set => this.PropertyBag.Set(Constants.Parameters.DisableRemovableDriveScanning, value);
    }
    
    public bool DisableRestorePoint
    {
        get => this.PropertyBag.Get<bool>(Constants.Parameters.DisableRestorePoint);
        set => this.PropertyBag.Set(Constants.Parameters.DisableRestorePoint, value);
    }
    
    public bool DisableScanningMappedNetworkDrivesForFullScan
    {
        get => this.PropertyBag.Get<bool>(Constants.Parameters.DisableScanningMappedNetworkDrivesForFullScan);
        set => this.PropertyBag.Set(Constants.Parameters.DisableScanningMappedNetworkDrivesForFullScan, value);
    }
    
    public bool DisableScanningNetworkFiles
    {
        get => this.PropertyBag.Get<bool>(Constants.Parameters.DisableScanningNetworkFiles);
        set => this.PropertyBag.Set(Constants.Parameters.DisableScanningNetworkFiles, value);
    }
    
    public bool DisableIntrusionPreventionSystem
    {
        get => this.PropertyBag.Get<bool>(Constants.Parameters.DisableIntrusionPreventionSystem);
        set => this.PropertyBag.Set(Constants.Parameters.DisableIntrusionPreventionSystem, value);
    }
    
    #endregion
    
    #region UI Lockdown
    
    public bool UILockdown
    {
        get => this.PropertyBag.Get<bool>(Constants.Parameters.UILockdown);
        set => this.PropertyBag.Set(Constants.Parameters.UILockdown, value);
    }
    
    #endregion
    
    #region Threat Default Actions
    
    public ThreatAction LowThreatDefaultAction
    {
        get => this.PropertyBag.Get<ThreatAction>(Constants.Parameters.LowThreatDefaultAction);
        set => this.PropertyBag.Set(Constants.Parameters.LowThreatDefaultAction, value);
    }
    
    public ThreatAction ModerateThreatDefaultAction
    {
        get => this.PropertyBag.Get<ThreatAction>(Constants.Parameters.ModerateThreatDefaultAction);
        set => this.PropertyBag.Set(Constants.Parameters.ModerateThreatDefaultAction, value);
    }
    
    public ThreatAction HighThreatDefaultAction
    {
        get => this.PropertyBag.Get<ThreatAction>(Constants.Parameters.HighThreatDefaultAction);
        set => this.PropertyBag.Set(Constants.Parameters.HighThreatDefaultAction, value);
    }
    
    public ThreatAction SevereThreatDefaultAction
    {
        get => this.PropertyBag.Get<ThreatAction>(Constants.Parameters.SevereThreatDefaultAction);
        set => this.PropertyBag.Set(Constants.Parameters.SevereThreatDefaultAction, value);
    }
    
    public ThreatAction UnknownThreatDefaultAction
    {
        get => this.PropertyBag.Get<ThreatAction>(Constants.Parameters.UnknownThreatDefaultAction);
        set => this.PropertyBag.Set(Constants.Parameters.UnknownThreatDefaultAction, value);
    }
    
    #endregion
    
    #region Cloud Protection
    
    public SubmitSamplesConsentType SubmitSamplesConsent
    {
        get => this.PropertyBag.Get<SubmitSamplesConsentType>(Constants.Parameters.SubmitSamplesConsent);
        set => this.PropertyBag.Set(Constants.Parameters.SubmitSamplesConsent, value);
    }
    
    public MAPSReportingType MAPSReporting
    {
        get => this.PropertyBag.Get<MAPSReportingType>(Constants.Parameters.MAPSReporting);
        set => this.PropertyBag.Set(Constants.Parameters.MAPSReporting, value);
    }
    
    #endregion
    
    public static WindowsDefenderResource Create(string name, Action<IWindowsDefenderResource> setValues)
    {
        var instance = new WindowsDefenderResource(name);
        setValues(instance);
        return instance;
    }
    
    public static WindowsDefenderResource Create(string name, Action<IWindowsDefenderResource> setValues, out WindowsDefenderResource instance)
    {
        instance = new WindowsDefenderResource(name);
        setValues(instance);
        return instance;
    }

    public override string ResourceId => Constants.ResourceId;

    public override bool HasEnsure => false;
}