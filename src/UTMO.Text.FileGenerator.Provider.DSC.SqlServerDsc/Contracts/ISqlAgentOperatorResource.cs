namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

public interface ISqlAgentOperatorResource : IDscResourceConfig
{
    string OperatorName { get; set; }

    string ServerName { get; set; }

    string InstanceName { get; set; }

    string EmailAddress { get; set; }
}
