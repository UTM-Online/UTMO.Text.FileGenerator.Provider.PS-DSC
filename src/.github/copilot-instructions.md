# Copilot Instructions for UTMO.Text.FileGenerator.Provider.PS-DSC

## Repository Summary

This is a .NET 9.0 library that generates PowerShell Desired State Configuration (DSC) scripts using Liquid templates. It provides a C# API to define DSC resources that are rendered into `.ps1` configuration scripts. The library is part of the UTMO ConfigGen ecosystem and uses the `UTMO.Text.FileGenerator` framework.

## Build and Test Commands

**Always run commands from the `src` directory.** All commands use the .NET CLI.

### Restore Dependencies
```powershell
dotnet restore
```
- NuGet packages come from Azure DevOps feed: `https://pkgs.dev.azure.com/utmo-public/_packaging/ConfigGen/nuget/v3/index.json`
- Package versions are managed centrally via `Directory.Packages.props`

### Build
```powershell
dotnet build                        # Debug build
dotnet build --configuration Release # Release build (generates NuGet packages)
```
- Build settings: `TreatWarningsAsErrors=True` (from `Directory.Build.props`)
- Projects generate NuGet packages on build (`GeneratePackageOnBuild=true`)

### Test
```powershell
dotnet test
```
- Tests use MSTest framework with parallel execution at method level
- Test project: `DSCProviderCore.Tests`
- 43 tests covering `DscConfigurationPropertyBag` functionality

### Clean and Rebuild
```powershell
dotnet clean ; dotnet build
```

## Project Structure

### Solution Layout (`UTMO.Text.FileGenerator.Provider.PS-DSC.sln`)

| Project | Purpose |
|---------|---------|
| `UTMO.Text.FileGenerator.Provider.DSC` | Main library - DSC generator, templates, and plugins |
| `UTMO.Text.FileGenerator.Provider.DSC.Abstract` | Base types, contracts, attributes, and enums |
| `UTMO.Text.FileGenerator.Provider.DSC.WindowsCore` | Windows DSC resources (PSDesiredStateConfiguration, NetworkingDsc, etc.) |
| `UTMO.Text.FileGenerator.Provider.DSC.AD-DSC` | Active Directory DSC resources |
| `UTMO.Text.FileGenerator.Provider.DSC.cChoco` | Chocolatey DSC resources |
| `WindowsDefenderDsc` | Windows Defender DSC resources |
| `DSCProviderCore.Tests` | Unit tests |

### Key Files and Directories

```
src/
├── Directory.Build.props        # Global build settings (TreatWarningsAsErrors=True)
├── Directory.Packages.props     # Central package version management
├── global.json                  # .NET SDK version: 9.0.0+ (rollForward: latestMajor)
├── nuget.config                 # Azure DevOps NuGet feed configuration
└── UTMO.Text.FileGenerator.Provider.DSC/
    ├── Templates/               # Liquid templates (*.liquid)
    ├── DscGenerator.cs          # Main generation entry point
    └── Plugins/                 # Generation plugins
```

## Code Patterns

### Creating a New DSC Resource

1. **Add Constants** in the appropriate `*Constants.cs` file:
```csharp
public static class MyResource
{
    public const string ResourceId = "MyResource";
    public static class Properties
    {
        public const string PropertyName = "PropertyName";
    }
}
```

2. **Create Interface** in `Contracts/IMyResource.cs`:
```csharp
public interface IMyResource : IDscResourceConfig
{
    string PropertyName { get; set; }
}
```

3. **Create Resource Class** inheriting from the appropriate base:
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

4. **Add Enums** if needed in `Enums/` folder

### Key Base Classes
- `DscResourceBase` → Abstract base for DSC resources (output: `.ps1`)
- `DscConfigurationItem` → Base for configuration items with `PropertyBag`
- `DscConfigurationPropertyBag` → Property storage that renders to Liquid templates
- `RequiredModule` → Base for PowerShell module definitions

### Attribute Usage
- `[UnquotedEnum]` on properties to render enum values without quotes in DSC output
- `[MemberName("name")]` to map C# properties to Liquid template variable names

### PropertyBag Pattern
- Properties use `PropertyBag.Get<T>(key)` and `PropertyBag.Set(key, value)`
- Enum values are automatically converted to strings
- Nullable enums are supported
- `ToLiquid()` renders values for PowerShell (bools → `$true`/`$false`, strings → quoted)

## Dependencies

- `UTMO.Text.FileGenerator` - Core file generation framework
- `UTMO.Text.FileGenerator.Validators` - Validation utilities
- `UTMO.Common.Guards` - Guard clauses
- `System.Management.Automation` - PowerShell types
- `MSTest` / `Moq` - Testing (test project only)

## Validation Checklist

After making changes, always:
1. `dotnet build` - Must succeed with no warnings (warnings are errors)
2. `dotnet test` - All 43 tests must pass
3. For new resources, follow the existing pattern in `SERVICE_RESOURCE_IMPLEMENTATION.md`

## Notes

- Pipeline configuration is at `..\.pipelines\UTMO.Text.FileGenerator.Provider.DSC-Unified.yml` (outside workspace)
- The solution uses JetBrains ReSharper settings (`.DotSettings` files)
- Test parallelization is enabled at method level
- NuGet packages are restored to `.packages` directory
- When adding a new resource, create comprehensive documentation for with the following parameters
  - It is a markdown file compatible with docfx
    - Use the following format for the documents uid `SoftwareDev/UTMO-FileGeneration/DSC/Resources/<<ModuleName>>/<<ResourceName>>`
    - Use PowerShell Commands to read and write files as needed
  - Include the namespace and assembly information
  - Include summary, syntax, properties, and examples sections
  - place the file here "S:\Repos\UTMO\CentralDocumentation\OneDocs\Docs\SoftwareDev\UTMO-FileGeneration\DSC\Resources"
  - update the toc file at "S:\Repos\UTMO\CentralDocumentation\OneDocs\Docs\SoftwareDev\UTMO-FileGeneration\DSC\Resources\toc.yml"

Trust these instructions. Only search the codebase if information here is incomplete or incorrect.
