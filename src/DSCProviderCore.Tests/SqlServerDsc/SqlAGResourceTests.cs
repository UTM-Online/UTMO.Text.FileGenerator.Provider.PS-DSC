namespace DSCProviderCore.Tests;

using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Resources;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.SqlServerDscConstants;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Enums;

[TestClass]
public class SqlAGResourceTests
{
    [TestMethod]
    public void Create_HappyPath_ReturnsResource()
    {
        var resource = SqlAGResource.Create("Test", r =>
        {
            r.AgName = "value";
            r.ServerName = "value";
            r.InstanceName = "value";
        });

        Assert.IsNotNull(resource);
    }

    [TestMethod]
    public void ResourceId_IsExpectedValue()
    {
        var resource = SqlAGResource.Create("Test", _ => { });
        Assert.AreEqual("SqlAG", resource.ResourceId);
    }

    [TestMethod]
    public async Task Validate_MissingRequiredProperties_ReturnsErrors()
    {
        var resource = SqlAGResource.Create("Test", _ => { });
        var errors = await resource.Validate();
        Assert.IsTrue(errors.Count > 0);
    }

    [TestMethod]
    public void EnumProperty_RendersAsExpected()
    {
        var resource = SqlAGResource.Create("Test", r =>
        {
            r.AutomatedBackupPreference = SqlAGAutomatedBackupPreference.Primary;
        });

        var liquidObject = resource.PropertyBag.ToLiquid();
        Assert.IsInstanceOfType(liquidObject, typeof(Dictionary<string, object>));
        var liquid = (Dictionary<string, object>)liquidObject;
        Assert.IsTrue(liquid.ContainsKey(Constants.SqlAG.Properties.AutomatedBackupPreference));
        Assert.AreEqual("\"Primary\"", liquid[Constants.SqlAG.Properties.AutomatedBackupPreference]);
    }
}
