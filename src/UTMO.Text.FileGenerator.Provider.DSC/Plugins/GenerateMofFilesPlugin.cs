namespace UTMO.Text.FileGenerator.Provider.DSC.Plugins;

using System.Management.Automation;
using System.Management.Automation.Runspaces;
using UTMO.Text.FileGenerator.Abstract;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

public class GenerateMofFilesPlugin : IRenderingPipelinePlugin
{
    public GenerateMofFilesPlugin(IGeneralFileWriter writer, ITemplateGenerationEnvironment environment)
    {
        this.Writer = writer;
        this.Environment = environment;
    }

    public void HandleTemplate(ITemplateModel model)
    {
        var scriptConfig  = model.ProduceOutputPath(this.Environment.OutputPath);
        var fileName      = model.ResourceName;
        var fileType      = model.GetType() == typeof(DscLcmConfiguration) ? "Configuration" : "Computers";
        var mofOutputFile = Path.GetFullPath(Path.Join(scriptConfig, @"..\..", "MOF", fileType, $"{fileName}.mof"));

        using var ps = PowerShell.Create(InitialSessionState.CreateDefault2());
        ps.AddCommand($"{scriptConfig} -OutputPath {mofOutputFile}");
        ps.Invoke();
    }

    public IGeneralFileWriter Writer { get; init; }

    public ITemplateGenerationEnvironment Environment { get; init; }
}