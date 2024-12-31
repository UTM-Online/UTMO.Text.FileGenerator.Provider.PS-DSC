namespace UTMO.Text.FileGenerator.Provider.DSC;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Text.FileGenerator.Abstract.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Models;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class DscGenerationEnvironment : GenerationEnvironmentBase
{
    public DscGenerationEnvironment(IConfiguration configuration, IGeneratorCliOptions generatorOptions, List<Type> resources, ILogger<DscGenerationEnvironment> logger) : base(configuration, generatorOptions)
    {
        this.LocalResources = resources;
        this.Logger = logger;
    }

    public override string EnvironmentName => "DSC";
    
    private List<Type> LocalResources { get; }
    
    private ILogger<DscGenerationEnvironment> Logger { get; }

    public override void Initialize()
    {
        if (this.GeneratorOptions is DscCliOptions options)
        {
            this.EnvironmentConstants["registration_key"] = options.RegistrationKey;
        }
        else
        {
            this.Logger.LogError("Invalid CLI Options provided for DSC Generation Environment");
        }
        
        Parallel.ForEach(this.LocalResources.Select(resource => (ITemplateModel) Activator.CreateInstance(resource)!), model => this.AddResource(model));
        
        this.Logger.LogTrace("DSC Generation Environment Initialized");
    }
}