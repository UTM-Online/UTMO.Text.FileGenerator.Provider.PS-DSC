namespace UTMO.Text.FileGenerator.Provider.DSC.CoreResources.ModuleDefinitions;

using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

public class CertificatesDsc : RequiredModule
{
    private CertificatesDsc()
    {
    }

    public override string ModuleName => "CertificatesDsc";
    public override string ModuleVersion => string.Empty;
    
    public static RequiredModule Instance { get; } = new CertificatesDsc();

    public override bool GenerateManifest => false;
}

