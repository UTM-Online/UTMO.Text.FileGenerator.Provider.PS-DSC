namespace DSCProviderCore.Tests;

using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Resources;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.SqlServerDscConstants;

[TestClass]
public class SqlAGDatabaseResourceTests
{
    [TestMethod]
    public void Create_HappyPath_ReturnsResource()
    {
        var resource = SqlAGDatabaseResource.Create("Test", r =>
        {
            r.ServerName = "value";
            r.InstanceName = "value";
            r.AvailabilityGroupName = "value";
        });

        Assert.IsNotNull(resource);
    }

    [TestMethod]
    public void ResourceId_IsExpectedValue()
    {
        var resource = SqlAGDatabaseResource.Create("Test", _ => { });
        Assert.AreEqual("SqlAGDatabase", resource.ResourceId);
    }

    [TestMethod]
    public async Task Validate_MissingRequiredProperties_ReturnsErrors()
    {
        var resource = SqlAGDatabaseResource.Create("Test", _ => { });
        var errors = await resource.Validate();
        Assert.IsTrue(errors.Count > 0);
    }
}
