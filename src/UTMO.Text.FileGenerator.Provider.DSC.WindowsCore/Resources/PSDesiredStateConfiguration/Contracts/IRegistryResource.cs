namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration.Enums;

public interface IRegistryResource : IDscResourceConfig
{
    string Key { get; set; }

    string ValueName { get; set; }

    string ValueData { get; set; }

    RegistryValueType ValueType { get; set; }
}