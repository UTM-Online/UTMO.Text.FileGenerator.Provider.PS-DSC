namespace DSCProviderCore.Tests;

#pragma warning disable MSTEST0037

using Microsoft.VisualStudio.TestTools.UnitTesting;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.CertificatesDsc;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.CertificatesDsc.Enums;

[TestClass]
public class CertReqResourceTests
{
    [TestMethod]
    public void Create_WithValidConfiguration_CreatesResource()
    {
        // Arrange & Act
        var resource = CertReqResource.Create("MyCertRequest", cert =>
        {
            cert.Subject = "CN=example.com";
            cert.CAServerFQDN = "ca.example.com";
            cert.CARootName = "ExampleRootCA";
            cert.KeyType = CertificateRequestKeyType.RSA;
            cert.KeyLength = 2048;
        });

        // Assert
        Assert.IsNotNull(resource);
        Assert.AreEqual("MyCertRequest", resource.Name);
        Assert.AreEqual("CN=example.com", resource.Subject);
        Assert.AreEqual("ca.example.com", resource.CAServerFQDN);
        Assert.AreEqual("ExampleRootCA", resource.CARootName);
        Assert.AreEqual(CertificateRequestKeyType.RSA, resource.KeyType);
        Assert.AreEqual(2048, resource.KeyLength);
    }

    [TestMethod]
    public void Create_WithOptionalProperties_SetsCorrectly()
    {
        // Arrange & Act
        var resource = CertReqResource.Create("MyCertRequest", cert =>
        {
            cert.Subject = "CN=example.com";
            cert.CAServerFQDN = "ca.example.com";
            cert.CARootName = "ExampleRootCA";
            cert.Exportable = true;
            cert.AutoRenew = true;
            cert.HashAlgorithm = CertificateRequestHashAlgorithm.SHA256;
        });

        // Assert
        Assert.IsTrue(resource.Exportable);
        Assert.IsTrue(resource.AutoRenew);
        Assert.AreEqual(CertificateRequestHashAlgorithm.SHA256, resource.HashAlgorithm);
    }

    [TestMethod]
    public void Create_WithSubjectAlternativeNames_SetsCorrectly()
    {
        // Arrange
        var sans = new[] { "example.com", "www.example.com" };

        // Act
        var resource = CertReqResource.Create("MyCertRequest", cert =>
        {
            cert.Subject = "CN=example.com";
            cert.CAServerFQDN = "ca.example.com";
            cert.CARootName = "ExampleRootCA";
            cert.SubjectAltName = sans;
        });

        // Assert
        Assert.IsNotNull(resource.SubjectAltName);
        Assert.AreEqual(2, resource.SubjectAltName.Length);
        Assert.AreEqual("example.com", resource.SubjectAltName[0]);
        Assert.AreEqual("www.example.com", resource.SubjectAltName[1]);
    }

    [TestMethod]
    public void Validate_WithMissingSubject_ReturnsError()
    {
        // Arrange
        var resource = CertReqResource.Create("MyCertRequest", cert =>
        {
            cert.CAServerFQDN = "ca.example.com";
            cert.CARootName = "ExampleRootCA";
        });

        // Act
        var errors = resource.Validate().Result;

        // Assert
        Assert.IsNotEmpty(errors);
    }

    [TestMethod]
    public void Validate_WithMissingCAServerFQDN_ReturnsError()
    {
        // Arrange
        var resource = CertReqResource.Create("MyCertRequest", cert =>
        {
            cert.Subject = "CN=example.com";
            cert.CARootName = "ExampleRootCA";
        });

        // Act
        var errors = resource.Validate().Result;

        // Assert
        Assert.IsNotEmpty(errors);
    }

    [TestMethod]
    public void Validate_WithMissingCARootName_ReturnsError()
    {
        // Arrange
        var resource = CertReqResource.Create("MyCertRequest", cert =>
        {
            cert.Subject = "CN=example.com";
            cert.CAServerFQDN = "ca.example.com";
        });

        // Act
        var errors = resource.Validate().Result;

        // Assert
        Assert.IsNotEmpty(errors);
    }

    [TestMethod]
    public void Validate_WithAllRequiredProperties_ReturnsNoErrors()
    {
        // Arrange
        var resource = CertReqResource.Create("MyCertRequest", cert =>
        {
            cert.Subject = "CN=example.com";
            cert.CAServerFQDN = "ca.example.com";
            cert.CARootName = "ExampleRootCA";
        });

        // Act
        var errors = resource.Validate().Result;

        // Assert
        Assert.IsEmpty(errors);
    }

    [TestMethod]
    public void ResourceId_ReturnsCorrectValue()
    {
        // Arrange & Act
        var resource = CertReqResource.Create("MyCertRequest", cert =>
        {
            cert.Subject = "CN=example.com";
            cert.CAServerFQDN = "ca.example.com";
            cert.CARootName = "ExampleRootCA";
        });

        // Assert
        Assert.AreEqual("CertReq", resource.ResourceId);
    }

    [TestMethod]
    public void Create_WithOutParameter_ReturnsResourceTwice()
    {
        // Arrange & Act
        var result = CertReqResource.Create("MyCertRequest", cert =>
        {
            cert.Subject = "CN=example.com";
            cert.CAServerFQDN = "ca.example.com";
            cert.CARootName = "ExampleRootCA";
        }, out var outResource);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsNotNull(outResource);
        Assert.AreSame(result, outResource);
    }

    [TestMethod]
    public void Create_WithKeyUsage_SetsCorrectly()
    {
        // Arrange
        var keyUsages = new[] { "DigitalSignature", "KeyEncipherment" };

        // Act
        var resource = CertReqResource.Create("MyCertRequest", cert =>
        {
            cert.Subject = "CN=example.com";
            cert.CAServerFQDN = "ca.example.com";
            cert.CARootName = "ExampleRootCA";
            cert.KeyUsage = keyUsages;
        });

        // Assert
        Assert.IsNotNull(resource.KeyUsage);
#pragma warning disable MSTEST0037
        Assert.AreEqual(2, resource.KeyUsage.Length);
#pragma warning restore MSTEST0037
        Assert.AreEqual("DigitalSignature", resource.KeyUsage[0]);
    }

    [TestMethod]
    public void Create_WithEnhancedKeyUsage_SetsCorrectly()
    {
        // Arrange
        var ekus = new[] { "1.3.6.1.5.5.7.3.1" };

        // Act
        var resource = CertReqResource.Create("MyCertRequest", cert =>
        {
            cert.Subject = "CN=example.com";
            cert.CAServerFQDN = "ca.example.com";
            cert.CARootName = "ExampleRootCA";
            cert.EnhancedKeyUsage = ekus;
        });

        // Assert
        Assert.IsNotNull(resource.EnhancedKeyUsage);
#pragma warning disable MSTEST0037
        Assert.AreEqual(1, resource.EnhancedKeyUsage.Length);
#pragma warning restore MSTEST0037
        Assert.AreEqual("1.3.6.1.5.5.7.3.1", resource.EnhancedKeyUsage[0]);
    }

    [TestMethod]
    public void SourceModule_ReturnsCertificatesDscModule()
    {
        // Arrange
        var resource = CertReqResource.Create("MyCertRequest", cert =>
        {
            cert.Subject = "CN=example.com";
            cert.CAServerFQDN = "ca.example.com";
            cert.CARootName = "ExampleRootCA";
        });

        // Act
        var module = resource.SourceModule;

        // Assert
        Assert.IsNotNull(module);
        Assert.AreEqual("CertificateDsc", module.ModuleName);
    }
}







