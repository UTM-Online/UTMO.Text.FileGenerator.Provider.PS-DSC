namespace UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

/// <summary>
/// Represents a value that should be emitted into DSC as a raw PowerShell expression.
/// </summary>
/// <remarks>
/// SECURITY WARNING: this interface bypasses normal string quoting and escaping in DSC output.
/// Implementations must only be composed from trusted, validated inputs and must never concatenate
/// or interpolate untrusted user-controlled strings.
/// Prefer vetted wrapper types (for example, <c>GmsaCredential</c>) instead of ad-hoc implementations.
/// </remarks>
public interface IPowerShellExpression
{
    /// <summary>
    /// Returns raw PowerShell that will be emitted directly into generated DSC without additional escaping.
    /// </summary>
    /// <remarks>
    /// The returned value is treated as executable PowerShell source. Do not return content built from
    /// untrusted input.
    /// </remarks>
    string ToPowerShell();
}
