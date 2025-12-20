﻿namespace UTMO.Text.FileGenerator.Provider.DSC.WindowsDefender.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

public interface IWindowsDefenderResource
{
    // Exclusions
    string[] ExclusionPath { get; set; }
    string[] ExclusionExtension { get; set; }
    string[] ExclusionProcess { get; set; }
    
    // Threat ID Default Actions
    ulong[] ThreatIDDefaultAction_Ids { get; set; }
    ThreatAction[] ThreatIDDefaultAction_Actions { get; set; }
    
    // Quarantine
    int QuarantinePurgeItemsAfterDelay { get; set; }
    
    // Real-time protection
    ScanDirection RealTimeScanDirection { get; set; }
    
    // Remediation Schedule
    ScheduleDays RemediationScheduleDay { get; set; }
    DateTime RemediationScheduleTime { get; set; }
    
    // Reporting timeouts
    int ReportingAdditionalActionTimeOut { get; set; }
    int ReportingNonCriticalTimeOut { get; set; }
    int ReportingCriticalFailureTimeOut { get; set; }
    
    // Scan settings
    int ScanAvgCPULoadFactor { get; set; }
    bool CheckForSignaturesBeforeRunningScan { get; set; }
    int ScanPurgeItemsAfterDelay { get; set; }
    bool ScanOnlyIfIdleEnabled { get; set; }
    ScanType ScanParameters { get; set; }
    ScheduleDays ScanScheduleDay { get; set; }
    DateTime ScanScheduleTime { get; set; }
    DateTime ScanScheduleQuickScanTime { get; set; }
    bool RandomizeScheduleTaskTimes { get; set; }
    
    // Signature updates
    int SignatureFirstAuGracePeriod { get; set; }
    int SignatureAuGracePeriod { get; set; }
    string SignatureDefinitionUpdateFileSharesSources { get; set; }
    bool SignatureDisableUpdateOnStartupWithoutEngine { get; set; }
    string SignatureFallbackOrder { get; set; }
    ScheduleDays SignatureScheduleDay { get; set; }
    DateTime SignatureScheduleTime { get; set; }
    int SignatureUpdateCatchupInterval { get; set; }
    int SignatureUpdateInterval { get; set; }
    
    // Disable features
    bool DisableRealtimeMonitoring { get; set; }
    bool DisableBehaviorMonitoring { get; set; }
    bool DisableBlockAtFirstSeen { get; set; }
    bool DisableIOAVProtection { get; set; }
    bool DisablePrivacyMode { get; set; }
    bool DisableScriptScanning { get; set; }
    bool DisableArchiveScanning { get; set; }
    bool DisableCatchupFullScan { get; set; }
    bool DisableCatchupQuickScan { get; set; }
    bool DisableEmailScanning { get; set; }
    bool DisableRemovableDriveScanning { get; set; }
    bool DisableRestorePoint { get; set; }
    bool DisableScanningMappedNetworkDrivesForFullScan { get; set; }
    bool DisableScanningNetworkFiles { get; set; }
    bool DisableIntrusionPreventionSystem { get; set; }
    
    // UI Lockdown
    bool UILockdown { get; set; }
    
    // Threat default actions
    ThreatAction LowThreatDefaultAction { get; set; }
    ThreatAction ModerateThreatDefaultAction { get; set; }
    ThreatAction HighThreatDefaultAction { get; set; }
    ThreatAction SevereThreatDefaultAction { get; set; }
    ThreatAction UnknownThreatDefaultAction { get; set; }
    
    // Cloud protection
    SubmitSamplesConsentType SubmitSamplesConsent { get; set; }
    MAPSReportingType MAPSReporting { get; set; }

    DscConfigurationItem AddDependency<T>(T resource) where T : DscConfigurationItem;
}