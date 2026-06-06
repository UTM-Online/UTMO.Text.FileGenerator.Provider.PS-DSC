namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Enums;

public interface ISqlDatabaseUserResource : IDscResourceConfig
{
    string UserName { get; set; }

    string InstanceName { get; set; }

    string DatabaseName { get; set; }

    string ServerName { get; set; }

    string LoginName { get; set; }

    string AsymmetricKeyName { get; set; }

    string CertificateName { get; set; }

    SqlDatabaseUserType? UserType { get; set; }

    bool? Force { get; set; }
}
