namespace UTMO.Text.FileGenerator.Provider.DSC.Abstract.Exceptions;

public class MandatoryParameterNullException : ApplicationException
{
    public MandatoryParameterNullException(string propertyName, string resourceId) : base($"Mandatory parameter '{propertyName}' is null for resource '{resourceId}'")
    {
    }
}