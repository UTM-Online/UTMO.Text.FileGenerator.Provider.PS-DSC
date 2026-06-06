namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

public interface ISqlAliasResource : IDscResourceConfig
{
    string AliasName { get; set; }

    string Protocol { get; set; }

    string ServerName { get; set; }

    ushort? TcpPort { get; set; }

    bool? UseDynamicTcpPort { get; set; }
}
