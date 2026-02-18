using FluentAssertions;

namespace L5Sharp.Tests.Core.Modules;

[TestFixture]
public class RockwellCatalogDatabaseTests
{
    [Test]
    public void FindAll_NoCatalogNumber_ShouldNotBeEmpty()
    {
        var catalog = new RockwellCatalogDatabase();

        var results = catalog.FindAll().ToList();

        results.Should().NotBeEmpty();
    }

    [Test]
    public void FindAll_KnownCatalogNumber_ShouldHaveExpectedCount()
    {
        var catalog = new RockwellCatalogDatabase();

        var results = catalog.FindAll("1756-EN2T");

        results.Should().HaveCount(7);
    }

    [Test]
    public void FindAll_InvalidCatalogNumber_ShouldBeEmpty()
    {
        var catalog = new RockwellCatalogDatabase();

        var results = catalog.FindAll("1234-ABC");

        results.Should().BeEmpty();
    }

    [Test]
    public void Find_ValidNumberAndRevision_ShouldBeExpected()
    {
        var catalog = new RockwellCatalogDatabase();

        var result = catalog.Find("1756-EN2T", 10.1);

        result.Should().NotBeNull();
        result.CatalogNumber.Should().Be("1756-EN2T");
        result.Revision.Should().Be(10.1);
        result.Vendor.Should().Be(1);
        result.ProductType.Should().Be(12);
        result.ProductCode.Should().Be(166);
        result.Ports.Should().HaveCount(2);
    }

    [Test]
    public void FindLatest_ValidNumber_ShouldBeExpected()
    {
        var catalog = new RockwellCatalogDatabase();

        var result = catalog.FindLatest("1756-EN2T");

        result.Should().NotBeNull();
        result.CatalogNumber.Should().Be("1756-EN2T");
        result.Revision.Should().Be(11.1);
        result.Vendor.Should().Be(1);
        result.ProductType.Should().Be(12);
        result.ProductCode.Should().Be(166);
        result.Ports.Should().HaveCount(2);
    }
}