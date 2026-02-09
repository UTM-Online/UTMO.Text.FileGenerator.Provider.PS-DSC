namespace UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Models;
using UTMO.Text.FileGenerator.Provider.DSC.Definitions.Resources.WebAdministrationDsc.Enums;
public class WebBindingInfo
{
    public BindingProtocol Protocol { get; set; }
    public string? BindingInformation { get; set; }
    public string? IPAddress { get; set; }
    public ushort? Port { get; set; }
    public string? HostName { get; set; }
    public string? CertificateThumbprint { get; set; }
    public string? CertificateSubject { get; set; }
    public CertificateStoreName? CertificateStoreName { get; set; }
    public string? SslFlags { get; set; }
}
