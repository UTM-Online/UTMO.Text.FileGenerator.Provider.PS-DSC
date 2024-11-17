namespace UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Enums;

public interface IDcsResourceConfig
{
    DscEnsure Ensure { get; }
    
    List<string> DependsOn { get; }
}