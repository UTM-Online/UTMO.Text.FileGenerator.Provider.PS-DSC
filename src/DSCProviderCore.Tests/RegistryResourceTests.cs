namespace DSCProviderCore.Tests;

using UTMO.Text.FileGenerator.Abstract;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration.Enums;
using RegistryConstants = UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Constants.PSDesiredStateConfigurationConstants.Registry;

[TestClass]
public class RegistryResourceTests
{
    [TestMethod]
    public void HexTrue_WithoutExplicitValueType_ShouldEmitDWordValueType()
    {
        // Arrange & Act
        var resource = RegistryResource.Create("SetRegistryValue", r =>
        {
            r.Key = "HKLM:\\Software\\Example";
            r.ValueName = "ExampleValue";
            r.ValueData = "42";
            r.Hex = true;
        });

        // Assert
        var liquid = GetLiquidPropertyBag(resource);
        Assert.IsTrue(liquid.TryGetValue(RegistryConstants.Properties.ValueType, out var valueType));
        Assert.AreEqual("\"DWord\"", valueType);
    }

    [TestMethod]
    public void HexTrue_WithStringValueType_ShouldForceCompatibleDWordValueType()
    {
        // Arrange & Act
        var resource = RegistryResource.Create("SetRegistryValue", r =>
        {
            r.Key = "HKLM:\\Software\\Example";
            r.ValueName = "ExampleValue";
            r.ValueData = "test";
            r.Hex = true;
            r.ValueType = RegistryValueType.String;
        });

        // Assert
        var liquid = GetLiquidPropertyBag(resource);
        Assert.IsTrue(liquid.TryGetValue(RegistryConstants.Properties.ValueType, out var valueType));
        Assert.AreEqual("\"DWord\"", valueType);
    }

    private static Dictionary<string, object> GetLiquidPropertyBag(RegistryResource resource)
    {
        var liquid = resource.PropertyBag.ToLiquid();

        if (liquid is not Dictionary<string, object> propertyBag)
        {
            Assert.Fail($"Expected PropertyBag.ToLiquid() to return Dictionary<string, object>, but got '{liquid?.GetType().FullName ?? "null"}'.");
            return new Dictionary<string, object>();
        }

        return propertyBag;
    }

    [TestMethod]
    public async Task Validate_WithHexAndStringValueType_ShouldReturnInvalidConfigurationError()
    {
        // Arrange
        var resource = RegistryResource.Create("SetRegistryValue", r =>
        {
            r.Key = "HKLM:\\Software\\Example";
            r.ValueName = "ExampleValue";
            r.ValueData = "test";
        });
        resource.PropertyBag.Set(RegistryConstants.Properties.Hex, true);
        resource.PropertyBag.Set(RegistryConstants.Properties.ValueType, RegistryValueType.String);

        // Act
        var errors = await resource.Validate();

        // Assert
        Assert.IsNotNull(errors);
        Assert.IsTrue(errors.Count > 0);
        Assert.IsTrue(errors.Any(error => error.ResourceName == RegistryConstants.Properties.ValueType && error.Category == ValidationFailureType.InvalidConfiguration));
    }
}
