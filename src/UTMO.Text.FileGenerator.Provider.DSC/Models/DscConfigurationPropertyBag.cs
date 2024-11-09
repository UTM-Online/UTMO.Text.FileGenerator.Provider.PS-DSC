namespace UTMO.Text.FileGenerator.Provider.DSC.Models;

using System.Diagnostics.CodeAnalysis;
using DotLiquid;

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
    
    public T Get<T>(string key)
    {
        if (!this._propertyBag.TryGetValue(key, out var value))
        {
            return default!;
        }

        return typeof(T) switch
                 {
                     {IsEnum: true} t => this.SafeParseEnum<T>(value.ToString()!),
                     var _            => (T) value,
                 };
    }
    
    private T SafeParseEnum<T>([NotNull]  string value)
    {
        try
        {
            return (T) Enum.Parse(typeof(T), value);
        }
        catch (Exception e)
        {
            return default!;
        }
    }
    
    public string Get(string key)
    {
        return this._propertyBag.TryGetValue(key, out var value) ? value.ToString()! : string.Empty;
    }

    public object ToLiquid()
    {
        return Hash.FromDictionary(this._propertyBag);
    }
}