using Microsoft.Extensions.Logging;
using Serilog.Events;

namespace UTMO.Text.FileGenerator.Provider.DSC;

using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;
using UTMO.Text.FileGenerator.Provider.DSC.Models;
using UTMO.Text.FileGenerator.Provider.DSC.Plugins.GenerateMofFiles;
using UTMO.Text.FileGenerator.Provider.DSC.Plugins.ProcessRequiredModules;
using UTMO.Text.FileGenerator.Provider.DSC.Plugins.RestoreRequiredModules;
using UTMO.Text.FileGenerator.Provider.DSC.Plugins.TrimMofComments;
using UTMO.Text.FileGenerator.Utils;

public class DscGenerator
{
    private DscGenerator()
    {
    }

    private FileGenerator FileGenerator { get; set; } = null!;

    private static ILogger Logger => Log.ForContext<DscGenerator>();

    public static DscGenerator Create(string[] args,  LogLevel logLevel = LogLevel.Information)
    {
        Logger.Debug(@"Creating DSC Generator");
        var generator = new DscGenerator
                        {
                            FileGenerator = FileGenerator.Create(args, logLevel),
                        };

        Logger.Information(@"Configuring DSC Generator");
        
        generator.FileGenerator.RegisterPipelinePlugin<RestoreRequiredModulesPlugin>()
                 .RegisterRendererPlugin<GenerateMofFilesPlugin>()
                 .RegisterPipelinePlugin<ProcessRequiredModules>()
                 .UseEnvironment<DscGenerationEnvironment>()
                 .RegisterCustomCliOptions<DscCliOptions>()
                 .RegisterPipelinePlugin<TrimMofCommentsProcessor>();

        Logger.Information(@"Scanning for DSC Configurations");
        var configurations = Assembly.GetCallingAssembly().GetTypes()
                                     .Where(t => t.IsSubclassOf(typeof(DscConfiguration)) && !t.IsAbstract)
                                     .ToList();

        Logger.Information(@"Scanning for DSC Computers");
        var computers = Assembly.GetCallingAssembly().GetTypes()
                                .Where(t => t.IsSubclassOf(typeof(DscLcmConfiguration)) && !t.IsAbstract)
                                .ToList();

        Logger.Information(@"Merging DSC Resources");
        var unifiedResources = configurations.Concat(computers).ToList();

        Logger.Information(@"Configuring DSC Generator Resources");
        generator.FileGenerator.ConfigureServices(svc => { svc.AddSingleton(unifiedResources); });

        Logger.Information(@"DSC Generator Created");
        return generator;
    }

    public DscGenerator ConfigureFeatures(Action<IConfigurationBuilder> configure)
    {
        this.FileGenerator.ConfigureHost(a => a.ConfigureHostConfiguration(configure));
        return this;
    }

    public void Run()
    {
        Logger.Information(@"Running DSC Generator");
        this.FileGenerator.Run();
    }
}