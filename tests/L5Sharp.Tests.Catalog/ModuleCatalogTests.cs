using FluentAssertions;
using L5Sharp.Catalog;
using L5Sharp.Catalog.Abstractions;
using L5Sharp.Core;

namespace L5Sharp.Tests.Catalog;

[TestFixture]
public class ModuleCatalogTests
{
    private IModuleCatalog _catalog;

    [SetUp]
    public void Setup()
    {
        _catalog = new ModuleCatalogBuilder()
            .AddDefinitionFor(new Module { CatalogNumber = "1756-L83E", Revision = new Revision(33, 1) })
            .AddDefinitionFor(new Module { CatalogNumber = "1756-IB16", Revision = new Revision(1, 1) })
            .AddDefinitionFor(new Module { CatalogNumber = "1756-IB16", Revision = new Revision(2, 1) })
            .Build();
    }

    [Test]
    public void Create_ValidCatalogNumber_ShouldReturnModule()
    {
        var module = _catalog.Create("MyModule", "1756-L83E");

        module.Should().NotBeNull();
        module.Name.Should().Be("MyModule");
        module.CatalogNumber.Should().Be("1756-L83E");
    }

    [Test]
    public void Create_ValidCatalogNumberAndRevision_ShouldReturnModule()
    {
        var module = _catalog.Create("MyModule", "1756-IB16", new Revision(1, 1));

        module.Should().NotBeNull();
        module.Revision.Should().Be(new Revision(1, 1));
    }

    [Test]
    public void Create_WithConfig_ShouldApplyConfiguration()
    {
        var module = _catalog.Create("MyModule", "1756-L83E", m => m.Description = "Test Description");

        module.Description.Should().Be("Test Description");
    }

    [Test]
    public void Create_NonExistentCatalogNumber_ShouldThrowKeyNotFoundException()
    {
        FluentActions.Invoking(() => _catalog.Create("MyModule", "NonExistent"))
            .Should().Throw<KeyNotFoundException>();
    }

    [Test]
    public void TryCreate_ValidCatalogNumber_ShouldReturnTrueAndModule()
    {
        var result = _catalog.TryCreate("MyModule", "1756-L83E", out var module);

        result.Should().BeTrue();
        module.Should().NotBeNull();
    }

    [Test]
    public void TryCreate_NonExistentCatalogNumber_ShouldReturnFalse()
    {
        var result = _catalog.TryCreate("MyModule", "NonExistent", out var module);

        result.Should().BeFalse();
        module.Should().BeNull();
    }

    [Test]
    public void GetDefinition_ValidCatalogNumber_ShouldReturnLatestRevision()
    {
        var definition = _catalog.GetDefinition("1756-IB16");

        definition.Should().NotBeNull();
        definition.Revision.Should().Be(new Revision(2, 1));
    }

    [Test]
    public void TryGetDefinition_ValidCatalogNumber_ShouldReturnTrueAndDefinition()
    {
        var result = _catalog.TryGetDefinition("1756-L83E", out var definition);

        result.Should().BeTrue();
        definition.Should().NotBeNull();
        definition.CatalogNumber.Should().Be("1756-L83E");
    }

    [Test]
    public void Definitions_NoFilter_ShouldReturnAll()
    {
        var definitions = _catalog.Definitions();

        definitions.Should().HaveCount(3);
    }

    [Test]
    public void Definitions_WithFilter_ShouldReturnFiltered()
    {
        var definitions = _catalog.Definitions("1756-IB16");

        definitions.Should().HaveCount(2);
    }
}