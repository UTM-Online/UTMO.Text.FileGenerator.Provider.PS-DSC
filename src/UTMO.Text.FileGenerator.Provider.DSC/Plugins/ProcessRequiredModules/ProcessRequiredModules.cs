namespace UTMO.Text.FileGenerator.Provider.DSC.Plugins.ProcessRequiredModules;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.Extensions.Logging;
using UTMO.Text.FileGenerator.Abstract;
using UTMO.Text.FileGenerator.Abstract.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Constants;
using UTMO.Text.FileGenerator.Provider.DSC.Models;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global", Justification = "API Surface, must remain public for consumers")]
public class ProcessRequiredModules : IPipelinePlugin
{
    public ProcessRequiredModules(IGeneralFileWriter writer, ILogger<ProcessRequiredModules> logger, IGeneratorCliOptions options)
    {
        this.Writer = writer;
        this.Logger = logger;
        this.Options = options;
    }

    public TimeSpan MaxRuntime => TimeSpan.FromMinutes(10);

    public async Task<bool> ProcessPlugin(ITemplateGenerationEnvironment environment)
    {
        var manifestPath = Path.Join(this.Options.OutputPath, "Manifests", environment.EnvironmentName, "RequiredModule.Manifest.json");

        if (!File.Exists(manifestPath))
        {
            this.Logger.LogError("Required Module Manifest not found at {ManifestPath}", manifestPath);
            return false;
        }
        
        this.Logger.LogInformation("Starting Process Required Modules Plugin");
        var rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var scriptPath = Path.Join(rootPath, "Scripts", ScriptConstants.ProcessRequiredModules);
        
        string? stdErr = null;
        
        var scriptArgs = $"-ExecutionPolicy Bypass -NoProfile -File {scriptPath} -ManifestPath \"{manifestPath}\" -OutputPath \"{this.Options.OutputPath}\"";

        if (this.Options is DscCliOptions {IsCiCd: false})
        {
            scriptArgs += " -NoArchive";
        }
        
        try
        {
            var processInfo = new ProcessStartInfo
                              {
                                  FileName = "powershell.exe", // Use Windows PowerShell explicitly
                                  Arguments = scriptArgs,
                                  RedirectStandardOutput = true,
                                  RedirectStandardError = true,
                                  UseShellExecute = false,
                                  CreateNoWindow = true,
                                  WorkingDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile), // Set working directory to user profile
                              };

            // Ensure PowerShell module path includes the current user's modules directory
            var userModulePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "WindowsPowerShell", "Modules");
            var currentPSModulePath = System.Environment.GetEnvironmentVariable("PSModulePath") ?? "";
            
            if (!currentPSModulePath.Contains(userModulePath))
            {
                processInfo.EnvironmentVariables["PSModulePath"] = $"{userModulePath};{currentPSModulePath}";
            }
            
            // Ensure the process runs with the current user's environment
            processInfo.EnvironmentVariables["USERPROFILE"] = System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile);
            processInfo.EnvironmentVariables["HOMEDRIVE"] = System.Environment.GetEnvironmentVariable("HOMEDRIVE") ?? "C:";
            processInfo.EnvironmentVariables["HOMEPATH"] = System.Environment.GetEnvironmentVariable("HOMEPATH") ?? "\\";
            processInfo.EnvironmentVariables["APPDATA"] = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            processInfo.EnvironmentVariables["LOCALAPPDATA"] = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);

            string? stdOut;
        
            using (var process = Process.Start(processInfo))
            {
                stdOut = await process?.StandardOutput.ReadToEndAsync()!;
                stdErr = await process?.StandardError.ReadToEndAsync()!;
                await process?.WaitForExitAsync()!;
            }
        
            if (!string.IsNullOrWhiteSpace(stdErr))
            {
                this.Logger.LogError("Error occurred while processing required modules:\r\n{Error}", stdErr);
                System.Environment.Exit(-260);
            }
            else
            {
                this.Logger.LogInformation("Required modules processed successfully:\r\n{Output}", stdOut);
            }
        }
        catch (Exception ex)
        {
            this.Logger.LogError(ex, "Error occurred while processing required modules");
            System.Environment.Exit(-261);
        }

        return true;
    }

    private ILogger<ProcessRequiredModules> Logger { get; }
    
    private IGeneratorCliOptions Options { get; }
    
    public IGeneralFileWriter Writer { get; init; }

    public ITemplateGenerationEnvironment Environment { get; init; } = null!;

    public PluginPosition Position => PluginPosition.After;
}