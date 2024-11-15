namespace UTMO.Text.FileGenerator.Provider.DSC.cChoco.Resources;

using Constants = UTMO.Text.FileGenerator.Provider.DSC.cChoco.cChocoConstants.ChocoPackageSource;

public class ChocoPackageSourceResource : cChocoBase
{
    private ChocoPackageSourceResource(string name) : base(name)
    {
        this.PropertyBag.Init(Constants.Parameters.Name);
        this.PropertyBag.Init(Constants.Parameters.Source);
    }

    public string SourceName
    {
        get => this.PropertyBag.Get(Constants.Parameters.Source);
        
        set => this.PropertyBag.Set(Constants.Parameters.Source, value);
    }
    
    public string SourceUri
    {
        get => this.PropertyBag.Get(Constants.Parameters.Source);
        
        set => this.PropertyBag.Set(Constants.Parameters.Source, value);
    }
    
    public static ChocoPackageSourceResource Create(string name, Action<ChocoPackageSourceResource> configure)
    {
        var resource = new ChocoPackageSourceResource(name);
        configure(resource);
        return resource;
    }
    
    public static ChocoPackageSourceResource Create(string name, Action<ChocoPackageSourceResource> configure,
                                                   out ChocoPackageSourceResource resourceRef)
    {
        var resource = new ChocoPackageSourceResource(name);
        configure(resource);
        resourceRef = resource;
        return resource;
    }

    public override string ResourceId => Constants.ResourceId;
}