namespace UTMO.Text.FileGenerator.Provider.DSC.cChoco.Contracts;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Contracts;

public interface IChocoPackageSourceResource : IDscResourceConfig
{
    string SourceName { get; set; }

    string SourceUri { get; set; }
}