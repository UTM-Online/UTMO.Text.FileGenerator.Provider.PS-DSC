namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Types;

using System.Text.RegularExpressions;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

/// <summary>
/// Represents a gMSA identity rendered as a PSCredential with an empty secure string password.
/// DSC compilation still treats this as credential material, so configurations must permit
/// plaintext passwords when this wrapper is used.
/// </summary>
public sealed partial class GmsaCredential : IPowerShellExpression, IRequiresPlainTextPassword
{
    public GmsaCredential(string accountName)
    {
        if (string.IsNullOrWhiteSpace(accountName))
        {
            throw new ArgumentException("A gMSA account name is required.", nameof(accountName));
        }

        if (ContainsWhitespaceOrControlCharacters(accountName))
        {
            throw new ArgumentException("gMSA account names cannot contain whitespace or control characters.", nameof(accountName));
        }

        if (!GmsaAccountNamePattern().IsMatch(accountName))
        {
            throw new ArgumentException("gMSA account names must be provided as 'account$' or 'domain\\account$' using SAM-compatible characters.", nameof(accountName));
        }

        this.AccountName = accountName;
    }

    public string AccountName { get; }

    public static GmsaCredential Create(string accountName) => new(accountName);

    public string ToPowerShell() => $"[PSCredential]::new('{EscapePowerShellSingleQuotedString(this.AccountName)}', [System.Security.SecureString]::new())";

    public override string ToString() => this.AccountName;

    private static string EscapePowerShellSingleQuotedString(string value) => value.Replace("'", "''", StringComparison.Ordinal);

    private static bool ContainsWhitespaceOrControlCharacters(string value)
    {
        foreach (var c in value)
        {
            if (char.IsWhiteSpace(c) || char.IsControl(c))
            {
                return true;
            }
        }

        return false;
    }

    [GeneratedRegex(@"^(?:(?:[A-Za-z0-9][A-Za-z0-9._-]*)\\)?[A-Za-z0-9][A-Za-z0-9._'-]*\$$", RegexOptions.CultureInvariant)]
    private static partial Regex GmsaAccountNamePattern();
}
