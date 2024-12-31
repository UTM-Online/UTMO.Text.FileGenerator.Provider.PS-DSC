// // ***********************************************************************
// // Assembly         : UTMO.Text.FileGenerator.Provider.DSC
// // Author           : Josh Irwin (joirwi)
// // Created          : 11/22/2023
// //
// // Last Modified By : Josh Irwin (joirwi)
// // Last Modified On : 11/22/2023 3:14 PM
// // ***********************************************************************
// // <copyright file="RequiredModule.cs" company="Microsoft Corp">
// //     Copyright (c) Microsoft Corporation. All rights reserved.
// // </copyright>
// // <summary></summary>
// // ***********************************************************************

namespace UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes
{
    using Models;
    using UTMO.Text.FileGenerator.Attributes;

    public abstract partial class RequiredModule : SubTemplateResourceBase
    {
        public override bool GenerateManifest => true;
        
        [MemberName("Name")]
        public abstract string ModuleName { get; }

        [MemberName("Version")]
        public abstract string ModuleVersion { get; }

        public virtual bool IsPrivate { get; } = false;
        
        public virtual bool AllowClobber { get; } = false;

        [IgnoreMember]
        public virtual string? RewriteModuleVersion { get; } = null;
        
        public virtual bool UseAlternateFormat { get; } = false;

        public override string ResourceTypeName => "/DSC/RequiredModule";

        public override string ResourceName => this.ModuleName;

        public override async Task<Dictionary<string, object>> ToTemplateContext()
        {
            var ctx = await base.ToTemplateContext();

            if (this.RewriteModuleVersion != null)
            {
                ctx["Version"] = this.RewriteModuleVersion;
            }

            return ctx;
        }

        public override Task<object?> ToManifest()
        {
            dynamic manifest = new
            {
                Name = this.ModuleName,
                Version = this.ModuleVersion,
                IsPrivate = this.IsPrivate,
                AllowClobber = this.AllowClobber,
                UseAlternateFormat = this.UseAlternateFormat
            };

            if (this.UseAlternateFormat)
            {
                manifest.AlternateVersion = this.RewriteModuleVersion;
            }
            
            return Task.FromResult((object?)manifest);
        }
    }
}