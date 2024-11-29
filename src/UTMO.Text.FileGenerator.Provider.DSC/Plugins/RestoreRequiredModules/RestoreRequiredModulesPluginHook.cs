namespace UTMO.Text.FileGenerator.Provider.DSC.Plugins.RestoreRequiredModules;

using UTMO.Text.FileGenerator.Abstract;

public static class RestoreRequiredModulesPluginHook
{
    public static PluginManager UseRestoreRequiredModulesPlugin(this PluginManager pluginManager, TimeSpan? maxRuntime = null)
    {
        var plugin = new RestoreRequiredModulesPlugin(pluginManager.Resolve<IGeneralFileWriter>(), maxRuntime ?? TimeSpan.FromMinutes(15));
        pluginManager.RegisterBeforePipelinePlugin(plugin);

        return pluginManager;
    }
}