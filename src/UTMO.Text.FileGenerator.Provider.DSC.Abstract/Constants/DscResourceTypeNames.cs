namespace UTMO.Text.FileGenerator.Provider.DSC.Abstract.Constants;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

public static class DscResourceTypeNames
{
    public const string DscConfiguration = "/DSC/Configurations";
    
    public const string DscLcmConfiguration = "/DSC/Computers";
    
    public static string DscComputerConfiguration => DscConfiguration;
}