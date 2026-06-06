namespace DSCProviderCore.Tests;

using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Resources;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.SqlServerDscConstants;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Enums;

[TestClass]
public class SqlProtocolResourceTests
{
    [TestMethod]
    public void Create_HappyPath_ReturnsResource()
    {
        var resource = SqlProtocolResource.Create("Test", r =>
        {
            r.InstanceName = "value";
        });

        Assert.IsNotNull(resource);
    }

    [TestMethod]
    public void ResourceId_IsExpectedValue()
    {
        var resource = SqlProtocolResource.Create("Test", _ => { });
        Assert.AreEqual("SqlProtocol", resource.ResourceId);
    }

    [TestMethod]
    public async Task Validate_MissingRequiredProperties_ReturnsErrors()
    {
        var resource = SqlProtocolResource.Create("Test", _ => { });
        var errors = await resource.Validate();
        Assert.IsTrue(errors.Count > 0);
    }

    [TestMethod]
    public void EnumProperty_RendersAsExpected()
    {
        var resource = SqlProtocolResource.Create("Test", r =>
        {
            r.ProtocolName = SqlProtocolName.TcpIp;
        });

        var liquidObject = resource.PropertyBag.ToLiquid();
        Assert.IsInstanceOfType(liquidObject, typeof(Dictionary<string, object>));
        var liquid = (Dictionary<string, object>)liquidObject;
        Assert.IsTrue(liquid.ContainsKey(Constants.SqlProtocol.Properties.ProtocolName));
        Assert.AreEqual("\"TcpIp\"", liquid[Constants.SqlProtocol.Properties.ProtocolName]);
    }
}
