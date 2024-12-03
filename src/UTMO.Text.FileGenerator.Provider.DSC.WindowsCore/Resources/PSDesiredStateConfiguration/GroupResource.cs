namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration;

using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.BaseDefinitions;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration.Contracts;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Constants.PSDesiredStateConfigurationConstants.Group;

public class GroupResource : PSDesiredStateConfigurationBase, IGroupResource
{
    private GroupResource(string name) : base(name)
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
    
    public static GroupResource Create(string name, Action<IGroupResource> configure)
    {
        var resource = new GroupResource(name);
        configure(resource);
        return resource;
    }
    
    public static GroupResource Create(string name, Action<IGroupResource> configure, out GroupResource resource)
    {
        resource = new GroupResource(name);
        configure(resource);
        return resource;
    }

    public override string ResourceId
    {
        get => Constants.ResourceId;
    }
}