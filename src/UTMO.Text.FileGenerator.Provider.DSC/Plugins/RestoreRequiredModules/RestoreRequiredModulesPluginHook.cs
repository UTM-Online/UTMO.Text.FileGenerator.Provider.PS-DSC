namespace UTMO.Text.FileGenerator.Provider.DSC.Plugins.RestoreRequiredModules;

using UTMO.Text.FileGenerator.Abstract;

public static class RestoreRequiredModulesPluginHook
{
    public static IRegisterPluginManager UseRestoreRequiredModulesPlugin(this IRegisterPluginManager pluginManager, TimeSpan? maxRuntime = null)
    {
        if (pluginManager is not PluginManager pm)
        {
            return pluginManager;
        }
        
        var plugin = new RestoreRequiredModulesPlugin(pm.Resolve<IGeneralFileWriter>(), maxRuntime ?? TimeSpan.FromMinutes(15));
        pluginManager.RegisterBeforePipelinePlugin(plugin);

        return pluginManager;
    }
}