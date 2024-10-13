namespace UTMO.Text.FileGenerator.Provider.DSC.Constants;

public static class ComputerManagementDscConstants
{
    public static class ScheduledTask
    {
        public const string ResourceId = "ScheduledTask";
        
        public static class Properties
        {
            public const string TaskName = "TaskName";
            
            public const string TaskPath = "TaskPath";
            
            public const string Enable = "Enable";
            
            public const string Description = "Description";
            
            public const string ActionExecutable = "ActionExecutable";
            
            public const string ActionArguments = "ActionArguments";
            
            public const string ScheduleType = "ScheduleType";
            
            public const string ActionWorkingPath = "ActionWorkingPath";
            
            public const string Priority = "Priority";
            
            public const string BuiltInAccount = "BuiltInAccount";
            
            public const string RunLevel = "RunLevel";
        }
    }
}