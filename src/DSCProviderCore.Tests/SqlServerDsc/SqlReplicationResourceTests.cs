namespace DSCProviderCore.Tests;

using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Resources;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.SqlServerDscConstants;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Enums;

[TestClass]
public class SqlReplicationResourceTests
{
    [TestMethod]
    public void Create_HappyPath_ReturnsResource()
    {
        var resource = SqlReplicationResource.Create("Test", r =>
        {
            r.InstanceName = "value";
            r.AdminLinkCredentials = "value";
            r.RemoteDistributor = "value";
        });

        Assert.IsNotNull(resource);
    }

    [TestMethod]
    public void ResourceId_IsExpectedValue()
    {
        var resource = SqlReplicationResource.Create("Test", _ => { });
        Assert.AreEqual("SqlReplication", resource.ResourceId);
    }

    [TestMethod]
    public async Task Validate_MissingRequiredProperties_ReturnsErrors()
    {
        var resource = SqlReplicationResource.Create("Test", _ => { });
        var errors = await resource.Validate();
        Assert.IsTrue(errors.Count > 0);
    }

    [TestMethod]
    public void EnumProperty_RendersAsExpected()
    {
        var resource = SqlReplicationResource.Create("Test", r =>
        {
            r.DistributorMode = SqlReplicationDistributorMode.Local;
        });

        var liquid = resource.PropertyBag.ToLiquid() as Dictionary<string, object>;
        Assert.IsNotNull(liquid);
        Assert.IsTrue(liquid.ContainsKey(Constants.SqlReplication.Properties.DistributorMode));
        Assert.AreEqual("\"Local\"", liquid[Constants.SqlReplication.Properties.DistributorMode]);
    }
}
