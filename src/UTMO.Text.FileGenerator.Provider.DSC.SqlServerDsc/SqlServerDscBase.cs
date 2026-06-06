namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

public abstract class SqlServerDscBase : DscConfigurationItem
{
    protected SqlServerDscBase(string name) : base(name) { }

    public sealed override RequiredModule SourceModule => SqlServerDsc.Instance;
}
