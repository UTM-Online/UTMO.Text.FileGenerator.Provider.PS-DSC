﻿// // ***********************************************************************
// // Assembly         : UTMO.Text.FileGenerator.Provider.DSC
// // Author           : Josh Irwin (joirwi)
// // Created          : 11/22/2023
// //
// // Last Modified By : Josh Irwin (joirwi)
// // Last Modified On : 11/22/2023 2:36 PM
// // ***********************************************************************
// // <copyright file="DscComputer.cs" company="Joshua S. Irwin">
// //     Copyright (c) 2026 Joshua S. Irwin. All rights reserved.
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
        
        [TemplateProperty]
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

        [TemplateProperty]
        [MemberName("partial_configs")]
        // ReSharper disable once MemberCanBePrivate.Global
        public List<DscConfiguration> DscConfiguration { get; } = new();
        
        [TemplateProperty]
        [MemberName("lcm_settings")]
        public virtual DscLcmSettings LcmSettings { get; } = new();
        
        [TemplateProperty]
        [MemberName("pull_server_web")]
        public abstract DscLcmWebResource PullServerWebResource { get; }
        
        [TemplateProperty]
        [MemberName("resource_repository_web")]
        public abstract DscLcmWebResource ResourceRepositoryWebResource { get; }
        
        [TemplateProperty]
        [MemberName("report_server_web")]
        public abstract DscLcmWebResource ReportServerWebResource { get; }
        
        [IgnoreMember]
        public virtual List<DscConfigurationItem> NodeConfigurations { get; } = new();
        
        [TemplateProperty]
        [MemberName("has_local_configuration")]
        public bool HasLocalConfiguration => this.NodeConfigurations.Count != 0;
        
        protected DscLcmConfiguration AddConfiguration<T>() where T : DscConfiguration, new()
        {
            var resource = new T();
            this.DscConfiguration.Add(resource);
            return this;
        }

        public sealed override Task<object?> ToManifest()
        {
            object? manifest;

            if (!this.HasLocalConfiguration)
            {
                manifest = new
                           {
                               this.NodeName,
                               this.Enabled,
                               this.IsClientNode,
                               this.RunAsAccounts,
                               PartialConfigs = this.DscConfiguration.Select(x => x.FullName).ToList(),
                           };
            }
            else
            {
                manifest = new
                           {
                               this.NodeName,
                               this.Enabled,
                               this.IsClientNode,
                               this.RunAsAccounts,
                               PartialConfigs = this.DscConfiguration.Select(x => x.FullName).Concat([this.NodeName]).ToList(),
                           };
            }

            return Task.FromResult<object?>(manifest);
        }
    }
}