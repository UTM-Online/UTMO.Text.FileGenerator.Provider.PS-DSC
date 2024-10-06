namespace UTMO.Text.FileGenerator.Provider.DSC;

using UTMO.Text.FileGenerator.Provider.DSC.Models;

public abstract class DscGenerationEnvironmentBase : GenerationEnvironmentBase
{
    protected DscGenerationEnvironmentBase(string templatePath) : base(templatePath)
    {
    }

    protected DscGenerationEnvironmentBase AddComputer<T>() where T : DscComputer, new()
    {
        this.AddResource<T>(new T());
        return this;
    }

    protected DscGenerationEnvironmentBase AddConfiguration<T>() where T : DscConfiguration, new()
    {
        this.AddResource<T>(new T());
        return this;
    }
}