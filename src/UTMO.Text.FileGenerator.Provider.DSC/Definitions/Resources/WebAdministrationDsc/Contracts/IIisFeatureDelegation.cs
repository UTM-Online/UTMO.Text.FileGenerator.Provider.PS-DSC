namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Enums;
public interface IIisFeatureDelegation : IDscResourceConfig
{
    string Path { get; set; }
    string Filter { get; set; }
    OverrideMode OverrideMode { get; set; }
}
