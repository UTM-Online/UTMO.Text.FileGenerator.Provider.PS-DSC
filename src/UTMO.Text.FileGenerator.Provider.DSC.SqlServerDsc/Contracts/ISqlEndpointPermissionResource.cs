namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

public interface ISqlEndpointPermissionResource : IDscResourceConfig
{
    string InstanceName { get; set; }

    string Principal { get; set; }

    string EndpointName { get; set; }

    string ServerName { get; set; }

    string Permission { get; set; }
}
