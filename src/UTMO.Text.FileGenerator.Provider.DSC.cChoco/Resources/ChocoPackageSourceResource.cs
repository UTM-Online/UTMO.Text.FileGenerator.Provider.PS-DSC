namespace UTMO.Text.FileGenerator.Provider.DSC.cChoco.Resources;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.cChoco.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.cChoco.cChocoConstants.ChocoPackageSource;

public class ChocoPackageSourceResource : cChocoBase, IChocoPackageSourceResource
{
    private ChocoPackageSourceResource(string name) : base(name)
    {
    }

    public string SourceName
    {
        get => this.PropertyBag.Get(Constants.Parameters.Name);
        
        set => this.PropertyBag.Set(Constants.Parameters.Name, value);
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

    public override Task<List<ValidationFailedException>> Validate()
    {
        var errors = this.ValidationBuilder()
                             .ValidateStringNotNullOrEmpty(this.SourceName, nameof(this.SourceName)).errors;
        
        return Task.FromResult(errors);
    }

    public override string ResourceId => Constants.ResourceId;
    
    public override bool HasEnsure => true;
}