namespace DSCProviderCore.Tests;

using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Resources;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.SqlServerDscConstants;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Enums;

[TestClass]
public class SqlDatabaseDefaultLocationResourceTests
{
    [TestMethod]
    public void Create_HappyPath_ReturnsResource()
    {
        var resource = SqlDatabaseDefaultLocationResource.Create("Test", r =>
        {
            r.InstanceName = "value";
            r.Path = "value";
        });

        Assert.IsNotNull(resource);
    }

    [TestMethod]
    public void ResourceId_IsExpectedValue()
    {
        var resource = SqlDatabaseDefaultLocationResource.Create("Test", _ => { });
        Assert.AreEqual("SqlDatabaseDefaultLocation", resource.ResourceId);
    }

    [TestMethod]
    public async Task Validate_MissingRequiredProperties_ReturnsErrors()
    {
        var resource = SqlDatabaseDefaultLocationResource.Create("Test", _ => { });
        var errors = await resource.Validate();
        Assert.IsTrue(errors.Count > 0);
    }

    [TestMethod]
    public void EnumProperty_RendersAsExpected()
    {
        var resource = SqlDatabaseDefaultLocationResource.Create("Test", r =>
        {
            r.Type = SqlDatabaseDefaultLocationType.Data;
        });

        var liquidObject = resource.PropertyBag.ToLiquid();
        Assert.IsInstanceOfType(liquidObject, typeof(Dictionary<string, object>));
        var liquid = (Dictionary<string, object>)liquidObject;
        Assert.IsTrue(liquid.ContainsKey(Constants.SqlDatabaseDefaultLocation.Properties.Type));
        Assert.AreEqual("\"Data\"", liquid[Constants.SqlDatabaseDefaultLocation.Properties.Type]);
    }
}
