namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.xWebAdministration;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.BaseDefinitions.Resources;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.xWebAdministration.Contracts;
using UTMO.Text.FileGenerator.Validators;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.Constants.xWebAdministrationConstants.xWebAppPool;

// ReSharper disable once InconsistentNaming
public class xWebAppPool : xWebAdministrationBase, IxWebAppPool
{
    private xWebAppPool(string name) : base(name)
    {
    }
    
    public string AppPoolName
    {
        get => this.PropertyBag.Get(Constants.Properties.Name);
        set => this.PropertyBag.Set(Constants.Properties.Name, value);
    }
    
    public xWebAppPoolIdentityType IdentityType
    {
        get => this.PropertyBag.Get<xWebAppPoolIdentityType>(Constants.Properties.IdentityType);
        set => this.PropertyBag.Set(Constants.Properties.IdentityType, value);
    }
    
    public static xWebAppPool Create(string name, Action<IxWebAppPool> configure)
    {
        var resource = new xWebAppPool(name);
        configure(resource);
        return resource;
    }
    
    public static xWebAppPool Create(string name, Action<IxWebAppPool> configure, out xWebAppPool resource)
    {
        resource = new xWebAppPool(name);
        configure(resource);
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var validations = this.ValidationBuilder()
                              .ValidateStringNotNullOrEmpty(this.AppPoolName, nameof(this.AppPoolName));
        
        return Task.FromResult(validations.errors);
    }

    public override string ResourceId => Constants.ResourceId;
}