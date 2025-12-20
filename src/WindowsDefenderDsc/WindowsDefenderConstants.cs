namespace UTMO.Text.FileGenerator.Provider.DSC.WindowsDefender;

public static class WindowsDefenderConstants
{
    public static class WindowsDefender
    {
        public const string ResourceId = "WindowsDefender";
        
        public static class Parameters
        {
            public const string IsSingleInstance = "IsSingleInstance";
            
            // Exclusions
            public const string ExclusionPath = "ExclusionPath";
            public const string ExclusionExtension = "ExclusionExtension";
            public const string ExclusionProcess = "ExclusionProcess";
            
            // Threat ID Default Actions
            public const string ThreatIDDefaultAction_Ids = "ThreatIDDefaultAction_Ids";
            public const string ThreatIDDefaultAction_Actions = "ThreatIDDefaultAction_Actions";
            
            // Quarantine
            public const string QuarantinePurgeItemsAfterDelay = "QuarantinePurgeItemsAfterDelay";
            
            // Real-time protection
            public const string RealTimeScanDirection = "RealTimeScanDirection";
            
            // Remediation
            public const string RemediationScheduleDay = "RemediationScheduleDay";
            public const string RemediationScheduleTime = "RemediationScheduleTime";
            
            // Reporting timeouts
            public const string ReportingAdditionalActionTimeOut = "ReportingAdditionalActionTimeOut";
            public const string ReportingNonCriticalTimeOut = "ReportingNonCriticalTimeOut";
            public const string ReportingCriticalFailureTimeOut = "ReportingCriticalFailureTimeOut";
            
            // Scan settings
            public const string ScanAvgCPULoadFactor = "ScanAvgCPULoadFactor";
            public const string CheckForSignaturesBeforeRunningScan = "CheckForSignaturesBeforeRunningScan";
            public const string ScanPurgeItemsAfterDelay = "ScanPurgeItemsAfterDelay";
            public const string ScanOnlyIfIdleEnabled = "ScanOnlyIfIdleEnabled";
            public const string ScanParameters = "ScanParameters";
            public const string ScanScheduleDay = "ScanScheduleDay";
            public const string ScanScheduleTime = "ScanScheduleTime";
            public const string ScanScheduleQuickScanTime = "ScanScheduleQuickScanTime";
            public const string RandomizeScheduleTaskTimes = "RandomizeScheduleTaskTimes";
            
            // Signature updates
            public const string SignatureFirstAuGracePeriod = "SignatureFirstAuGracePeriod";
            public const string SignatureAuGracePeriod = "SignatureAuGracePeriod";
            public const string SignatureDefinitionUpdateFileSharesSources = "SignatureDefinitionUpdateFileSharesSources";
            public const string SignatureDisableUpdateOnStartupWithoutEngine = "SignatureDisableUpdateOnStartupWithoutEngine";
            public const string SignatureFallbackOrder = "SignatureFallbackOrder";
            public const string SignatureScheduleDay = "SignatureScheduleDay";
            public const string SignatureScheduleTime = "SignatureScheduleTime";
            public const string SignatureUpdateCatchupInterval = "SignatureUpdateCatchupInterval";
            public const string SignatureUpdateInterval = "SignatureUpdateInterval";
            
            // Disable features
            public const string DisableRealtimeMonitoring = "DisableRealtimeMonitoring";
            public const string DisableBehaviorMonitoring = "DisableBehaviorMonitoring";
            public const string DisableBlockAtFirstSeen = "DisableBlockAtFirstSeen";
            public const string DisableIOAVProtection = "DisableIOAVProtection";
            public const string DisablePrivacyMode = "DisablePrivacyMode";
            public const string DisableScriptScanning = "DisableScriptScanning";
            public const string DisableArchiveScanning = "DisableArchiveScanning";
            public const string DisableCatchupFullScan = "DisableCatchupFullScan";
            public const string DisableCatchupQuickScan = "DisableCatchupQuickScan";
            public const string DisableEmailScanning = "DisableEmailScanning";
            public const string DisableRemovableDriveScanning = "DisableRemovableDriveScanning";
            public const string DisableRestorePoint = "DisableRestorePoint";
            public const string DisableScanningMappedNetworkDrivesForFullScan = "DisableScanningMappedNetworkDrivesForFullScan";
            public const string DisableScanningNetworkFiles = "DisableScanningNetworkFiles";
            public const string DisableIntrusionPreventionSystem = "DisableIntrusionPreventionSystem";
            
            // UI Lockdown
            public const string UILockdown = "UILockdown";
            
            // Threat default actions
            public const string LowThreatDefaultAction = "LowThreatDefaultAction";
            public const string ModerateThreatDefaultAction = "ModerateThreatDefaultAction";
            public const string HighThreatDefaultAction = "HighThreatDefaultAction";
            public const string SevereThreatDefaultAction = "SevereThreatDefaultAction";
            public const string UnknownThreatDefaultAction = "UnknownThreatDefaultAction";
            
            // Cloud protection
            public const string SubmitSamplesConsent = "SubmitSamplesConsent";
            public const string MAPSReporting = "MAPSReporting";
        }
    }
}

