using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using UTMO.Text.FileGenerator.Abstract;
using UTMO.Text.FileGenerator.Abstract.Contracts;

namespace UTMO.Text.FileGenerator.Provider.DSC.Plugins.ProcessRequiredModules;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global",
  Justification = "API Surface, must remain public for consumers")]
public class ProcessRequiredModules : IPipelinePlugin
{
  public ProcessRequiredModules(IGeneralFileWriter writer, ILogger<ProcessRequiredModules> logger,
    IGeneratorCliOptions options)
  {
    Writer = writer;
    Logger = logger;
    Options = options;
  }

  private ILogger<ProcessRequiredModules> Logger { get; }

  private IGeneratorCliOptions Options { get; }

  public TimeSpan MaxRuntime => TimeSpan.FromMinutes(10);

  public async Task ProcessPlugin(ITemplateGenerationEnvironment environment)
  {
    var manifestPath = Path.Join(Options.OutputPath, "Manifests", environment.EnvironmentName,
      "RequiredModule.Manifest.json");

    if (!File.Exists(manifestPath))
    {
      Logger.LogError("Required Module Manifest not found at {ManifestPath}", manifestPath);
      return;
    }

    Logger.LogInformation("Starting Process Required Modules Plugin");

    try
    {
      // Read and parse the manifest
      var manifestJson = await File.ReadAllTextAsync(manifestPath);
      var packages = JsonSerializer.Deserialize<List<ModulePackage>>(manifestJson);

      if (packages == null || packages.Count == 0)
      {
        Logger.LogWarning("No modules found in manifest");
        return;
      }

      // Setup PowerShell module path
      var userModulesPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile),
        "Documents", "WindowsPowerShell", "Modules");

      if (!Directory.Exists(userModulesPath)) Directory.CreateDirectory(userModulesPath);

      var currentPSModulePath = System.Environment.GetEnvironmentVariable("PSModulePath") ?? "";
      if (!currentPSModulePath.Contains(userModulesPath))
      {
        System.Environment.SetEnvironmentVariable("PSModulePath", $"{userModulesPath};{currentPSModulePath}");
        Logger.LogInformation("Updated PSModulePath to include user modules directory: {UserModulesPath}",
          userModulesPath);
      }

      Logger.LogInformation("PSModulePath: {PSModulePath}", System.Environment.GetEnvironmentVariable("PSModulePath"));
      Logger.LogInformation("Working Directory: {WorkingDirectory}", Directory.GetCurrentDirectory());

      // Verify required modules are available
      Logger.LogInformation("Verifying required modules are available...");
      await VerifyModulesAsync(packages);

      // Create temp folder for non-archive mode
      string? tempFolder = null;
      var noArchive = false; // Can be made configurable if needed

      if (!noArchive)
      {
        var tmpId = Random.Shared.Next();
        tempFolder = Path.Combine(Path.GetTempPath(), tmpId.ToString());
        Directory.CreateDirectory(tempFolder);
        Logger.LogDebug("Created temporary folder: {TempFolder}", tempFolder);
      }

      var outputPath = Path.Combine(Options.OutputPath, "Modules");
      if (!Directory.Exists(outputPath)) Directory.CreateDirectory(outputPath);

      var loopExceptions = new List<Exception>();

      try
      {
        // Process each package
        foreach (var package in packages)
          try
          {
            Logger.LogInformation("Processing Package: {PackageName}", package.Name);

            var workingPath = noArchive ? outputPath : tempFolder!;

            // Save module using PowerShell
            await SaveModuleAsync(package.Name, package.Version, workingPath);

            // Remove .git directories
            await RemoveGitDirectoriesAsync(workingPath);

            if (!noArchive)
            {
              // Create zip archive
              var packagePath = Path.Combine(tempFolder!, package.Name, package.Version);

              if (Directory.Exists(packagePath))
              {
                var fileName = package.UseAlternateFormat && !string.IsNullOrEmpty(package.AlternateVersion)
                  ? $"{package.Name}_{package.AlternateVersion}.zip"
                  : $"{package.Name}_{package.Version}.zip";

                var zipPath = Path.Combine(outputPath, fileName);

                // Delete existing zip if present
                if (File.Exists(zipPath)) File.Delete(zipPath);

                ZipFile.CreateFromDirectory(packagePath, zipPath, CompressionLevel.Optimal, false);
                Logger.LogInformation("Created archive: {FileName}", fileName);
              }
              else
              {
                Logger.LogWarning("Package path not found: {PackagePath}", packagePath);
              }
            }
          }
          catch (Exception ex)
          {
            loopExceptions.Add(ex);
            Logger.LogWarning(ex, "Unable to process module {ModuleName} due to exception: {Message}",
              package.Name, ex.Message);
          }
      }
      finally
      {
        // Cleanup temp folder
        var skipCleanup = System.Environment.GetEnvironmentVariable("SkipCleanup") == "1";

        if (!skipCleanup && !noArchive && tempFolder != null && Directory.Exists(tempFolder))
        {
          Logger.LogInformation("Cleaning Up");
          try
          {
            Directory.Delete(tempFolder, true);
          }
          catch (Exception ex)
          {
            Logger.LogWarning(ex, "Failed to cleanup temporary folder: {TempFolder}", tempFolder);
          }
        }
        else
        {
          Logger.LogInformation("Skipping Cleanup");
        }
      }

      if (loopExceptions.Count > 0)
      {
        var aggregateException = new AggregateException("Failed to process modules", loopExceptions);
        Logger.LogError(aggregateException, "Failed to process one or more modules");
        throw aggregateException;
      }

      Logger.LogInformation("Finished Process Required Modules Plugin");
    }
    catch (Exception ex)
    {
      Logger.LogError(ex, "Error processing required modules: {Message}", ex.Message);
      throw;
    }
  }

  public IGeneralFileWriter Writer { get; init; }

  public ITemplateGenerationEnvironment Environment { get; init; } = null!;

  public PluginPosition Position => PluginPosition.After;

  private async Task VerifyModulesAsync(List<ModulePackage> packages)
  {
    foreach (var package in packages)
      try
      {
        var result = await ExecutePowerShellAsync(ps =>
        {
          ps.AddCommand("Get-InstalledModule")
            .AddParameter("Name", package.Name)
            .AddParameter("RequiredVersion", package.Version)
            .AddParameter("ErrorAction", "SilentlyContinue");
        });

        if (result.Success && result.Output.Count > 0)
        {
          Logger.LogInformation("✓ Module {ModuleName} v{ModuleVersion} is available",
            package.Name, package.Version);
        }
        else
        {
          // Get all available versions
          var versionsResult = await ExecutePowerShellAsync(ps =>
          {
            ps.AddCommand("Get-InstalledModule")
              .AddParameter("Name", package.Name)
              .AddParameter("AllVersions", true)
              .AddParameter("ErrorAction", "SilentlyContinue");
          });

          var versions = string.Join(", ", versionsResult.Output
            .Select(o => o?.Properties["Version"]?.Value?.ToString() ?? "")
            .Where(v => !string.IsNullOrEmpty(v)));

          Logger.LogWarning(
            "Module {ModuleName} v{ModuleVersion} is not installed. This may cause Save-Module to fail. Available versions: {Versions}",
            package.Name, package.Version, string.IsNullOrEmpty(versions) ? "None" : versions);
        }
      }
      catch (Exception ex)
      {
        Logger.LogWarning(ex, "Failed to verify module {ModuleName}: {Message}", package.Name, ex.Message);
      }
  }

  private async Task SaveModuleAsync(string moduleName, string moduleVersion, string outputPath)
  {
    var result = await ExecutePowerShellAsync(ps =>
    {
      ps.AddCommand("Save-Module")
        .AddParameter("Name", moduleName)
        .AddParameter("RequiredVersion", moduleVersion)
        .AddParameter("Repository", "DSCResources")
        .AddParameter("Path", outputPath)
        .AddParameter("Force", true);
    });

    if (!result.Success)
    {
      var errorMessage = string.Join("; ", result.Errors.Select(e => e.ToString()));
      throw new InvalidOperationException($"Failed to save module {moduleName} v{moduleVersion}: {errorMessage}");
    }

    Logger.LogDebug("Successfully saved module {ModuleName} v{ModuleVersion} to {OutputPath}",
      moduleName, moduleVersion, outputPath);
  }

  private async Task RemoveGitDirectoriesAsync(string path)
  {
    try
    {
      var gitDirs = Directory.GetDirectories(path, ".git", SearchOption.AllDirectories);

      foreach (var gitDir in gitDirs)
        try
        {
          Directory.Delete(gitDir, true);
          Logger.LogDebug("Removed .git directory: {GitDir}", gitDir);
        }
        catch (Exception ex)
        {
          Logger.LogWarning(ex, "Failed to remove .git directory: {GitDir}", gitDir);
        }
    }
    catch (Exception ex)
    {
      Logger.LogWarning(ex, "Error searching for .git directories in {Path}", path);
    }
  }

  private async Task<PowerShellResult> ExecutePowerShellAsync(Action<PowerShell> configureCommand)
  {
    using var runspace = RunspaceFactory.CreateRunspace();
    runspace.Open();

    // Configure the runspace environment
    var userModulePath = Path.Combine(
      System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments),
      "WindowsPowerShell", "Modules");

    var currentPSModulePath = System.Environment.GetEnvironmentVariable("PSModulePath") ?? "";
    if (!currentPSModulePath.Contains(userModulePath))
      runspace.SessionStateProxy.SetVariable("env:PSModulePath", $"{userModulePath};{currentPSModulePath}");

    // Set other environment variables
    runspace.SessionStateProxy.SetVariable("env:USERPROFILE",
      System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile));
    runspace.SessionStateProxy.SetVariable("env:HOMEDRIVE",
      System.Environment.GetEnvironmentVariable("HOMEDRIVE") ?? "C:");
    runspace.SessionStateProxy.SetVariable("env:HOMEPATH",
      System.Environment.GetEnvironmentVariable("HOMEPATH") ?? "\\");
    runspace.SessionStateProxy.SetVariable("env:APPDATA",
      System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData));
    runspace.SessionStateProxy.SetVariable("env:LOCALAPPDATA",
      System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData));

    using var ps = PowerShell.Create();
    ps.Runspace = runspace;

    // Configure error action preference
    ps.AddCommand("Set-Variable")
      .AddParameter("Name", "ErrorActionPreference")
      .AddParameter("Value", "Stop");
    ps.Invoke();
    ps.Commands.Clear();

    // Configure the actual command
    configureCommand(ps);

    var output = new Collection<PSObject>();
    var errors = new Collection<ErrorRecord>();

    using var cancellationTokenSource = new CancellationTokenSource(MaxRuntime);
    var invocationTask = Task.Run(() =>
    {
      try
      {
        ps.Streams.Error.DataAdded += (sender, args) =>
        {
          var errorRecord = ps.Streams.Error[args.Index];
          errors.Add(errorRecord);
          Logger.LogWarning("PowerShell Error: {ErrorMessage}", errorRecord.ToString());
        };

        ps.Streams.Warning.DataAdded += (sender, args) =>
        {
          var warning = ps.Streams.Warning[args.Index];
          Logger.LogDebug("PowerShell Warning: {WarningMessage}", warning.Message);
        };

        ps.Streams.Verbose.DataAdded += (sender, args) =>
        {
          var verbose = ps.Streams.Verbose[args.Index];
          Logger.LogTrace("PowerShell Verbose: {VerboseMessage}", verbose.Message);
        };

        output = ps.Invoke();
      }
      catch (Exception ex)
      {
        Logger.LogError(ex, "PowerShell execution failed");
        throw;
      }
    }, cancellationTokenSource.Token);

    try
    {
      await invocationTask;
    }
    catch (OperationCanceledException)
    {
      ps.Stop();
      throw new TimeoutException($"PowerShell execution timed out after {MaxRuntime}");
    }

    var hasErrors = errors.Count > 0 || ps.HadErrors;

    if (hasErrors)
      foreach (var error in errors)
        Logger.LogWarning("PowerShell Error Detail: {ErrorDetail}", error.ToString());

    return new PowerShellResult
    {
      Success = !hasErrors,
      Output = output,
      Errors = errors
    };
  }

  private class ModulePackage
  {
    public string Name { get; } = null!;
    public string Version { get; } = null!;
    public bool UseAlternateFormat { get; set; }
    public string? AlternateVersion { get; set; }
    public bool IsPrivate { get; set; }
    public bool AllowClobber { get; set; }
  }

  private class PowerShellResult
  {
    public bool Success { get; init; }
    public Collection<PSObject> Output { get; init; } = [];
    public Collection<ErrorRecord> Errors { get; init; } = [];
  }
}