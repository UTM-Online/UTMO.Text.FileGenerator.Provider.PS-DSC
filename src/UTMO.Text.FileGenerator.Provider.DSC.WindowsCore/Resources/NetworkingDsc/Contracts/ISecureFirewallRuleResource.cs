namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.NetworkingDsc.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.NetworkingDsc.Enums;

public interface ISecureFirewallRuleResource : IDscResourceConfig
{
    bool Enabled { get; set; }

    string RuleName { get; set; }

    string Group { get; set; }

    string DisplayName { get; set; }

    FirewallRuleActions Action { get; set; }

    FirewallRuleDirection Direction { get; set; }

    FirewallRuleProtocols Protocol { get; set; }

    string RemoteMachine { get; set; }

    FirewallRuleAuthentication Authentication { get; set; }

    FirewallRuleEncryption Encryption { get; set; }

    string[] LocalPort { get; set; }
}