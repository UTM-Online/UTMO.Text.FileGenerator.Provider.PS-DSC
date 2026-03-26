namespace DSCProviderCore.Tests;

using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration;
using EnvironmentConstants = UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Constants.PSDesiredStateConfigurationConstants.Environment;

[TestClass]
public class EnvironmentResourceTests
{
    [TestMethod]
    public void Create_WithEnvironmentName_ShouldSetNameInPropertyBag()
    {
        // Arrange & Act
        var resource = EnvironmentResource.Create("SetPath", r =>
        {
            r.EnvironmentName = "PATH";
        });

        // Assert
        var liquid = resource.PropertyBag.ToLiquid() as Dictionary<string, object>;
        Assert.IsNotNull(liquid);
        Assert.IsTrue(liquid.ContainsKey(EnvironmentConstants.Properties.Name));
        Assert.AreEqual("\"PATH\"", liquid[EnvironmentConstants.Properties.Name]);
    }

    [TestMethod]
    public void Create_WithPathTrue_ShouldSetPathInPropertyBag()
    {
        // Arrange & Act
        var resource = EnvironmentResource.Create("SetPath", r =>
        {
            r.EnvironmentName = "PATH";
            r.Path = true;
        });

        // Assert
        var liquid = resource.PropertyBag.ToLiquid() as Dictionary<string, object>;
        Assert.IsNotNull(liquid);
        Assert.IsTrue(liquid.ContainsKey(EnvironmentConstants.Properties.Path));
        Assert.AreEqual("$true", liquid[EnvironmentConstants.Properties.Path]);
    }

    [TestMethod]
    public void Create_WithPathFalse_ShouldSetPathInPropertyBag()
    {
        // Arrange & Act
        var resource = EnvironmentResource.Create("SetPath", r =>
        {
            r.EnvironmentName = "MY_VAR";
            r.Path = false;
        });

        // Assert
        var liquid = resource.PropertyBag.ToLiquid() as Dictionary<string, object>;
        Assert.IsNotNull(liquid);
        Assert.IsTrue(liquid.ContainsKey(EnvironmentConstants.Properties.Path));
        Assert.AreEqual("$false", liquid[EnvironmentConstants.Properties.Path]);
    }

    [TestMethod]
    public void Create_WithoutPath_ShouldNotIncludePathInPropertyBag()
    {
        // Arrange & Act
        var resource = EnvironmentResource.Create("SetVar", r =>
        {
            r.EnvironmentName = "MY_VAR";
        });

        // Assert
        var liquid = resource.PropertyBag.ToLiquid() as Dictionary<string, object>;
        Assert.IsNotNull(liquid);
        Assert.IsFalse(liquid.ContainsKey(EnvironmentConstants.Properties.Path));
    }

    [TestMethod]
    public void Create_WithValue_ShouldSetValueInPropertyBag()
    {
        // Arrange & Act
        var resource = EnvironmentResource.Create("SetPath", r =>
        {
            r.EnvironmentName = "PATH";
            r.Value = "C:\\Tools\\bin";
        });

        // Assert
        var liquid = resource.PropertyBag.ToLiquid() as Dictionary<string, object>;
        Assert.IsNotNull(liquid);
        Assert.IsTrue(liquid.ContainsKey(EnvironmentConstants.Properties.Value));
        Assert.AreEqual("\"C:\\Tools\\bin\"", liquid[EnvironmentConstants.Properties.Value]);
    }

    [TestMethod]
    public void Create_WithoutValue_ShouldNotIncludeValueInPropertyBag()
    {
        // Arrange & Act
        var resource = EnvironmentResource.Create("SetVar", r =>
        {
            r.EnvironmentName = "MY_VAR";
        });

        // Assert
        var liquid = resource.PropertyBag.ToLiquid() as Dictionary<string, object>;
        Assert.IsNotNull(liquid);
        Assert.IsFalse(liquid.ContainsKey(EnvironmentConstants.Properties.Value));
    }

    [TestMethod]
    public void ResourceId_ShouldBeEnvironment()
    {
        // Arrange & Act
        var resource = EnvironmentResource.Create("SetVar", r =>
        {
            r.EnvironmentName = "MY_VAR";
        });

        // Assert
        Assert.AreEqual("Environment", resource.ResourceId);
    }

    [TestMethod]
    public void Create_WithOutParameter_ShouldReturnSameInstance()
    {
        // Arrange & Act
        var returned = EnvironmentResource.Create("SetVar", r =>
        {
            r.EnvironmentName = "MY_VAR";
        }, out var outResource);

        // Assert
        Assert.AreSame(returned, outResource);
    }

    [TestMethod]
    public async Task Validate_WithoutEnvironmentName_ShouldReturnValidationError()
    {
        // Arrange
        var resource = EnvironmentResource.Create("SetVar", r => { /* no name set */ });

        // Act
        var errors = await resource.Validate();

        // Assert
        Assert.IsNotNull(errors);
        Assert.IsTrue(errors.Count > 0);
    }

    [TestMethod]
    public async Task Validate_WithEnvironmentName_ShouldReturnNoErrors()
    {
        // Arrange
        var resource = EnvironmentResource.Create("SetVar", r =>
        {
            r.EnvironmentName = "MY_VAR";
        });

        // Act
        var errors = await resource.Validate();

        // Assert
        Assert.IsNotNull(errors);
        Assert.AreEqual(0, errors.Count);
    }
}
