namespace UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

using System.Runtime.CompilerServices;
using DotLiquid;
using DotLiquid.Tags;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Exceptions;

public class DscConfigurationPropertyBag : ILiquidizable
{
    private readonly Dictionary<string,object> _propertyBag = new();
    
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
            throw new ArgumentNullException(nameof(key));
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
            throw new ArgumentNullException(nameof(key));
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
            throw new MandatoryParameterNullException(key, caller.Split('\\').Last().TrimEnd(".cs".ToCharArray()));
        }
        catch (InvalidCastException ice)
        {
            throw new InvalidPropertyBagCastException(ice, key, caller.Split('\\').Last().TrimEnd(".cs".ToCharArray()));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
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
                    continue;
                }
                case string s when string.IsNullOrWhiteSpace(s):
                {
                    continue;
                }
            }

            liquidObject[prop.Key] = prop.Value;
        }
        
        return liquidObject;
    }
}