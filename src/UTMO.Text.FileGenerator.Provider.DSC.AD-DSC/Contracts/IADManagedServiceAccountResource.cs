namespace UTMO.Text.FileGenerator.Provider.DSC.AD_DSC.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Enums;

public interface IADManagedServiceAccountResource
{
    string AccountName { get; set; }

    AdServiceAccountType AccountType { get; set; }

    string CommonName { get; set; }

    string DisplayName { get; set; }

    KerberosEncryptionType EncryptionType { get; set; }

    bool TrustedForDelegation { get; set; }

    MemberShipAttribute MemberShipAttribute { get; set; }

    string Path { get; set; }

    string Name { get; set; }

    string Description { get; set; }

    DscEnsure Ensure { get; set; }

    DscConfigurationItem AddDependency<T>(T resource) where T : DscConfigurationItem;
    
    IADManagedServiceAccountResource RegisterPrinciple<T>() where T : DscLcmConfiguration, new();
}