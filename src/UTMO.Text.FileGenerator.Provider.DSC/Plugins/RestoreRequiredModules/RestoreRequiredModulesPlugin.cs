namespace UTMO.Text.FileGenerator.Provider.DSC.Plugins.RestoreRequiredModules;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
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
            // Use Windows PowerShell (powershell.exe) instead of PowerShell Core for compatibility
            var processInfo = new ProcessStartInfo
            {
                FileName = "powershell.exe", // Explicitly use Windows PowerShell
                Arguments = $"-ExecutionPolicy Bypass -NoProfile -File \"{scriptPath}\" -moduleManifestPath \"{manifestPath}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            this.Logger.LogDebug("Starting Windows PowerShell process for module restoration");
            
            using var process = new Process();
            process.StartInfo = processInfo;
            process.Start();
            
            var stdOutTask = process.StandardOutput.ReadToEndAsync();
            var stdErrTask = process.StandardError.ReadToEndAsync();
            
            using var cancellationTokenSource = new CancellationTokenSource(this.MaxRuntime);
            var processTask = process.WaitForExitAsync(cancellationTokenSource.Token);
            
            await Task.WhenAll(stdOutTask, stdErrTask, processTask);
            
            var stdOut = await stdOutTask;
            var stdErr = await stdErrTask;
            
            this.Logger.LogTrace(LogMessages.RestoreModulesStdOut, stdOut ?? "None");
            
            if (!string.IsNullOrWhiteSpace(stdErr) || process.ExitCode != 0)
            {
                var errorMessage = !string.IsNullOrWhiteSpace(stdErr) ? stdErr : $"Process exited with code {process.ExitCode}";
                this.Logger.LogError(LogMessages.RestoreRequiredModulesFailed, errorMessage);
                throw new InvalidOperationException($"Windows PowerShell script execution failed: {errorMessage}");
            }
            else
            {
                this.Logger.LogInformation(LogMessages.RestoredRequiredModulesSucceeded);
            }
        }
        catch (OperationCanceledException)
        {
            var timeoutMessage = $"Windows PowerShell script execution timed out after {this.MaxRuntime}";
            this.Logger.LogError(LogMessages.RestoreRequiredModulesFailed, timeoutMessage);
            throw new TimeoutException(timeoutMessage);
        }
        catch (Exception ex)
        {
            this.Logger.LogError(ex, LogMessages.RestoreRequiredModulesFailed, ex.Message);
            throw;
        }
    }



    public IGeneralFileWriter Writer { get; init; }

    public ITemplateGenerationEnvironment Environment { get; init; } = null!;

    public PluginPosition Position => PluginPosition.Before;
}