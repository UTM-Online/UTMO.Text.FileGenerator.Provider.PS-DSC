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
    
    public int QuarantinePurgeItemsAfterDelay
    {
        get => this.PropertyBag.Get<int>(Constants.Parameters.QuarantinePurgeItemsAfterDelay);
        set => this.PropertyBag.Set(Constants.Parameters.QuarantinePurgeItemsAfterDelay, value);
    }
    
    public ScheduleDays ScheduleDay
    {
        get => this.PropertyBag.Get<ScheduleDays>(Constants.Parameters.RemediationScheduleDay);
        set => this.PropertyBag.Set(Constants.Parameters.RemediationScheduleDay, value);
    }
    
    public DateTime RemediationScheduleTime
    {
        get => this.PropertyBag.Get<DateTime>(Constants.Parameters.RemediationScheduleTime);
        set => this.PropertyBag.Set(Constants.Parameters.RemediationScheduleTime, value);
    }
    
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
    
    public string ScanParameters
    {
        get => this.PropertyBag.Get<string>(Constants.Parameters.ScanParameters);
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