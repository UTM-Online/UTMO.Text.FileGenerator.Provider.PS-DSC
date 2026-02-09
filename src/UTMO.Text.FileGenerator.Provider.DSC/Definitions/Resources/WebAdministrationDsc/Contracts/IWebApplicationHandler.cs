namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Enums;
public interface IWebApplicationHandler : IDscResourceConfig
{
    string HandlerName { get; set; }
    string? PhysicalHandlerPath { get; set; }
    string? Verb { get; set; }
    string[] Path { get; set; }
    string? Type { get; set; }
    string? Modules { get; set; }
    string? ScriptProcessor { get; set; }
    string? PreCondition { get; set; }
    RequireAccess? RequireAccess { get; set; }
    string? ResourceType { get; set; }
    bool? AllowPathInfo { get; set; }
    uint? ResponseBufferLimit { get; set; }
    string? Location { get; set; }
}
