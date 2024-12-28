namespace UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

using System.Diagnostics.CodeAnalysis;
using UTMO.Text.FileGenerator.Models;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Enums;

public abstract partial class DscLcmConfiguration
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class DscLcmSettings : SubTemplateResourceBase
    {
        public int RefreshFrequencyMins { get; set; } = 30;
            
        public int ConfigurationModeFrequencyMins { get; set; } = 15;
            
        public DscLcmConfigurationMode ConfigurationMode { get; set; } = DscLcmConfigurationMode.ApplyAndAutoCorrect;
            
        public bool RebootNodeIfNeeded { get; set; } = true;
            
        public bool AllowModuleOverwrite { get; set; } = true;
            
        public DscMode RefreshMode { get; set; } = DscMode.Pull;

        public sealed override bool GenerateManifest => false;

        public override Task<Dictionary<string, object>> ToTemplateContext()
        {
            var context = new Dictionary<string,object>
                          {
                              { "RefreshFrequencyMins", this.RefreshFrequencyMins },
                              { "ConfigurationModeFrequencyMins", this.ConfigurationModeFrequencyMins },
                              { "ConfigurationMode", this.ConfigurationMode },
                              { "RebootNodeIfNeeded", $"${this.RebootNodeIfNeeded.ToString().ToLower()}" },
                              { "AllowModuleOverwrite", $"${this.AllowModuleOverwrite.ToString().ToLower()}" },
                              { "RefreshMode", this.RefreshMode },
                          };
                
            return Task.FromResult(context);
        }
    }
}