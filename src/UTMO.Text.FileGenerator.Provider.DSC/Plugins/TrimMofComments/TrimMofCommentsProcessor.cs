namespace UTMO.Text.FileGenerator.Provider.DSC.Plugins.TrimMofComments;

using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using UTMO.Common.Guards;
using UTMO.Text.FileGenerator.Abstract;
using UTMO.Text.FileGenerator.Abstract.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Constants;

public class TrimMofCommentsProcessor : IPipelinePlugin
{
    public TrimMofCommentsProcessor(IGeneralFileWriter writer, IGeneratorCliOptions options, ILogger<TrimMofCommentsProcessor> logger)
    {
        this.Writer = writer;
        this.Logger = logger;
        this.OutputPath = options.OutputPath;
    }

    public TimeSpan MaxRuntime => TimeSpan.FromMinutes(2);

    public async Task<bool> ProcessPlugin(ITemplateGenerationEnvironment environment)
    {
        this.Logger.LogInformation("Starting plugin: trim comments from MOF files");
        foreach (var resource in environment.Resources)
        {
            var resourceType  = resource.ResourceTypeName == DscResourceTypeNames.DscConfiguration ? "Configurations" : "Computers";
            var mofOutputFile = Path.Combine(this.OutputPath, $@"MOF\{resourceType}");

            switch (resourceType)
            {
                case "Computers":
                {
                    mofOutputFile = Path.Combine(mofOutputFile, $"{resource.ResourceName}.meta.mof");
                    break;
                }
                case "Configurations":
                {
                    mofOutputFile = Path.Combine(mofOutputFile, $"{resource.ResourceName}.mof");
                    break;
                }
            }

            if (resource is IManifestProducer producer && producer.GenerateManifest)
            {
                Guard.StringNotNull(nameof(mofOutputFile), mofOutputFile);
                Guard.Requires<InvalidOperationException>(File.Exists(mofOutputFile), $"!ERROR! MOF output file does not exist: {mofOutputFile}");
                var fileText = await File.ReadAllTextAsync(mofOutputFile);

                if (this.HeaderMatcher.IsMatch(fileText))
                {
                    await File.WriteAllTextAsync(mofOutputFile, this.HeaderMatcher.Match(fileText).Groups["Body"].Value);
                }
            }
        }
        
        this.Logger.LogInformation("Finished plugin: trim comments from MOF files");
        return true;
    }

    public IGeneralFileWriter? Writer { get; init; }

    public ITemplateGenerationEnvironment? Environment { get; init; }

    public PluginPosition Position => PluginPosition.After;
    
    private Regex HeaderMatcher = new Regex(@"(?<comments>\/\*[\s\S]*?\*\/)\v*(?<Body>[\s\S]*)", RegexOptions.Compiled);
    
    private string OutputPath { get; }
    
    private ILogger<TrimMofCommentsProcessor> Logger { get; }
}