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

        public virtual string TemplateName => "_Configuration_Base";
		
        public List<RequiredModule> RequiredModules => this.ConfigurationItems.Select(x => x.SourceModule).Distinct().ToList();
        
        public List<DscConfigurationItem> ConfigurationItems { get; } = new();
        
        public DscConfiguration AddConfigurationItem<T>(T item) where T : DscConfigurationItem
        {
            this.ConfigurationItems.Add(item);
            return this;
        }
        
        public DscConfiguration AddConfigurationItem<T>(Action<T> configure) where T : DscConfigurationItem, new()
        {
            var item = new T();
            configure(item);
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