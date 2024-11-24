namespace UTMO.Text.FileGenerator.Provider.DSC.Plugins.GenerateMofFiles;

using System.Diagnostics;
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
            mofOutputFile = Path.Combine(this.Environment.OutputPath, $@"MOF\{fileType}\{fileName}");
        }
        catch (Exception)
        {
            Console.WriteLine($"Encountered an error while trying to produce the output path for {model.ResourceName}");
            throw;
        }

        Guard.StringNotNull(nameof(mofOutputFile), mofOutputFile);

        string? stdErr = null;
        try
        {
            var processInfo = new ProcessStartInfo
                              {
                                  FileName = "PowerShell.exe",
                                  Arguments = $"-ExecutionPolicy Bypass -File {scriptConfig} -OutputPath {mofOutputFile}",
                                  RedirectStandardOutput = true,
                                  RedirectStandardError = true,
                                  UseShellExecute = false,
                                  CreateNoWindow = true,
                              };
            
            var     process = Process.Start(processInfo);
            var     stdOut  = process?.StandardOutput.ReadToEnd();
            stdErr = process?.StandardError.ReadToEnd();
            process?.WaitForExit();
            Console.WriteLine($"MOF file for {model.ResourceName} has been generated successfully");
        }
        catch (Exception)
        {
            Console.WriteLine($"Encountered an error while trying to generate the MOF file for {model.ResourceName}");

            if (!string.IsNullOrWhiteSpace(stdErr))
            {
                Console.WriteLine($"Standard Error: {stdErr}");
            }
            
            throw;
        }
    }

    public IGeneralFileWriter Writer { get; init; }

    public ITemplateGenerationEnvironment Environment { get; init; } = null!;

    private string OutputPath { get; init; }
}