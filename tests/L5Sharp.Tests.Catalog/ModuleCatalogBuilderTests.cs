using FluentAssertions;
using L5Sharp.Catalog;
using L5Sharp.Core;

namespace L5Sharp.Tests.Catalog;

[TestFixture]
public class ModuleCatalogBuilderTests
{
    [Test]
    public void Build_Empty_ShouldNotBeNull()
    {
        var catalog = new ModuleCatalogBuilder().Build();

        catalog.Should().NotBeNull();
        catalog.Definitions().Should().BeEmpty();
    }

    [Test]
    public void Build_WithDefaultModules_ShouldNotBeNull()
    {
        var catalog = new ModuleCatalogBuilder()
            .WithDefaultModules()
            .Build();

        catalog.Should().NotBeNull();
        catalog.Definitions().Should().NotBeEmpty();
    }

    [Test]
    public void WithModulesFromL5X_ValidFile_ShouldIncludeDefinitions()
    {
        var filePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.L5X");
        var content = L5X.Empty();
        content.Add(new Module { Name = "TestModule", CatalogNumber = "1756-IB16", Revision = new Revision(1, 1) });
        content.Save(filePath);

        try
        {
            var catalog = new ModuleCatalogBuilder()
                .WithModulesFromL5X(filePath)
                .Build();

            catalog.Definitions("1756-IB16").Should().NotBeEmpty();
        }
        finally
        {
            if (File.Exists(filePath)) File.Delete(filePath);
        }
    }

    [Test]
    public void WithModulesFromL5X_NonExistentFile_ShouldThrowFileNotFoundException()
    {
        var builder = new ModuleCatalogBuilder();

        FluentActions.Invoking(() => builder.WithModulesFromL5X("NonExistent.L5X"))
            .Should().Throw<FileNotFoundException>();
    }

    [Test]
    public void WithModulesFromRAD_NonExistentFile_ShouldThrowFileNotFoundException()
    {
        var builder = new ModuleCatalogBuilder();

        FluentActions.Invoking(() => builder.WithModulesFromRAD("NonExistent.xml"))
            .Should().Throw<FileNotFoundException>();
    }

    [Test]
    public void AddDefinitionFor_ValidModule_ShouldIncludeInCatalog()
    {
        var module = new Module { CatalogNumber = "1756-L83E", Revision = new Revision(33, 1) };
        var catalog = new ModuleCatalogBuilder()
            .AddDefinitionFor(module)
            .Build();

        catalog.Definitions().Should().HaveCount(1);
        catalog.Definitions("1756-L83E").Should().HaveCount(1);
    }

    [Test]
    public void AddDefinitionFor_NullModule_ShouldThrowArgumentNullException()
    {
        var builder = new ModuleCatalogBuilder();

        FluentActions.Invoking(() => builder.AddDefinitionFor(null!))
            .Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void MultipleSources_ShouldAggregateDefinitions()
    {
        var module1 = new Module { CatalogNumber = "1756-IB16", Revision = new Revision(1, 1) };
        var module2 = new Module { CatalogNumber = "1756-OB16", Revision = new Revision(1, 1) };

        var catalog = new ModuleCatalogBuilder()
            .AddDefinitionFor(module1)
            .AddDefinitionFor(module2)
            .Build();

        catalog.Definitions().Should().HaveCount(2);
    }
}