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

    private static List<Type> GetAllLoadedDscTypes<T>() where T : class
    {
        try
        {
            Logger.Debug("Scanning loaded assemblies for types inheriting from {TargetType}", typeof(T).FullName);
            
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.IsDynamic && a.FullName != null)
                .ToList();

            var types = new List<Type>();
            var baseTypeName = typeof(T).FullName;
            
            foreach (var assembly in loadedAssemblies)
            {
                try
                {
                    // Check if assembly contains DSC types by looking for our base types or known DSC assemblies
                    bool shouldScanAssembly = false;
                    
                    // Always scan our own DSC assemblies
                    if (assembly.FullName!.Contains("UTMO.Text.FileGenerator.Provider.DSC") || 
                        assembly.FullName.Contains("WindowsDefender"))
                    {
                        shouldScanAssembly = true;
                    }
                    
                    // For other assemblies, check if they reference our base types
                    if (!shouldScanAssembly)
                    {
                        try
                        {
                            var referencedAssemblies = assembly.GetReferencedAssemblies();
                            shouldScanAssembly = referencedAssemblies.Any(ra => 
                                ra.FullName.Contains("UTMO.Text.FileGenerator.Provider.DSC.Abstract") ||
                                ra.FullName.Contains("UTMO.Text.FileGenerator.Provider.DSC"));
                        }
                        catch
                        {
                            // If we can't check references, try a quick type scan
                            try
                            {
                                var hasRelevantTypes = assembly.GetTypes()
                                    .Any(t => t.BaseType?.FullName == baseTypeName || 
                                             (t.BaseType != null && t.BaseType.IsSubclassOf(typeof(T))));
                                shouldScanAssembly = hasRelevantTypes;
                            }
                            catch
                            {
                                // Skip this assembly if we can't analyze it
                            }
                        }
                    }

                    if (shouldScanAssembly)
                    {
                        var assemblyTypes = assembly.GetTypes()
                            .Where(t => t.IsSubclassOf(typeof(T)) && !t.IsAbstract)
                            .ToList();
                        
                        types.AddRange(assemblyTypes);
                        
                        if (assemblyTypes.Any())
                        {
                            Logger.Debug($@"Found {assemblyTypes.Count} {typeof(T).Name} types in assembly {assembly.GetName().Name}");
                        }
                    }
                }
                catch (ReflectionTypeLoadException ex)
                {
                    Logger.Warning(ex, $@"Could not load types from assembly {assembly.GetName().Name}");
                    
                    // Try to get the types that did load successfully
                    var loadedTypes = ex.Types?.Where(t => t != null && t.IsSubclassOf(typeof(T)) && !t.IsAbstract).ToList();
                    if (loadedTypes?.Any() == true)
                    {
                        types.AddRange(loadedTypes!);
                        Logger.Debug($@"Partial success: Found {loadedTypes.Count} {typeof(T).Name} types in assembly {assembly.GetName().Name}");
                    }
                }
                catch (Exception ex)
                {
                    Logger.Debug(ex, $@"Skipping assembly {assembly.GetName().Name} due to error");
                }
            }
            
            Logger.Information($@"Found {types.Count} total {typeof(T).Name} types across all loaded assemblies");
            return types;
        }
        catch (Exception ex)
        {
            Logger.Error(ex, $@"Error scanning for {typeof(T).Name} types, falling back to calling assembly only");
            
            // Fallback to original behavior if the new approach fails
            try
            {
                return Assembly.GetCallingAssembly().GetTypes()
                    .Where(t => t.IsSubclassOf(typeof(T)) && !t.IsAbstract)
                    .ToList();
            }
            catch
            {
                Logger.Warning($@"Fallback also failed, returning empty list for {typeof(T).Name}");
                return new List<Type>();
            }
        }
    }

    public static DscGenerator Create(string[] args,  LogEventLevel logLevel = LogEventLevel.Information)
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
        var configurations = GetAllLoadedDscTypes<DscConfiguration>();

        Logger.Information(@"Scanning for DSC Computers");
        var computers = GetAllLoadedDscTypes<DscLcmConfiguration>();

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