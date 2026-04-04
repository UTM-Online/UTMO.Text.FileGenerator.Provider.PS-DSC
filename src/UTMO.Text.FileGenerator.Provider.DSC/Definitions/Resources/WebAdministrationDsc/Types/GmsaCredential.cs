namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Types;

using System.Text.RegularExpressions;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

/// <summary>
/// Represents a gMSA identity rendered as a PSCredential with an empty secure string password.
/// </summary>
public sealed partial class GmsaCredential : IPowerShellExpression
{
    public GmsaCredential(string accountName)
    {
        if (string.IsNullOrWhiteSpace(accountName))
        {
            throw new ArgumentException("A gMSA account name is required.", nameof(accountName));
        }

        var normalizedAccountName = accountName.Trim();

        if (!GmsaAccountNamePattern().IsMatch(normalizedAccountName))
        {
            throw new ArgumentException("gMSA account names must be provided as 'account$' or 'domain\\account$'.", nameof(accountName));
        }

        this.AccountName = normalizedAccountName;
    }

    public string AccountName { get; }

    public static GmsaCredential Create(string accountName) => new(accountName);

    public string ToPowerShell() => $"[PSCredential]::new('{EscapePowerShellSingleQuotedString(this.AccountName)}', [System.Security.SecureString]::new())";

    public override string ToString() => this.AccountName;

    private static string EscapePowerShellSingleQuotedString(string value) => value.Replace("'", "''", StringComparison.Ordinal);

    [GeneratedRegex(@"^(?:[^\\/:*?\""<>|]+\\)?[^\\/:*?\""<>|]+\$$", RegexOptions.CultureInvariant)]
    private static partial Regex GmsaAccountNamePattern();
}
