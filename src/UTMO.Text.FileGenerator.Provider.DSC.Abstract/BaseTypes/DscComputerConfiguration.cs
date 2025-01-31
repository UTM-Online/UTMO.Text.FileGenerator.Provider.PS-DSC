namespace UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;

using UTMO.Text.FileGenerator.Attributes;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Constants;

public class DscComputerConfiguration : DscResourceBase
{
    public override string ResourceTypeName => DscResourceTypeNames.DscComputerConfiguration;

    public override string TemplatePath => "DscNodeConfiguration";

    public override string ResourceName => this.NodeName;
    
    [MemberName("node_name")]
    public string NodeName { get; init; }
    
    [MemberName("ConfigurationResources")]
    public List<DscConfigurationItem> NodeConfigurations { get; init; }
    
    [MemberName("RequiredModules")]
    public List<RequiredModule> RequiredModules { get; init; }

    public static explicit operator DscComputerConfiguration(DscLcmConfiguration cfg)
    {
        return new DscComputerConfiguration()
               {
                   NodeName = cfg.NodeName,
                   NodeConfigurations = cfg.NodeConfigurations,
                   RequiredModules = cfg.NodeConfigurations.Select(a => a.SourceModule).DistinctBy(a => a.ModuleName).ToList(),
               };
    }
}