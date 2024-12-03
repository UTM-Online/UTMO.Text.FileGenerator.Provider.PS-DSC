namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.NetworkingDsc.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.NetworkingDsc.Enums;

public interface ISimpleFirewallRuleResource : IDscResourceConfig
{
    string RuleName { get; set; }

    string DisplayName { get; set; }

    FirewallRuleActions Action { get; set; }

    FirewallRuleDirection Direction { get; set; }

    string[] LocalPort { get; set; }

    string[] RemotePort { get; set; }

    string LocalAddress { get; set; }

    string[] RemoteAddress { get; set; }

    FirewallRuleProtocols Protocol { get; set; }

    string Group { get; set; }

    bool Enabled { get; set; }
}