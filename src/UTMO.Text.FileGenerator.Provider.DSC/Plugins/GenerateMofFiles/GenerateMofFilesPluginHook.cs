namespace UTMO.Text.FileGenerator.Provider.DSC.Plugins.GenerateMofFiles;

using UTMO.Text.FileGenerator.Abstract;

public static class GenerateMofFilesPluginHook
{
    public static IRegisterPluginManager UseGenerateMofFilesPlugin(this IRegisterPluginManager pluginManager)
    {
        if (pluginManager is IPluginManager pm)
        {
            var plugin = new GenerateMofFilesPlugin(pm.Resolve<IGeneralFileWriter>(), pm.Resolve<ITemplateGenerationEnvironment>());
            pluginManager.RegisterAfterRenderPlugin(plugin);
        }

        return pluginManager;
    }
}