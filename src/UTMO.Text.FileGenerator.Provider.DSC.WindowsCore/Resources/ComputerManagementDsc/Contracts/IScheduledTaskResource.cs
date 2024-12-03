namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.ComputerManagementDsc.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.ComputerManagementDsc.Enums;

public interface IScheduledTaskResource : IDscResourceConfig
{
    string TaskDescription { get; set; }

    int Priority { get; set; }

    string ActionArguments { get; set; }

    string ActionExecutable { get; set; }

    string ActionWorkingPath { get; set; }

    ScheduleTaskRunLevel RunLevel { get; set; }

    ScheduleTaskScheduleType ScheduleType { get; set; }

    string TaskName { get; set; }

    string TaskPath { get; set; }

    string BuiltInAccount { get; set; }

    bool Enable { get; set; }

    TimeSpan RepeatInterval { get; set; }

    TimeSpan RepetitionDuration { get; set; }

    string Description { get; set; }
}