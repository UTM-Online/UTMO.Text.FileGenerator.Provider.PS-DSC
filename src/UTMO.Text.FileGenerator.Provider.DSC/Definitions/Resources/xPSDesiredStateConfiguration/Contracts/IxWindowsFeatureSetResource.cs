namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.xPSDesiredStateConfiguration.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

public interface IxWindowsFeatureSetResource : IDscResourceConfig
{
    string[] FeatureName { get; set; }

    string Source { get; set; }

    bool IncludeAllSubFeature { get; set; }

    string LogPath { get; set; }
}