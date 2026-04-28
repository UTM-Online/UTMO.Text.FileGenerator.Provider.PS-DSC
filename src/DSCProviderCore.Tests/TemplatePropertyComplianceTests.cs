namespace DSCProviderCore.Tests;

using System.Reflection;
using UTMO.Text.FileGenerator.Attributes;
using UTMO.Text.FileGenerator.Models;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

/// <summary>
/// Compliance tests validating that all template-bound types in the provider are
/// correctly annotated for engine v2.16+ compatibility (opt-in <c>[TemplateProperty]</c> model).
/// </summary>
[TestClass]
public class TemplatePropertyComplianceTests
{
    /// <summary>
    /// Returns the provider assemblies that contain DSC template-bound types.
    /// </summary>
    private static IEnumerable<Assembly> ProviderAssemblies()
    {
        // Ensure each assembly is loaded by referencing a known type from it.
        yield return typeof(DscConfigurationItem).Assembly;               // DSC.Abstract
        yield return typeof(UTMO.Text.FileGenerator.Provider.DSC.DscGenerator).Assembly; // DSC (main)
        yield return typeof(UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Constants.PSDesiredStateConfigurationConstants).Assembly; // WindowsCore
    }

    /// <summary>
    /// Returns all concrete and abstract types in the provider assemblies that
    /// are template-resource types (i.e., inherit from <see cref="SubTemplateResourceBase"/>
    /// or <see cref="TemplateResourceBase"/>).
    /// </summary>
    private static IEnumerable<Type> TemplateResourceTypes()
    {
        var subTemplateBase = typeof(SubTemplateResourceBase);
        var templateBase = typeof(TemplateResourceBase);

        return ProviderAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => !t.IsInterface
                        && (subTemplateBase.IsAssignableFrom(t) || templateBase.IsAssignableFrom(t)));
    }

    /// <summary>
    /// Every property decorated with <see cref="MemberNameAttribute"/> must also be decorated
    /// with <see cref="TemplatePropertyAttribute"/> so the engine v2.16+ opt-in model can
    /// expose it to templates.
    /// </summary>
    [TestMethod]
    public void AllMemberNameProperties_MustHaveTemplatePropertyAttribute()
    {
        var violations = new List<string>();

        foreach (var type in TemplateResourceTypes())
        {
            var props = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            foreach (var prop in props)
            {
                var hasMemberName = prop.GetCustomAttribute<MemberNameAttribute>() != null;
                var hasTemplateProperty = prop.GetCustomAttribute<TemplatePropertyAttribute>() != null;

                if (hasMemberName && !hasTemplateProperty)
                {
                    violations.Add($"{type.FullName}.{prop.Name}: has [MemberName] but is missing [TemplateProperty]");
                }
            }
        }

        if (violations.Count > 0)
        {
            Assert.Fail(
                $"The following template-bound properties are missing [TemplateProperty] ({violations.Count} violation(s)):\n"
                + string.Join("\n", violations));
        }
    }

    /// <summary>
    /// Every property decorated with <see cref="TemplatePropertyAttribute"/> must be
    /// publicly accessible. Non-public properties are never exposed by the engine
    /// regardless of attribute presence.
    /// </summary>
    [TestMethod]
    public void AllTemplatePropertyAttributes_MustBeOnPublicProperties()
    {
        var violations = new List<string>();

        foreach (var type in TemplateResourceTypes())
        {
            // Check all non-public properties for accidental [TemplateProperty] annotation.
            var nonPublicProps = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            foreach (var prop in nonPublicProps)
            {
                var hasTemplateProperty = prop.GetCustomAttribute<TemplatePropertyAttribute>() != null;

                if (hasTemplateProperty)
                {
                    violations.Add($"{type.FullName}.{prop.Name}: is non-public but has [TemplateProperty] (engine will never expose it)");
                }
            }
        }

        if (violations.Count > 0)
        {
            Assert.Fail(
                $"The following non-public properties have [TemplateProperty] which has no effect ({violations.Count} violation(s)):\n"
                + string.Join("\n", violations));
        }
    }

    /// <summary>
    /// Non-public properties must not carry <see cref="MemberNameAttribute"/>, since the engine
    /// cannot expose them regardless of the feature flag.
    /// </summary>
    [TestMethod]
    public void NonPublicProperties_MustNotHaveMemberNameAttribute()
    {
        var violations = new List<string>();

        foreach (var type in TemplateResourceTypes())
        {
            var nonPublicProps = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            foreach (var prop in nonPublicProps)
            {
                var hasMemberName = prop.GetCustomAttribute<MemberNameAttribute>() != null;
                var hasIgnoreMember = prop.GetCustomAttribute<IgnoreMemberAttribute>() != null;

                // A non-public property with [MemberName] but not [IgnoreMember] is a compliance error.
                if (hasMemberName && !hasIgnoreMember)
                {
                    violations.Add($"{type.FullName}.{prop.Name}: is non-public and has [MemberName] — it will never be exposed by the engine");
                }
            }
        }

        if (violations.Count > 0)
        {
            Assert.Fail(
                $"The following non-public properties have [MemberName] and cannot be template-exposed ({violations.Count} violation(s)):\n"
                + string.Join("\n", violations));
        }
    }

    /// <summary>
    /// Validates that the core <see cref="DscConfigurationItem"/> base class exposes
    /// all properties required by the DSC configuration templates.
    /// </summary>
    [TestMethod]
    public void DscConfigurationItem_RequiredTemplateProperties_ArePresent()
    {
        var type = typeof(DscConfigurationItem);
        var requiredNames = new[] { "resource_id", "name", "description", "ensure", "depends_on", "property_bag", "has_ensure" };

        foreach (var memberName in requiredNames)
        {
            var prop = type
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
                .FirstOrDefault(p =>
                {
                    var attr = p.GetCustomAttribute<MemberNameAttribute>();
                    return attr?.Name == memberName;
                });

            Assert.IsNotNull(prop, $"DscConfigurationItem is missing a property with [MemberName(\"{memberName}\")]");

            var hasTemplateProperty = prop.GetCustomAttribute<TemplatePropertyAttribute>() != null;
            Assert.IsTrue(hasTemplateProperty, $"DscConfigurationItem.{prop.Name} (member name '{memberName}') is missing [TemplateProperty]");
        }
    }

    /// <summary>
    /// Validates that <see cref="DscConfiguration"/> exposes all properties required
    /// by the DscConfiguration Liquid template.
    /// </summary>
    [TestMethod]
    public void DscConfiguration_RequiredTemplateProperties_ArePresent()
    {
        var type = typeof(DscConfiguration);

        // These are the direct-name properties (no MemberName mapping needed — template uses exact property name)
        var directProps = new[] { "FullName", "RequiredModules", "ConfigurationResources" };
        foreach (var propName in directProps)
        {
            var prop = type.GetProperty(propName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
            Assert.IsNotNull(prop, $"DscConfiguration is missing public property '{propName}'");
            var hasTemplateProperty = prop.GetCustomAttribute<TemplatePropertyAttribute>() != null;
            Assert.IsTrue(hasTemplateProperty, $"DscConfiguration.{propName} is missing [TemplateProperty]");
        }

        // These use [MemberName] mapping
        var memberNamedProps = new[] { "requires_plaintext_password", "module_source", "config_source" };
        foreach (var memberName in memberNamedProps)
        {
            var prop = type
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
                .FirstOrDefault(p => p.GetCustomAttribute<MemberNameAttribute>()?.Name == memberName);

            Assert.IsNotNull(prop, $"DscConfiguration is missing a property with [MemberName(\"{memberName}\")]");
            var hasTemplateProperty = prop.GetCustomAttribute<TemplatePropertyAttribute>() != null;
            Assert.IsTrue(hasTemplateProperty, $"DscConfiguration.{prop.Name} (member name '{memberName}') is missing [TemplateProperty]");
        }

        // Mode must be public (used in LCM template as cfg.Mode)
        var modeProp = type.GetProperty("Mode", BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
        Assert.IsNotNull(modeProp, "DscConfiguration.Mode must be public");
        Assert.IsTrue(modeProp.GetCustomAttribute<TemplatePropertyAttribute>() != null, "DscConfiguration.Mode is missing [TemplateProperty]");
    }

    /// <summary>
    /// Validates that <see cref="DscLcmConfiguration"/> exposes all properties required
    /// by the LcmConfiguration Liquid template, and that the web resource properties are public.
    /// </summary>
    [TestMethod]
    public void DscLcmConfiguration_RequiredTemplateProperties_ArePresent()
    {
        var type = typeof(DscLcmConfiguration);

        var memberNamedProps = new[]
        {
            "node_name", "partial_configs", "lcm_settings",
            "pull_server_web", "resource_repository_web", "report_server_web",
            "has_local_configuration"
        };

        foreach (var memberName in memberNamedProps)
        {
            // Must be public
            var prop = type
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
                .FirstOrDefault(p => p.GetCustomAttribute<MemberNameAttribute>()?.Name == memberName);

            Assert.IsNotNull(prop, $"DscLcmConfiguration is missing a public property with [MemberName(\"{memberName}\")]");
            var hasTemplateProperty = prop.GetCustomAttribute<TemplatePropertyAttribute>() != null;
            Assert.IsTrue(hasTemplateProperty, $"DscLcmConfiguration.{prop.Name} (member name '{memberName}') is missing [TemplateProperty]");
        }
    }

    /// <summary>
    /// Validates that <see cref="RequiredModule"/> exposes properties required by DSC configuration templates.
    /// </summary>
    [TestMethod]
    public void RequiredModule_RequiredTemplateProperties_ArePresent()
    {
        var type = typeof(RequiredModule);

        // "Name" maps from ModuleName, "Version" maps from ModuleVersion
        var memberNamedProps = new[] { "Name", "Version" };
        foreach (var memberName in memberNamedProps)
        {
            var prop = type
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
                .FirstOrDefault(p => p.GetCustomAttribute<MemberNameAttribute>()?.Name == memberName);

            Assert.IsNotNull(prop, $"RequiredModule is missing a property with [MemberName(\"{memberName}\")]");
            var hasTemplateProperty = prop.GetCustomAttribute<TemplatePropertyAttribute>() != null;
            Assert.IsTrue(hasTemplateProperty, $"RequiredModule.{prop.Name} (member name '{memberName}') is missing [TemplateProperty]");
        }

        // UseAlternateFormat is accessed directly by template name
        var useAltProp = type.GetProperty("UseAlternateFormat", BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
        Assert.IsNotNull(useAltProp, "RequiredModule is missing public property 'UseAlternateFormat'");
        Assert.IsTrue(useAltProp.GetCustomAttribute<TemplatePropertyAttribute>() != null, "RequiredModule.UseAlternateFormat is missing [TemplateProperty]");
    }

    /// <summary>
    /// Validates that <see cref="NodeNetworkSettings.SecondaryNetworkAdapters"/> is public
    /// so it can carry <see cref="TemplatePropertyAttribute"/> and be exposed to templates.
    /// </summary>
    [TestMethod]
    public void NodeNetworkSettings_SecondaryNetworkAdapters_IsPublic()
    {
        var type = typeof(NodeNetworkSettings);
        var prop = type.GetProperty("SecondaryNetworkAdapters", BindingFlags.Public | BindingFlags.Instance);

        Assert.IsNotNull(prop, "NodeNetworkSettings.SecondaryNetworkAdapters must be public");
        Assert.IsTrue(prop.GetCustomAttribute<TemplatePropertyAttribute>() != null,
            "NodeNetworkSettings.SecondaryNetworkAdapters is missing [TemplateProperty]");
    }
}
