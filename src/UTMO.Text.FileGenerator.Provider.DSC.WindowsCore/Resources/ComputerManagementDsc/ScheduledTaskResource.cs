namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.ComputerManagementDsc;

using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.BaseDefinitions;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.ComputerManagementDsc.Enums;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Constants.ComputerManagementDscConstants.ScheduledTask;

public class ScheduledTaskResource : ComputerManagementDscBase
{
    private ScheduledTaskResource(string name) : base(name)
    {
        this.PropertyBag.Init(Constants.Properties.ActionArguments);
        this.PropertyBag.Init(Constants.Properties.ActionExecutable);
        this.PropertyBag.Init<ScheduleTaskScheduleType>(Constants.Properties.ScheduleType);
        this.PropertyBag.Init(Constants.Properties.TaskName);
        this.PropertyBag.Init(Constants.Properties.TaskPath);
        this.PropertyBag.Init<bool>(Constants.Properties.Enable);
    }

    public string TaskDescription
    {
        get => this.PropertyBag.Get(Constants.Properties.Description);
        
        set => this.PropertyBag.Set(Constants.Properties.Description, value);
    }
    
    public int Priority
    {
        get => this.PropertyBag.Get<int>(Constants.Properties.Priority);
        
        set => this.PropertyBag.Set(Constants.Properties.Priority, value);
    }
    
    public string ActionArguments
    {
        get => this.PropertyBag.Get(Constants.Properties.ActionArguments);
        
        set => this.PropertyBag.Set(Constants.Properties.ActionArguments, value);
    }
    
    public string ActionExecutable
    {
        get => this.PropertyBag.Get(Constants.Properties.ActionExecutable);
        
        set => this.PropertyBag.Set(Constants.Properties.ActionExecutable, value);
    }
    
    public string ActionWorkingPath
    {
        get => this.PropertyBag.Get(Constants.Properties.ActionWorkingPath);
        
        set => this.PropertyBag.Set(Constants.Properties.ActionWorkingPath, value);
    }
    
    public ScheduleTaskRunLevel RunLevel
    {
        get => this.PropertyBag.Get<ScheduleTaskRunLevel>(Constants.Properties.RunLevel);
        
        set => this.PropertyBag.Set(Constants.Properties.RunLevel, value);
    }
    
    public ScheduleTaskScheduleType ScheduleType
    {
        get => this.PropertyBag.Get<ScheduleTaskScheduleType>(Constants.Properties.ScheduleType);
        
        set => this.PropertyBag.Set(Constants.Properties.ScheduleType, value);
    }
    
    public string TaskName
    {
        get => this.PropertyBag.Get(Constants.Properties.TaskName);
        
        set => this.PropertyBag.Set(Constants.Properties.TaskName, value);
    }
    
    public string TaskPath
    {
        get => this.PropertyBag.Get(Constants.Properties.TaskPath);
        
        set => this.PropertyBag.Set(Constants.Properties.TaskPath, value);
    }
    
    public string BuiltInAccount
    {
        get => this.PropertyBag.Get(Constants.Properties.BuiltInAccount);
        
        set => this.PropertyBag.Set(Constants.Properties.BuiltInAccount, value);
    }
    
    public bool Enable
    {
        get => this.PropertyBag.Get<bool>(Constants.Properties.Enable);
        
        set => this.PropertyBag.Set(Constants.Properties.Enable, value);
    }
    
    public TimeSpan RepeatInterval
    {
        get => this.PropertyBag.Get<TimeSpan>(Constants.Properties.RepeatInterval);
        
        set => this.PropertyBag.Set(Constants.Properties.RepeatInterval, value);
    }
    
    public TimeSpan RepetitionDuration
    {
        get => this.PropertyBag.Get<TimeSpan>(Constants.Properties.RepetitionDuration);
        
        set => this.PropertyBag.Set(Constants.Properties.RepetitionDuration, value);
    }
    
    public static ScheduledTaskResource Create(string name, Action<ScheduledTaskResource> configure)
    {
        var resource = new ScheduledTaskResource(name);
        configure(resource);
        return resource;
    }
    
    public static ScheduledTaskResource Create(string name, Action<ScheduledTaskResource> configure, out ScheduledTaskResource resourceRef)
    {
        var resource = new ScheduledTaskResource(name);
        configure(resource);
        resourceRef = resource;
        return resource;
    }

    public override string ResourceId => Constants.ResourceId;
}