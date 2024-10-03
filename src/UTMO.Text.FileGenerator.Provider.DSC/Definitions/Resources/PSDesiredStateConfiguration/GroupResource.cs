namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.PSDesiredStateConfiguration;

using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.PSDesiredStateConfigurationConstants.Group;

public class GroupResource : PSDesiredStateConfigurationBase
{
    public GroupResource(string name) : base(name)
    {
        this.PropertyBag.Add(Constants.Properties.GroupName, string.Empty);
        this.PropertyBag.Add(Constants.Properties.MembersToInclude, Array.Empty<string>());
    }

    public override string ResourceId => Constants.ResourceId;
}