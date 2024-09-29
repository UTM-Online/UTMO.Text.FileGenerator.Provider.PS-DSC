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

namespace UTMO.Text.FileGenerator.Provider.DSC.Resources
{
    using BaseTypes;
    using Enums;
    using SubResources;

    public abstract class DscConfiguration : DscResourceBase
    {
        public override string ResourceTypeName => "/DSC/Configurations";

        public override string TemplatePath => this.FullName;

        public abstract string Description { get; }
        
        public abstract string FullName { get; }

        public override string ResourceName => this.FullName;

        protected virtual DscMode Mode { get; } = DscMode.Pull;

        public virtual string TemplateName => this.FullName;
		
        public List<RequiredModule> RequiredModules { get; } = new List<RequiredModule>();
        
        public List<DscConfigurationItem> ConfigurationItems { get; } = new List<DscConfigurationItem>();
        
        public DscConfiguration AddRequiredModule<T>() where T : RequiredModule, new()
        {
            this.RequiredModules.Add(new T());
            return this;
        }
        
        public DscConfiguration AddConfigurationItem<T>(T item) where T : DscConfigurationItem
        {
            this.ConfigurationItems.Add(item);
            return this;
        }

        public sealed override object? ToManifest()
        {
            return new
            {
                ConfigurationName = this.FullName,
                Mode = this.Mode.ToString(),
                RequiredModules = this.RequiredModules.Where(x => x.ModuleName != "PSDesiredStateConfiguration").Select(x => x.ModuleName).ToList()
            };
        }
    }
}