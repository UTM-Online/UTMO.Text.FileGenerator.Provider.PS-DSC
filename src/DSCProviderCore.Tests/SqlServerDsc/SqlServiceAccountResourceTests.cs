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

        var liquid = resource.PropertyBag.ToLiquid() as Dictionary<string, object>;
        Assert.IsNotNull(liquid);
        Assert.IsTrue(liquid.ContainsKey(Constants.SqlServiceAccount.Properties.ServiceType));
        Assert.AreEqual("DatabaseEngine", liquid[Constants.SqlServiceAccount.Properties.ServiceType]);
    }
}
