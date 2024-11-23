namespace UTMO.Text.FileGenerator.Provider.DSC.Plugins.GenerateMofFiles;

using UTMO.Text.FileGenerator.Abstract;

public static class GenerateMofFilesPluginHook
{
    public static IRegisterPluginManager UseGenerateMofFilesPlugin(this IRegisterPluginManager pluginManager, string outputPath)
    {
        if (pluginManager is not IPluginManager pm)
        {
            return pluginManager;
        }

        var plugin = new GenerateMofFilesPlugin(pm.Resolve<IGeneralFileWriter>(), outputPath);
        pluginManager.RegisterAfterRenderPlugin(plugin);

        return pluginManager;
    }
}