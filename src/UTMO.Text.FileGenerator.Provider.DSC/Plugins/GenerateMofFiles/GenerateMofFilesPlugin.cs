namespace UTMO.Text.FileGenerator.Provider.DSC.Plugins.GenerateMofFiles;

using System.Management.Automation;
using UTMO.Common.Guards;
using UTMO.Text.FileGenerator.Abstract;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Constants;

public class GenerateMofFilesPlugin : IRenderingPipelinePlugin
{
    internal GenerateMofFilesPlugin(IGeneralFileWriter writer, string outputPath)
    {
        this.Writer = writer;
        this.OutputPath = outputPath;
    }

    public void HandleTemplate(ITemplateModel model)
    {
        Guard.StringNotNull(nameof(model.ResourceTypeName), model.ResourceTypeName);

        if (model.ResourceTypeName != DscResourceTypeNames.DscConfiguration && model.ResourceTypeName != DscResourceTypeNames.DscLcmConfiguration)
        {
            Console.WriteLine($"Skipping {model.ResourceName} as it is not a DSC Configuration or LCM Configuration");
            return;
        }

        Console.WriteLine($"Beginning generate MOF file for {model.ResourceName}");

        string scriptConfig;

        try
        {
            scriptConfig = model.ProduceOutputPath(this.OutputPath);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Encountered an error while trying to produce the output path for {model.ResourceName}");
            throw;
        }

        Guard.StringNotNull(nameof(scriptConfig), scriptConfig);

        var fileName = model.ResourceName;
        var fileType = model.ResourceTypeName == DscResourceTypeNames.DscConfiguration ? "Configurations" : "Computers";

        string mofOutputFile;

        try
        {
            mofOutputFile = Path.GetFullPath($@"..\..\MOF\{fileType}\{fileName}.mof", System.Environment.CurrentDirectory);
        }
        catch (Exception)
        {
            Console.WriteLine($"Encountered an error while trying to produce the output path for {model.ResourceName}");
            throw;
        }

        Guard.StringNotNull(nameof(mofOutputFile), mofOutputFile);

        try
        {
            // launch a new instance of powershell and run the script
            using var ps = PowerShell.Create();
            var script = File.ReadAllText(scriptConfig);
            Guard.StringNotNull(nameof(script), script);
            ps.AddScript(script);
            ps.AddParameter("OutputPath", mofOutputFile);
            ps.Invoke();
        }
        catch (Exception)
        {
            Console.WriteLine($"Encountered an error while trying to generate the MOF file for {model.ResourceName}");
            throw;
        }
    }

    public IGeneralFileWriter Writer { get; init; }

    public ITemplateGenerationEnvironment Environment { get; init; } = null!;

    private string OutputPath { get; init; }
}