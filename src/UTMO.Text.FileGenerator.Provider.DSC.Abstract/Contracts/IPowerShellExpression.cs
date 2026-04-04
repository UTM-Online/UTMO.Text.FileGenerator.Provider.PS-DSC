namespace UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

/// <summary>
/// Represents a value that should be emitted into DSC as a raw PowerShell expression.
/// </summary>
public interface IPowerShellExpression
{
    string ToPowerShell();
}
