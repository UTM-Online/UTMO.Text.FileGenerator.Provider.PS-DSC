## Service Resource Implementation

The Service resource type from the PSDesiredStateConfiguration module has been successfully implemented.

### Files Created:

1. **ServiceResource.cs** - Main resource implementation
   - Location: `UTMO.Text.FileGenerator.Provider.DSC.WindowsCore\Resources\PSDesiredStateConfiguration\ServiceResource.cs`
   
2. **IServiceResource.cs** - Interface contract
   - Location: `UTMO.Text.FileGenerator.Provider.DSC.WindowsCore\Resources\PSDesiredStateConfiguration\Contracts\IServiceResource.cs`
   
3. **ServiceState.cs** - Enum for service state (Running/Stopped)
   - Location: `UTMO.Text.FileGenerator.Provider.DSC.WindowsCore\Resources\PSDesiredStateConfiguration\Enums\ServiceState.cs`
   
4. **ServiceStartupType.cs** - Enum for startup type (Automatic/Manual/Disabled)
   - Location: `UTMO.Text.FileGenerator.Provider.DSC.WindowsCore\Resources\PSDesiredStateConfiguration\Enums\ServiceStartupType.cs`

### Files Modified:

1. **PSDesiredStateConfigurationConstants.cs** - Added Service resource constants
   - Location: `UTMO.Text.FileGenerator.Provider.DSC.WindowsCore\Constants\PSDesiredStateConfigurationConstants.cs`

### Properties Implemented:

- **ServiceName** (string, required) - The name of the service
- **State** (ServiceState?, optional) - The desired state (Running/Stopped)
- **StartupType** (ServiceStartupType?, optional) - The startup type (Automatic/Manual/Disabled)
- **BuiltInAccount** (string, optional) - Built-in account to run the service under
- **Credential** (string, optional) - Credential to run the service under
- **Dependencies** (string[], optional) - Service dependencies
- **ServiceDescription** (string, optional) - Service description
- **DisplayName** (string, optional) - Display name for the service
- **Path** (string, optional) - Path to the service executable

### Usage Example:

```csharp
var serviceResource = ServiceResource.Create("MyService", svc =>
{
    svc.ServiceName = "MyWindowsService";
    svc.State = ServiceState.Running;
    svc.StartupType = ServiceStartupType.Automatic;
    svc.DisplayName = "My Windows Service";
    svc.ServiceDescription = "This is my custom Windows service";
});
```

### Pattern Followed:

The implementation follows the same pattern as other DSC resources in the solution:
- Inherits from `PSDesiredStateConfigurationBase`
- Uses PropertyBag for storing resource properties
- Maps C# property names to DSC resource properties via constants
- Implements validation for required properties
- Provides static `Create` factory methods with Action<T> configuration pattern
- Uses separate property names (ServiceName, ServiceDescription) to avoid conflicts with base class properties (Name, Description)

