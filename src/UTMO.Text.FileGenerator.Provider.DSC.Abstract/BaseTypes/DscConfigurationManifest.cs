using UTMO.Text.FileGenerator.Abstract.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Enums;

namespace UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

public class DscConfigurationManifest : IManifest
{
    public required string ConfigurationName { get; init; }

    public required DscMode ConfigurationMode { get; init; }

    public required List<string> RequiredModules { get; init; }

    public required List<DscConfigurationItemManifest> ConfigurationItems { get; init; }
}
