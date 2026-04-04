namespace DSCProviderCore.Tests;

using UTMO.Text.FileGenerator.Provider.DSC.Constants;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Enums;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Types;

[TestClass]
public class WebAppPoolResourceTests
{
    [TestMethod]
    public void PropertyBag_ToLiquid_RendersGmsaCredentialAsRawPsCredentialExpression()
    {
        // Arrange
        var resource = WebAppPoolResource.Create("MyAppPool", pool =>
        {
            pool.PoolName = "MyAppPool";
            pool.IdentityType = AppPoolIdentityType.SpecificUser;
            pool.Credential = new GmsaCredential(@"CONTOSO\svc-web$");
        });

        // Act
        var result = resource.PropertyBag.ToLiquid() as Dictionary<string, object>;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("[PSCredential]::new('CONTOSO\\svc-web$', [System.Security.SecureString]::new())", result[WebAdministrationDscConstants.WebAppPool.Properties.Credential]);
    }

    [TestMethod]
    public async Task Validate_WhenIdentityTypeIsSpecificUserAndCredentialMissing_ReturnsError()
    {
        // Arrange
        var resource = WebAppPoolResource.Create("MyAppPool", pool =>
        {
            pool.PoolName = "MyAppPool";
            pool.IdentityType = AppPoolIdentityType.SpecificUser;
        });

        // Act
        var errors = await resource.Validate();

        // Assert
        Assert.AreEqual(1, errors.Count);
    }

    [TestMethod]
    public async Task Validate_WhenCredentialIsSetForNonSpecificUserIdentity_ReturnsError()
    {
        // Arrange
        var resource = WebAppPoolResource.Create("MyAppPool", pool =>
        {
            pool.PoolName = "MyAppPool";
            pool.IdentityType = AppPoolIdentityType.ApplicationPoolIdentity;
            pool.Credential = new GmsaCredential(@"CONTOSO\svc-web$");
        });

        // Act
        var errors = await resource.Validate();

        // Assert
        Assert.AreEqual(1, errors.Count);
    }

    [TestMethod]
    public async Task Validate_WhenIdentityTypeIsSpecificUserAndGmsaCredentialIsProvided_ReturnsNoErrors()
    {
        // Arrange
        var resource = WebAppPoolResource.Create("MyAppPool", pool =>
        {
            pool.PoolName = "MyAppPool";
            pool.IdentityType = AppPoolIdentityType.SpecificUser;
            pool.Credential = new GmsaCredential(@"CONTOSO\svc-web$");
        });

        // Act
        var errors = await resource.Validate();

        // Assert
        Assert.AreEqual(0, errors.Count);
    }

    [TestMethod]
    public void GmsaCredential_ToPowerShell_EscapesSingleQuotes()
    {
        // Arrange
        var credential = new GmsaCredential(@"CONTOSO\svc'o$");

        // Act
        var result = credential.ToPowerShell();

        // Assert
        Assert.AreEqual("[PSCredential]::new('CONTOSO\\svc''o$', [System.Security.SecureString]::new())", result);
    }

    [TestMethod]
    public void GmsaCredential_WhenAccountNameIsNotGmsa_ThrowsArgumentException()
    {
        // Arrange
        var invalidAccountNames = new[]
        {
            string.Empty,
            "svc-web",
            @"CONTOSO\svc-web",
            "username:password",
        };

        foreach (var accountName in invalidAccountNames)
        {
            // Act / Assert
            Assert.ThrowsExactly<ArgumentException>(() => new GmsaCredential(accountName));
        }
    }
}


