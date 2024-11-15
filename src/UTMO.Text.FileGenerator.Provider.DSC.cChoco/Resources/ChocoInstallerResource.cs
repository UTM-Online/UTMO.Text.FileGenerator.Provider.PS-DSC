namespace UTMO.Text.FileGenerator.Provider.DSC.cChoco.Resources;

using Constants = UTMO.Text.FileGenerator.Provider.DSC.cChoco.cChocoConstants.ChocoInstaller;

public class ChocoInstallerResource : cChocoBase
{
    private ChocoInstallerResource(string name) : base(name)
    {
        this.PropertyBag.Init(Constants.Parameters.InstallDirectory);
        this.PropertyBag.Init(Constants.Parameters.ChocoInstallScriptUrl);
    }

    public string InstallDirectory
    {
        get => this.PropertyBag.Get(Constants.Parameters.InstallDirectory);
        set => this.PropertyBag.Set(Constants.Parameters.InstallDirectory, value);
    }
    
    public string ChocoInstallScriptUrl
    {
        get => this.PropertyBag.Get(Constants.Parameters.ChocoInstallScriptUrl);
        set => this.PropertyBag.Set(Constants.Parameters.ChocoInstallScriptUrl, value);
    }
    
    public static ChocoInstallerResource Create(string name, Action<ChocoInstallerResource> action)
    {
        var resource = new ChocoInstallerResource(name);
        action(resource);
        return resource;
    }
    
    public static ChocoInstallerResource Create(string name, Action<ChocoInstallerResource> action, out ChocoInstallerResource resource)
    {
        resource = new ChocoInstallerResource(name);
        action(resource);
        return resource;
    }

    public override string ResourceId => Constants.ResourceId;
}