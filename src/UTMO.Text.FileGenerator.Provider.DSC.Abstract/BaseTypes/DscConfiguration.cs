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

namespace UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using Serilog;
using Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Abstract;
using UTMO.Text.FileGenerator.Attributes;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Constants;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Enums;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Messaging;

[SuppressMessage("ReSharper", "TemplateIsNotCompileTimeConstantProblem")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "ReplaceAutoPropertyWithComputedProperty")]
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
    public List<RequiredModule> RequiredModules => this.ConfigurationItems().Select(x => x.SourceModule).Distinct().ToList();
    
    // ReSharper disable once MemberCanBeProtected.Global
    protected abstract IEnumerable<DscConfigurationItem> ConfigurationItems();
        
    [MemberName(nameof(ConfigurationResources))]
    public List<DscConfigurationItem> ConfigurationResources => this.ConfigurationItems().ToList();
        
    [MemberName("module_source")]
    public abstract string ModuleSource { get; }
        
    [MemberName("config_source")]
    public abstract string ConfigSource { get; }

    public sealed override string OutputExtension => "ps1";

    public sealed override bool GenerateManifest => false;

    public sealed override Task<dynamic?> ToManifest()
    {
        return Task.FromResult<dynamic?>(null);
    }
        
    public override async Task<List<ValidationFailedException>> Validate()
    {
        Log.Debug(ValidationMessages.BeginningValidation, this.ResourceTypeName, this.ResourceName);
        var validationErrors = new List<ValidationFailedException>();

        await ValidateResourceTypeAndNameUnique(this.ConfigurationItems(), validationErrors);

        return validationErrors;
    }

    [SuppressMessage("Performance", "CA1854:Prefer the \'IDictionary.TryGetValue(TKey, out TValue)\' method")]
    private static Task ValidateResourceTypeAndNameUnique(IEnumerable<DscConfigurationItem> configurationItems, List<ValidationFailedException> validationErrors)
    {
        var seenNames = new Dictionary<string, HashSet<string>>();

        foreach (var item in configurationItems)
        {
            if (item.ResourceId.Equals("NaN"))
            {
                continue;
            }

            if (!seenNames.ContainsKey(item.ResourceId))
            {
                seenNames[item.ResourceId] = new HashSet<string>();
            }

            if (!seenNames[item.ResourceId].Add(item.Name))
            {
                validationErrors.Add(new ValidationFailedException(item.Name, item.GetType().Name, ValidationFailureType.InvalidResource, "Resource name must be unique within a configuration"));
            }
        }
            
        return Task.CompletedTask;
    }
}