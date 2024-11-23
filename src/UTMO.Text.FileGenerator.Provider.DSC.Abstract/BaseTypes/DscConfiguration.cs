// // ***********************************************************************
// // Assembly         : UTMO.Text.FileGenerator.Provider.DSC
// // Author           : Josh Irwin (joirwi)
// // Created          : 11/22/2023
// //
// // Last Modified By : Josh Irwin (joirwi)
// // Last Modified On : 11/22/2023 2:39 PM
// // ***********************************************************************
// // <copyright file="DscConfiguration.cs" company="Microsoft Corp">
// //     Copyright (c) Microsoft Corporation. All rights reserved.
// // </copyright>
// // <summary></summary>
// // ***********************************************************************

namespace UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes
{
    using UTMO.Text.FileGenerator.Attributes;
    using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Constants;
    using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Enums;

    public abstract class DscConfiguration : DscResourceBase
    {
        public sealed override string ResourceTypeName => DscResourceTypeNames.DscConfiguration;

        public sealed override string TemplatePath => nameof(DscConfiguration);

        public virtual string Description { get; } = string.Empty;
        
        public abstract string FullName { get; }

        public override string ResourceName => this.FullName;

        protected virtual DscMode Mode { get; } = DscMode.Pull;

        public virtual string TemplateName => nameof(DscConfiguration);
		
        [MemberName(nameof(RequiredModules))]
        public List<RequiredModule> RequiredModules => this.ConfigurationItems.Select(x => x.SourceModule).Distinct().ToList();
        
        [IgnoreMember]
        public abstract IEnumerable<DscConfigurationItem> ConfigurationItems { get; }
        
        [MemberName(nameof(ConfigurationResources))]
        public List<DscConfigurationItem> ConfigurationResources => this.ConfigurationItems.ToList();
        
        [MemberName("module_source")]
        public abstract string ModuleSource { get; }
        
        [MemberName("config_source")]
        public abstract string ConfigSource { get; }

        public sealed override string OutputExtension => "ps1";

        public sealed override bool GenerateManifest => false;

        public sealed override object? ToManifest()
        {
            return null;
        }
    }
}