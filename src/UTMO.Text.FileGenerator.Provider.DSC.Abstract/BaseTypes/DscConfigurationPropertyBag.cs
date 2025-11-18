namespace UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using DotLiquid;
using Microsoft.Extensions.Logging;
using Utils;
using UTMO.Text.FileGenerator.Abstract;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Constants;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Messaging;

public class DscConfigurationPropertyBag : ILiquidizable
{
    private readonly Dictionary<string,object> _propertyBag = new();
    private readonly HashSet<string> _quotedEnumKeys = new();

    private ILogger<DscConfigurationPropertyBag>? Logger { get; set; }
    
    internal void SetLogger(ILogger<DscConfigurationPropertyBag> logger)
    {
        this.Logger = logger;
    }
    
    public void Set<T>(string key, T value, [CallerMemberName] string? callerMemberName = null)
    {
        if (value is null)
        {
            return;
        }

        switch (typeof(T))
        {
            case {IsEnum: true}:
            {
                this._propertyBag[key] = value.ToString()!;
                
                // Check if the calling property has the QuotedEnum attribute
                if (!string.IsNullOrEmpty(callerMemberName))
                {
                    this.CheckAndMarkQuotedEnum(callerMemberName, key);
                }
                break;
            }

            default:
            {
                if (this._propertyBag.TryAdd(key, value))
                {
                    return;
                }

                this._propertyBag[key] = value;
                break;
            }
        }
    }
    
    private void CheckAndMarkQuotedEnum(string propertyName, string key)
    {
        try
        {
            // Get the calling type from the stack trace
            var stackTrace = new System.Diagnostics.StackTrace();
            for (int i = 0; i < stackTrace.FrameCount; i++)
            {
                var frame = stackTrace.GetFrame(i);
                var method = frame?.GetMethod();
                if (method != null && method.DeclaringType != null && method.DeclaringType != typeof(DscConfigurationPropertyBag))
                {
                    var property = method.DeclaringType.GetProperty(propertyName);
                    if (property != null)
                    {
                        var hasQuotedEnumAttribute = property.GetCustomAttributes(typeof(Attributes.QuotedEnumAttribute), false).Any();
                        if (hasQuotedEnumAttribute)
                        {
                            this._quotedEnumKeys.Add(key);
                        }
                        break;
                    }
                }
            }
        }
        catch
        {
            // If reflection fails, just continue without marking it as quoted
        }
    }
    
    /// <summary>
    /// Sets the value of the property bag with the specified key using the types default value.
    /// </summary>
    /// <param name="key">Property Key</param>
    public void Init<T>(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            this.Logger?.Fatal(LogMessages.MandatoryPropertyBagParameterNameNull, true, 22, nameof(key));
        }

        this._propertyBag[key] = typeof(T) switch
                                 {
                                     T when typeof(T) == typeof(string) => string.Empty,
                                     var _                              => default(object)!,
                                 };
    }

    public void Init(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            this.Logger?.Fatal(LogMessages.MandatoryPropertyBagParameterNameNull, true, 22, nameof(key));
        }
        
        this._propertyBag[key] = string.Empty;
    }
    
    public T Get<T>(string key, [CallerFilePath] string caller = "")
    {
        try
        {
            if (!this._propertyBag.TryGetValue(key, out var value))
            {
                return default!;
            }

            return typeof(T) switch
            {
                { IsEnum: true } _ => this.SafeParseEnum<T>(value.ToString()!),
                var _ => (T)value,
            };
        }
        catch (NullReferenceException)
        {
            this.Logger?.Fatal(LogMessages.MandatoryPropertyNullException, true, 22, key, caller.Split('\\').Last().TrimEnd(".cs".ToCharArray()));
            throw;
        }
        catch (InvalidCastException ice)
        {
            var ex = new InvalidPropertyBagCastException(ice, key, caller.Split('\\').Last().TrimEnd(".cs".ToCharArray()));
            this.Logger?.Fatal(ex.Message, true, 23);
            throw;
        }
        catch (Exception e)
        {
            this.Logger?.Fatal(e.Message, true, 24);
            throw;
        }
    }
    
    #pragma warning disable CS8601 // Possible null reference assignment.
    private T SafeParseEnum<T>(string value, T defaultValue = default)
        #pragma warning restore CS8601 // Possible null reference assignment.
    {
        try
        {
            return (T) Enum.Parse(typeof(T), value);
        }
        catch (Exception)
        {
            return defaultValue!;
        }
    }
    
    public string Get(string key, [CallerFilePath] string caller = "")
    {
        // ReSharper disable once ExplicitCallerInfoArgument
        return this.Get<string>(key, caller);
    }

    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public object ToLiquid()
    {
        var liquidObject = new Dictionary<string, object>();

        foreach (var prop in this._propertyBag)
        {
            // If prop.Value is an array of enums of the same type, parse it to a list of strings
            if (prop.Value is IEnumerable enumerable)
            {
                var enumerator = enumerable.GetEnumerator();
                using (enumerator as IDisposable)
                {
                    if (enumerator.MoveNext() && enumerator.Current is Enum)
                    {
                        liquidObject[prop.Key] = $"@({string.Join(", ", enumerable.Cast<Enum>().Select(e => $"\"{e}\""))})";
                        continue;
                    }
                }
            }
            
            switch (prop.Value)
            {
                case bool b:
                {
                    liquidObject[prop.Key] = b ? "$true" : "$false";
                    break;
                }
                case string s:
                {
                    if (!string.IsNullOrWhiteSpace(s) && !s.Equals(PropertyBagValues.NoValue))
                    {
                        liquidObject[prop.Key] = $"\"{s}\"";
                    }
                    else if (s.Equals(PropertyBagValues.NoValue))
                    {
                        liquidObject[prop.Key] = "\"\"";
                    }

                    break;
                }
                case null:
                {
                    continue;
                }
                case IEnumerable<string> list:
                {
                    liquidObject[prop.Key] = $"@({string.Join(", ", list.Select(a => $"\"{a}\""))})";
                    break;
                }
                case TimeSpan span:
                {
                    liquidObject[prop.Key] = $"\"{span}\"";
                    break;
                }
                case char ch:
                {
                    liquidObject[prop.Key] = $"\"{ch}\"";
                    break;
                }
                default:
                {
                    // Check if this is a quoted enum value
                    if (this._quotedEnumKeys.Contains(prop.Key) && prop.Value is Enum enumValue)
                    {
                        liquidObject[prop.Key] = $"\"{enumValue}\"";
                    }
                    else
                    {
                        liquidObject[prop.Key] = prop.Value;
                    }
                    break;
                }
            }
        }
        
        return liquidObject;
    }
}