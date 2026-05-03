namespace DSCProviderCore.Tests;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Enums;
using UTMO.Text.FileGenerator.Provider.DSC.UtmoStorage;
using UTMO.Text.FileGenerator.Provider.DSC.UtmoStorage.Resources;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.UtmoStorage.UtmoStorageDscConstants.XReadOnlyDrive;

[TestClass]
public class ReadOnlyDriveResourceTests
{
    // --- DriveLetter normalization ---

    [TestMethod]
    [DataRow("E", "E")]
    [DataRow("E:", "E")]
    [DataRow("e", "E")]
    [DataRow("e:", "E")]
    [DataRow(" E ", "E")]
    [DataRow(" e: ", "E")]
    public void DriveLetter_NormalizesToUppercaseSingleLetter(string input, string expected)
    {
        // Arrange & Act
        var resource = ReadOnlyDriveResource.Create("DataDisk", r => r.DriveLetter = input);

        // Assert
        Assert.AreEqual(expected, resource.DriveLetter);
    }

    [TestMethod]
    public void DriveLetter_WhenStoredInPropertyBag_RendersAsQuotedString()
    {
        // Arrange & Act
        var resource = ReadOnlyDriveResource.Create("DataDisk", r => r.DriveLetter = "E:");

        // Assert
        var liquid = resource.PropertyBag.ToLiquid() as Dictionary<string, object>;
        Assert.IsNotNull(liquid);
        Assert.IsTrue(liquid.ContainsKey(Constants.Properties.DriveLetter));
        Assert.AreEqual("\"E\"", liquid[Constants.Properties.DriveLetter]);
    }

    // --- Validate: valid inputs ---

    [TestMethod]
    public async Task Validate_WithSingleLetterDriveLetter_ReturnsNoErrors()
    {
        // Arrange
        var resource = ReadOnlyDriveResource.Create("DataDisk", r => r.DriveLetter = "E");

        // Act
        var errors = await resource.Validate();

        // Assert
        Assert.AreEqual(0, errors.Count);
    }

    [TestMethod]
    public async Task Validate_WithLetterColonFormat_ReturnsNoErrors()
    {
        // Arrange
        var resource = ReadOnlyDriveResource.Create("DataDisk", r => r.DriveLetter = "E:");

        // Act
        var errors = await resource.Validate();

        // Assert
        Assert.AreEqual(0, errors.Count);
    }

    // --- Validate: invalid inputs ---

    [TestMethod]
    public async Task Validate_WithNullDriveLetter_ReturnsError()
    {
        // Arrange
        var resource = ReadOnlyDriveResource.Create("DataDisk", r => { });

        // Act
        var errors = await resource.Validate();

        // Assert
        Assert.IsTrue(errors.Count > 0);
    }

    [TestMethod]
    [DataRow("1")]
    [DataRow("!")]
    [DataRow("EE")]
    public async Task Validate_WithInvalidDriveLetter_ReturnsError(string driveLetter)
    {
        // Arrange — set directly to bypass normalization for multi-char cases
        var resource = ReadOnlyDriveResource.Create("DataDisk", r => { });
        resource.DriveLetter = driveLetter;

        // Act
        var errors = await resource.Validate();

        // Assert
        Assert.IsTrue(errors.Count > 0);
    }

    // --- ResourceId and module ---

    [TestMethod]
    public void ResourceId_ReturnsXReadOnlyDrive()
    {
        // Arrange & Act
        var resource = ReadOnlyDriveResource.Create("DataDisk", r => r.DriveLetter = "E");

        // Assert
        Assert.AreEqual("xReadOnlyDrive", resource.ResourceId);
    }

    [TestMethod]
    public void SourceModule_ReturnsUtmoStorageDscModule()
    {
        // Arrange & Act
        var resource = ReadOnlyDriveResource.Create("DataDisk", r => r.DriveLetter = "E");

        // Assert
        Assert.IsNotNull(resource.SourceModule);
        Assert.AreEqual("UtmoStorageDsc", resource.SourceModule.ModuleName);
    }

    [TestMethod]
    public void HasEnsure_ReturnsTrue()
    {
        // Arrange & Act
        var resource = ReadOnlyDriveResource.Create("DataDisk", r => r.DriveLetter = "E");

        // Assert
        Assert.IsTrue(resource.HasEnsure);
    }

    // --- Ensure property ---

    [TestMethod]
    public void Ensure_Present_IsSetCorrectly()
    {
        // Arrange & Act
        var resource = ReadOnlyDriveResource.Create("DataDisk", r =>
        {
            r.DriveLetter = "E";
            r.Ensure = DscEnsure.Present;
        });

        // Assert
        Assert.AreEqual(DscEnsure.Present, resource.Ensure);
    }

    [TestMethod]
    public void Ensure_Absent_IsSetCorrectly()
    {
        // Arrange & Act
        var resource = ReadOnlyDriveResource.Create("DataDisk", r =>
        {
            r.DriveLetter = "E";
            r.Ensure = DscEnsure.Absent;
        });

        // Assert
        Assert.AreEqual(DscEnsure.Absent, resource.Ensure);
    }

    // --- Create out-parameter overload ---

    [TestMethod]
    public void Create_WithOutParameter_ReturnsSameInstance()
    {
        // Arrange & Act
        var returned = ReadOnlyDriveResource.Create("DataDisk", r => r.DriveLetter = "E", out var outResource);

        // Assert
        Assert.AreSame(returned, outResource);
    }
}
