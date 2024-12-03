namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.WmiNameSpaceSecurity.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;
using UTMO.Text.FileGenerator.Provider.DSC.CoreResources.Resources.WmiNameSpaceSecurity.Enums;

public interface IWmiNamespaceSecurityResource : IDscResourceConfig
{
    string Path { get; set; }

    string Principal { get; set; }

    NamespacePermissions[] Permission { get; set; }

    /// <summary>
    /// Available Options in <see cref="WmiSecurityAccessType"/>
    /// </summary>
    string AccessType { get; set; }

    WmiSecurityAppliesTo AppliesTo { get; set; }
}