using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using UTMO.Text.FileGenerator.Abstract.Contracts;
using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Constants;
using UTMO.Text.FileGenerator.Provider.DSC.Plugins.GenerateMofFiles;

namespace DSCProviderCore.Tests;

[TestClass]
public class GenerateMofFilesPluginTests
{
    [TestMethod]
    public async Task HandleTemplate_WhenOnlyGenerationDateChanges_PreservesExistingMofFile()
    {
        var outputRoot = CreateOutputRoot();
        var model = new TestTemplateModel
        {
            ResourceName = "WebServer",
            ResourceTypeName = DscResourceTypeNames.DscConfiguration,
            GenerateManifest = false,
        };

        var existingContent = """
                              /*
                              @TargetNode=localhost
                              @GenerationDate=05/18/2026 12:00:00
                              */
                              instance of ExampleResource
                              {
                                  Name = "Value";
                              };
                              """;

        var generatedContent = """
                               /*
                               @TargetNode=localhost
                               @GenerationDate=05/19/2026 12:00:00
                               */
                               instance of ExampleResource
                               {
                                   Name = "Value";
                               };
                               """;

        try
        {
            WriteExistingMof(outputRoot, model, existingContent);
            var plugin = CreatePlugin(outputRoot, generatedContent);

            var result = await plugin.HandleTemplate(model);

            Assert.IsTrue(result);
            Assert.AreEqual(existingContent, await File.ReadAllTextAsync(GetMofFilePath(outputRoot, model)));
        }
        finally
        {
            CleanupOutputRoot(outputRoot);
        }
    }

    [TestMethod]
    public async Task HandleTemplate_WhenManifestResourceBodyMatchesGeneratedMof_PreservesExistingMofFile()
    {
        var outputRoot = CreateOutputRoot();
        var model = new TestTemplateModel
        {
            ResourceName = "LcmConfig",
            ResourceTypeName = DscResourceTypeNames.DscLcmConfiguration,
            GenerateManifest = true,
        };

        var existingContent = """
                              instance of MSFT_DSCMetaConfiguration as $MSFT_DSCMetaConfiguration1ref
                              {
                                  ConfigurationMode = "ApplyOnly";
                              };
                              """;

        var generatedContent = """
                               /*
                               @TargetNode=localhost
                               @GenerationDate=05/19/2026 12:00:00
                               */
                               instance of MSFT_DSCMetaConfiguration as $MSFT_DSCMetaConfiguration1ref
                               {
                                   ConfigurationMode = "ApplyOnly";
                               };
                               """;

        try
        {
            WriteExistingMof(outputRoot, model, existingContent);
            var plugin = CreatePlugin(outputRoot, generatedContent);

            var result = await plugin.HandleTemplate(model);

            Assert.IsTrue(result);
            Assert.AreEqual(existingContent, await File.ReadAllTextAsync(GetMofFilePath(outputRoot, model)));
        }
        finally
        {
            CleanupOutputRoot(outputRoot);
        }
    }

    [TestMethod]
    public async Task HandleTemplate_WhenMeaningfulContentChanges_OverwritesExistingMofFile()
    {
        var outputRoot = CreateOutputRoot();
        var model = new TestTemplateModel
        {
            ResourceName = "WebServer",
            ResourceTypeName = DscResourceTypeNames.DscConfiguration,
            GenerateManifest = false,
        };

        var existingContent = """
                              /*
                              @TargetNode=localhost
                              @GenerationDate=05/18/2026 12:00:00
                              */
                              instance of ExampleResource
                              {
                                  Name = "Value";
                              };
                              """;

        var generatedContent = """
                               /*
                               @TargetNode=localhost
                               @GenerationDate=05/19/2026 12:00:00
                               */
                               instance of ExampleResource
                               {
                                   Name = "NewValue";
                               };
                               """;

        try
        {
            WriteExistingMof(outputRoot, model, existingContent);
            var plugin = CreatePlugin(outputRoot, generatedContent);

            var result = await plugin.HandleTemplate(model);

            Assert.IsTrue(result);
            Assert.AreEqual(generatedContent, await File.ReadAllTextAsync(GetMofFilePath(outputRoot, model)));
        }
        finally
        {
            CleanupOutputRoot(outputRoot);
        }
    }

    [TestMethod]
    public async Task HandleTemplate_WhenInlineBodyCommentDiffers_OverwritesExistingMofFile()
    {
        var outputRoot = CreateOutputRoot();
        var model = new TestTemplateModel
        {
            ResourceName = "WebServer",
            ResourceTypeName = DscResourceTypeNames.DscConfiguration,
            GenerateManifest = false,
        };

        var existingContent = """
                              prefix text
                              /* inline comment */
                              value-one
                              """;

        var generatedContent = """
                               prefix text
                               /* inline comment */
                               value-two
                               """;

        try
        {
            WriteExistingMof(outputRoot, model, existingContent);
            var plugin = CreatePlugin(outputRoot, generatedContent);

            var result = await plugin.HandleTemplate(model);

            Assert.IsTrue(result);
            Assert.AreEqual(generatedContent, await File.ReadAllTextAsync(GetMofFilePath(outputRoot, model)));
        }
        finally
        {
            CleanupOutputRoot(outputRoot);
        }
    }

    private static TestableGenerateMofFilesPlugin CreatePlugin(string outputRoot, string generatedContent)
    {
        var options = new Mock<IGeneratorCliOptions>();
        options.SetupGet(x => x.OutputPath).Returns(outputRoot);

        return new TestableGenerateMofFilesPlugin(
            Mock.Of<IGeneralFileWriter>(),
            options.Object,
            generatedContent);
    }

    private static void WriteExistingMof(string outputRoot, TestTemplateModel model, string content)
    {
        var mofPath = GetMofFilePath(outputRoot, model);
        Directory.CreateDirectory(Path.GetDirectoryName(mofPath)!);
        File.WriteAllText(mofPath, content);
    }

    private static string GetMofFilePath(string outputRoot, TestTemplateModel model)
    {
        var normalizedOutputRoot = NormalizeOutputRoot(outputRoot);
        var directoryName = model.ResourceTypeName == DscResourceTypeNames.DscConfiguration ? "Configurations" : "Computers";
        var fileName = model.ResourceTypeName == DscResourceTypeNames.DscConfiguration
            ? $"{model.ResourceName}.mof"
            : $"{model.ResourceName}.meta.mof";
        var safeFileName = Path.GetFileName(fileName);
        var safeMofSegment = EnsureNotRooted("MOF");

        return Path.Join(normalizedOutputRoot, safeMofSegment, EnsureNotRooted(directoryName), EnsureNotRooted(safeFileName));
    }

    private static string CreateOutputRoot()
    {
        var uniqueFolderName = Guid.NewGuid().ToString("N");
        return Path.Join(Path.GetTempPath(), nameof(GenerateMofFilesPluginTests), uniqueFolderName);
    }

    private static void CleanupOutputRoot(string outputRoot)
    {
        if (Directory.Exists(outputRoot))
        {
            Directory.Delete(outputRoot, recursive: true);
        }
    }

    private sealed class TestableGenerateMofFilesPlugin : GenerateMofFilesPlugin
    {
        private readonly string generatedContent;

        public TestableGenerateMofFilesPlugin(IGeneralFileWriter writer, IGeneratorCliOptions options, string generatedContent)
            : base(writer, options, NullLogger<GenerateMofFilesPlugin>.Instance)
        {
            this.generatedContent = generatedContent;
        }

        protected override Task<bool> GenerateMofAsync(ITemplateModel model, string scriptConfig, string mofOutputFile)
        {
            Directory.CreateDirectory(mofOutputFile);
            File.WriteAllText(GetMofFilePath(mofOutputFile, model), this.generatedContent);
            return Task.FromResult(true);
        }

        private static string GetMofFilePath(string outputDirectory, ITemplateModel model)
        {
            var fileName = model.ResourceTypeName == DscResourceTypeNames.DscConfiguration
                ? $"{model.ResourceName}.mof"
                : $"{model.ResourceName}.meta.mof";
            var safeFileName = Path.GetFileName(fileName);
            var validatedFileName = EnsureNotRooted(safeFileName);

            return Path.Join(outputDirectory, validatedFileName);
        }
    }

    private sealed class TestTemplateModel : ITemplateModel, IManifestProducer
    {
        public string ResourceTypeName { get; init; } = string.Empty;

        public string TemplatePath => "template.liquid";

        public string OutputExtension => ".ps1";

        public string ResourceName { get; init; } = string.Empty;

        public bool EnableGeneration => true;

        public bool UseAlternateName => false;

        public bool GenerateManifest { get; init; }

        public Task<List<ValidationFailedException>> Validate()
        {
            return Task.FromResult(new List<ValidationFailedException>());
        }

        public Task<Dictionary<string, object>> ToTemplateContext()
        {
            return Task.FromResult(new Dictionary<string, object>());
        }

        public string ProduceOutputPath(string basePath)
        {
            var safeFileName = Path.GetFileName($"{this.ResourceName}.ps1");
            var validatedFileName = EnsureNotRooted(safeFileName);
            return Path.Join(basePath, validatedFileName);
        }

        public ITemplateModel AddAdditionalProperty<T>(string key, T value)
        {
            return this;
        }

        public Task<object?> ToManifest()
        {
            return Task.FromResult<object?>(new object());
        }
    }

    private static string EnsureNotRooted(string segment)
    {
        if (Path.IsPathRooted(segment))
        {
            throw new ArgumentException($"Path segment '{segment}' cannot be rooted.", nameof(segment));
        }

        return segment;
    }

    private static string NormalizeOutputRoot(string outputRoot)
    {
        if (string.IsNullOrWhiteSpace(outputRoot))
        {
            throw new ArgumentException("Output root cannot be null or whitespace.", nameof(outputRoot));
        }

        return Path.GetFullPath(outputRoot);
    }
}
