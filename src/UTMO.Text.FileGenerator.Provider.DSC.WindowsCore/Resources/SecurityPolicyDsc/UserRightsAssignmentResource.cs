namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.SecurityPolicyDsc;

using System.Text.RegularExpressions;
using Text.FileGenerator.Abstract;
using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.BaseDefinitions;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.SecurityPolicyDsc.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Constants.SecurityPolicyDscConstants.UserRightsAssignment;

public class UserRightsAssignmentResource : SecurityPolicyDscBase, IUserRightsAssignmentResource
{
    private UserRightsAssignmentResource(string name) : base(name)
    {
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
    
    public static UserRightsAssignmentResource Create(string name, Action<IUserRightsAssignmentResource> configure)
    {
        var resource = new UserRightsAssignmentResource(name);
        configure(resource);
        return resource;
    }
    
    public static UserRightsAssignmentResource Create(string name, Action<IUserRightsAssignmentResource> configure, out UserRightsAssignmentResource resource)
    {
        resource = new UserRightsAssignmentResource(name);
        configure(resource);
        return resource;
    }
    
    public override Task<List<ValidationFailedException>> Validate()
    {
        var errors = this.ValidationBuilder()
            .ValidateStringNotNullOrEmpty(this.Policy, nameof(this.Policy))
            .ValidateArrayNotNullOrEmpty(this.Identity, nameof(this.Identity))
            .errors;
        
        if (this.Identity.Any(a => this.UnescapedTokenMatcher.IsMatch(a)))
        {
            errors.Add(new ValidationFailedException(this.Name, nameof(UserRightsAssignmentResource), ValidationFailureType.InvalidConfiguration, "The $ character must be escaped  with the ` character in the Identity property."));
        }

        return Task.FromResult(errors);
    }

    public override string ResourceId => Constants.ResourceId;

    private Regex UnescapedTokenMatcher => new(@"^(?:.+?)(?:[^`])\$(?:.+?)", RegexOptions.Compiled);
}