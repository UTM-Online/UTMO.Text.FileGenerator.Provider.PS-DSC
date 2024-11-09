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

namespace UTMO.Text.FileGenerator.Provider.DSC.Models
{
    using Attributes;
    using UTMO.Text.FileGenerator.Provider.DSC.BaseTypes;
    using UTMO.Text.FileGenerator.Provider.DSC.Enums;
    using UTMO.Text.FileGenerator.Provider.DSC.SubResources;

    public abstract class DscConfiguration : DscResourceBase
    {
        public sealed override string ResourceTypeName => "/DSC/Configurations";

        public sealed override string TemplatePath => nameof(DscConfiguration);

        public virtual string Description { get; } = string.Empty;
        
        public abstract string FullName { get; }

        public override string ResourceName => this.FullName;

        protected virtual DscMode Mode { get; } = DscMode.Pull;

        public virtual string TemplateName => nameof(DscConfiguration);
		
        public List<RequiredModule> RequiredModules => this.ConfigurationItems.Select(x => x.SourceModule).Distinct().ToList();
        
        [MemberName("ConfigurationResources")]
        public abstract IEnumerable<DscConfigurationItem> ConfigurationItems { get; }

        public sealed override string OutputExtension => "ps1";

        public sealed override bool GenerateManifest => false;

        public sealed override object? ToManifest()
        {
            return null;
        }
    }
}