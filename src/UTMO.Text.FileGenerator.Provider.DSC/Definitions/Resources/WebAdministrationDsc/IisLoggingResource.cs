namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc;
using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Enums;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.WebAdministrationDscConstants.IisLogging;
public sealed class IisLoggingResource : WebAdministrationDscBase, IIisLogging
{
    private IisLoggingResource(string name) : base(name) { }
    public string LogPath
    {
        get => this.PropertyBag.Get(Constants.Properties.LogPath);
        set => this.PropertyBag.Set(Constants.Properties.LogPath, value);
    }
    public LogFlags[]? LogFlags
    {
        get => this.PropertyBag.Get<LogFlags[]?>(Constants.Properties.LogFlags);
        set => this.PropertyBag.Set(Constants.Properties.LogFlags, value);
    }
    public LogPeriod? LogPeriod
    {
        get => this.PropertyBag.Get<LogPeriod?>(Constants.Properties.LogPeriod);
        set => this.PropertyBag.Set(Constants.Properties.LogPeriod, value);
    }
    public string? LogTruncateSize
    {
        get => this.PropertyBag.Get(Constants.Properties.LogTruncateSize);
        set => this.PropertyBag.Set(Constants.Properties.LogTruncateSize, value);
    }
    public bool? LoglocalTimeRollover
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.LoglocalTimeRollover);
        set => this.PropertyBag.Set(Constants.Properties.LoglocalTimeRollover, value);
    }
    public LogFormat? LogFormat
    {
        get => this.PropertyBag.Get<LogFormat?>(Constants.Properties.LogFormat);
        set => this.PropertyBag.Set(Constants.Properties.LogFormat, value);
    }
    public string? LogTargetW3C
    {
        get => this.PropertyBag.Get(Constants.Properties.LogTargetW3C);
        set => this.PropertyBag.Set(Constants.Properties.LogTargetW3C, value);
    }
    public static IisLoggingResource Create(string name, Action<IIisLogging> configure)
    {
        var resource = new IisLoggingResource(name);
        configure(resource);
        return resource;
    }
    public override Task<List<ValidationFailedException>> Validate()
    {
        var errors = this.ValidationBuilder()
            .ValidateStringNotNullOrEmpty(this.LogPath, nameof(this.LogPath))
            .errors;
        return Task.FromResult(errors);
    }
    public override string ResourceId => Constants.ResourceId;
}
