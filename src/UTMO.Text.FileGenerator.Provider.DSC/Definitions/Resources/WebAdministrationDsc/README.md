# WebAdministrationDsc Resources

This module provides DSC resources for configuring IIS (Internet Information Services) web servers using the WebAdministrationDsc PowerShell module version 4.2.1.

## Module Information

- **Module Name**: WebAdministrationDsc
- **Version**: 4.2.1
- **PowerShell Gallery**: https://www.powershellgallery.com/packages/WebAdministrationDsc/4.2.1

## Available Resources

| Resource | Description |
|----------|-------------|
| [IisFeatureDelegation](#iisfeaturedelegation) | Configures IIS feature delegation (lock/unlock configuration sections) |
| [IisLogging](#iislogging) | Configures IIS logging settings at the server level |
| [IisMimeTypeMapping](#iismimetypemapping) | Maps file extensions to MIME types |
| [IisModule](#iismodule) | Configures IIS modules (handlers) |
| [SslSettings](#sslsettings) | Configures SSL settings for a website |
| [WebAppPool](#webapppool) | Creates and configures IIS application pools |
| [WebAppPoolDefaults](#webapppooldefaults) | Configures default settings for application pools |
| [WebApplication](#webapplication) | Creates and configures IIS web applications |
| [WebApplicationHandler](#webapplicationhandler) | Configures IIS request handlers |
| [WebConfigProperty](#webconfigproperty) | Sets individual web.config properties |
| [WebConfigPropertyCollection](#webconfigpropertycollection) | Manages web.config property collections |
| [WebSite](#website) | Creates and configures IIS websites |
| [WebSiteDefaults](#websitedefaults) | Configures default settings for websites |
| [WebVirtualDirectory](#webvirtualdirectory) | Creates and configures virtual directories |

## Resource Details

### IisFeatureDelegation

Configures whether configuration sections are locked or unlocked at the IIS level.

**Key Properties:**
- `Path` - The configuration path
- `Filter` - The configuration section to lock/unlock
- `OverrideMode` - Allow or Deny

### IisLogging

Configures IIS logging at the server level.

**Key Properties:**
- `LogPath` - Directory for log files
- `LogFlags` - W3C logging fields to include
- `LogPeriod` - How often to rotate log files
- `LogFormat` - Log file format (IIS, W3C, NCSA, Custom)

### IisMimeTypeMapping

Maps file extensions to MIME types.

**Key Properties:**
- `ConfigurationPath` - The IIS configuration path
- `Extension` - File extension (e.g., .json)
- `MimeType` - MIME type (e.g., application/json)

### IisModule

Registers and configures IIS modules.

**Key Properties:**
- `ModulePath` - Path to the module DLL
- `ModuleName` - Logical name of the module
- `RequestPath` - Request path pattern (e.g., *.php)
- `Verb` - Supported HTTP verbs

### SslSettings

Configures SSL settings for a website.

**Key Properties:**
- `SiteName` - Name of the website
- `Bindings` - SSL binding flags (Ssl, SslNegotiateCert, SslRequireCert, Ssl128)

### WebAppPool

Creates and configures IIS application pools.

**Key Properties:**
- `PoolName` - Name of the application pool
- `State` - Started or Stopped
- `ManagedPipelineMode` - Integrated or Classic
- `ManagedRuntimeVersion` - CLR version (v4.0, v2.0, or empty for no managed code)
- `IdentityType` - Identity type (ApplicationPoolIdentity, NetworkService, etc.)
- `StartMode` - OnDemand or AlwaysRunning

### WebAppPoolDefaults

Configures default settings for all application pools.

**Key Properties:**
- `IsSingleInstance` - Must be "Yes"
- `ManagedRuntimeVersion` - Default CLR version
- `IdentityType` - Default identity type

### WebApplication

Creates and configures IIS web applications.

**Key Properties:**
- `Website` - Parent website name
- `ApplicationName` - Name of the application
- `WebAppPool` - Application pool to use
- `PhysicalPath` - Physical path on disk
- `EnabledProtocols` - Enabled protocols

### WebApplicationHandler

Configures IIS request handlers.

**Key Properties:**
- `HandlerName` - Name of the handler
- `Path` - Request paths
- `Verb` - HTTP verbs
- `Type` - Managed type (for managed handlers)
- `ScriptProcessor` - Script processor path

### WebConfigProperty

Sets individual properties in web.config.

**Key Properties:**
- `WebsitePath` - Path to the website
- `Filter` - XPath filter to locate the property
- `PropertyName` - Name of the property
- `Value` - Value to set

### WebConfigPropertyCollection

Manages collection items in web.config.

**Key Properties:**
- `WebsitePath` - Path to the website
- `Filter` - XPath filter
- `CollectionName` - Name of the collection
- `ItemName` - Name of collection items
- `ItemKeyName` / `ItemKeyValue` - Key to identify the item
- `ItemPropertyName` / `ItemPropertyValue` - Property to set

### WebSite

Creates and configures IIS websites.

**Key Properties:**
- `SiteName` - Name of the website
- `PhysicalPath` - Physical path on disk
- `State` - Started or Stopped
- `ApplicationPool` - Application pool name
- `BindingInfo` - Binding information (protocol, IP, port, hostname)
- `DefaultPage` - Default documents

### WebSiteDefaults

Configures default settings for all websites.

**Key Properties:**
- `IsSingleInstance` - Must be "Yes"
- `LogFormat` - Default log format
- `LogDirectory` - Default log directory
- `DefaultApplicationPool` - Default application pool

### WebVirtualDirectory

Creates and configures virtual directories.

**Key Properties:**
- `Website` - Parent website name
- `WebApplication` - Parent application name
- `VirtualDirectoryName` - Name of the virtual directory
- `PhysicalPath` - Physical path on disk

## Usage Example

```csharp
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Enums;

// Create an application pool
var appPool = WebAppPoolResource.Create("MyAppPool", pool =>
{
    pool.PoolName = "MyAppPool";
    pool.State = AppPoolState.Started;
    pool.ManagedPipelineMode = ManagedPipelineMode.Integrated;
    pool.ManagedRuntimeVersion = "v4.0";
    pool.IdentityType = AppPoolIdentityType.ApplicationPoolIdentity;
});

// Create a website
var website = WebSiteResource.Create("MySite", site =>
{
    site.SiteName = "MySite";
    site.PhysicalPath = @"C:\inetpub\wwwroot\mysite";
    site.State = WebSiteState.Started;
    site.ApplicationPool = "MyAppPool";
});
```

## Notes

- All resources inherit from `WebAdministrationDscBase` which provides the module definition
- The module requires the WebAdministrationDsc PowerShell module to be installed
- Properties use the PropertyBag pattern for serialization to Liquid templates

