namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.BaseDefinitions;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.ModuleDefinitions;

public abstract class ComputerManagementDscBase : DscConfigurationItem
{
    protected ComputerManagementDscBase(string name) : base(name)
    {
    }

    public sealed override RequiredModule SourceModule => ComputerManagementDsc.Instance;
}