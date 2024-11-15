namespace UTMO.Text.FileGenerator.Provider.DSC.Models;

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;
using DotLiquid;
using Exceptions;

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
                { IsEnum: true } t => this.SafeParseEnum<T>(value.ToString()!),
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
    
    private T SafeParseEnum<T>(string value, T defaultValue = default)
    {
        try
        {
            return (T) Enum.Parse(typeof(T), value);
        }
        catch (Exception e)
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
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        return Hash.FromDictionary(this._propertyBag.Where(a => a.Value != default || (a.Value is string valString && !string.IsNullOrWhiteSpace(valString))).Select(a =>
                                   {
                                       if (a.Value is bool valBool)
                                       {
                                           return new KeyValuePair<string, object>(a.Key, $"${valBool.ToString().ToLower()}");
                                       }
                                       
                                       if (a.Value is string valString)
                                       {
                                           return new KeyValuePair<string, object>(a.Key, $"'{valString}'");
                                       }

                                       return a;
                                   }).ToDictionary(a => a.Key, a => a.Value));
    }
}