namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.xWebAdministration.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Enums;

public interface IxWebConfigProperty
{
    string WebsitePath { get; set; }

    string Filter { get; set; }

    string PropertyName { get; set; }

    string Value { get; set; }

    string Description { get; set; }

    DscEnsure Ensure { get; set; }

    DscConfigurationItem AddDependency<T>(T resource) where T : DscConfigurationItem;
}