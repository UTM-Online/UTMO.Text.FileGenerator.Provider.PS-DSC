namespace DSCProviderCore.Tests;

using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Resources;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.SqlServerDscConstants;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Enums;

[TestClass]
public class SqlAgentFailsafeResourceTests
{
    [TestMethod]
    public void Create_HappyPath_ReturnsResource()
    {
        var resource = SqlAgentFailsafeResource.Create("Test", r =>
        {
            r.OperatorName = "value";
            r.InstanceName = "value";
        });

        Assert.IsNotNull(resource);
    }

    [TestMethod]
    public void ResourceId_IsExpectedValue()
    {
        var resource = SqlAgentFailsafeResource.Create("Test", _ => { });
        Assert.AreEqual("SqlAgentFailsafe", resource.ResourceId);
    }

    [TestMethod]
    public async Task Validate_MissingRequiredProperties_ReturnsErrors()
    {
        var resource = SqlAgentFailsafeResource.Create("Test", _ => { });
        var errors = await resource.Validate();
        Assert.IsTrue(errors.Count > 0);
    }

    [TestMethod]
    public void EnumProperty_RendersAsExpected()
    {
        var resource = SqlAgentFailsafeResource.Create("Test", r =>
        {
            r.NotificationMethod = SqlAgentNotificationMethod.None;
        });

        var liquid = resource.PropertyBag.ToLiquid() as Dictionary<string, object>;
        Assert.IsNotNull(liquid);
        Assert.IsTrue(liquid.ContainsKey(Constants.SqlAgentFailsafe.Properties.NotificationMethod));
        Assert.AreEqual("\"None\"", liquid[Constants.SqlAgentFailsafe.Properties.NotificationMethod]);
    }
}
