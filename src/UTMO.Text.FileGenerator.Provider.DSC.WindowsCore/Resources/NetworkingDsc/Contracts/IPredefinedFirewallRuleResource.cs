namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.NetworkingDsc.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

public interface IPredefinedFirewallRuleResource : IDscResourceConfig
{
    bool EnableRule { get; set; }

    string RuleName { get; set; }
}