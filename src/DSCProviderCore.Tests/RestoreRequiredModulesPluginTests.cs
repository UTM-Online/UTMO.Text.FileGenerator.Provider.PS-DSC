using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using UTMO.Text.FileGenerator.Abstract.Contracts;
using UTMO.Text.FileGenerator.Abstract;
using UTMO.Text.FileGenerator.Provider.DSC.Constants;
using UTMO.Text.FileGenerator.Provider.DSC.Plugins.RestoreRequiredModules;

namespace DSCProviderCore.Tests;

[TestClass]
[DoNotParallelize]
public class RestoreRequiredModulesPluginTests
{
    [TestMethod]
    public async Task ProcessPlugin_WhenWaitTimesOut_KillsProcessTreeAndReturnsFalse()
    {
        // Arrange
        var outputRoot = CreateOutputRootWithManifest("dev");
        var scriptSetup = EnsureRestoreScriptExists();

        try
        {
            var options = new Mock<IGeneratorCliOptions>();
            options.SetupGet(x => x.OutputPath).Returns(outputRoot);

            var environment = new Mock<ITemplateGenerationEnvironment>();
            environment.SetupGet(x => x.EnvironmentName).Returns("dev");

            var plugin = new TestableRestoreRequiredModulesPlugin(
                Mock.Of<IGeneralFileWriter>(),
                NullLogger<RestoreRequiredModulesPlugin>.Instance,
                options.Object,
                failStreamReads: false);

            // Act
            var result = await plugin.ProcessPlugin(environment.Object);

            // Assert
            Assert.IsFalse(result);
            Assert.IsTrue(plugin.KillInvoked, "Expected timed-out process to be killed.");
            Assert.IsTrue(plugin.StdOutDrainCompleted, "Expected stdout drain task to complete during timeout cleanup.");
            Assert.IsTrue(plugin.StdErrDrainCompleted, "Expected stderr drain task to complete during timeout cleanup.");
        }
        finally
        {
            CleanupOutputRoot(outputRoot);
            RestoreScript(scriptSetup.path, scriptSetup.existed, scriptSetup.originalContent);
        }
    }

    [TestMethod]
    public async Task ProcessPlugin_WhenTimeoutAndStreamReadFails_HandlesDrainWithoutThrowing()
    {
        // Arrange
        var outputRoot = CreateOutputRootWithManifest("dev");
        var scriptSetup = EnsureRestoreScriptExists();

        try
        {
            var options = new Mock<IGeneratorCliOptions>();
            options.SetupGet(x => x.OutputPath).Returns(outputRoot);

            var environment = new Mock<ITemplateGenerationEnvironment>();
            environment.SetupGet(x => x.EnvironmentName).Returns("dev");

            var plugin = new TestableRestoreRequiredModulesPlugin(
                Mock.Of<IGeneralFileWriter>(),
                NullLogger<RestoreRequiredModulesPlugin>.Instance,
                options.Object,
                failStreamReads: true);

            // Act
            var result = await plugin.ProcessPlugin(environment.Object);

            // Assert
            Assert.IsFalse(result);
            Assert.IsTrue(plugin.KillInvoked, "Expected timed-out process to be killed.");
            Assert.IsTrue(plugin.StdOutDrainCompleted, "Expected stdout drain task to be observed even when it faults.");
            Assert.IsTrue(plugin.StdErrDrainCompleted, "Expected stderr drain task to be observed even when it faults.");
        }
        finally
        {
            CleanupOutputRoot(outputRoot);
            RestoreScript(scriptSetup.path, scriptSetup.existed, scriptSetup.originalContent);
        }
    }

    private static string CreateOutputRootWithManifest(string environmentName)
    {
        var outputRoot = Path.Combine(Path.GetTempPath(), "RestoreRequiredModulesPluginTests", Guid.NewGuid().ToString("N"));
        var manifestDirectory = Path.Combine(outputRoot, "Manifests", environmentName);
        Directory.CreateDirectory(manifestDirectory);
        File.WriteAllText(Path.Combine(manifestDirectory, "RequiredModule.Manifest.json"), "{}");
        return outputRoot;
    }

    private static (string path, bool existed, string? originalContent) EnsureRestoreScriptExists()
    {
        var providerAssemblyPath = typeof(RestoreRequiredModulesPlugin).Assembly.Location;
        var providerAssemblyDirectory = Path.GetDirectoryName(providerAssemblyPath) ?? throw new InvalidOperationException("Could not resolve provider assembly directory.");
        var scriptsDirectory = Path.Combine(providerAssemblyDirectory, "Scripts");
        Directory.CreateDirectory(scriptsDirectory);

        var scriptPath = Path.Combine(scriptsDirectory, ScriptConstants.RestoreRequiredModules);
        var existed = File.Exists(scriptPath);
        var originalContent = existed ? File.ReadAllText(scriptPath) : null;

        // Keep the process alive long enough for timeout path to execute deterministically.
        var testScript = "param([string]$moduleManifestPath)`nStart-Sleep -Seconds 30";
        File.WriteAllText(scriptPath, testScript);

        return (scriptPath, existed, originalContent);
    }

    private static void RestoreScript(string scriptPath, bool existed, string? originalContent)
    {
        if (existed)
        {
            File.WriteAllText(scriptPath, originalContent ?? string.Empty);
            return;
        }

        if (File.Exists(scriptPath))
        {
            File.Delete(scriptPath);
        }
    }

    private static void CleanupOutputRoot(string outputRoot)
    {
        if (Directory.Exists(outputRoot))
        {
            Directory.Delete(outputRoot, recursive: true);
        }
    }

    private class TestableRestoreRequiredModulesPlugin : RestoreRequiredModulesPlugin
    {
        private readonly TaskCompletionSource<string> stdOutTaskSource = new(TaskCreationOptions.RunContinuationsAsynchronously);
        private readonly TaskCompletionSource<string> stdErrTaskSource = new(TaskCreationOptions.RunContinuationsAsynchronously);
        private readonly bool failStreamReads;

        public TestableRestoreRequiredModulesPlugin(
            IGeneralFileWriter writer,
            ILogger<RestoreRequiredModulesPlugin> logger,
            IGeneratorCliOptions options,
            bool failStreamReads)
            : base(writer, logger, options)
        {
            this.failStreamReads = failStreamReads;
        }

        public bool KillInvoked { get; private set; }

        public bool StdOutDrainCompleted => this.stdOutTaskSource.Task.IsCompleted;

        public bool StdErrDrainCompleted => this.stdErrTaskSource.Task.IsCompleted;

        protected override Task<string> ReadStandardOutputAsync(Process process)
        {
            return this.stdOutTaskSource.Task;
        }

        protected override Task<string> ReadStandardErrorAsync(Process process)
        {
            return this.stdErrTaskSource.Task;
        }

        protected override Task WaitForExitAsync(Process process, CancellationToken cancellationToken)
        {
            return Task.FromException(new OperationCanceledException(cancellationToken));
        }

        protected override void KillProcess(Process process)
        {
            this.KillInvoked = true;

            if (this.failStreamReads)
            {
                this.stdOutTaskSource.TrySetException(new InvalidOperationException("stdout failure"));
                this.stdErrTaskSource.TrySetException(new InvalidOperationException("stderr failure"));
            }
            else
            {
                this.stdOutTaskSource.TrySetResult("stdout");
                this.stdErrTaskSource.TrySetResult("stderr");
            }

            base.KillProcess(process);
        }
    }
}




