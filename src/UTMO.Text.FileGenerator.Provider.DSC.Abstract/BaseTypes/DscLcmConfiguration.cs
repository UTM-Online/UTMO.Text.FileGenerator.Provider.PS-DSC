// // ***********************************************************************
// // Assembly         : UTMO.Text.FileGenerator.Provider.DSC
// // Author           : Josh Irwin (joirwi)
// // Created          : 11/22/2023
// //
// // Last Modified By : Josh Irwin (joirwi)
// // Last Modified On : 11/22/2023 2:36 PM
// // ***********************************************************************
// // <copyright file="DscComputer.cs" company="Microsoft Corp">
// //     Copyright (c) Microsoft Corporation. All rights reserved.
// // </copyright>
// // <summary></summary>
// // ***********************************************************************

namespace UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes
{
    using Enums;
    using UTMO.Text.FileGenerator.Attributes;
    using UTMO.Text.FileGenerator.Extensions;

    public abstract class DscLcmConfiguration : DscResourceBase
    {
        public sealed override string ResourceTypeName => "/DSC/Computers";

        public sealed override string TemplatePath => "LcmConfiguration";
        
        [MemberName("node_name")]
        public abstract string NodeName { get; }

        public sealed override string ResourceName => this.NodeName;

        public virtual bool Enabled { get; } = true;
        
        public virtual bool IsClientNode { get; } = false;
        
        public List<string> RunAsAccounts { get; } = new();
        
        [MemberName("partial_configs")]
        public List<DscConfiguration> DscConfiguration { get; } = new();
        
        [MemberName("lcm_settings")]
        public virtual DscLcmSettings LcmSettings { get; } = new();

        [MemberName("web_resources")]
        public Dictionary<DscWebResourceTypes, DscLcmWebResource> WebResources { get; } = new();
        
        public Dictionary<DscWebResourceTypes, DscLcmWebResource> AddWebResource(DscWebResourceTypes type, Action<DscLcmWebResource> resourceDefinition)
        {
            var resource = new DscLcmWebResource();
            resourceDefinition(resource);
            this.WebResources.Add(type, resource);
            return this.WebResources;
        }
        
        protected DscLcmConfiguration AddConfiguration<T>() where T : DscConfiguration, new()
        {
            var resource = new T();
            this.DscConfiguration.Add(resource);
            return this;
        }

        public sealed override dynamic? ToManifest()
        {
            return new
                       {
                           NodeName = this.NodeName,
                           Enabled = this.Enabled,
                           IsClientNode = this.IsClientNode,
                           RunAsAccounts = this.RunAsAccounts,
                           PartialConfigs = this.DscConfiguration.Select(x => x.FullName).ToList<string>()
                       };
        }
        
        public class DscLcmSettings : RelatedTemplateResourceBase
        {
            public int RefreshFrequencyMins { get; set; } = 30;
            
            public int ConfigurationModeFrequencyMins { get; set; } = 15;
            
            public DscLcmConfigurationMode ConfigurationMode { get; set; } = DscLcmConfigurationMode.ApplyAndAutoCorrect;
            
            public bool RebootNodeIfNeeded { get; set; } = true;
            
            public bool AllowModuleOverwrite { get; set; } = true;
            
            public DscMode RefreshMode { get; set; } = DscMode.Pull;

            public sealed override bool GenerateManifest => false;

            public override Dictionary<string, object> ToTemplateContext()
            {
                return new()
                           {
                               { "RefreshFrequencyMins", this.RefreshFrequencyMins },
                               { "ConfigurationModeFrequencyMins", this.ConfigurationModeFrequencyMins },
                               { "ConfigurationMode", this.ConfigurationMode },
                               { "RebootNodeIfNeeded", $"${this.RebootNodeIfNeeded}" },
                               { "AllowModuleOverwrite", $"${this.AllowModuleOverwrite}" },
                               { "RefreshMode", this.RefreshMode }
                           };
            }
        }
    }
}