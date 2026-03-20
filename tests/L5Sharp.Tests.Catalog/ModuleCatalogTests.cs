using System.Net;
using FluentAssertions;
using L5Sharp.Catalog;
using L5Sharp.Core;
using L5Sharp.Tests.Samples;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests.Catalog;

[TestFixture]
public class ModuleCatalogTests
{
    [Test]
    public void Create_ValidCatalogNumber_ShouldReturnModule()
    {
        var catalog = new ModuleCatalogBuilder().WithDefaultModules().Build();

        var module = catalog.Create("MyModule", "1756-L83E");

        module.Should().NotBeNull();
        module.Name.Should().Be("MyModule");
        module.CatalogNumber.Should().Be("1756-L83E");
        module.Revision.Should().NotBeNull();
        module.Vendor.Should().BeGreaterThan(0);
        module.ProductType.Should().BeGreaterThan(0);
    }

    [Test]
    public void Create_ValidCatalogNumberAndRevision_ShouldReturnModule()
    {
        var catalog = new ModuleCatalogBuilder()
            .AddDefinitionFor(new Module { CatalogNumber = "1756-IB16", Revision = 1.1 })
            .Build();

        var module = catalog.Create("MyModule", "1756-IB16", new Revision(1, 1));

        module.Should().NotBeNull();
        module.Revision.Should().Be(new Revision(1, 1));
    }

    [Test]
    public void Create_WithConfig_ShouldApplyConfiguration()
    {
        var catalog = new ModuleCatalogBuilder().WithDefaultModules().Build();

        var module = catalog.Create("MyModule", "1756-L83E", m => m.Description = "Test Description");

        module.Description.Should().Be("Test Description");
    }

    [Test]
    public void Create_NonExistentCatalogNumber_ShouldThrowKeyNotFoundException()
    {
        var catalog = new ModuleCatalogBuilder().WithDefaultModules().Build();

        FluentActions.Invoking(() => catalog.Create("MyModule", "NonExistent"))
            .Should().Throw<KeyNotFoundException>();
    }

    [Test]
    public void TryCreate_ValidCatalogNumber_ShouldReturnTrueAndModule()
    {
        var catalog = new ModuleCatalogBuilder().WithDefaultModules().Build();

        var result = catalog.TryCreate("MyModule", "1756-L83E", out var module);

        result.Should().BeTrue();
        module.Should().NotBeNull();
    }

    [Test]
    public void TryCreate_NonExistentCatalogNumber_ShouldReturnFalse()
    {
        var catalog = new ModuleCatalogBuilder().WithDefaultModules().Build();

        var result = catalog.TryCreate("MyModule", "NonExistent", out var module);

        result.Should().BeFalse();
        module.Should().BeNull();
    }

    [Test]
    public void GetDefinition_ValidCatalogNumber_ShouldReturnLatestRevision()
    {
        var catalog = new ModuleCatalogBuilder().WithDefaultModules().Build();

        var definition = catalog.GetDefinition("1756-IB16");

        definition.Should().NotBeNull();
        definition.Revision.Should().Be(new Revision(3, 1));
    }

    [Test]
    public void TryGetDefinition_ValidCatalogNumber_ShouldReturnTrueAndDefinition()
    {
        var catalog = new ModuleCatalogBuilder().WithDefaultModules().Build();

        var result = catalog.TryGetDefinition("1756-L83E", out var definition);

        result.Should().BeTrue();
        definition.Should().NotBeNull();
        definition.CatalogNumber.Should().Be("1756-L83E");
    }

    [Test]
    public void Definitions_NoFilter_ShouldReturnAll()
    {
        var catalog = new ModuleCatalogBuilder().WithDefaultModules().Build();

        var definitions = catalog.Definitions();

        definitions.Should().NotBeEmpty();
    }

    [Test]
    public void Definitions_WithFilter_ShouldReturnFiltered()
    {
        var catalog = new ModuleCatalogBuilder().WithDefaultModules().Build();

        var definitions = catalog.Definitions("1756-IB16");

        definitions.Should().HaveCount(2);
    }

    [Test]
    public Task Create_ValidNameAndCatalogNumber_ShouldBeVerified()
    {
        var catalog = new ModuleCatalogBuilder().WithDefaultModules().Build();

        var module = catalog.Create("Test", "1756-EN2T");

        var xml = module.Serialize().ToString();
        return Verify(xml);
    }

    [Test]
    public Task Create_CatalogNumberAndConfig_ShouldBeVerified()
    {
        var catalog = new ModuleCatalogBuilder().WithDefaultModules().Build();

        var module = catalog.Create("MyModule", "1783-ETAP", m =>
        {
            m.IP = IPAddress.Any;
            m.Revision = "1.2";
            m.Description = "This is a test of the create factory method using the module catalog.";
            m.Keying = ElectronicKeying.ExactMatch;
        });

        return Verify(module.Serialize().ToString());
    }

    [Test]
    public Task Create_ExistingValidCatalogNumber_ShouldBeVerified()
    {
        var catalog = new ModuleCatalogBuilder()
            .WithModulesFromL5X(TestContent.PathTo(TestFiles.Projects.Test))
            .Build();

        var module = catalog.Create("NewName", "1756-EN2T", m =>
        {
            m.Revision = 1.23;
            m.Description = "This is a new module instance based on an existing template";
            m.Inhibited = true;
        });

        return Verify(module.Serialize().ToString());
    }

    [Test]
    public void Create_InvalidCatalogNumber_ShouldThrowKeyNotFoundException_New()
    {
        var catalog = new ModuleCatalogBuilder().WithDefaultModules().Build();

        var action = () => catalog.Create("MyCard", "1234-ABCD", m => { m.Description = "This will fail"; });

        action.Should().Throw<KeyNotFoundException>();
    }

    [Test]
    public Task Local_ValidCatalogNumber_ShouldBeVerified()
    {
        var catalog = new ModuleCatalogBuilder().WithDefaultModules().Build();

        var module = catalog.Create("Local", "1756-L83E", m => { m.Revision = "33.1"; });

        var xml = module.Serialize().ToString();
        return Verify(xml);
    }

    [Test]
    public void Connect_ValidChildModule_ShouldHaveExpectedParentProperties()
    {
        var catalog = new ModuleCatalogBuilder().WithDefaultModules().Build();

        var module = catalog.Create("TestCard", "1756-EN2T", m => { m.IP = IPAddress.Loopback; });
        var child = catalog.Create("ChildCard", "1756-IF8/A");

        module.Connect(child);

        child.ParentModule.Should().Be(module.Name);
        child.ParentModPortId.Should().Be(1);
        child.Ports.Should().Contain(p => p.Upstream && p.Type == "ICP");
    }

    [Test]
    public void Connect_ChildWithNotMatchingPort_ShouldThrowException()
    {
        var catalog = new ModuleCatalogBuilder().WithDefaultModules().Build();

        var module = catalog.Create("TestCard", "1756-EN2T", m => { m.IP = IPAddress.Loopback; });
        var child = catalog.Create("ChildCard", "5094-OF8/A");
        var action = () => module.Connect(child);

        action.Should().Throw<InvalidOperationException>()
            .WithMessage(
                "Failed to connect ChildCard (5094-OF8/A) to TestCard (1756-EN2T). No matching ports available for connection.");
    }

    [Test]
    public void Connect_ChildWithMultipleMatchingAvailablePorts_ShouldConnectToFirstPort()
    {
        var catalog = new ModuleCatalogBuilder().WithDefaultModules().Build();

        var module = catalog.Create("TestCard", "1756-EN2T", m => { m.IP = IPAddress.Loopback; });
        var child = catalog.Create("ChildCard", "1756-EN2T");
        module.Connect(child);

        child.Ports.Should().Contain(p => p.Upstream && p.Type == "ICP");
    }
}