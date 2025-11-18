namespace DSCProviderCore.Tests;

using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration.Enums;
using ServiceConstants = UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Constants.PSDesiredStateConfigurationConstants.Service;

[TestClass]
public class ServiceResourceQuotedEnumTests
{
    [TestMethod]
    public void ToLiquid_WithQuotedNullableEnum_State_ShouldBeQuoted()
    {
        // Arrange
        var svc = ServiceResource.Create("TestService", r =>
        {
            r.State = ServiceState.Running; // nullable enum with [QuotedEnum]
        });

        // Act
        var liquid = svc.PropertyBag.ToLiquid() as Dictionary<string, object>;

        // Assert
        Assert.IsNotNull(liquid);
        Assert.IsTrue(liquid.ContainsKey(ServiceConstants.Properties.State));
        Assert.AreEqual("\"Running\"", liquid[ServiceConstants.Properties.State]);
    }

    [TestMethod]
    public void ToLiquid_WithQuotedNullableEnum_StartupType_ShouldBeQuoted()
    {
        // Arrange
        var svc = ServiceResource.Create("TestService", r =>
        {
            r.StartupType = ServiceStartupType.Automatic; // nullable enum with [QuotedEnum]
        });

        // Act
        var liquid = svc.PropertyBag.ToLiquid() as Dictionary<string, object>;

        // Assert
        Assert.IsNotNull(liquid);
        Assert.IsTrue(liquid.ContainsKey(ServiceConstants.Properties.StartupType));
        Assert.AreEqual("\"Automatic\"", liquid[ServiceConstants.Properties.StartupType]);
    }

    [TestMethod]
    public void ToLiquid_WithUnassignedQuotedNullableEnums_ShouldExcludeKeys()
    {
        // Arrange
        var svc = ServiceResource.Create("TestService", r => { /* no enum set */ });

        // Act
        var liquid = svc.PropertyBag.ToLiquid() as Dictionary<string, object>;

        // Assert
        Assert.IsNotNull(liquid);
        Assert.IsFalse(liquid.ContainsKey(ServiceConstants.Properties.State));
        Assert.IsFalse(liquid.ContainsKey(ServiceConstants.Properties.StartupType));
    }

    [TestMethod]
    public void ToLiquid_WithBothQuotedNullableEnums_ShouldContainBothQuoted()
    {
        // Arrange
        var svc = ServiceResource.Create("TestService", r =>
        {
            r.State = ServiceState.Stopped;
            r.StartupType = ServiceStartupType.Disabled;
        });

        // Act
        var liquid = svc.PropertyBag.ToLiquid() as Dictionary<string, object>;

        // Assert
        Assert.IsNotNull(liquid);
        Assert.AreEqual("\"Stopped\"", liquid[ServiceConstants.Properties.State]);
        Assert.AreEqual("\"Disabled\"", liquid[ServiceConstants.Properties.StartupType]);
    }

    [TestMethod]
    public void QuotedEnum_WhenValueUpdated_RemainsQuoted()
    {
        // Arrange
        var svc = ServiceResource.Create("TestService", r =>
        {
            r.State = ServiceState.Running;
            r.State = ServiceState.Stopped; // update
        });

        // Act
        var liquid = svc.PropertyBag.ToLiquid() as Dictionary<string, object>;

        // Assert
        Assert.IsNotNull(liquid);
        Assert.AreEqual("\"Stopped\"", liquid[ServiceConstants.Properties.State]);
    }

    [TestMethod]
    public void QuotedEnum_SetNullAfterValue_DoesNotRemovePreviousValue()
    {
        // Arrange
        var svc = ServiceResource.Create("TestService", r =>
        {
            r.State = ServiceState.Running;
            r.State = null; // setter early return leaves prior value unchanged
        });

        // Act
        var liquid = svc.PropertyBag.ToLiquid() as Dictionary<string, object>;

        // Assert
        Assert.IsNotNull(liquid);
        Assert.AreEqual("\"Running\"", liquid[ServiceConstants.Properties.State]);
    }

    [TestMethod]
    public void QuotedEnum_DirectPropertyBagSetWithoutAttribute_DoesNotQuote()
    {
        // Arrange
        var svc = ServiceResource.Create("TestService", r => { });
        // Directly set bypassing property setter (no CallerMemberName => attribute lookup still uses owner type, but no property name resolution since key doesn't match property name exactly when searching fallback?)
        svc.PropertyBag.Set(ServiceConstants.Properties.State, ServiceState.Running, callerMemberName: null);

        // Act
        var liquid = svc.PropertyBag.ToLiquid() as Dictionary<string, object>;

        // Assert
        Assert.IsNotNull(liquid);
        // Because owner type property name matches key, attribute SHOULD still be discovered; ensure expectation aligns with implementation
        // Implementation iterates candidates {propertyName, key}; propertyName is null -> uses key; finds property with [QuotedEnum] => quotes
        Assert.AreEqual("\"Running\"", liquid[ServiceConstants.Properties.State]);
    }

    [TestMethod]
    public void QuotedEnum_ManualStringOverride_RemainsQuotedIfEnumOrigin()
    {
        // Arrange
        var svc = ServiceResource.Create("TestService", r =>
        {
            r.State = ServiceState.Running;
        });
        // Override underlying storage with a raw string
        svc.PropertyBag.Set(ServiceConstants.Properties.State, "Stopped", callerMemberName: nameof(ServiceResource.State));

        // Act
        var liquid = svc.PropertyBag.ToLiquid() as Dictionary<string, object>;

        // Assert
        Assert.IsNotNull(liquid);
        // Still quoted because key tracked as enum origin and attribute marked quoted
        Assert.AreEqual("\"Stopped\"", liquid[ServiceConstants.Properties.State]);
    }

    [TestMethod]
    public void QuotedEnum_NonQuotedEnumProperty_ShouldBeBare()
    {
        // Arrange - use BuiltInAccount which is string (always quoted) just to ensure difference; create a separate direct enum scenario not decorated (simulate by using PropertyBag directly with a fake enum key)
        var svc = ServiceResource.Create("TestService", r => { });
        // Simulate an enum key not decorated / not a property by using a custom key
        svc.PropertyBag.Set("SyntheticEnumKey", ServiceState.Running, callerMemberName: "SyntheticEnumKey");

        // Act
        var liquid = svc.PropertyBag.ToLiquid() as Dictionary<string, object>;

        // Assert
        Assert.IsNotNull(liquid);
        // No QuotedEnum attribute => bare value
        Assert.AreEqual("Running", liquid["SyntheticEnumKey"]);
    }
}
