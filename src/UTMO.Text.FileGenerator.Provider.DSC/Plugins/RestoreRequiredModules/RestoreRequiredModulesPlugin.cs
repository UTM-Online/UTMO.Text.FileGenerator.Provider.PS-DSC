namespace UTMO.Text.FileGenerator.Provider.DSC.Plugins.RestoreRequiredModules;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
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

    public async Task<bool> ProcessPlugin(ITemplateGenerationEnvironment environment)
    {
        // RestoreRequiredModules is Windows-specific (DSC and PowerShell modules are Windows-only)
        if (!this.IsPlatformSupported)
        {
            this.Logger.LogError("RestoreRequiredModules plugin requires Windows. Current platform: {Platform}", RuntimeInformation.OSDescription);
            return false;
        }

        var manifestPath = Path.Join(this.Options.OutputPath, "Manifests", environment.EnvironmentName, "RequiredModule.Manifest.json");

        if (!File.Exists(manifestPath))
        {
            this.Logger.LogError("Required Module Manifest not found at {ManifestPath}", manifestPath);
            return false;
        }
        
        this.Logger.LogInformation(LogMessages.StartingRestoreRequiredModules);
        var scriptPath = Path.Combine(AppContext.BaseDirectory, "Scripts", ScriptConstants.RestoreRequiredModules);

        if (!this.ScriptFileExists(scriptPath))
        {
            this.Logger.LogError("PowerShell script not found at {ScriptPath}", scriptPath);
            return false;
        }

        Process? process = null;
        Task<string>? stdOutTask = null;
        Task<string>? stdErrTask = null;

        try
        {
            var processInfo = new ProcessStartInfo
            {
                FileName = this.PowerShellExecutable,
                Arguments = $"-ExecutionPolicy Bypass -NoProfile -File \"{scriptPath}\" -moduleManifestPath \"{manifestPath}\"",
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
                processInfo.EnvironmentVariables["PSModulePath"] = $"{userModulePath}{Path.PathSeparator}{currentPSModulePath}";
            }
            
            // Ensure the process runs with the current user's environment
            processInfo.EnvironmentVariables["USERPROFILE"] = System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile);
            processInfo.EnvironmentVariables["HOMEDRIVE"] = System.Environment.GetEnvironmentVariable("HOMEDRIVE") ?? "C:";
            processInfo.EnvironmentVariables["HOMEPATH"] = System.Environment.GetEnvironmentVariable("HOMEPATH") ?? "\\";
            processInfo.EnvironmentVariables["APPDATA"] = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            processInfo.EnvironmentVariables["LOCALAPPDATA"] = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);

            this.Logger.LogDebug("Starting Windows PowerShell process for module restoration");

            process = this.StartProcess(processInfo);
            
            stdOutTask = this.ReadStandardOutputAsync(process);
            stdErrTask = this.ReadStandardErrorAsync(process);
            
            using var cancellationTokenSource = new CancellationTokenSource(this.MaxRuntime);
            await this.WaitForExitAsync(process, cancellationTokenSource.Token);
            
            var stdOut = await this.ReadStreamSafelyAsync(stdOutTask, "stdout");
            var stdErr = await this.ReadStreamSafelyAsync(stdErrTask, "stderr");
            
            this.Logger.LogTrace(LogMessages.RestoreModulesStdOut, stdOut);
            
            if (process.ExitCode != 0)
            {
                var errorMessage = !string.IsNullOrWhiteSpace(stdErr) ? stdErr : $"Process exited with code {process.ExitCode}";
                this.Logger.LogError(LogMessages.RestoreRequiredModulesFailed, errorMessage);
                return false;
            }

            if (!string.IsNullOrWhiteSpace(stdErr))
            {
                this.Logger.LogWarning("RestoreRequiredModules.ps1 completed with warnings: {Warnings}", stdErr);
            }
            this.Logger.LogInformation(LogMessages.RestoredRequiredModulesSucceeded);
            return true;
        }
        catch (OperationCanceledException)
        {
            if (process != null)
            {
                try
                {
                    if (!this.HasProcessExited(process))
                    {
                        this.KillProcess(process);
                    }

                    await this.WaitForExitAsync(process);
                }
                catch (Exception ex)
                {
                    this.Logger.LogWarning(ex, "Failed to terminate timed-out PowerShell process tree cleanly.");
                }
            }

            _ = await this.ReadStreamSafelyAsync(stdOutTask, "stdout");
            _ = await this.ReadStreamSafelyAsync(stdErrTask, "stderr");

            this.Logger.LogError(LogMessages.RestoreRequiredModulesTimedOut, this.MaxRuntime);
            return false;
        }
        catch (Exception ex)
        {
            this.Logger.LogError(ex, LogMessages.RestoreRequiredModulesException, ex.Message);
            return false;
        }
        finally
        {
            process?.Dispose();
        }
    }

    private async Task<string> ReadStreamSafelyAsync(Task<string>? streamReadTask, string streamName)
    {
        if (streamReadTask == null)
        {
            return string.Empty;
        }

        try
        {
            return await streamReadTask;
        }
        catch (Exception ex)
        {
            this.Logger.LogWarning(ex, "Failed to read PowerShell process {StreamName} stream.", streamName);
            return string.Empty;
        }
    }

    protected virtual Task<string> ReadStandardOutputAsync(Process process)
    {
        return process.StandardOutput.ReadToEndAsync();
    }

    protected virtual Task<string> ReadStandardErrorAsync(Process process)
    {
        return process.StandardError.ReadToEndAsync();
    }

    protected virtual Task WaitForExitAsync(Process process, CancellationToken cancellationToken)
    {
        return process.WaitForExitAsync(cancellationToken);
    }

    protected virtual Task WaitForExitAsync(Process process)
    {
        return process.WaitForExitAsync();
    }

    protected virtual string PowerShellExecutable =>
        RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "powershell.exe" : "pwsh";

    protected virtual void KillProcess(Process process)
    {
        process.Kill(entireProcessTree: true);
    }

    protected virtual bool IsPlatformSupported => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

    protected virtual bool ScriptFileExists(string scriptPath) => File.Exists(scriptPath);

    protected virtual Process StartProcess(ProcessStartInfo processInfo)
    {
        var process = new Process { StartInfo = processInfo };
        process.Start();
        return process;
    }

    protected virtual bool HasProcessExited(Process process) => process.HasExited;

    public IGeneralFileWriter? Writer { get; init; }

    public ITemplateGenerationEnvironment? Environment { get; init; }

    public PluginPosition Position => PluginPosition.Before;
}