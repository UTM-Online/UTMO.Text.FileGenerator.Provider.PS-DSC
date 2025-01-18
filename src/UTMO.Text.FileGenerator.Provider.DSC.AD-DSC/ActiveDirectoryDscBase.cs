namespace UTMO.Text.FileGenerator.Provider.DSC.AD_DSC;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

public abstract class ActiveDirectoryDscBase : DscConfigurationItem
{
    protected ActiveDirectoryDscBase(string name) : base(name)
    {
    }
    
    public sealed override RequiredModule SourceModule => ActiveDirectoryDsc.Instance;
}