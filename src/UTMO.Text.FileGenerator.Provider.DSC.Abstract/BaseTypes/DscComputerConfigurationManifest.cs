using UTMO.Text.FileGenerator.Abstract.Contracts;

namespace UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

public class DscComputerConfigurationManifest : IManifest
{
    public required string NodeName { get; init; }

    public bool Enabled { get; init; }

    public bool IsClientNode { get; init; }

    public List<string> RunAsAccounts { get; init; } = new();

    public List<string> PartialConfigs { get; init; } = new();
}
