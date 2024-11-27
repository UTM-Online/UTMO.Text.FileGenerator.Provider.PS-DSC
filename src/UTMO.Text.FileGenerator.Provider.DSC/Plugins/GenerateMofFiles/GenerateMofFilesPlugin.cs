namespace UTMO.Text.FileGenerator.Provider.DSC.Plugins.GenerateMofFiles;

using System.Diagnostics;
using System.Management.Automation;
using UTMO.Common.Guards;
using UTMO.Text.FileGenerator.Abstract;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Constants;
using UTMO.Text.FileGenerator.Provider.DSC.LoggingMessages;

public class GenerateMofFilesPlugin : IRenderingPipelinePlugin
{
    internal GenerateMofFilesPlugin(IGeneralFileWriter writer, string outputPath, bool enhancedLogging, TimeSpan? overrideMaxRuntime = null)
    {
        this.Writer = writer;
        this.OutputPath = outputPath;
        this.EnhancedLogging = enhancedLogging;
        this.MaxRuntime = overrideMaxRuntime ?? TimeSpan.FromSeconds(45);
        this.Logger = PluginManager.Instance.Resolve<IGeneratorLogger>();
    }

    public void HandleTemplate(ITemplateModel model)
    {
        Guard.StringNotNull(nameof(model.ResourceTypeName), model.ResourceTypeName);

        if (model.ResourceTypeName != DscResourceTypeNames.DscConfiguration && model.ResourceTypeName != DscResourceTypeNames.DscLcmConfiguration)
        {
            this.Logger.Warning(LogMessages.SkippingNonDscResource, model.ResourceName);
            return;
        }

        this.Logger.Information(LogMessages.StartingMofFileGeneration, model.ResourceName);

        string scriptConfig;

        try
        {
            scriptConfig = model.ProduceOutputPath(this.OutputPath);
        }
        catch (Exception)
        {
            this.Logger.Error(LogMessages.ErrorGeneratingOutputPath, model.ResourceName);
            throw;
        }

        Guard.StringNotNull(nameof(scriptConfig), scriptConfig);
        
        this.Logger.Verbose(LogMessages.ScriptConfigPath, scriptConfig);

        var fileName = model.ResourceName;
        var fileType = model.ResourceTypeName == DscResourceTypeNames.DscConfiguration ? "Configurations" : "Computers";

        string mofOutputFile;

        try
        {
            mofOutputFile = Path.Combine(this.OutputPath, $@"MOF\{fileType}");
        }
        catch (Exception)
        {
            this.Logger.Error(LogMessages.ErrorGeneratingMofOutputPath, model.ResourceName);
            throw;
        }

        Guard.StringNotNull(nameof(mofOutputFile), mofOutputFile);
        
        this.Logger.Verbose(LogMessages.MofOutputPath, mofOutputFile);

        string? stdErr = null;
        try
        {
            var processInfo = new ProcessStartInfo
                              {
                                  FileName = "PowerShell.exe",
                                  Arguments = $"-ExecutionPolicy Bypass -NoProfile -File {scriptConfig} -OutputPath {mofOutputFile}",
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
            
            this.Logger.Verbose(LogMessages.MofGenerationStdOut, stdOut ?? "None");
            
            if (!string.IsNullOrWhiteSpace(stdErr))
            {
                this.Logger.Error(LogMessages.MofGenerationFailed, model.ResourceName, stdErr);
                stdErr = null;
            }
            else
            {
                this.Logger.Information(LogMessages.MofGenerationSucceeded, model.ResourceName);
            }
        }
        catch (Exception)
        {
            this.Logger.Error(LogMessages.MofGenerationFailed, model.ResourceName, stdErr ?? "None");
            throw;
        }
    }

    public IGeneralFileWriter Writer { get; init; }

    public ITemplateGenerationEnvironment Environment { get; init; } = null!;

    private string OutputPath { get; init; }
    
    private bool EnhancedLogging { get; init; }

    public TimeSpan MaxRuntime { get; }
    
    private IGeneratorLogger Logger { get; init; }
}