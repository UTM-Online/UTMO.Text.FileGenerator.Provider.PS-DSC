namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

public interface ISqlScriptResource : IDscResourceConfig
{
    string Id { get; set; }

    string InstanceName { get; set; }

    string SetFilePath { get; set; }

    string GetFilePath { get; set; }

    string TestFilePath { get; set; }

    string ServerName { get; set; }

    string Credential { get; set; }

    string[] Variable { get; set; }

    bool? DisableVariables { get; set; }

    uint? QueryTimeout { get; set; }

    string Encrypt { get; set; }
}
