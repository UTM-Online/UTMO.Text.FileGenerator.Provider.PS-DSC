# UTMO.Text.FileGenerator.Provider.PS-DSC

A .NET 9.0 library that generates PowerShell Desired State Configuration (DSC) scripts using Liquid templates. It provides a C# API to define DSC resources that are rendered into `.ps1` configuration scripts. This library is part of the UTMO ConfigGen ecosystem and uses the `UTMO.Text.FileGenerator` framework.

## Features

- **Type-safe DSC resource definitions** - Define DSC configurations using strongly-typed C# classes
- **Liquid template rendering** - Generate PowerShell DSC scripts from Liquid templates
- **Multiple DSC module support** - Built-in support for common DSC modules:
  - PSDesiredStateConfiguration (Service, Registry, File, etc.)
  - ActiveDirectoryDsc
  - NetworkingDsc
  - CertificateDsc
  - ComputerManagementDsc
  - SecurityPolicyDsc
  - cChoco (Chocolatey)
  - WindowsDefenderDsc
- **Validation** - Built-in validation for required properties and configuration correctness
- **MOF generation** - Automatic MOF file generation from DSC configurations

## Getting Started

### Prerequisites

- .NET 9.0 SDK or later

### Installation

Packages are available from the public NuGet repository:

```
https://packages.public.utmonline.net/nuget/TextFileGeneration/v3/index.json
```

Add the following package reference to your project:

```xml
<PackageReference Include="UTMO.Text.FileGenerator.Provider.DSC" Version="x.x.x" />
```

Configure your `nuget.config` to include the NuGet feed:

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <add key="TextFileGeneration" value="https://packages.public.utmonline.net/nuget/TextFileGeneration/v3/index.json" />
  </packageSources>
</configuration>
```

## Usage

### Creating a DSC Configuration

```csharp
using UTMO.Text.FileGenerator.Provider.DSC;

// Create the DSC Generator
var generator = DscGenerator.Create(args);

// Define a service resource
var serviceResource = ServiceResource.Create("MyService", svc =>
{
    svc.ServiceName = "MyWindowsService";
    svc.State = ServiceState.Running;
    svc.StartupType = ServiceStartupType.Automatic;
    svc.DisplayName = "My Windows Service";
    svc.ServiceDescription = "This is my custom Windows service";
});

// Run the generator
generator.Run();
```

### Creating a Certificate Request Resource

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

### Resource Pattern

All DSC resources follow a consistent pattern:

1. **Factory method** - Use the static `Create(name, configure)` method
2. **Fluent configuration** - Configure properties via the action delegate
3. **Validation** - Resources validate required properties automatically

## Project Structure

| Project | Description |
|---------|-------------|
| `UTMO.Text.FileGenerator.Provider.DSC` | Main library - DSC generator, templates, and plugins |
| `UTMO.Text.FileGenerator.Provider.DSC.Abstract` | Base types, contracts, attributes, and enums |
| `UTMO.Text.FileGenerator.Provider.DSC.WindowsCore` | Windows DSC resources (PSDesiredStateConfiguration, NetworkingDsc, CertificateDsc, etc.) |
| `UTMO.Text.FileGenerator.Provider.DSC.AD-DSC` | Active Directory DSC resources |
| `UTMO.Text.FileGenerator.Provider.DSC.cChoco` | Chocolatey DSC resources |
| `WindowsDefenderDsc` | Windows Defender DSC resources |
| `DSCProviderCore.Tests` | Unit tests |

## Build and Test

All commands should be run from the `src` directory.

### Restore Dependencies

```powershell
dotnet restore
```

### Build

```powershell
dotnet build                         # Debug build
dotnet build --configuration Release # Release build
```

### Test

```powershell
dotnet test
```

### Clean and Rebuild

```powershell
dotnet clean ; dotnet build
```

## Dependencies

- `UTMO.Text.FileGenerator` - Core file generation framework
- `UTMO.Text.FileGenerator.Validators` - Validation utilities
- `UTMO.Common.Guards` - Guard clauses
- `System.Management.Automation` - PowerShell types

## Creating Custom DSC Resources

To create a new DSC resource:

1. **Add Constants** in the appropriate `*Constants.cs` file
2. **Create Interface** defining the resource properties
3. **Create Resource Class** inheriting from the appropriate base class
4. **Add Enums** if needed for property values

Example resource implementation:

```csharp
public sealed class MyResource : PSDesiredStateConfigurationBase, IMyResource
{
    private MyResource(string name) : base(name) { }

    public string PropertyName
    {
        get => this.PropertyBag.Get(Constants.Properties.PropertyName);
        set => this.PropertyBag.Set(Constants.Properties.PropertyName, value);
    }

    public static MyResource Create(string name, Action<IMyResource> configure)
    {
        var resource = new MyResource(name);
        configure(resource);
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var errors = this.ValidationBuilder()
            .ValidateStringNotNullOrEmpty(this.PropertyName, nameof(this.PropertyName))
            .errors;
        return Task.FromResult(errors);
    }

    public override string ResourceId => Constants.ResourceId;
}
```

### Key Attributes

- `[UnquotedEnum]` - Renders enum values without quotes in DSC output
- `[MemberName("name")]` - Maps C# properties to Liquid template variable names

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes following the existing patterns
4. Ensure all tests pass: `dotnet test`
5. Ensure no build warnings (warnings are treated as errors)
6. Submit a pull request

## License

Licensed under the Apache License, Version 2.0. See [LICENSE](LICENSE) for the full license text.

Copyright 2026 Joshua S. Irwin. All rights reserved. Use of this source code is governed by the Apache License that can be found in the LICENSE file.
