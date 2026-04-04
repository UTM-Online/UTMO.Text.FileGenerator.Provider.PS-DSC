namespace DSCProviderCore.Tests;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;
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
    public void RequiresPlainTextPassword_WhenGmsaCredentialIsSet_ReturnsTrue()
    {
        // Arrange
        var resource = WebAppPoolResource.Create("MyAppPool", pool =>
        {
            pool.PoolName = "MyAppPool";
            pool.IdentityType = AppPoolIdentityType.SpecificUser;
            pool.Credential = new GmsaCredential(@"CONTOSO\svc-web$");
        });

        // Act / Assert
        Assert.IsTrue(resource.RequiresPlainTextPassword);
    }

    [TestMethod]
    public void RequiresPlainTextPassword_WhenGmsaCredentialIsNotSet_ReturnsFalse()
    {
        // Arrange
        var resource = WebAppPoolResource.Create("MyAppPool", pool =>
        {
            pool.PoolName = "MyAppPool";
            pool.IdentityType = AppPoolIdentityType.ApplicationPoolIdentity;
        });

        // Act / Assert
        Assert.IsFalse(resource.RequiresPlainTextPassword);
    }

    [TestMethod]
    public void DscConfiguration_RequiresPlainTextPassword_WhenGmsaCredentialIsUsed_ReturnsTrue()
    {
        // Arrange
        var resource = WebAppPoolResource.Create("MyAppPool", pool =>
        {
            pool.PoolName = "MyAppPool";
            pool.IdentityType = AppPoolIdentityType.SpecificUser;
            pool.Credential = new GmsaCredential(@"CONTOSO\svc-web$");
        });

        var configuration = new TestDscConfiguration(resource);

        // Act / Assert
        Assert.IsTrue(configuration.RequiresPlainTextPassword);
    }

    [TestMethod]
    public void DscConfiguration_RequiresPlainTextPassword_WhenGmsaCredentialIsNotUsed_ReturnsFalse()
    {
        // Arrange
        var resource = WebAppPoolResource.Create("MyAppPool", pool =>
        {
            pool.PoolName = "MyAppPool";
            pool.IdentityType = AppPoolIdentityType.ApplicationPoolIdentity;
        });

        var configuration = new TestDscConfiguration(resource);

        // Act / Assert
        Assert.IsFalse(configuration.RequiresPlainTextPassword);
    }

    [TestMethod]
    public void DscComputerConfiguration_RequiresPlainTextPassword_WhenGmsaCredentialIsUsed_ReturnsTrue()
    {
        // Arrange
        var resource = WebAppPoolResource.Create("MyAppPool", pool =>
        {
            pool.PoolName = "MyAppPool";
            pool.IdentityType = AppPoolIdentityType.SpecificUser;
            pool.Credential = new GmsaCredential(@"CONTOSO\svc-web$");
        });

        var configuration = new DscComputerConfiguration
        {
            NodeName = "localhost",
            NodeConfigurations = [resource],
        };

        // Act / Assert
        Assert.IsTrue(configuration.RequiresPlainTextPassword);
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
            @"CONTOSO\svc web$",
            @"CONTOSO\svc-web$ ",
            @" CONTOSO\svc-web$",
            "CONTOSO\\svc\nweb$",
            "CONTOSO\\svc\tweb$",
            @"CONTOSO\svc/web$",
            @"CONTOSO\svc[web$",
        };

        foreach (var accountName in invalidAccountNames)
        {
            // Act / Assert
            Assert.ThrowsExactly<ArgumentException>(() => new GmsaCredential(accountName));
        }
    }

    [TestMethod]
    public void GmsaCredential_WhenAccountNameIsSamCompatible_AllowsAccountOrDomainQualifiedForms()
    {
        // Arrange
        var validAccountNames = new[]
        {
            "svc-web$",
            @"CONTOSO\svc-web$",
            @"CONTOSO\svc'o$",
            @"contoso.local\svc.web_01$",
        };

        foreach (var accountName in validAccountNames)
        {
            // Act
            var credential = new GmsaCredential(accountName);

            // Assert
            Assert.AreEqual(accountName, credential.AccountName);
        }
    }

    [TestMethod]
    public void GmsaCredential_WhenAccountNameContainsLineBreak_ThrowsArgumentException()
    {
        // Act / Assert
        Assert.ThrowsExactly<ArgumentException>(() => new GmsaCredential("CONTOSO\\svc\r\nweb$"));
    }

    private sealed class TestDscConfiguration(params DscConfigurationItem[] items) : DscConfiguration
    {
        private readonly IReadOnlyList<DscConfigurationItem> _items = items;

        public override string FullName => nameof(TestDscConfiguration);

        public override string ModuleSource => nameof(TestDscConfiguration);

        public override string ConfigSource => nameof(TestDscConfiguration);

        protected override IEnumerable<DscConfigurationItem> ConfigurationItems() => this._items;
    }
}
