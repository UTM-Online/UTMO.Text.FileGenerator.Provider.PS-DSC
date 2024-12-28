namespace UTMO.Text.FileGenerator.Provider.DSC.Plugins.GenerateMofFiles;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Management.Automation;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Text.FileGenerator.Abstract.Contracts;
using UTMO.Common.Guards;
using UTMO.Text.FileGenerator.Abstract;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Constants;
using UTMO.Text.FileGenerator.Provider.DSC.LoggingMessages;

[SuppressMessage("ReSharper", "TemplateIsNotCompileTimeConstantProblem")]
[SuppressMessage("Usage", "CA2254:Template should be a static expression")]
[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class GenerateMofFilesPlugin : IRenderingPipelinePlugin
{
    public GenerateMofFilesPlugin(IGeneralFileWriter writer, IGeneratorCliOptions options, ILogger<GenerateMofFilesPlugin> logger)
    {
        this.Writer = writer;
        this.OutputPath = options.OutputPath;
        this.Logger = logger;
        this.MaxRuntime = TimeSpan.FromSeconds(45);
    }

    public async Task HandleTemplate(ITemplateModel model)
    {
        Guard.StringNotNull(nameof(model.ResourceTypeName), model.ResourceTypeName);

        if (model.ResourceTypeName != DscResourceTypeNames.DscConfiguration && model.ResourceTypeName != DscResourceTypeNames.DscLcmConfiguration)
        {
            this.Logger.LogWarning(LogMessages.SkippingNonDscResource, model.ResourceName);
            return;
        }

        this.Logger.LogDebug(LogMessages.StartingMofFileGeneration, model.ResourceName);

        string scriptConfig;

        try
        {
            scriptConfig = model.ProduceOutputPath(this.OutputPath);
        }
        catch (Exception)
        {
            this.Logger.LogError(LogMessages.ErrorGeneratingOutputPath, model.ResourceName);
            throw;
        }

        Guard.StringNotNull(nameof(scriptConfig), scriptConfig);
        
        this.Logger.LogTrace(LogMessages.ScriptConfigPath, scriptConfig);

        var fileType = model.ResourceTypeName == DscResourceTypeNames.DscConfiguration ? "Configurations" : "Computers";

        string mofOutputFile;

        try
        {
            mofOutputFile = Path.Combine(this.OutputPath, $@"MOF\{fileType}");
        }
        catch (Exception)
        {
            this.Logger.LogError(LogMessages.ErrorGeneratingMofOutputPath, model.ResourceName);
            throw;
        }

        Guard.StringNotNull(nameof(mofOutputFile), mofOutputFile);
        
        this.Logger.LogTrace(LogMessages.MofOutputPath, mofOutputFile);

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
                stdOut = await process?.StandardOutput.ReadToEndAsync()!;
                stdErr = await process?.StandardError.ReadToEndAsync()!;
                await process?.WaitForExitAsync()!;
            }
            
            this.Logger.LogTrace(LogMessages.MofGenerationStdOut, stdOut ?? "None");
            
            if (!string.IsNullOrWhiteSpace(stdErr) && this.ErrorParser.IsMatch(stdErr))
            {
                stdErr = this.ErrorParser.Match(stdErr).Groups["ErrorText"].Value;
                
                this.Logger.LogError(LogMessages.MofGenerationFailed, model.ResourceName, stdErr);
                stdErr = null;
            }
            else if (!string.IsNullOrWhiteSpace(stdErr))
            {
                this.Logger.LogError(LogMessages.MofGenerationFailed, model.ResourceName, stdErr);
            }
            else
            {
                this.Logger.LogTrace(LogMessages.MofGenerationSucceeded, model.ResourceName);
            }
        }
        catch (Exception ex)
        {
            string parsedError;
            
            if (!string.IsNullOrWhiteSpace(stdErr) && this.ErrorParser.IsMatch(stdErr))
            {
                parsedError = this.ErrorParser.Match(stdErr).Groups["ErrorText"].Value;
            }
            else
            {
                parsedError = stdErr ?? ex.Message;
            }
            
            this.Logger.LogError(LogMessages.MofGenerationException, ex.GetType().Name, model.ResourceName, parsedError);
            throw;
        }
    }

    public IGeneralFileWriter Writer { get; init; }

    public ITemplateGenerationEnvironment Environment { get; init; } = null!;

    public PluginPosition Position => PluginPosition.After;

    private string OutputPath { get; init; }

    public TimeSpan MaxRuntime { get; }
    
    private ILogger<GenerateMofFilesPlugin> Logger { get; }
    
    private readonly Regex ErrorParser = new(@"^(?<ErrorText>(?<Source>.*?)\s:\s(?<Message>.*?))(?:\vAt\v)", RegexOptions.Compiled | RegexOptions.Singleline);
}