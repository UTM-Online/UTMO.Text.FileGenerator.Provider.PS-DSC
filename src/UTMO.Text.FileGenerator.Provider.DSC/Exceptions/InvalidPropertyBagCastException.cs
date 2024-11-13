namespace UTMO.Text.FileGenerator.Provider.DSC.Exceptions;

using System.Text.RegularExpressions;

public class InvalidPropertyBagCastException : ApplicationException
{
    public InvalidPropertyBagCastException(InvalidCastException ice, string propertyName, string resourceId) : base($"Encountered an attempt to perform an invalid cast from '{MessageTypeParser.Match(ice.Message).Groups["Source"].Value} to '{MessageTypeParser.Match(ice.Message).Groups["Destination"].Value} access property \"{propertyName}\" of resource type {resourceId}")
    {
        this.SourceTypeName = MessageTypeParser.Match(ice.Message).Groups["Source"].Value;
        this.DestinationTypeName = MessageTypeParser.Match(ice.Message).Groups["Destination"].Value;
        this.ResourceTypeName = resourceId;
        this.PropertyName = propertyName;
    }

    public string SourceTypeName { get; }
    
    public string DestinationTypeName { get; }
    
    public string ResourceTypeName { get; }
    
    public string PropertyName { get; }

    private static Regex MessageTypeParser => new(@"System\.InvalidCastException: Unable to cast object of type '(?<Source>\w*\.\w*?)' to type '(?<Destination>\w*\.\w*?)'.", RegexOptions.Compiled);
}