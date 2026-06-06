namespace DSCProviderCore.Tests;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;
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
            r.AdminLinkCredentials = new TestCredentialExpression("$adminCredential");
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

        var liquidObject = resource.PropertyBag.ToLiquid();
        Assert.IsInstanceOfType(liquidObject, typeof(Dictionary<string, object>));
        var liquid = (Dictionary<string, object>)liquidObject;
        Assert.IsTrue(liquid.TryGetValue(Constants.SqlReplication.Properties.DistributorMode, out var distributorMode));
        Assert.AreEqual("\"Local\"", distributorMode);
    }

    private sealed class TestCredentialExpression(string expression) : IPowerShellExpression
    {
        public string ToPowerShell() => expression;
    }
}
