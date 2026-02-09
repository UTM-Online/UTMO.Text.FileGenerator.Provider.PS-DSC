namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Models;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Enums;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Enums;
public class LogCustomField
{
    public string LogFieldName { get; set; } = string.Empty;
    public string SourceName { get; set; } = string.Empty;
    public LogCustomFieldSourceType SourceType { get; set; }
    public DscEnsure? Ensure { get; set; }
}
