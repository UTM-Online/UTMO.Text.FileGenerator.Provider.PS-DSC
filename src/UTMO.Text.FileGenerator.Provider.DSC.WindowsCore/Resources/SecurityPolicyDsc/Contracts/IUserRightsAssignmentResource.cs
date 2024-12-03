namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.SecurityPolicyDsc.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

public interface IUserRightsAssignmentResource : IDscResourceConfig
{
    string[] Identity { get; set; }

    string Policy { get; set; }

    bool Force { get; set; }
}