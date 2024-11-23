namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

public interface IGroupResource : IDscResourceConfig
{
    string GroupName { get; set; }

    string[] MembersToInclude { get; set; }
}