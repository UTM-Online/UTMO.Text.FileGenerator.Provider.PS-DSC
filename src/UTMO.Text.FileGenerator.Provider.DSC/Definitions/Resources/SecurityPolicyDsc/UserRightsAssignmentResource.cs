namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.SecurityPolicyDsc;

using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.SecurityPolicyDscConstants.UserRightsAssignment;

public class UserRightsAssignmentResource : SecurityPolicyDscBase
{
    public UserRightsAssignmentResource(string name) : base(name)
    {
        this.PropertyBag.Init<string[]>(Constants.Properties.Identity);
        this.PropertyBag.Init(Constants.Properties.Policy);
        this.PropertyBag.Init<bool>(Constants.Properties.Force);
    }
    
    public string[] Identity
    {
        get => this.PropertyBag.Get<string[]>(Constants.Properties.Identity);
        set => this.PropertyBag.Set(Constants.Properties.Identity, value);
    }
    
    public string Policy
    {
        get => this.PropertyBag.Get(Constants.Properties.Policy);
        set => this.PropertyBag.Set(Constants.Properties.Policy, value);
    }
    
    public bool Force
    {
        get => this.PropertyBag.Get<bool>(Constants.Properties.Force);
        set => this.PropertyBag.Set(Constants.Properties.Force, value);
    }
    
    public static UserRightsAssignmentResource Create(string name, Action<UserRightsAssignmentResource> configure)
    {
        var resource = new UserRightsAssignmentResource(name);
        configure(resource);
        return resource;
    }

    public override string ResourceId
    {
        get => Constants.ResourceId;
    }
}