namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

public interface ISqlSecureConnectionResource : IDscResourceConfig
{
    string InstanceName { get; set; }

    string Thumbprint { get; set; }

    bool? ForceEncryption { get; set; }

    string ServiceAccount { get; set; }

    bool? SuppressRestart { get; set; }

    string ServerName { get; set; }
}
