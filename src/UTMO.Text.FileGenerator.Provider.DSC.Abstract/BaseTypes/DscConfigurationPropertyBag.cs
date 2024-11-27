namespace UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

using System.Runtime.CompilerServices;
using DotLiquid;
using UTMO.Text.FileGenerator.Abstract;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Messaging;

public class DscConfigurationPropertyBag : ILiquidizable
{
    private readonly Dictionary<string,object> _propertyBag = new();

    private IGeneratorLogger Logger => PluginManager.Instance.ResolveLogger();
    
    public void Set<T>(string key, T value)
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
    
    /// <summary>
    /// Sets the value of the property bag with the specified key using the types default value.
    /// </summary>
    /// <param name="key">Property Key</param>
    public void Init<T>(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            this.Logger.Fatal(LogMessages.MandatoryPropertyBagParameterNameNull, true, 22, nameof(key));
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
            this.Logger.Fatal(LogMessages.MandatoryPropertyBagParameterNameNull, true, 22, nameof(key));
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
            this.Logger.Fatal(LogMessages.MandatoryPropertyNullException, true, 22, key, caller.Split('\\').Last().TrimEnd(".cs".ToCharArray()));
            throw;
        }
        catch (InvalidCastException ice)
        {
            var ex = new InvalidPropertyBagCastException(ice, key, caller.Split('\\').Last().TrimEnd(".cs".ToCharArray()));
            this.Logger.Fatal(ex.Message, true, 23);
            throw;
        }
        catch (Exception e)
        {
            this.Logger.Fatal(e.Message, true, 24);
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

    public object ToLiquid()
    {
        var liquidObject = new Dictionary<string, object>();

        foreach (var prop in this._propertyBag)
        {
            switch (prop.Value)
            {
                case bool b:
                {
                    liquidObject[prop.Key] = b ? "$true" : "$false";
                    break;
                }
                case string s when !string.IsNullOrWhiteSpace(s):
                {
                    liquidObject[prop.Key] = $"'{s}'";
                    break;
                }
                case string:
                {
                    continue;
                }
                case null:
                {
                    continue;
                }
                case IEnumerable<string> list:
                {
                    liquidObject[prop.Key] = $"@({string.Join(", ", list.Select(a => $"'{a}'"))})";
                    break;
                }
                case TimeSpan span:
                {
                    liquidObject[prop.Key] = $"'{span}'";
                    break;
                }
                case char ch:
                {
                    liquidObject[prop.Key] = $"'{ch}'";
                    break;
                }
                default:
                {
                    liquidObject[prop.Key] = prop.Value;
                    break;
                }
            }
        }
        
        return liquidObject;
    }
}