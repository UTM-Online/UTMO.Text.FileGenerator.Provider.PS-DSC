namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

public class SqlServerDsc : RequiredModule
{
    private SqlServerDsc() { }

    public override string ModuleName => "SqlServerDsc";
    public override string ModuleVersion => "17.5.1";

    public static RequiredModule Instance { get; } = new SqlServerDsc();
}
