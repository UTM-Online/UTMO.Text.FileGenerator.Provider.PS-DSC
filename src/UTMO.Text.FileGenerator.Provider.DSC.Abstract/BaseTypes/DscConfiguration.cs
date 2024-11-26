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
    using UTMO.Text.FileGenerator.Abstract;
    using UTMO.Text.FileGenerator.Attributes;
    using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Constants;
    using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Enums;
    using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Messaging;

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
        
        protected IGeneratorLogger Logger { get; } = PluginManager.Instance.Resolve<IGeneratorLogger>();

        public sealed override object? ToManifest()
        {
            return null;
        }

        public override void Validate()
        {
            this.Logger.Verbose(ValidationMessages.BeginningValidation, this.ResourceTypeName, this.ResourceName);
            var validationErrors = new List<string>();

            this.ValidateResourceTypeAndNameUnique(this.ConfigurationItems, validationErrors);

            if (validationErrors.Any())
            {
                foreach (var error in validationErrors)
                {
                    this.Logger.Error(error);
                }
                
                this.Logger.Fatal(ValidationMessages.ValidationFailed, true, 99, this.ResourceName, this.ResourceTypeName);
            }
            else
            {
                this.Logger.Verbose(ValidationMessages.ValidationSucceded, this.ResourceTypeName, this.ResourceName);
            }
        }

        public override Dictionary<string, object> ToTemplateContext()
        {
            this.Validate();
            return base.ToTemplateContext();
        }

        private void ValidateResourceTypeAndNameUnique(IEnumerable<DscConfigurationItem> configurationItems, List<string> validationErrors)
        {
            var resourceGroupings = configurationItems.GroupBy(x => x.ResourceTypeName).ToList();

            foreach (var group in resourceGroupings)
            {
                if (group.Key.Equals("NaN"))
                {
                    continue;
                }
                
                // Ensure that the Name property is unique for each resource type
                var duplicateNames = group.GroupBy(x => x.Name).Where(x => x.Count() > 1).Select(a => a.Key).ToList();
                
                if (duplicateNames.Any())
                {
                    foreach (var name in duplicateNames)
                    {
                        validationErrors.Add(string.Format(ValidationMessages.DuplicateResourceNameError, group.Key, name));
                    }
                }
            }
        }
    }
}