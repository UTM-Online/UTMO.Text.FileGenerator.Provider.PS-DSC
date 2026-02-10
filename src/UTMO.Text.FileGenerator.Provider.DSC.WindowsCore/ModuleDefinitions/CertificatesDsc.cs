namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.ModuleDefinitions;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

public class CertificatesDsc : RequiredModule
{
    private CertificatesDsc()
    {
    }

    public override string ModuleName => "CertificateDsc";
    public override string ModuleVersion => "6.0.0";

    public static RequiredModule Instance { get; } = new CertificatesDsc();

    public override bool GenerateManifest => false;
}

