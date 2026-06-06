namespace DSCProviderCore.Tests;

using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Resources;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.SqlServerDscConstants;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Enums;

[TestClass]
public class SqlServiceAccountResourceTests
{
    [TestMethod]
    public void Create_HappyPath_ReturnsResource()
    {
        var resource = SqlServiceAccountResource.Create("Test", r =>
        {
            r.InstanceName = "value";
            r.ServiceAccount = "value";
        });

        Assert.IsNotNull(resource);
    }

    [TestMethod]
    public void ResourceId_IsExpectedValue()
    {
        var resource = SqlServiceAccountResource.Create("Test", _ => { });
        Assert.AreEqual("SqlServiceAccount", resource.ResourceId);
    }

    [TestMethod]
    public async Task Validate_MissingRequiredProperties_ReturnsErrors()
    {
        var resource = SqlServiceAccountResource.Create("Test", _ => { });
        var errors = await resource.Validate();
        Assert.IsTrue(errors.Count > 0);
    }

    [TestMethod]
    public void EnumProperty_RendersAsExpected()
    {
        var resource = SqlServiceAccountResource.Create("Test", r =>
        {
            r.ServiceType = SqlServiceType.DatabaseEngine;
        });

        var liquidObject = resource.PropertyBag.ToLiquid();
        Assert.IsInstanceOfType(liquidObject, typeof(Dictionary<string, object>));
        var liquid = (Dictionary<string, object>)liquidObject;
        Assert.IsTrue(liquid.TryGetValue(Constants.SqlServiceAccount.Properties.ServiceType, out var serviceType));
        Assert.AreEqual("DatabaseEngine", serviceType);
    }
}
