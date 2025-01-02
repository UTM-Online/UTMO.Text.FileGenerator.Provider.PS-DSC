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
    using System.Diagnostics.CodeAnalysis;
    using UTMO.Text.FileGenerator.Attributes;
    using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Constants;

    [SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public abstract partial class DscLcmConfiguration : DscResourceBase
    {
        public sealed override string ResourceTypeName => DscResourceTypeNames.DscLcmConfiguration;

        public sealed override string TemplatePath => "LcmConfiguration";
        
        [MemberName("node_name")]
        public abstract string NodeName { get; }

        public sealed override string ResourceName => this.NodeName;

        // ReSharper disable once MemberCanBeProtected.Global
        public virtual bool Enabled { get; } = true;
        
        // ReSharper disable once MemberCanBeProtected.Global
        public virtual bool IsClientNode { get; } = false;
        
        // ReSharper disable once MemberCanBePrivate.Global
        public List<string> RunAsAccounts { get; } = new();

        public override bool GenerateManifest => true;

        [MemberName("partial_configs")]
        // ReSharper disable once MemberCanBePrivate.Global
        public List<DscConfiguration> DscConfiguration { get; } = new();
        
        [MemberName("lcm_settings")]
        public virtual DscLcmSettings LcmSettings { get; } = new();
        
        [MemberName("pull_server_web")]
        protected abstract DscLcmWebResource PullServerWebResource { get; }
        
        [MemberName("resource_repository_web")]
        protected abstract DscLcmWebResource ResourceRepositoryWebResource { get; }
        
        [MemberName("report_server_web")]
        protected abstract DscLcmWebResource ReportServerWebResource { get; }
        
        protected DscLcmConfiguration AddConfiguration<T>() where T : DscConfiguration, new()
        {
            var resource = new T();
            this.DscConfiguration.Add(resource);
            return this;
        }

        public sealed override Task<object?> ToManifest()
        {
            object? manifest =  new
                       {
                           this.NodeName,
                           this.Enabled,
                           this.IsClientNode,
                           this.RunAsAccounts,
                           PartialConfigs = this.DscConfiguration.Select(x => x.FullName).ToList(),
                       };
            
            return Task.FromResult<object?>(manifest);
        }
    }
}