namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Enums;

public interface ISqlLoginResource : IDscResourceConfig
{
    string LoginName { get; set; }

    string InstanceName { get; set; }

    string ServerName { get; set; }

    IPowerShellExpression? LoginCredential { get; set; }

    bool? LoginMustChangePassword { get; set; }

    bool? LoginPasswordExpirationEnabled { get; set; }

    bool? LoginPasswordPolicyEnforced { get; set; }

    bool? Disabled { get; set; }

    string DefaultDatabase { get; set; }

    string Language { get; set; }

    string Sid { get; set; }

    SqlLoginType? LoginType { get; set; }
}
