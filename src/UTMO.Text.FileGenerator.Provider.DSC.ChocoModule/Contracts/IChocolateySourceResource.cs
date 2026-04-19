namespace UTMO.Text.FileGenerator.Provider.DSC.ChocoModule.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

/// <summary>
/// Contract for the ChocolateySource DSC resource.
/// Registers, updates, or removes Chocolatey package sources/feeds.
/// </summary>
public interface IChocolateySourceResource : IDscResourceConfig
{
    string SourceName { get; set; }

    string? Source { get; set; }

    bool? Disabled { get; set; }

    bool? ByPassProxy { get; set; }

    bool? SelfService { get; set; }

    int? Priority { get; set; }

    string? Username { get; set; }

    string? Password { get; set; }
}


