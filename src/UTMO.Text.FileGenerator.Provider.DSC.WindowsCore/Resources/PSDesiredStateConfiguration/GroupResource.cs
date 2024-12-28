namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.BaseDefinitions;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Constants.PSDesiredStateConfigurationConstants.Group;

public class GroupResource : PSDesiredStateConfigurationBase, IGroupResource
{
    private GroupResource(string name) : base(name)
    {
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
    
    public override Task<List<ValidationFailedException>> Validate()
    {
        var errors = this.ValidationBuilder()
            .ValidateStringNotNullOrEmpty(this.GroupName, nameof(this.GroupName))
            .errors;

        return Task.FromResult(errors);
    }

    public override string ResourceId => Constants.ResourceId;
}