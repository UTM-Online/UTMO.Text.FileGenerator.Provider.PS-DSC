namespace UTMO.Text.FileGenerator.Provider.DSC.Abstract.Constants;

public partial class WindowsFeatureNames
{
    public class NetClassic
    {
        public const string NetFx3 = "NET-Framework-Core";
    }

    public class RSAT
    {
        public const string ActiveDirectory = "RSAT-AD-Tools";
    }

    public class ActiveDirectory
    {
        public const string DomainServices = "AD-Domain-Services";

        public const string DNS = "DNS";
    }

    public class FileReplication
    {
        public const string Replication = "FS-DFS-Replication";
    }
}
