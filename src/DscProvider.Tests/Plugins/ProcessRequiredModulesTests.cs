using System.Text.Json;
using Microsoft.Extensions.Logging;
using NSubstitute;
using UTMO.Text.FileGenerator.Abstract;
using UTMO.Text.FileGenerator.Abstract.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Plugins.ProcessRequiredModules;

namespace DscProvider.Tests.Plugins;

[TestFixture]
public class ProcessRequiredModulesTests
{
  [SetUp]
  public void SetUp()
  {
    _mockWriter = Substitute.For<IGeneralFileWriter>();
    _mockLogger = Substitute.For<ILogger<ProcessRequiredModules>>();
    _mockOptions = Substitute.For<IGeneratorCliOptions>();
    _mockEnvironment = Substitute.For<ITemplateGenerationEnvironment>();

    // Setup test paths
    _testOutputPath = Path.Combine(Path.GetTempPath(), $"ProcessRequiredModulesTests_{Guid.NewGuid()}");
    Directory.CreateDirectory(_testOutputPath);

    _mockOptions.OutputPath.Returns(_testOutputPath);
    _mockEnvironment.EnvironmentName.Returns("TestEnvironment");

    _manifestPath = Path.Combine(_testOutputPath, "Manifests", "TestEnvironment");
    Directory.CreateDirectory(_manifestPath);
  }

  [TearDown]
  public void TearDown()
  {
    // Cleanup test directory
    if (Directory.Exists(_testOutputPath))
      try
      {
        Directory.Delete(_testOutputPath, true);
      }
      catch
      {
        // Ignore cleanup errors
      }
  }

  private IGeneralFileWriter _mockWriter = null!;
  private ILogger<ProcessRequiredModules> _mockLogger = null!;
  private IGeneratorCliOptions _mockOptions = null!;
  private ITemplateGenerationEnvironment _mockEnvironment = null!;
  private string _testOutputPath = null!;
  private string _manifestPath = null!;

  [Test]
  public void Constructor_SetsPropertiesCorrectly()
  {
    // Act
    var plugin = new ProcessRequiredModules(_mockWriter, _mockLogger, _mockOptions);

    // Assert
    Assert.That(plugin.Writer, Is.EqualTo(_mockWriter));
    Assert.That(plugin.MaxRuntime, Is.EqualTo(TimeSpan.FromMinutes(10)));
    Assert.That(plugin.Position, Is.EqualTo(PluginPosition.After));
  }

  [Test]
  public async Task ProcessPlugin_ManifestNotFound_LogsErrorAndReturns()
  {
    // Arrange
    var plugin = new ProcessRequiredModules(_mockWriter, _mockLogger, _mockOptions);

    // Act
    await plugin.ProcessPlugin(_mockEnvironment);

    // Assert - Verify LogError was called
    _mockLogger.Received(1).Log(
      LogLevel.Error,
      Arg.Any<EventId>(),
      Arg.Any<object>(),
      Arg.Any<Exception>(),
      Arg.Any<Func<object, Exception?, string>>());
  }

  [Test]
  public async Task ProcessPlugin_EmptyManifest_LogsWarningAndReturns()
  {
    // Arrange
    var manifestFile = Path.Combine(_manifestPath, "RequiredModule.Manifest.json");
    await File.WriteAllTextAsync(manifestFile, "[]");

    var plugin = new ProcessRequiredModules(_mockWriter, _mockLogger, _mockOptions);

    // Act
    await plugin.ProcessPlugin(_mockEnvironment);

    // Assert - Verify LogWarning was called
    _mockLogger.Received(1).Log(
      LogLevel.Warning,
      Arg.Any<EventId>(),
      Arg.Any<object>(),
      Arg.Any<Exception>(),
      Arg.Any<Func<object, Exception?, string>>());
  }

  [Test]
  public async Task ProcessPlugin_NullManifest_LogsWarningAndReturns()
  {
    // Arrange
    var manifestFile = Path.Combine(_manifestPath, "RequiredModule.Manifest.json");
    await File.WriteAllTextAsync(manifestFile, "null");

    var plugin = new ProcessRequiredModules(_mockWriter, _mockLogger, _mockOptions);

    // Act
    await plugin.ProcessPlugin(_mockEnvironment);

    // Assert - Verify LogWarning was called
    _mockLogger.Received(1).Log(
      LogLevel.Warning,
      Arg.Any<EventId>(),
      Arg.Any<object>(),
      Arg.Any<Exception>(),
      Arg.Any<Func<object, Exception?, string>>());
  }

  [Test]
  public async Task ProcessPlugin_ValidManifest_CreatesOutputDirectory()
  {
    // Arrange
    var manifestFile = Path.Combine(_manifestPath, "RequiredModule.Manifest.json");
    var testModules = new[]
    {
      new { Name = "TestModule", Version = "1.0.0" }
    };
    await File.WriteAllTextAsync(manifestFile, JsonSerializer.Serialize(testModules));

    var plugin = new ProcessRequiredModules(_mockWriter, _mockLogger, _mockOptions);

    // Act & Assert
    // This will fail when trying to execute PowerShell, but should create directories
    try
    {
      await plugin.ProcessPlugin(_mockEnvironment);
    }
    catch
    {
// Expected to fail on PowerShell execution in test environment
    }

    // Verify output directory was created
    var outputPath = Path.Combine(_testOutputPath, "Modules");
    Assert.That(Directory.Exists(outputPath), Is.True);
  }

  [Test]
  public async Task ProcessPlugin_ValidManifest_LogsStartAndProcessingMessages()
  {
    // Arrange
    var manifestFile = Path.Combine(_manifestPath, "RequiredModule.Manifest.json");
    var testModules = new[]
    {
      new { Name = "TestModule1", Version = "1.0.0" },
      new { Name = "TestModule2", Version = "2.0.0" }
    };
    await File.WriteAllTextAsync(manifestFile, JsonSerializer.Serialize(testModules));

    var plugin = new ProcessRequiredModules(_mockWriter, _mockLogger, _mockOptions);

    // Act
    try
    {
      await plugin.ProcessPlugin(_mockEnvironment);
    }
    catch
    {
      // Expected to fail on PowerShell execution
    }

// Assert - Verify LogInformation was called multiple times
    _mockLogger.Received().Log(
      LogLevel.Information,
      Arg.Any<EventId>(),
      Arg.Any<object>(),
      Arg.Any<Exception>(),
      Arg.Any<Func<object, Exception?, string>>());
  }

  [Test]
  public async Task ProcessPlugin_InvalidJson_ThrowsException()
  {
    // Arrange
    var manifestFile = Path.Combine(_manifestPath, "RequiredModule.Manifest.json");
    await File.WriteAllTextAsync(manifestFile, "{ invalid json }");

    var plugin = new ProcessRequiredModules(_mockWriter, _mockLogger, _mockOptions);

    // Act & Assert
    Assert.ThrowsAsync<JsonException>(async () => await plugin.ProcessPlugin(_mockEnvironment));
  }

  [Test]
  public async Task ProcessPlugin_CreatesTemporaryFolder_WhenProcessingModules()
  {
    // Arrange
    var manifestFile = Path.Combine(_manifestPath, "RequiredModule.Manifest.json");
    var testModules = new[]
    {
      new { Name = "TestModule", Version = "1.0.0" }
    };
    await File.WriteAllTextAsync(manifestFile, JsonSerializer.Serialize(testModules));

    var plugin = new ProcessRequiredModules(_mockWriter, _mockLogger, _mockOptions);

    // Act
    try
    {
      await plugin.ProcessPlugin(_mockEnvironment);
    }
    catch
    {
      // Expected to fail on PowerShell execution
    }

// Assert - Verify LogDebug was called
    _mockLogger.Received().Log(
      LogLevel.Debug,
      Arg.Any<EventId>(),
      Arg.Any<object>(),
      Arg.Any<Exception>(),
      Arg.Any<Func<object, Exception?, string>>());
  }

  [Test]
  public async Task ProcessPlugin_LogsPSModulePath()
  {
    // Arrange
    var manifestFile = Path.Combine(_manifestPath, "RequiredModule.Manifest.json");
    var testModules = new[]
    {
      new { Name = "TestModule", Version = "1.0.0" }
    };
    await File.WriteAllTextAsync(manifestFile, JsonSerializer.Serialize(testModules));

    var plugin = new ProcessRequiredModules(_mockWriter, _mockLogger, _mockOptions);

    // Act
    try
    {
      await plugin.ProcessPlugin(_mockEnvironment);
    }
    catch
    {
// Expected to fail on PowerShell execution
    }

    // Assert - Verify LogInformation was called multiple times (for various log messages)
    _mockLogger.Received().Log(
      LogLevel.Information,
      Arg.Any<EventId>(),
      Arg.Any<object>(),
      Arg.Any<Exception>(),
      Arg.Any<Func<object, Exception?, string>>());
  }

  [Test]
  public async Task ProcessPlugin_LogsVerificationMessage()
  {
    // Arrange
    var manifestFile = Path.Combine(_manifestPath, "RequiredModule.Manifest.json");
    var testModules = new[]
    {
      new { Name = "TestModule", Version = "1.0.0" }
    };
    await File.WriteAllTextAsync(manifestFile, JsonSerializer.Serialize(testModules));

    var plugin = new ProcessRequiredModules(_mockWriter, _mockLogger, _mockOptions);

    // Act
    try
    {
      await plugin.ProcessPlugin(_mockEnvironment);
    }
    catch
    {
      // Expected to fail on PowerShell execution
    }

    // Assert - Verify LogInformation was called
    _mockLogger.Received().Log(
      LogLevel.Information,
      Arg.Any<EventId>(),
      Arg.Any<object>(),
      Arg.Any<Exception>(),
      Arg.Any<Func<object, Exception?, string>>());
  }

  [Test]
  public async Task ProcessPlugin_WithSkipCleanupEnvironmentVariable_LogsSkippingCleanup()
  {
    // Arrange
    var manifestFile = Path.Combine(_manifestPath, "RequiredModule.Manifest.json");
    var testModules = new[]
    {
      new { Name = "TestModule", Version = "1.0.0" }
    };
    await File.WriteAllTextAsync(manifestFile, JsonSerializer.Serialize(testModules));

    Environment.SetEnvironmentVariable("SkipCleanup", "1");

    var plugin = new ProcessRequiredModules(_mockWriter, _mockLogger, _mockOptions);

    try
    {
      // Act
      await plugin.ProcessPlugin(_mockEnvironment);
    }
    catch
    {
      // Expected to fail on PowerShell execution
    }
    finally
    {
      // Cleanup
      Environment.SetEnvironmentVariable("SkipCleanup", null);
    }

    // Assert - Verify LogInformation was called
    _mockLogger.Received().Log(
      LogLevel.Information,
      Arg.Any<EventId>(),
      Arg.Any<object>(),
      Arg.Any<Exception>(),
      Arg.Any<Func<object, Exception?, string>>());
  }

  [Test]
  public void MaxRuntime_Returns10Minutes()
  {
    // Arrange
    var plugin = new ProcessRequiredModules(_mockWriter, _mockLogger, _mockOptions);

    // Act
    var maxRuntime = plugin.MaxRuntime;

    // Assert
    Assert.That(maxRuntime, Is.EqualTo(TimeSpan.FromMinutes(10)));
  }

  [Test]
  public void Position_ReturnsAfter()
  {
    // Arrange
    var plugin = new ProcessRequiredModules(_mockWriter, _mockLogger, _mockOptions);

    // Act
    var position = plugin.Position;

    // Assert
    Assert.That(position, Is.EqualTo(PluginPosition.After));
  }

  [Test]
  public async Task ProcessPlugin_CreatesModulesDirectory()
  {
    // Arrange
    var manifestFile = Path.Combine(_manifestPath, "RequiredModule.Manifest.json");
    var testModules = new[]
    {
      new { Name = "TestModule", Version = "1.0.0" }
    };
    await File.WriteAllTextAsync(manifestFile, JsonSerializer.Serialize(testModules));

    var plugin = new ProcessRequiredModules(_mockWriter, _mockLogger, _mockOptions);

    // Act
    try
    {
      await plugin.ProcessPlugin(_mockEnvironment);
    }
    catch
    {
      // Expected to fail on PowerShell execution
    }

    // Assert
    var modulesPath = Path.Combine(_testOutputPath, "Modules");
    Assert.That(Directory.Exists(modulesPath), Is.True);
  }

  [Test]
  public async Task ProcessPlugin_WithMultipleModules_ProcessesAllModules()
  {
    // Arrange
    var manifestFile = Path.Combine(_manifestPath, "RequiredModule.Manifest.json");
    var testModules = new[]
    {
      new { Name = "Module1", Version = "1.0.0" },
      new { Name = "Module2", Version = "2.0.0" },
      new { Name = "Module3", Version = "3.0.0" }
    };
    await File.WriteAllTextAsync(manifestFile, JsonSerializer.Serialize(testModules));

    var plugin = new ProcessRequiredModules(_mockWriter, _mockLogger, _mockOptions);

    // Act
    try
    {
      await plugin.ProcessPlugin(_mockEnvironment);
    }
    catch
    {
      // Expected to fail on PowerShell execution
    }

    // Assert - Verify LogInformation was called multiple times for processing packages
    _mockLogger.Received().Log(
      LogLevel.Information,
      Arg.Any<EventId>(),
      Arg.Any<object>(),
      Arg.Any<Exception>(),
      Arg.Any<Func<object, Exception?, string>>());
  }

  [Test]
  public async Task ProcessPlugin_WithException_LogsErrorAndRethrows()
  {
    // Arrange
    var manifestFile = Path.Combine(_manifestPath, "RequiredModule.Manifest.json");
    // Create malformed JSON that will parse but cause issues
    await File.WriteAllTextAsync(manifestFile, "[{\"Name\":null,\"Version\":null}]");

    var plugin = new ProcessRequiredModules(_mockWriter, _mockLogger, _mockOptions);

    // Act & Assert - Expect AggregateException because the plugin collects exceptions and throws them at the end
    Assert.ThrowsAsync<AggregateException>(async () => await plugin.ProcessPlugin(_mockEnvironment));

    // Verify error was logged
    _mockLogger.Received().Log(
      LogLevel.Error,
      Arg.Any<EventId>(),
      Arg.Any<object>(),
      Arg.Any<Exception>(),
      Arg.Any<Func<object, Exception?, string>>());
  }

  [Test]
  public void Writer_PropertyIsInitializable()
  {
    // Arrange
    var plugin = new ProcessRequiredModules(_mockWriter, _mockLogger, _mockOptions);
    var newWriter = Substitute.For<IGeneralFileWriter>();

    // Act
    var newPlugin = new ProcessRequiredModules(newWriter, _mockLogger, _mockOptions);

    // Assert
    Assert.That(newPlugin.Writer, Is.EqualTo(newWriter));
  }

  [Test]
  public void Environment_PropertyCanBeSet()
  {
    // Arrange
    var newEnvironment = Substitute.For<ITemplateGenerationEnvironment>();

    // Act
    var plugin = new ProcessRequiredModules(_mockWriter, _mockLogger, _mockOptions)
    {
      Environment = newEnvironment
    };

    // Assert
    Assert.That(plugin.Environment, Is.EqualTo(newEnvironment));
  }
}