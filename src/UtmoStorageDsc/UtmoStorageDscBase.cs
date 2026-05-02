namespace UTMO.Text.FileGenerator.Provider.DSC.UtmoStorage;

using Abstract.BaseTypes;

public abstract class UtmoStorageDscBase(string name) : DscConfigurationItem(name)
{
    public sealed override RequiredModule SourceModule => UtmoStorageDsc.Instance;
}
