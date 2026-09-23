namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration;

using UTMO.Text.FileGenerator.Abstract;
using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.BaseDefinitions;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.PSDesiredStateConfiguration.Enums;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Constants.PSDesiredStateConfigurationConstants.Registry;

public class RegistryResource : PSDesiredStateConfigurationBase, IRegistryResource
{
    private RegistryResource(string name) : base(name)
    {
    }

    public string Key
    {
        get => this.PropertyBag.Get(Constants.Properties.Key);

        set => this.PropertyBag.Set(Constants.Properties.Key, value);
    }

    public string ValueName
    {
        get => this.PropertyBag.Get(Constants.Properties.ValueName);

        set => this.PropertyBag.Set(Constants.Properties.ValueName, value);
    }

    public string ValueData
    {
        get => this.PropertyBag.Get(Constants.Properties.ValueData);

        set => this.PropertyBag.Set(Constants.Properties.ValueData, value);
    }

    public RegistryValueType ValueType
    {
        get => this.PropertyBag.Get<RegistryValueType>(Constants.Properties.ValueType);

        set => this.PropertyBag.Set(Constants.Properties.ValueType, value);
    }

    public bool Force
    {
        get => this.PropertyBag.Get<bool>(Constants.Properties.Force);
        set => this.PropertyBag.Set(Constants.Properties.Force, value);
    }

    public bool Hex
    {
        get => this.PropertyBag.Get<bool>(Constants.Properties.Hex);
        set
        {
            this.PropertyBag.Set(Constants.Properties.Hex, value);

            if (!value)
            {
                return;
            }

            var liquidBag = this.PropertyBag.ToLiquid() as Dictionary<string, object>;
            var hasExplicitValueType = liquidBag is not null && liquidBag.ContainsKey(Constants.Properties.ValueType);

            if (!hasExplicitValueType)
            {
                this.PropertyBag.Set(Constants.Properties.ValueType, RegistryValueType.DWord);
            }
        }
    }

    public static RegistryResource Create(string name, Action<IRegistryResource> configure)
    {
        var resource = new RegistryResource(name);
        configure(resource);
        return resource;
    }

    public static RegistryResource Create(string name, Action<IRegistryResource> configure, out RegistryResource resource)
    {
        resource = new RegistryResource(name);
        configure(resource);
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var errors = this.ValidationBuilder()
            .ValidateStringNotNullOrEmpty(this.Key, nameof(this.Key))
            .ValidateStringNotNullOrEmpty(this.ValueName, nameof(this.ValueName))
            .errors;

        if (this.Hex && !RegistryResource.IsCompatibleHexValueType(this.ValueType))
        {
            errors.Add(new ValidationFailedException(
                Constants.Properties.ValueType,
                nameof(RegistryResource),
                ValidationFailureType.InvalidConfiguration,
                $"Hex is only valid when {nameof(this.ValueType)} is {RegistryValueType.DWord} or {RegistryValueType.QWord}."));
        }

        return Task.FromResult(errors);
    }

    private static bool IsCompatibleHexValueType(RegistryValueType valueType)
    {
        return valueType is RegistryValueType.DWord or RegistryValueType.QWord;
    }

    public override string ResourceId => Constants.ResourceId;
}
