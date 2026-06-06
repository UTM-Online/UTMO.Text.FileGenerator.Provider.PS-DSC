namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

public interface ISqlMaxDopResource : IDscResourceConfig
{
    bool? DynamicAlloc { get; set; }

    int? MaxDop { get; set; }

    string ServerName { get; set; }

    string InstanceName { get; set; }

    bool? ProcessOnlyOnActiveNode { get; set; }
}
