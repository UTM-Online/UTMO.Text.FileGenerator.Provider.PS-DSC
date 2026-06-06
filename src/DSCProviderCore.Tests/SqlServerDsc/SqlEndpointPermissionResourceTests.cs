namespace DSCProviderCore.Tests;

using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Resources;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.SqlServerDscConstants;

[TestClass]
public class SqlEndpointPermissionResourceTests
{
    [TestMethod]
    public void Create_HappyPath_ReturnsResource()
    {
        var resource = SqlEndpointPermissionResource.Create("Test", r =>
        {
            r.InstanceName = "value";
            r.Principal = "value";
            r.EndpointName = "value";
        });

        Assert.IsNotNull(resource);
    }

    [TestMethod]
    public void ResourceId_IsExpectedValue()
    {
        var resource = SqlEndpointPermissionResource.Create("Test", _ => { });
        Assert.AreEqual("SqlEndpointPermission", resource.ResourceId);
    }

    [TestMethod]
    public async Task Validate_MissingRequiredProperties_ReturnsErrors()
    {
        var resource = SqlEndpointPermissionResource.Create("Test", _ => { });
        var errors = await resource.Validate();
        Assert.IsTrue(errors.Count > 0);
    }
}
