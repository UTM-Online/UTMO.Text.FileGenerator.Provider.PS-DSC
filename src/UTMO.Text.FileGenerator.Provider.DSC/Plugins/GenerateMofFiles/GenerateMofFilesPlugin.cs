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
    private const string GenerationDatePlaceholder = "@GenerationDate=__UTMO_IGNORED__";

    public GenerateMofFilesPlugin(IGeneralFileWriter writer, IGeneratorCliOptions options, ILogger<GenerateMofFilesPlugin> logger)
    {
        this.Writer = writer;
        this.OutputPath = options.OutputPath;
        this.Logger = logger;
        this.MaxRuntime = TimeSpan.FromSeconds(45);
    }

    public async Task<bool> HandleTemplate(ITemplateModel model)
    {
        Guard.StringNotNull(nameof(model.ResourceTypeName), model.ResourceTypeName);

        if (model.ResourceTypeName != DscResourceTypeNames.DscConfiguration && model.ResourceTypeName != DscResourceTypeNames.DscLcmConfiguration)
        {
            this.Logger.LogWarning(LogMessages.SkippingNonDscResource, model.ResourceName);
            return true;
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
            return false;
        }

        Guard.StringNotNull(nameof(scriptConfig), scriptConfig);

        this.Logger.LogTrace(LogMessages.ScriptConfigPath, scriptConfig);

        var fileType = model.ResourceTypeName == DscResourceTypeNames.DscConfiguration ? "Configurations" : "Computers";

        string mofOutputFile;

        try
        {
            var safeFileType = this.NormalizePathSegment(fileType);
            mofOutputFile = Path.Combine(this.OutputPath, "MOF", safeFileType);
        }
        catch (Exception)
        {
            this.Logger.LogError(LogMessages.ErrorGeneratingMofOutputPath, model.ResourceName);
            return false;
        }

        Guard.StringNotNull(nameof(mofOutputFile), mofOutputFile);

        this.Logger.LogTrace(LogMessages.MofOutputPath, mofOutputFile);

        var tempMofOutputPath = this.CreateTemporaryOutputPath(fileType);

        try
        {
            this.EnsureDirectoryExists(tempMofOutputPath);
            var generated = await this.GenerateMofAsync(model, scriptConfig, tempMofOutputPath);

            if (!generated)
            {
                return false;
            }

            await this.CopyGeneratedMofIfChangedAsync(model, tempMofOutputPath, mofOutputFile);

            return true;
        }
        finally
        {
            if (this.DirectoryExists(tempMofOutputPath))
            {
                this.DeleteDirectory(tempMofOutputPath);
            }
        }
    }

    public IGeneralFileWriter Writer { get; init; }

    public ITemplateGenerationEnvironment Environment { get; init; } = null!;

    public PluginPosition Position => PluginPosition.After;

    private string OutputPath { get; init; }

    public TimeSpan MaxRuntime { get; }

    public bool RequiresGeneration => true;

    private ILogger<GenerateMofFilesPlugin> Logger { get; }

    private readonly Regex ErrorParser = new(@"^(?<ErrorText>(?<Source>.*?)\s:\s(?<Message>.*?))(?:\vAt\v)", RegexOptions.Compiled | RegexOptions.Singleline);

    private readonly Regex HeaderMatcher = new(@"(?<comments>\/\*[\s\S]*?\*\/)\v*(?<Body>[\s\S]*)", RegexOptions.Compiled);

    private readonly Regex GenerationDateMatcher = new(@"(?m)^(?<Prefix>\s*@GenerationDate\s*=\s*).*$", RegexOptions.Compiled);

    protected virtual async Task<bool> GenerateMofAsync(ITemplateModel model, string scriptConfig, string mofOutputFile)
    {
        string? stdErr = null;

        try
        {
            var processInfo = new ProcessStartInfo
                              {
                                  FileName = "powershell.exe", // Use Windows PowerShell explicitly
                                  Arguments = $"-ExecutionPolicy Bypass -NoProfile -File \"{scriptConfig}\" -OutputPath \"{mofOutputFile}\"",
                                  RedirectStandardOutput = true,
                                  RedirectStandardError = true,
                                  UseShellExecute = false,
                                  CreateNoWindow = true,
                                  WorkingDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile), // Set working directory to user profile
                              };

            var userModulePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "WindowsPowerShell", "Modules");
            var currentPSModulePath = System.Environment.GetEnvironmentVariable("PSModulePath") ?? "";

            if (!currentPSModulePath.Contains(userModulePath))
            {
                processInfo.EnvironmentVariables["PSModulePath"] = $"{userModulePath};{currentPSModulePath}";
            }

            processInfo.EnvironmentVariables["USERPROFILE"] = System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile);
            processInfo.EnvironmentVariables["HOMEDRIVE"] = System.Environment.GetEnvironmentVariable("HOMEDRIVE") ?? "C:";
            processInfo.EnvironmentVariables["HOMEPATH"] = System.Environment.GetEnvironmentVariable("HOMEPATH") ?? "\\";
            processInfo.EnvironmentVariables["APPDATA"] = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            processInfo.EnvironmentVariables["LOCALAPPDATA"] = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);

            string? stdOut;

            using (var process = this.StartProcess(processInfo))
            {
                stdOut = await this.ReadStandardOutputAsync(process);
                stdErr = await this.ReadStandardErrorAsync(process);
                await this.WaitForExitAsync(process);
            }

            this.Logger.LogTrace(LogMessages.MofGenerationStdOut, stdOut ?? "None");

            if (!string.IsNullOrWhiteSpace(stdErr) && this.ErrorParser.IsMatch(stdErr))
            {
                stdErr = this.ErrorParser.Match(stdErr).Groups["ErrorText"].Value;

                this.Logger.LogError(LogMessages.MofGenerationFailed, model.ResourceName, stdErr);
                return false;
            }

            if (!string.IsNullOrWhiteSpace(stdErr))
            {
                this.Logger.LogError(LogMessages.MofGenerationFailed, model.ResourceName, stdErr);
                return false;
            }

            this.Logger.LogTrace(LogMessages.MofGenerationSucceeded, model.ResourceName);
            return true;
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
            return false;
        }
    }

    protected virtual Process StartProcess(ProcessStartInfo processInfo)
    {
        var process = new Process { StartInfo = processInfo };
        process.Start();
        return process;
    }

    protected virtual Task<string> ReadStandardOutputAsync(Process process)
    {
        return process.StandardOutput.ReadToEndAsync();
    }

    protected virtual Task<string> ReadStandardErrorAsync(Process process)
    {
        return process.StandardError.ReadToEndAsync();
    }

    protected virtual Task WaitForExitAsync(Process process)
    {
        return process.WaitForExitAsync();
    }

    private string CreateTemporaryOutputPath(string fileType)
    {
        var safeFileType = this.NormalizePathSegment(fileType);
        return Path.Combine(Path.GetTempPath(), nameof(GenerateMofFilesPlugin), Guid.NewGuid().ToString("N"), "MOF", safeFileType);
    }

    private async Task CopyGeneratedMofIfChangedAsync(ITemplateModel model, string sourceDirectory, string destinationDirectory)
    {
        var sourceFile = this.GetGeneratedMofFilePath(model, sourceDirectory);
        Guard.Requires<FileNotFoundException>(this.FileExists(sourceFile), $"Generated MOF output file does not exist: {sourceFile}");

        var destinationFile = this.GetGeneratedMofFilePath(model, destinationDirectory);
        if (this.FileExists(destinationFile))
        {
            var existingContent = await this.ReadAllTextAsync(destinationFile);
            var generatedContent = await this.ReadAllTextAsync(sourceFile);

            if (this.NormalizeMofContent(existingContent) == this.NormalizeMofContent(generatedContent)
                || this.StripHeaderComment(existingContent) == this.StripHeaderComment(generatedContent))
            {
                this.Logger.LogInformation("Skipping MOF overwrite for {ResourceName} because only the PowerShell DSC generation timestamp changed.", model.ResourceName);
                return;
            }
        }

        var destinationFolder = Path.GetDirectoryName(destinationFile);
        Guard.StringNotNull(nameof(destinationFolder), destinationFolder);
        this.EnsureDirectoryExists(destinationFolder!);
        this.CopyFile(sourceFile, destinationFile, overwrite: true);
    }

    private string GetGeneratedMofFilePath(ITemplateModel model, string outputDirectory)
    {
        var fileName = model.ResourceTypeName == DscResourceTypeNames.DscConfiguration
            ? $"{model.ResourceName}.mof"
            : $"{model.ResourceName}.meta.mof";

        var safeFileName = this.EnsureFileNameOnly(fileName, nameof(model.ResourceName));
        return Path.Combine(outputDirectory, safeFileName);
    }

    private string NormalizePathSegment(string segment)
    {
        var normalized = segment.TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        return this.EnsureFileNameOnly(normalized, nameof(segment));
    }

    private string EnsureFileNameOnly(string value, string paramName)
    {
        if (Path.IsPathRooted(value) || value != Path.GetFileName(value))
        {
            throw new ArgumentException($"Path segment '{value}' is not a valid file name.", paramName);
        }

        return value;
    }

    private string NormalizeMofContent(string content)
    {
        var normalizedContent = content.Replace("\r\n", "\n");
        if (!this.HeaderMatcher.IsMatch(normalizedContent))
        {
            return normalizedContent.Trim();
        }

        var match = this.HeaderMatcher.Match(normalizedContent);
        var header = match.Groups["comments"].Value;
        var body = match.Groups["Body"].Value;

        header = this.GenerationDateMatcher.Replace(header, "${Prefix}" + GenerationDatePlaceholder);
        return $"{header}\n{body}".Trim();
    }

    private string StripHeaderComment(string content)
    {
        var normalizedContent = content.Replace("\r\n", "\n");
        if (!this.HeaderMatcher.IsMatch(normalizedContent))
        {
            return normalizedContent.Trim();
        }

        return this.HeaderMatcher.Match(normalizedContent).Groups["Body"].Value.Trim();
    }

    protected virtual Task<string> ReadAllTextAsync(string path)
    {
        return File.ReadAllTextAsync(path);
    }

    protected virtual bool FileExists(string path)
    {
        return File.Exists(path);
    }

    protected virtual void EnsureDirectoryExists(string path)
    {
        Directory.CreateDirectory(path);
    }

    protected virtual void CopyFile(string sourcePath, string destinationPath, bool overwrite)
    {
        File.Copy(sourcePath, destinationPath, overwrite);
    }

    protected virtual bool DirectoryExists(string path)
    {
        return Directory.Exists(path);
    }

    protected virtual void DeleteDirectory(string path)
    {
        Directory.Delete(path, recursive: true);
    }
}
