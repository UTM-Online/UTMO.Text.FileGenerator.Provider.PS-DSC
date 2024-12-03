namespace UTMO.Text.FileGenerator.Provider.DSC.Plugins.RestoreRequiredModules;

using System.Diagnostics;
using UTMO.Text.FileGenerator.Abstract;
using UTMO.Text.FileGenerator.Provider.DSC.Constants;
using UTMO.Text.FileGenerator.Provider.DSC.LoggingMessages;

public class RestoreRequiredModulesPlugin : IPipelinePlugin
{
    public RestoreRequiredModulesPlugin(IGeneralFileWriter writer, TimeSpan? maxRuntime = null)
    {
        this.Writer = writer;
        this.MaxRuntime = maxRuntime ?? TimeSpan.FromMinutes(10);
        this.Logger = PluginManager.Instance.Resolve<IGeneratorLogger>();
    }

    private IGeneratorLogger Logger { get; }

    public TimeSpan MaxRuntime { get; }

    public void ProcessPlugin(ITemplateGenerationEnvironment environment)
    {
        var manifestPath = Path.Join(environment.OutputPath, "Manifests", "RequiredModule.Manifest.json");
        // Write the embedded RestoreRequiredModules.ps1 to the output path
        this.Logger.Information(LogMessages.StartingRestoreRequiredModules);
        var scriptName = ScriptConstants.RestoreRequiredModules;
        var outputPath = Path.Join(environment.OutputPath, "Scripts");
        var scriptPath = Path.Join(outputPath, scriptName);
        this.Logger.Information(LogMessages.WritingRestoreRequiredModules);
        this.Writer.WriteEmbeddedResource(scriptName, outputPath, EmbeddedResourceType.Script);
        
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
        
            using (var process = Process.Start(processInfo))
            {
                stdOut = process?.StandardOutput.ReadToEnd();
                stdErr = process?.StandardError.ReadToEnd();
                process?.WaitForExit();
            }
        
            this.Logger.Verbose(LogMessages.RestoreModulesStdOut, stdOut ?? "None");
        
            if (!string.IsNullOrWhiteSpace(stdErr))
            {
                this.Logger.Fatal(LogMessages.RestoreRequiredModulesFailed, true, 25, stdErr);
            }
            else
            {
                this.Logger.Information(LogMessages.RestoredRequiredModulesSucceeded);
            }
        }
        catch (Exception)
        {
            this.Logger.Fatal(LogMessages.RestoreRequiredModulesFailed, true, 25, stdErr ?? "None");
        }
    }

    public IGeneralFileWriter Writer { get; init; }

    public ITemplateGenerationEnvironment Environment { get; init; }
}