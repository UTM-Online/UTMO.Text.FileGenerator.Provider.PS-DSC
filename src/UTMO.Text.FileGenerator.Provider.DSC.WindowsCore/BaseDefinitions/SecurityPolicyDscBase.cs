namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.BaseDefinitions;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.ModuleDefinitions;

public abstract class SecurityPolicyDscBase : DscConfigurationItem
{
    protected SecurityPolicyDscBase(string name) : base(name)
    {
    }

    public override RequiredModule SourceModule => SecurityPolicyDsc.Instance;
    
    public override bool HasEnsure => true;
}