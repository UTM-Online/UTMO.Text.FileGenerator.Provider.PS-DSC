namespace UTMO.Text.FileGenerator.Provider.DSC.Plugins.RestoreRequiredModules;

using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Text.FileGenerator.Abstract.Contracts;
using UTMO.Text.FileGenerator.Abstract;
using UTMO.Text.FileGenerator.Provider.DSC.Constants;
using UTMO.Text.FileGenerator.Provider.DSC.LoggingMessages;

[SuppressMessage("ReSharper", "TemplateIsNotCompileTimeConstantProblem")]
[SuppressMessage("Usage", "CA2254:Template should be a static expression")]
public class RestoreRequiredModulesPlugin : IPipelinePlugin
{
    public RestoreRequiredModulesPlugin(IGeneralFileWriter writer, ILogger<RestoreRequiredModulesPlugin> logger, IGeneratorCliOptions options)
    {
        this.Writer = writer;
        this.MaxRuntime = TimeSpan.FromMinutes(10);
        this.Logger = logger;
        this.Options = options;
    }

    private ILogger<RestoreRequiredModulesPlugin> Logger { get; }
    
    private IGeneratorCliOptions Options { get; }

    public TimeSpan MaxRuntime { get; }

    public async Task ProcessPlugin(ITemplateGenerationEnvironment environment)
    {
        var manifestPath = Path.Join(this.Options.OutputPath, "Manifests", environment.EnvironmentName, "RequiredModule.Manifest.json");

        if (!File.Exists(manifestPath))
        {
            this.Logger.LogError("Required Module Manifest not found at {ManifestPath}", manifestPath);
            return;
        }
        
        this.Logger.LogInformation(LogMessages.StartingRestoreRequiredModules);
        var rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var scriptPath = Path.Join(rootPath, "Scripts", ScriptConstants.RestoreRequiredModules);

        if (!File.Exists(scriptPath))
        {
            this.Logger.LogError("PowerShell script not found at {ScriptPath}", scriptPath);
            return;
        }

        try
        {
            // Create a minimal initial session state to avoid snap-in loading issues
            var initialSessionState = InitialSessionState.CreateDefault2();
            
            using var runspace = RunspaceFactory.CreateRunspace(initialSessionState);
            runspace.Open();
            
            using var powerShell = PowerShell.Create();
            powerShell.Runspace = runspace;
            
            // Configure PowerShell streams for better monitoring
            this.ConfigurePowerShellStreams(powerShell);
            
            // Set execution policy for this session
            powerShell.AddCommand("Set-ExecutionPolicy")
                     .AddParameter("ExecutionPolicy", "Bypass")
                     .AddParameter("Scope", "Process")
                     .AddParameter("Force", true);
            
            await this.InvokePowerShellAsync(powerShell);
            powerShell.Commands.Clear();

            // Load and execute the script with parameters
            var scriptContent = await File.ReadAllTextAsync(scriptPath);
            powerShell.AddScript(scriptContent)
                     .AddParameter("moduleManifestPath", manifestPath);

            this.Logger.LogDebug("Starting PowerShell script execution for module restoration");
            
            using var cancellationTokenSource = new CancellationTokenSource(this.MaxRuntime);
            var results = await this.InvokePowerShellAsync(powerShell, cancellationTokenSource.Token);

            // Process all output streams
            this.ProcessPowerShellOutput(powerShell, results);

            if (powerShell.HadErrors)
            {
                var errors = powerShell.Streams.Error.Select(e => $"{e.CategoryInfo.Category}: {e.Exception?.Message ?? e.ToString()}");
                var errorMessage = string.Join(System.Environment.NewLine, errors);
                this.Logger.LogError(LogMessages.RestoreRequiredModulesFailed, errorMessage);
                throw new InvalidOperationException($"PowerShell script execution failed: {errorMessage}");
            }
            else
            {
                this.Logger.LogInformation(LogMessages.RestoredRequiredModulesSucceeded);
            }
        }
        catch (OperationCanceledException)
        {
            var timeoutMessage = $"PowerShell script execution timed out after {this.MaxRuntime}";
            this.Logger.LogError(LogMessages.RestoreRequiredModulesFailed, timeoutMessage);
            throw new TimeoutException(timeoutMessage);
        }
        catch (Exception ex)
        {
            this.Logger.LogError(ex, LogMessages.RestoreRequiredModulesFailed, ex.Message);
            throw;
        }
    }

    private void ConfigurePowerShellStreams(PowerShell powerShell)
    {
        // Configure event handlers for real-time output processing
        powerShell.Streams.Progress.DataAdded += (_, args) =>
        {
            var progressRecord = powerShell.Streams.Progress[args.Index];
            this.Logger.LogDebug("PowerShell Progress: {Activity} - {Status} ({PercentComplete}%)", 
                progressRecord.Activity, 
                progressRecord.StatusDescription, 
                progressRecord.PercentComplete);
        };

        powerShell.Streams.Information.DataAdded += (_, args) =>
        {
            var infoRecord = powerShell.Streams.Information[args.Index];
            this.Logger.LogInformation("PowerShell Info: {Message}", infoRecord.MessageData);
        };

        powerShell.Streams.Warning.DataAdded += (_, args) =>
        {
            var warningRecord = powerShell.Streams.Warning[args.Index];
            this.Logger.LogWarning("PowerShell Warning: {Message}", warningRecord.Message);
        };

        powerShell.Streams.Verbose.DataAdded += (_, args) =>
        {
            var verboseRecord = powerShell.Streams.Verbose[args.Index];
            this.Logger.LogDebug("PowerShell Verbose: {Message}", verboseRecord.Message);
        };

        powerShell.Streams.Debug.DataAdded += (_, args) =>
        {
            var debugRecord = powerShell.Streams.Debug[args.Index];
            this.Logger.LogTrace("PowerShell Debug: {Message}", debugRecord.Message);
        };
    }

    private void ProcessPowerShellOutput(PowerShell powerShell, Collection<PSObject> results)
    {
        // Log the main output
        if (results.Count != 0)
        {
            var output = string.Join(System.Environment.NewLine, results.Select(r => r.ToString()).Where(s => !string.IsNullOrEmpty(s)));
            if (!string.IsNullOrWhiteSpace(output))
            {
                this.Logger.LogTrace(LogMessages.RestoreModulesStdOut, output);
            }
        }

        // Process any remaining items in streams that weren't caught by event handlers
        foreach (var warning in powerShell.Streams.Warning)
        {
            this.Logger.LogWarning("PowerShell Warning: {Message}", warning.Message);
        }

        foreach (var info in powerShell.Streams.Information)
        {
            this.Logger.LogInformation("PowerShell Info: {Message}", info.MessageData);
        }
    }

    private async Task<Collection<PSObject>> InvokePowerShellAsync(PowerShell powerShell, CancellationToken cancellationToken = default)
    {
        var task = Task.Run(() => powerShell.Invoke(), cancellationToken);
        return await task;
    }

    public IGeneralFileWriter Writer { get; init; }

    public ITemplateGenerationEnvironment Environment { get; init; } = null!;

    public PluginPosition Position => PluginPosition.Before;
}