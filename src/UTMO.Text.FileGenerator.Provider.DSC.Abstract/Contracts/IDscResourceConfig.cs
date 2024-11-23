namespace UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Enums;

public interface IDscResourceConfig
{
    DscEnsure Ensure { get; set; }
    
    List<string> DependsOn { get; }
}