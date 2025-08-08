namespace UTMO.Text.FileGenerator.Provider.DSC.Plugins.RestoreRequiredModules;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Text.FileGenerator.Abstract.Contracts;
using Utils;
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
        
        string? stdErr = null;

        try
        {
            var processInfo = new ProcessStartInfo
                              {
                                  FileName = "PowerShell.exe",
                                  Arguments = $"-ExecutionPolicy Bypass -NoProfile -File {scriptPath} -moduleManifestPath \"{manifestPath}\"",
                                  RedirectStandardOutput = true,
                                  RedirectStandardError = true,
                                  UseShellExecute = false,
                                  CreateNoWindow = true,
                              };

            string? stdOut;
        
            this.Logger.LogDebug("Starting PowerShell process");
            
            using (var process = Process.Start(processInfo))
            {
                
                if (process == null)
                {
                    this.Logger.Fatal(LogMessages.RestoreRequiredModulesFailed, true, 25, "Process could not be started.");
                    return;
                }
                
                stdOut = await process.StandardOutput.ReadToEndAsync()!;
                stdErr = await process.StandardError.ReadToEndAsync()!;
                await process?.WaitForExitAsync(new CancellationTokenSource(this.MaxRuntime).Token)!;
            }
        
            this.Logger.LogTrace(LogMessages.RestoreModulesStdOut, stdOut ?? "None");
        
            if (!string.IsNullOrWhiteSpace(stdErr))
            {
                this.Logger.Fatal(LogMessages.RestoreRequiredModulesFailed, true, 25, stdErr);
            }
            else
            {
                this.Logger.LogInformation(LogMessages.RestoredRequiredModulesSucceeded);
            }
        }
        catch (Exception)
        {
            this.Logger.Fatal(LogMessages.RestoreRequiredModulesFailed, true, 25, stdErr ?? "None");
        }
    }

    public IGeneralFileWriter Writer { get; init; }

    public ITemplateGenerationEnvironment Environment { get; init; } = null!;

    public PluginPosition Position => PluginPosition.Before;
}