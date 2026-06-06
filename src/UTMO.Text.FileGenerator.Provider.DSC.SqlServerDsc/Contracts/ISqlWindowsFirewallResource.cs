namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

public interface ISqlWindowsFirewallResource : IDscResourceConfig
{
    string SourcePath { get; set; }

    string Features { get; set; }

    string InstanceName { get; set; }

    string SourceCredential { get; set; }
}
