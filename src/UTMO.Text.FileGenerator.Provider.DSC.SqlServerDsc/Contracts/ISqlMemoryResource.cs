namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

public interface ISqlMemoryResource : IDscResourceConfig
{
    string InstanceName { get; set; }

    string ServerName { get; set; }

    bool? DynamicAlloc { get; set; }

    int? MinMemory { get; set; }

    int? MaxMemory { get; set; }

    int? MinMemoryPercent { get; set; }

    int? MaxMemoryPercent { get; set; }

    bool? ProcessOnlyOnActiveNode { get; set; }
}
