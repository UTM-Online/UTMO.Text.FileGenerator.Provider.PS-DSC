namespace UTMO.Text.FileGenerator.Provider.DSC.SqlServerDsc.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

public interface ISqlDatabaseMailResource : IDscResourceConfig
{
    string AccountName { get; set; }

    string InstanceName { get; set; }

    string EmailAddress { get; set; }

    string MailServerName { get; set; }

    string ProfileName { get; set; }

    string ServerName { get; set; }

    string DisplayName { get; set; }

    string ReplyToAddress { get; set; }

    string MailDescription { get; set; }

    string LoggingLevel { get; set; }

    ushort? TcpPort { get; set; }

    bool? UseDefaultCredentials { get; set; }
}
