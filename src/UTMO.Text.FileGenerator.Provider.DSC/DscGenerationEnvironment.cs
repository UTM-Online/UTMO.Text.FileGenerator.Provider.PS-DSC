namespace UTMO.Text.FileGenerator.Provider.DSC;

public sealed class DscGenerationEnvironment : DscGenerationEnvironmentBase
{
    private DscGenerationEnvironment(string templatePath) : base(templatePath)
    {
    }
    
    public static DscGenerationEnvironment Create(string templatePath)
    {
        return new DscGenerationEnvironment(templatePath);
    }
}