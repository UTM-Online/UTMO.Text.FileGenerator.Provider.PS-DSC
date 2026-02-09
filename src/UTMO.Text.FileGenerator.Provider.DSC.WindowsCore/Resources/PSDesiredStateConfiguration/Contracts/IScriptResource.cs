namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

public interface IScriptResource : IDscResourceConfig
{
    /// <summary>
    /// Gets or sets a script block that returns the current state of the resource.
    /// The script must return a hashtable with at least a Result key.
    /// </summary>
    string GetScript { get; set; }
    
    /// <summary>
    /// Gets or sets a script block that sets the resource to the desired state.
    /// </summary>
    string SetScript { get; set; }
    
    /// <summary>
    /// Gets or sets a script block that tests if the resource is in the desired state.
    /// The script must return $true or $false.
    /// </summary>
    string TestScript { get; set; }
    
    /// <summary>
    /// Gets or sets the credential to use for running the scripts.
    /// </summary>
    string Credential { get; set; }
}

