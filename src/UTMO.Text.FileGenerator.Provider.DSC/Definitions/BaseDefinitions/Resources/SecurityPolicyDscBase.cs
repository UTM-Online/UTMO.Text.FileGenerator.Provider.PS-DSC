namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;

using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Modules;
using UTMO.Text.FileGenerator.Provider.DSC.Models;
using UTMO.Text.FileGenerator.Provider.DSC.SubResources;

public abstract class SecurityPolicyDscBase : DscConfigurationItem
{
    protected SecurityPolicyDscBase(string name) : base(name)
    {
    }

    public override RequiredModule SourceModule => SecurityPolicyDsc.Instance;
}