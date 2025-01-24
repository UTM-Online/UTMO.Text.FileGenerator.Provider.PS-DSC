namespace UTMO.Text.FileGenerator.Provider.DSC.WindowsDefender.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

public interface IWindowsDefenderResource
{
    string[] ExclusionPath { get; set; }

    string[] ExclusionExtension { get; set; }

    string[] ExclusionProcess { get; set; }

    int QuarantinePurgeItemsAfterDelay { get; set; }

    ScheduleDays ScheduleDay { get; set; }

    DateTime RemediationScheduleTime { get; set; }

    int ReportingAdditionalActionTimeOut { get; set; }

    int ReportingNonCriticalTimeOut { get; set; }

    int ReportingCriticalFailureTimeOut { get; set; }

    int ScanAvgCPULoadFactor { get; set; }

    bool CheckForSignaturesBeforeRunningScan { get; set; }

    int ScanPurgeItemsAfterDelay { get; set; }

    bool ScanOnlyIfIdleEnabled { get; set; }

    string ScanParameters { get; set; }

    ScheduleDays ScanScheduleDay { get; set; }

    DateTime ScanScheduleTime { get; set; }

    DateTime ScanScheduleQuickScanTime { get; set; }

    bool RandomizeScheduleTaskTimes { get; set; }

    DscConfigurationItem AddDependency<T>(T resource) where T : DscConfigurationItem;
}