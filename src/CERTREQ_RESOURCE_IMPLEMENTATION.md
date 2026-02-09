# CertReq Resource Implementation

The CertReq resource from the CertificatesDsc module has been successfully implemented in the UTMO.Text.FileGenerator.Provider.DSC.WindowsCore library.

## Files Created

### Module Definition
- **CertificatesDsc.cs** - Module definition for the CertificatesDsc PowerShell module
  - Location: `UTMO.Text.FileGenerator.Provider.DSC.WindowsCore\ModuleDefinitions\CertificatesDsc.cs`

### Base Class
- **CertificatesDscBase.cs** - Abstract base class for CertificatesDsc resources
  - Location: `UTMO.Text.FileGenerator.Provider.DSC.WindowsCore\BaseDefinitions\CertificatesDscBase.cs`
  - Inherits from `DscConfigurationItem`
  - Provides `SourceModule` property for CertificatesDsc module
  - Supports `Ensure` property for DSC configurations

### Resource Implementation
- **CertReqResource.cs** - Main CertReq resource implementation
  - Location: `UTMO.Text.FileGenerator.Provider.DSC.WindowsCore\Resources\CertificatesDsc\CertReqResource.cs`
  - Inherits from `CertificatesDscBase`
  - Implements `ICertReqResource` interface

### Interface
- **ICertReqResource.cs** - Contract for CertReq resource properties
  - Location: `UTMO.Text.FileGenerator.Provider.DSC.WindowsCore\Resources\CertificatesDsc\Contracts\ICertReqResource.cs`
  - Inherits from `IDscResourceConfig`

### Enums
- **CertificateRequestKeyType.cs** - Enum for key types (RSA, ECDH_P256, ECDH_P384, ECDH_P521)
  - Location: `UTMO.Text.FileGenerator.Provider.DSC.WindowsCore\Resources\CertificatesDsc\Enums\CertificateRequestKeyType.cs`

- **CertificateRequestHashAlgorithm.cs** - Enum for hash algorithms (SHA1, SHA256, SHA384, SHA512)
  - Location: `UTMO.Text.FileGenerator.Provider.DSC.WindowsCore\Resources\CertificatesDsc\Enums\CertificateRequestHashAlgorithm.cs`

### Constants
- **CertificatesDscConstants.cs** - Resource constants and property names
  - Location: `UTMO.Text.FileGenerator.Provider.DSC.WindowsCore\Constants\CertificatesDscConstants.cs`
  - Defines resource ID and property mapping

### Tests
- **CertReqResourceTests.cs** - Comprehensive unit tests
  - Location: `DSCProviderCore.Tests\CertReqResourceTests.cs`
  - 12 test methods covering all functionality
  - Tests validate resource creation, property assignment, and error handling

## Properties Implemented

The CertReqResource supports the following properties:

### Required Properties
- **Subject** (string) - The subject of the certificate request
- **CAServerFQDN** (string) - The fully qualified domain name of the certificate authority server
- **CARootName** (string) - The name of the root certificate authority

### Optional Properties
- **KeyType** (CertificateRequestKeyType?) - The type of key to use (RSA or ECDH)
- **KeyLength** (int?) - The length of the key in bits (1024, 2048, 4096)
- **Exportable** (bool?) - Whether the certificate private key should be exportable
- **ProviderName** (string) - The name of the key storage provider
- **OID** (string) - The object identifier (OID) for the certificate
- **KeyUsage** (string[]) - The key usage extensions for the certificate
- **EnhancedKeyUsage** (string[]) - The enhanced key usage extensions for the certificate
- **SubjectAltName** (string[]) - The subject alternative names for the certificate
- **Credential** (string) - The credential to use for the certificate request
- **AutoRenew** (bool?) - Whether the certificate should be automatically renewed
- **CAType** (string) - The type of certificate authority (Enterprise or Standalone)
- **CertificateTemplate** (string) - The certificate template to use for the request
- **SubjectFormat** (string) - The format for the subject name
- **SAN** (string[]) - The subject alternative names (alias for SubjectAltName)
- **KeyAlgorithm** (string) - The key algorithm to use
- **HashAlgorithm** (CertificateRequestHashAlgorithm?) - The hash algorithm to use

## Usage Example

```csharp
var certResource = CertReqResource.Create("MyCertRequest", cert =>
{
    cert.Subject = "CN=example.com";
    cert.CAServerFQDN = "ca.example.com";
    cert.CARootName = "ExampleRootCA";
    cert.KeyType = CertificateRequestKeyType.RSA;
    cert.KeyLength = 2048;
    cert.Exportable = true;
    cert.HashAlgorithm = CertificateRequestHashAlgorithm.SHA256;
    cert.SubjectAltName = new[] { "example.com", "www.example.com" };
});
```

## Attributes Used
- **[UnquotedEnum]** - Applied to `KeyType` and `HashAlgorithm` properties to render enum values without quotes in DSC output

## Validation
- All three required properties (Subject, CAServerFQDN, CARootName) are validated to ensure they are not null or empty
- Validation is performed asynchronously via the `Validate()` method
- Returns a list of `ValidationFailedException` objects if validation fails

## Test Results
- Total Tests: 55
  - Original Tests: 43
  - New CertReqResource Tests: 12
- All tests passed successfully
- Test coverage includes:
  - Resource creation with valid configuration
  - Optional property assignment
  - Subject alternative names handling
  - Validation for missing required properties
  - Validation with all required properties
  - Resource ID verification
  - Out parameter creation
  - Key usage and enhanced key usage handling
  - Source module verification

## Pattern Followed
The implementation follows the established patterns in the UTMO.Text.FileGenerator.Provider.DSC framework:
- Inherits from appropriate base class (`CertificatesDscBase`)
- Uses PropertyBag for storing resource properties
- Maps C# property names to DSC resource properties via constants
- Implements validation for required properties
- Provides static `Create` factory methods with Action<T> configuration pattern
- Includes comprehensive XML documentation comments
- Uses proper enum handling with [UnquotedEnum] attribute where needed

