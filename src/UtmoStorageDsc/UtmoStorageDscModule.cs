namespace UTMO.Text.FileGenerator.Provider.DSC.UtmoStorage;

using Abstract.BaseTypes;

public class UtmoStorageDsc : RequiredModule
{
    private UtmoStorageDsc()
    {
    }

    public override string ModuleName => "UtmoStorageDsc";

    public override string ModuleVersion => "1.0.0";

    public static RequiredModule Instance { get; } = new UtmoStorageDsc();
}
