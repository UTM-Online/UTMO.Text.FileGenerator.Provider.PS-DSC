namespace UTMO.Text.FileGenerator.Provider.DSC.Plugins.GenerateMofFiles;

using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
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
            mofOutputFile = $@"..\..\MOF\{fileType}\{fileName}.mof";
        }
        catch (Exception e)
        {
            Console.WriteLine($"Encountered an error while trying to produce the output path for {model.ResourceName}");
            throw;
        }

        Guard.StringNotNull(nameof(mofOutputFile), mofOutputFile);

        try
        {
            // launch a new instance of powershell and run the script
            using var runspace = RunspaceFactory.CreateRunspace();
            runspace.Open();
            using var pipeline = PowerShell.Create(RunspaceMode.NewRunspace);
            pipeline.Runspace = runspace;
            var sb = new StringBuilder();
            sb.AppendLine($"$exp = \"{scriptConfig}\" -OutputPath {mofOutputFile}");
            sb.AppendLine("Invoke-Expression $exp");
            var script = sb.ToString();
            pipeline.AddScript(script);
            pipeline.Invoke();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Encountered an error while trying to generate the MOF file for {model.ResourceName}");
            throw;
        }
    }

    public IGeneralFileWriter Writer { get; init; }

    public ITemplateGenerationEnvironment Environment { get; init; } = null!;

    private string OutputPath { get; init; }
}