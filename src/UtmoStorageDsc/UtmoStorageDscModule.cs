namespace UTMO.Text.FileGenerator.Provider.DSC.UtmoStorage;

using Abstract.BaseTypes;

public class UtmoStorageDsc : RequiredModule
{
    private UtmoStorageDsc()
    {
    }

    public override string ModuleName => "UtmoStorageDsc";

    public override string ModuleVersion => "0.1.3";

    public static RequiredModule Instance { get; } = new UtmoStorageDsc();
}
