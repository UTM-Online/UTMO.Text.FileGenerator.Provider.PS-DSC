namespace UTMO.Text.FileGenerator.Provider.DSC.Abstract.Attributes;

/// <summary>
/// Marks a DSC resource property as a PowerShell script block.
/// When applied, the property value will be rendered wrapped in curly braces { } 
/// rather than double quotes, producing valid DSC script block syntax.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class ScriptBlockAttribute : Attribute
{
}

