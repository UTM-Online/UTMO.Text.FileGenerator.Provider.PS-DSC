namespace DSCProviderCore.Tests;

using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Resources;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.SqlServerDscConstants;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Enums;

[TestClass]
public class SqlSetupResourceTests
{
    [TestMethod]
    public void Create_HappyPath_ReturnsResource()
    {
        var resource = SqlSetupResource.Create("Test", r =>
        {
            r.SourcePath = "value";
            r.InstanceName = "value";
        });

        Assert.IsNotNull(resource);
    }

    [TestMethod]
    public void ResourceId_IsExpectedValue()
    {
        var resource = SqlSetupResource.Create("Test", _ => { });
        Assert.AreEqual("SqlSetup", resource.ResourceId);
    }

    [TestMethod]
    public async Task Validate_MissingRequiredProperties_ReturnsErrors()
    {
        var resource = SqlSetupResource.Create("Test", _ => { });
        var errors = await resource.Validate();
        Assert.IsTrue(errors.Count > 0);
    }

    [TestMethod]
    public void EnumProperty_RendersAsExpected()
    {
        var resource = SqlSetupResource.Create("Test", r =>
        {
            r.Action = SqlSetupAction.Install;
        });

        var liquidObject = resource.PropertyBag.ToLiquid();
        Assert.IsInstanceOfType(liquidObject, typeof(Dictionary<string, object>));
        var liquid = (Dictionary<string, object>)liquidObject;
        Assert.IsTrue(liquid.ContainsKey(Constants.SqlSetup.Properties.Action));
        Assert.AreEqual("\"Install\"", liquid[Constants.SqlSetup.Properties.Action]);
    }
}
