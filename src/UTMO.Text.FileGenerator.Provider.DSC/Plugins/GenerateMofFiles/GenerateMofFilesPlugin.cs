namespace UTMO.Text.FileGenerator.Provider.DSC.Plugins.GenerateMofFiles;

using System.Management.Automation;
using UTMO.Common.Guards;
using UTMO.Text.FileGenerator.Abstract;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Constants;

public class GenerateMofFilesPlugin : IRenderingPipelinePlugin
{
    public GenerateMofFilesPlugin(IGeneralFileWriter writer, ITemplateGenerationEnvironment environment)
    {
        this.Writer = writer;
        this.Environment = environment;
    }

    public void HandleTemplate(ITemplateModel model)
    {
        Guard.StringNotNull(nameof(model.ResourceTypeName), model.ResourceTypeName);
        Guard.StringNotNull("Environment.OutputPath", this.Environment.OutputPath);

        if (model.ResourceTypeName != DscResourceTypeNames.DscConfiguration && model.ResourceTypeName != DscResourceTypeNames.DscLcmConfiguration)
        {
            Console.WriteLine($"Skipping {model.ResourceName} as it is not a DSC Configuration or LCM Configuration");
            return;
        }

        Console.WriteLine($"Beginning generate MOF file for {model.ResourceName}");

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

        Guard.StringNotNull(nameof(scriptConfig), scriptConfig);

        var fileName = model.ResourceName;
        var fileType = model.ResourceTypeName == DscResourceTypeNames.DscConfiguration ? "Configurations" : "Computers";

        string mofOutputFile;

        try
        {
            mofOutputFile = Path.GetFullPath(Path.Join(scriptConfig, @"..\..", "MOF", fileType, $"{fileName}.mof"));
        }
        catch (Exception e)
        {
            Console.WriteLine($"Encountered an error while trying to produce the output path for {model.ResourceName}");
            throw;
        }

        Guard.StringNotNull(nameof(mofOutputFile), mofOutputFile);
        var instructionCounter = 0;

        try
        {
            using var ps = PowerShell.Create(RunspaceMode.NewRunspace); // 0
            instructionCounter++;
            ps.AddScript(scriptConfig); // 1
            instructionCounter++;
            ps.AddParameter("OutputPath", mofOutputFile); // 2
            instructionCounter++;
            ps.Invoke(); // 3
        }
        catch (Exception e)
        {
            Console.WriteLine($"Encountered an error while trying to generate the MOF file for {model.ResourceName} at instruction {instructionCounter}");
            throw;
        }
    }

    public IGeneralFileWriter Writer { get; init; }

    public ITemplateGenerationEnvironment Environment { get; init; }
}