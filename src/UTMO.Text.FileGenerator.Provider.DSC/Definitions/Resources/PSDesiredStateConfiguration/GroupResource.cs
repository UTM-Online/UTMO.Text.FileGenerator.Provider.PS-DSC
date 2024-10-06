namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.PSDesiredStateConfiguration;

using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.PSDesiredStateConfigurationConstants.Group;

public class GroupResource : PSDesiredStateConfigurationBase
{
    public GroupResource(string name) : base(name)
    {
        this.PropertyBag.Init(Constants.Properties.GroupName);
        this.PropertyBag.Init<string[]>(Constants.Properties.MembersToInclude);
    }
    
    public string GroupName
    {
        get => this.PropertyBag.Get(Constants.Properties.GroupName);
        set => this.PropertyBag.Set(Constants.Properties.GroupName, value);
    }
    
    public string[] MembersToInclude
    {
        get => this.PropertyBag.Get<string[]>(Constants.Properties.MembersToInclude);
        set => this.PropertyBag.Set(Constants.Properties.MembersToInclude, value);
    }
    
    public static GroupResource Create(string name, Action<GroupResource> configure)
    {
        var resource = new GroupResource(name);
        configure(resource);
        return resource;
    }

    public override string ResourceId
    {
        get => Constants.ResourceId;
    }
}