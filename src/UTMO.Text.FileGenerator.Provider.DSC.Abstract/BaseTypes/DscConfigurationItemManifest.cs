using UTMO.Text.FileGenerator.Abstract.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Enums;

namespace UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

public class DscConfigurationItemManifest : IManifest
{
    public required string ResourceId { get; init; }

    public required string Name { get; init; }

    public required DscEnsure Ensure { get; init; }

    public List<string> DependsOn { get; init; } = new();

    public required string DependencyName { get; init; }
}
