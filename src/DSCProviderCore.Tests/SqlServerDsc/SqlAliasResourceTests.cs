namespace DSCProviderCore.Tests;

using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Resources;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.SqlServerDscConstants;

[TestClass]
public class SqlAliasResourceTests
{
    [TestMethod]
    public void Create_HappyPath_ReturnsResource()
    {
        var resource = SqlAliasResource.Create("Test", r =>
        {
            r.AliasName = "value";
        });

        Assert.IsNotNull(resource);
    }

    [TestMethod]
    public void ResourceId_IsExpectedValue()
    {
        var resource = SqlAliasResource.Create("Test", _ => { });
        Assert.AreEqual("SqlAlias", resource.ResourceId);
    }

    [TestMethod]
    public async Task Validate_MissingRequiredProperties_ReturnsErrors()
    {
        var resource = SqlAliasResource.Create("Test", _ => { });
        var errors = await resource.Validate();
        Assert.IsTrue(errors.Count > 0);
    }
}
