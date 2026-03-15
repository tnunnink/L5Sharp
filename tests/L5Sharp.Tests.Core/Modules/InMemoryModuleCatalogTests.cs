using FluentAssertions;

namespace L5Sharp.Tests.Core.Modules;

[TestFixture]
public class InMemoryModuleCatalogTests
{
    private ModuleDefinition _definition1 = null!;
    private ModuleDefinition _definition2 = null!;
    private ModuleDefinition _definition3 = null!;

    [SetUp]
    public void SetUp()
    {
        _definition1 = CreateModuleDefinition("1756-EN2T", 10.1);
        _definition2 = CreateModuleDefinition("1756-EN2T", 11.1);
        _definition3 = CreateModuleDefinition("1756-IB16", 1.1);
    }

    private static ModuleDefinition CreateModuleDefinition(string catalogNumber, Revision revision)
    {
        var module = new Module("Test", catalogNumber, revision);
        return ModuleDefinition.Generate(module);
    }

    [Test]
    public void Add_ValidDefinition_ShouldBeStored()
    {
        var catalog = new InMemoryModuleCatalog();
        catalog.Add(_definition1);

        var results = catalog.FindAll("1756-EN2T").ToList();

        results.Should().ContainSingle().Which.Should().Be(_definition1);
    }

    [Test]
    public void AddRange_MultipleDefinitions_ShouldAllBeStored()
    {
        var catalog = new InMemoryModuleCatalog();
        catalog.AddRange([_definition1, _definition2, _definition3]);

        var results = catalog.FindAll().ToList();

        results.Should().HaveCount(3);
        results.Should().Contain([_definition1, _definition2, _definition3]);
    }

    [Test]
    public void FindAll_NoArguments_ShouldReturnAllDefinitions()
    {
        var catalog = new InMemoryModuleCatalog();
        catalog.AddRange([_definition1, _definition2, _definition3]);

        var results = catalog.FindAll().ToList();

        results.Should().HaveCount(3);
    }

    [Test]
    public void FindAll_SpecificCatalogNumber_ShouldReturnMatchingDefinitions()
    {
        var catalog = new InMemoryModuleCatalog();
        catalog.AddRange([_definition1, _definition2, _definition3]);

        var results = catalog.FindAll("1756-EN2T").ToList();

        results.Should().HaveCount(2);
        results.Should().OnlyContain(d => d.CatalogNumber == "1756-EN2T");
    }

    [Test]
    public void FindAll_NonExistentCatalogNumber_ShouldReturnEmpty()
    {
        var catalog = new InMemoryModuleCatalog();
        catalog.Add(_definition1);

        var results = catalog.FindAll("NonExistent");

        results.Should().BeEmpty();
    }

    [Test]
    public void Find_ValidCatalogAndRevision_ShouldReturnDefinition()
    {
        var catalog = new InMemoryModuleCatalog();
        catalog.AddRange([_definition1, _definition2]);

        var result = catalog.Find("1756-EN2T", 10.1);

        result.Should().Be(_definition1);
    }

    [Test]
    public void Find_InvalidCatalogOrRevision_ShouldThrowInvalidOperationException()
    {
        var catalog = new InMemoryModuleCatalog();
        catalog.Add(_definition1);

        Action act = () => catalog.Find("1756-EN2T", 11.1);

        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Module definition for '1756-EN2T' and revision '11.1' not found.");
    }

    [Test]
    public void TryFind_ValidCatalogAndRevision_ShouldReturnTrueAndDefinition()
    {
        var catalog = new InMemoryModuleCatalog();
        catalog.AddRange([_definition1, _definition2]);

        var success = catalog.TryFind("1756-EN2T", 10.1, out var definition);

        success.Should().BeTrue();
        definition.Should().Be(_definition1);
    }

    [Test]
    public void TryFind_InvalidCatalogOrRevision_ShouldReturnFalseAndNull()
    {
        var catalog = new InMemoryModuleCatalog();
        catalog.Add(_definition1);

        var success = catalog.TryFind("1756-EN2T", 11.1, out var definition);

        success.Should().BeFalse();
        definition.Should().BeNull();
    }

    [Test]
    public void FindLatest_ValidCatalog_ShouldReturnLatestRevision()
    {
        var catalog = new InMemoryModuleCatalog();
        catalog.AddRange([_definition1, _definition2]);

        var result = catalog.FindLatest("1756-EN2T");

        result.Should().Be(_definition2);
    }

    [Test]
    public void FindLatest_InvalidCatalog_ShouldThrowInvalidOperationException()
    {
        var catalog = new InMemoryModuleCatalog();

        Action act = () => catalog.FindLatest("NonExistent");

        act.Should().Throw<InvalidOperationException>()
            .WithMessage("No module definitions found for catalog number 'NonExistent'.");
    }

    [Test]
    public void TryFindLatest_ValidCatalog_ShouldReturnTrueAndLatest()
    {
        var catalog = new InMemoryModuleCatalog();
        catalog.AddRange([_definition1, _definition2]);

        var success = catalog.TryFindLatest("1756-EN2T", out var definition);

        success.Should().BeTrue();
        definition.Should().Be(_definition2);
    }

    [Test]
    public void TryFindLatest_InvalidCatalog_ShouldReturnFalseAndNull()
    {
        var catalog = new InMemoryModuleCatalog();

        var success = catalog.TryFindLatest("NonExistent", out var definition);

        success.Should().BeFalse();
        definition.Should().BeNull();
    }

    [Test]
    public void FindWhere_WithPredicate_ShouldReturnMatchingDefinitions()
    {
        var catalog = new InMemoryModuleCatalog();
        catalog.AddRange([_definition1, _definition2, _definition3]);

        var results = catalog.FindWhere(d => d.CatalogNumber.Contains("IB")).ToList();

        results.Should().ContainSingle().Which.Should().Be(_definition3);
    }
}