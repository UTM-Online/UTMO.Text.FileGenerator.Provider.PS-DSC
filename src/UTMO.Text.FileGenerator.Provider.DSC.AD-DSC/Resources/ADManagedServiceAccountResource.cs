namespace UTMO.Text.FileGenerator.Provider.DSC.AD_DSC.Resources;

using System.Runtime.CompilerServices;
using UTMO.Text.FileGenerator.Abstract;
using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;
using UTMO.Text.FileGenerator.Provider.DSC.AD_DSC.Contracts;
using Constants = UTMO.Text.FileGenerator.Provider.DSC.AD_DSC.ActiveDirectoryDscConstants.ADManagedServiceAccount;

public class ADManagedServiceAccountResource  : ActiveDirectoryDscBase, IADManagedServiceAccountResource
{
    private ADManagedServiceAccountResource(string name) : base(name)
    {
    }

    public string AccountName
    {
        get => this.PropertyBag.Get(Constants.Parameters.ServiceAccountName);
        
        set => this.PropertyBag.Set(Constants.Parameters.ServiceAccountName, value);
    }
    
    public AdServiceAccountType AccountType
    {
        get => this.PropertyBag.Get<AdServiceAccountType>(Constants.Parameters.AccountType);
        
        set => this.PropertyBag.Set(Constants.Parameters.AccountType, value);
    }
    
    public string CommonName
    {
        get => this.PropertyBag.Get(Constants.Parameters.CommonName);
        
        set => this.PropertyBag.Set(Constants.Parameters.CommonName, value);
    }
    
    public string DisplayName
    {
        get => this.PropertyBag.Get(Constants.Parameters.DisplayName);
        
        set => this.PropertyBag.Set(Constants.Parameters.DisplayName, value);
    }
    
    public KerberosEncryptionType EncryptionType
    {
        get => this.PropertyBag.Get<KerberosEncryptionType>(Constants.Parameters.KerberosEncryptionType);
        
        set => this.PropertyBag.Set(Constants.Parameters.KerberosEncryptionType, value);
    }
    
    public bool TrustedForDelegation
    {
        get => this.PropertyBag.Get<bool>(Constants.Parameters.TrustedForDelegation);
        
        set => this.PropertyBag.Set(Constants.Parameters.TrustedForDelegation, value);
    }
    
    public string[] ManagedPasswordPrincipals
    {
        get => this.PropertyBag.Get<string[]>(Constants.Parameters.ManagedPasswordPrincipals);
        
        set => this.PropertyBag.Set(Constants.Parameters.ManagedPasswordPrincipals, value);
    }
    
    public MemberShipAttribute MemberShipAttribute
    {
        get => this.PropertyBag.Get<MemberShipAttribute>(Constants.Parameters.MembershipAttribute);
        
        set => this.PropertyBag.Set(Constants.Parameters.MembershipAttribute, value);
    }
    
    public string Path
    {
        get => this.PropertyBag.Get(Constants.Parameters.Path);
        
        set => this.PropertyBag.Set(Constants.Parameters.Path, value);
    }

    public IADManagedServiceAccountResource RegisterPrinciple<T>() where T : DscLcmConfiguration, new()
    {
        var principle = new T();
        var principles = this.ManagedPasswordPrincipals?.ToList() ?? [];
        
        principles.Add($"{principle.NodeName}$");
        this.ManagedPasswordPrincipals = principles.ToArray();
        
        return this;
    }
    
    public static ADManagedServiceAccountResource Create(string name, Action<IADManagedServiceAccountResource> action)
    {
        var resource = new ADManagedServiceAccountResource(name);
        action(resource);
        return resource;
    }
    
    public static ADManagedServiceAccountResource Create(string name, Action<IADManagedServiceAccountResource> action, out ADManagedServiceAccountResource resource)
    {
        resource = new ADManagedServiceAccountResource(name);
        action(resource);
        return resource;
    }

    public override Task<List<ValidationFailedException>> Validate()
    {
        var exceptions = new List<ValidationFailedException>();
        
        if (string.IsNullOrWhiteSpace(this.AccountName))
        {
            exceptions.Add(new ValidationFailedException(Constants.Parameters.ServiceAccountName, nameof(this.AccountName), ValidationFailureType.RequiredPropertyMissing, "Service Account Name is required"));
        }

        if (this.AccountName.Length > 15)
        {
            exceptions.Add(new ValidationFailedException(Constants.Parameters.ServiceAccountName, nameof(this.AccountName), ValidationFailureType.InvalidConfiguration, "Service Account Name must be 15 characters or less."));
        }
        
        return Task.FromResult(exceptions);
    }

    public override string ResourceId => Constants.ResourceId;

    public override bool HasEnsure => true;

    public override bool GenerateManifest => true;
}