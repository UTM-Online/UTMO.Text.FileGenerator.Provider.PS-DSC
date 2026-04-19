namespace UTMO.Text.FileGenerator.Provider.DSC.ChocoModule.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

/// <summary>
/// Contract for the ChocolateyFeature DSC resource.
/// Enables or disables Chocolatey features.
/// </summary>
public interface IChocolateyFeatureResource : IDscResourceConfig
{
    string FeatureName { get; set; }
}


