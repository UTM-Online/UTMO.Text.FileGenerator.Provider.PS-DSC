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
using System.Linq;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Attributes;
using System.Reflection;

public class DscConfigurationPropertyBag : ILiquidizable
{
    private readonly Dictionary<string,object> _propertyBag = new();
    private readonly HashSet<string> _unquotedEnumKeys = new();
    // Track keys whose values originated from enum-typed properties
    private readonly HashSet<string> _enumKeys = new();

    private ILogger<DscConfigurationPropertyBag>? Logger { get; set; }
    private Type? _ownerType;

    internal void SetLogger(ILogger<DscConfigurationPropertyBag> logger)
    {
        this.Logger = logger;
    }

    internal void SetOwner(Type ownerType)
    {
        this._ownerType = ownerType;
    }
    
    public void Set<T>(string key, T value, [CallerMemberName] string? callerMemberName = null)
    {
        if (value is null)
        {
            return;
        }

        // Treat both enum and nullable enum values uniformly via runtime check
        if (value is Enum)
        {
            this._propertyBag[key] = value.ToString()!;
            this._enumKeys.Add(key);

            // Always check for UnquotedEnum attribute, even if callerMemberName is null
            // The CheckAndMarkUnquotedEnum method can handle null propertyName and will use the key as fallback
            this.CheckAndMarkUnquotedEnum(callerMemberName, key);
            return;
        }

        switch (typeof(T))
        {
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
    
    private void CheckAndMarkUnquotedEnum(string? propertyName, string key)
    {
        if (string.IsNullOrWhiteSpace(propertyName) && string.IsNullOrWhiteSpace(key))
        {
            return;
        }
        
        try
        {
            // Prefer using the known owner type to resolve the property
            if (this._ownerType != null)
            {
                foreach (var candidate in new[] { propertyName, key }.Where(n => !string.IsNullOrWhiteSpace(n)))
                {
                    var propInfo = this._ownerType.GetProperty(
                        candidate!,
                        BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
                    if (propInfo != null)
                    {
                        var hasUnquotedEnumAttribute = propInfo.GetCustomAttributes(typeof(UnquotedEnumAttribute), inherit: true).Any();
                        if (hasUnquotedEnumAttribute)
                        {
                            this._unquotedEnumKeys.Add(key);
                            return;
                        }
                    }
                }
            }

            // Fallback: walk the stack to infer the property from an accessor
            var stackTrace = new System.Diagnostics.StackTrace();
            for (int i = 0; i < stackTrace.FrameCount; i++)
            {
                var frame = stackTrace.GetFrame(i);
                var method = frame?.GetMethod();
                if (method == null)
                {
                    continue;
                }

                var declaringType = method.DeclaringType;
                if (declaringType == null || declaringType == typeof(DscConfigurationPropertyBag))
                {
                    continue;
                }

                // Derive property name from accessor when possible
                string? candidateName = null;
                if (method.IsSpecialName && (method.Name.StartsWith("set_") || method.Name.StartsWith("get_")))
                {
                    candidateName = method.Name.Substring(4);
                }
                else
                {
                    candidateName = propertyName;
                }

                if (string.IsNullOrWhiteSpace(candidateName))
                {
                    continue;
                }

                var fallbackProp = declaringType.GetProperty(
                    candidateName,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);

                if (fallbackProp == null)
                {
                    continue;
                }

                var hasAttr = fallbackProp.GetCustomAttributes(typeof(UnquotedEnumAttribute), inherit: true).Any();
                if (hasAttr)
                {
                    this._unquotedEnumKeys.Add(key);
                }
                break;
            }
        }
        catch
        {
            // If reflection fails, just continue without marking it as unquoted
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

            var targetType = typeof(T);
            var underlyingType = Nullable.GetUnderlyingType(targetType);
            
            // Check if T is an enum or a nullable enum
            if (targetType.IsEnum || (underlyingType != null && underlyingType.IsEnum))
            {
                return this.SafeParseEnum<T>(value.ToString()!);
            }
            
            return (T)value;
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
            var targetType = typeof(T);
            var underlyingType = Nullable.GetUnderlyingType(targetType);
            
            // If T is a nullable enum, parse to the underlying enum type first
            if (underlyingType != null && underlyingType.IsEnum)
            {
                var enumValue = Enum.Parse(underlyingType, value);
                // Boxing is necessary to convert from the underlying enum type to Nullable<TEnum>
                return (T)(object)enumValue!;
            }
            
            // T is a non-nullable enum
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
                        // If this key originated from an enum property, only skip quoting when marked as UnquotedEnum
                        if (this._enumKeys.Contains(prop.Key))
                        {
                            if (this._unquotedEnumKeys.Contains(prop.Key))
                            {
                                // Emit bare enum value when marked as unquoted
                                liquidObject[prop.Key] = s;
                            }
                            else
                            {
                                // Quote enum values by default
                                liquidObject[prop.Key] = $"\"{s}\"";
                            }
                        }
                        else
                        {
                            // Regular string property: always quote
                            liquidObject[prop.Key] = $"\"{s}\"";
                        }
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
                    // For non-string values, just pass through as-is
                    liquidObject[prop.Key] = prop.Value;
                    break;
                }
            }
        }
        
        return liquidObject;
    }
}