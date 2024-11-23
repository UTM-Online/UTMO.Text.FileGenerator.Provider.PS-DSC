namespace UTMO.Text.FileGenerator.Provider.DSC.Plugins.GenerateMofFiles;

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
        if (model.GetType() != typeof(DscLcmConfiguration) && model.GetType() != typeof(DscConfiguration))
        {
            Console.WriteLine($"Skipping {model.ResourceName} as it is not a DSC Configuration or LCM Configuration");
            return;
        }
        else
        {
            Console.WriteLine($"Generating MOF file for {model.ResourceName}");
        }
        
        string scriptConfig;

        try
        {
            scriptConfig = model.ProduceOutputPath(this.Environment.OutputPath);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Encountered an error while trying to produce the output path for {model.ResourceName}");
            throw;
        }
        
        var    fileName      = model.ResourceName;
        var    fileType      = model.GetType() == typeof(DscLcmConfiguration) ? "Configuration" : "Computers";
        string    mofOutputFile;
        
        try
        {
            mofOutputFile = Path.GetFullPath(Path.Join(scriptConfig, @"..\..", "MOF", fileType, $"{fileName}.mof"));
        }
        catch (Exception e)
        {
            Console.WriteLine($"Encountered an error while trying to produce the output path for {model.ResourceName}");
            throw;
        }

        try
        {
            using var ps = PowerShell.Create(InitialSessionState.CreateDefault2());
            ps.AddCommand($"{scriptConfig} -OutputPath {mofOutputFile}");
            ps.Invoke();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Encountered an error while trying to generate the MOF file for {model.ResourceName}");
            throw;
        }
    }

    public IGeneralFileWriter Writer { get; init; }

    public ITemplateGenerationEnvironment Environment { get; init; }
}