using UTMO.Text.FileGenerator.Abstract.Contracts;

namespace UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

public class RequiredModuleManifest : IManifest
{
    public required string Name { get; init; }

    public required string Version { get; init; }

    public bool IsPrivate { get; init; }

    public bool AllowClobber { get; init; }

    public bool UseAlternateFormat { get; init; }

    public string? AlternateVersion  { get; init; }
}
