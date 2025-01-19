namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

public interface IWindowsFeatureResource : IDscResourceConfig
{
    string FeatureName { get; set; }
    
    string Source { get; set; }
}