namespace DSCProviderCore.Tests;

using UTMO.Text.FileGenerator.Abstract.Exceptions;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.BaseTypes;
using UTMO.Text.FileGenerator.Provider.DSC.Abstract.Enums;

[TestClass]
public class DscLcmWebResourceTests
{
    [TestMethod]
    public void ConfigurationNames_WhenAllConfigurationsArePull_ReturnsAllNames()
    {
        var lcm = new TestLcmConfiguration();
        lcm.DscConfiguration.Add(new TestDscConfiguration("Config1", DscMode.Pull));
        lcm.DscConfiguration.Add(new TestDscConfiguration("Config2", DscMode.Pull));

        var resource = new DscLcmWebResource(lcm)
        {
            LcmResourceType = DscWebResourceTypes.ConfigurationRepositoryWeb,
            LcmResourceName = "PullServer",
            ServerUrl = "https://pull.example.com",
        };

        var names = resource.ConfigurationNames;

        CollectionAssert.AreEquivalent(new[] { "Config1", "Config2" }, names);
    }

    [TestMethod]
    public void ConfigurationNames_WhenSomeConfigurationsArePush_ExcludesPushConfigurations()
    {
        var lcm = new TestLcmConfiguration();
        lcm.DscConfiguration.Add(new TestDscConfiguration("PullConfig", DscMode.Pull));
        lcm.DscConfiguration.Add(new TestDscConfiguration("PushConfig", DscMode.Push));

        var resource = new DscLcmWebResource(lcm)
        {
            LcmResourceType = DscWebResourceTypes.ConfigurationRepositoryWeb,
            LcmResourceName = "PullServer",
            ServerUrl = "https://pull.example.com",
        };

        var names = resource.ConfigurationNames;

        CollectionAssert.AreEquivalent(new[] { "PullConfig" }, names);
        CollectionAssert.DoesNotContain(names, "PushConfig");
    }

    [TestMethod]
    public void ConfigurationNames_WhenAllConfigurationsArePush_ReturnsEmpty()
    {
        var lcm = new TestLcmConfiguration();
        lcm.DscConfiguration.Add(new TestDscConfiguration("PushConfig1", DscMode.Push));
        lcm.DscConfiguration.Add(new TestDscConfiguration("PushConfig2", DscMode.Push));

        var resource = new DscLcmWebResource(lcm)
        {
            LcmResourceType = DscWebResourceTypes.ConfigurationRepositoryWeb,
            LcmResourceName = "PullServer",
            ServerUrl = "https://pull.example.com",
        };

        var names = resource.ConfigurationNames;

        Assert.AreEqual(0, names.Count);
    }

    [TestMethod]
    public void ConfigurationNames_WhenResourceTypeIsNotConfigurationRepositoryWeb_ReturnsEmpty()
    {
        var lcm = new TestLcmConfiguration();
        lcm.DscConfiguration.Add(new TestDscConfiguration("PullConfig", DscMode.Pull));

        var resource = new DscLcmWebResource(lcm)
        {
            LcmResourceType = DscWebResourceTypes.ResourceRepositoryWeb,
            LcmResourceName = "ResourceRepo",
            ServerUrl = "https://resource.example.com",
        };

        var names = resource.ConfigurationNames;

        Assert.AreEqual(0, names.Count);
    }

    private sealed class TestLcmConfiguration : DscLcmConfiguration
    {
        public override string NodeName => "TestNode";

        public override DscLcmWebResource PullServerWebResource => new DscLcmWebResource(this)
        {
            LcmResourceType = DscWebResourceTypes.ConfigurationRepositoryWeb,
            LcmResourceName = "PullServer",
            ServerUrl = "https://pull.example.com",
        };

        public override DscLcmWebResource ResourceRepositoryWebResource => new DscLcmWebResource(this)
        {
            LcmResourceType = DscWebResourceTypes.ResourceRepositoryWeb,
            LcmResourceName = "ResourceRepo",
            ServerUrl = "https://resource.example.com",
        };

        public override DscLcmWebResource ReportServerWebResource => new DscLcmWebResource(this)
        {
            LcmResourceType = DscWebResourceTypes.ReportServerWeb,
            LcmResourceName = "ReportServer",
            ServerUrl = "https://report.example.com",
        };
    }

    private sealed class TestDscConfiguration : DscConfiguration
    {
        private readonly string fullName;
        private readonly DscMode mode;

        public TestDscConfiguration(string fullName, DscMode mode)
        {
            this.fullName = fullName;
            this.mode = mode;
        }

        public override string FullName => this.fullName;

        public override DscMode Mode => this.mode;

        public override string ModuleSource => "TestModule";

        public override string ConfigSource => "TestConfigSource";

        protected override IEnumerable<DscConfigurationItem> ConfigurationItems() => [];

        public override Task<List<ValidationFailedException>> Validate() =>
            Task.FromResult(new List<ValidationFailedException>());
    }
}
