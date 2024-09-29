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

namespace UTMO.Text.FileGenerator.Provider.DSC.Resources
{
    using Abstract;
    using Attributes;
    using BaseTypes;
    using Extensions;
    using SubResources;

    public abstract class DscComputer : DscResourceBase
    {
        public sealed override string ResourceTypeName => "/DSC/Computers";

        public sealed override string TemplatePath => "LcmConfiguration";
        
        [MemberName("node_name")]
        public abstract string NodeName { get; }

        public override string ResourceName => this.NodeName;

        public virtual bool Enabled { get; } = true;
        
        public virtual bool IsClientNode { get; } = false;
        
        public List<string> RunAsAccounts { get; } = new List<string>();
        
        [MemberName("required_modules")]
        public List<Dictionary<string,object>> RequiredModules => this.ComputeRequiredModules();
        
        [MemberName("partial_configs")]
        public List<DscConfiguration> DscConfiguration { get; } = new List<DscConfiguration>();
        
        public NodeNetworkSettings NodeNetworkSettings { get; } = new NodeNetworkSettings();
        
        protected DscComputer AddConfiguration<T>() where T : DscConfiguration, new()
        {
            var resource = new T();
            this.DscConfiguration.Add(resource);
            return this;
        }
        
        private List<Dictionary<string, object>> ComputeRequiredModules()
        {
            var requiredModules = new Dictionary<string, RequiredModule>();
            
            foreach (var dscConfiguration in this.DscConfiguration)
            {
                foreach (var requiredModule in dscConfiguration.RequiredModules)
                {
                    requiredModules.AddOrUpdate(requiredModule.ModuleName, requiredModule);
                }
            }

            return requiredModules.ToTemplateContext();
        }

        public sealed override dynamic? ToManifest()
        {
            return new
                       {
                           NodeName = this.NodeName,
                           Enabled = this.Enabled,
                           IsClientNode = this.IsClientNode,
                           RunAsAccounts = this.RunAsAccounts,
                           PartialConfigs = this.DscConfiguration.Select(x => x.FullName).ToList()
                       };
        }
    }
}