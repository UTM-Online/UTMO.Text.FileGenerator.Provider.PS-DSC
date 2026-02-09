namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc;
using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Enums;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.WebAdministrationDscConstants.WebAppPool;
public sealed class WebAppPoolResource : WebAdministrationDscBase, IWebAppPool
{
    private WebAppPoolResource(string name) : base(name) { }
    public string PoolName
    {
        get => this.PropertyBag.Get(Constants.Properties.Name);
        set => this.PropertyBag.Set(Constants.Properties.Name, value);
    }
    public AppPoolState? State
    {
        get => this.PropertyBag.Get<AppPoolState?>(Constants.Properties.State);
        set => this.PropertyBag.Set(Constants.Properties.State, value);
    }
    public bool? AutoStart
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.AutoStart);
        set => this.PropertyBag.Set(Constants.Properties.AutoStart, value);
    }
    public bool? Enable32BitAppOnWin64
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.Enable32BitAppOnWin64);
        set => this.PropertyBag.Set(Constants.Properties.Enable32BitAppOnWin64, value);
    }
    public ManagedPipelineMode? ManagedPipelineMode
    {
        get => this.PropertyBag.Get<ManagedPipelineMode?>(Constants.Properties.ManagedPipelineMode);
        set => this.PropertyBag.Set(Constants.Properties.ManagedPipelineMode, value);
    }
    public string? ManagedRuntimeVersion
    {
        get => this.PropertyBag.Get(Constants.Properties.ManagedRuntimeVersion);
        set => this.PropertyBag.Set(Constants.Properties.ManagedRuntimeVersion, value);
    }
    public AppPoolStartMode? StartMode
    {
        get => this.PropertyBag.Get<AppPoolStartMode?>(Constants.Properties.StartMode);
        set => this.PropertyBag.Set(Constants.Properties.StartMode, value);
    }
    public AppPoolIdentityType? IdentityType
    {
        get => this.PropertyBag.Get<AppPoolIdentityType?>(Constants.Properties.IdentityType);
        set => this.PropertyBag.Set(Constants.Properties.IdentityType, value);
    }
    public string? Credential
    {
        get => this.PropertyBag.Get(Constants.Properties.Credential);
        set => this.PropertyBag.Set(Constants.Properties.Credential, value);
    }
    public string? IdleTimeout
    {
        get => this.PropertyBag.Get(Constants.Properties.IdleTimeout);
        set => this.PropertyBag.Set(Constants.Properties.IdleTimeout, value);
    }
    public IdleTimeoutAction? IdleTimeoutAction
    {
        get => this.PropertyBag.Get<IdleTimeoutAction?>(Constants.Properties.IdleTimeoutAction);
        set => this.PropertyBag.Set(Constants.Properties.IdleTimeoutAction, value);
    }
    public uint? MaxProcesses
    {
        get => this.PropertyBag.Get<uint?>(Constants.Properties.MaxProcesses);
        set => this.PropertyBag.Set(Constants.Properties.MaxProcesses, value);
    }
    public bool? RapidFailProtection
    {
        get => this.PropertyBag.Get<bool?>(Constants.Properties.RapidFailProtection);
        set => this.PropertyBag.Set(Constants.Properties.RapidFailProtection, value);
    }
    public string? RestartTimeLimit
    {
        get => this.PropertyBag.Get(Constants.Properties.RestartTimeLimit);
        set => this.PropertyBag.Set(Constants.Properties.RestartTimeLimit, value);
    }
    public string[]? RestartSchedule
    {
        get => this.PropertyBag.Get<string[]?>(Constants.Properties.RestartSchedule);
        set => this.PropertyBag.Set(Constants.Properties.RestartSchedule, value);
    }
    public static WebAppPoolResource Create(string name, Action<IWebAppPool> configure)
    {
        var resource = new WebAppPoolResource(name);
        configure(resource);
        return resource;
    }
    public override Task<List<ValidationFailedException>> Validate()
    {
        var errors = this.ValidationBuilder()
            .ValidateStringNotNullOrEmpty(this.PoolName, nameof(this.PoolName))
            .errors;
        return Task.FromResult(errors);
    }
    public override string ResourceId => Constants.ResourceId;
}
