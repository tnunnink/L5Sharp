using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Rockwell;
using NUnit.Framework;

namespace L5Sharp.Internal.Tests.Catalog
{
    public class LogixCatalogTests
    {
        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var catalog = new ModuleCatalog();

            catalog.Should().NotBeNull();
        }

        [Test]
        public void Lookup_ValidNumber_ShouldNotBeNull()
        {
            var catalog = new ModuleCatalog();

            var definition = catalog.Lookup("1756-L83E");

            definition.Should().NotBeNull();
        }

        [Test]
        public void Lookup_L83E_ShouldHaveExpectedProperties()
        {
            var catalog = new ModuleCatalog();

            var definition = catalog.Lookup("1756-L83E");

            definition.CatalogNumber.Should().BeEquivalentTo("1756-L83E");
            definition.Vendor.Id.Should().Be(1);
            definition.Vendor.Name.Should().Be("Rockwell Automation/Allen-Bradley");
            definition.ProductType.Id.Should().Be(14);
            definition.ProductType.Name.Should().Be("Programmable Logic Controller");
            definition.ProductCode.Should().Be(166);
            definition.Revisions.Should().HaveCount(5);
            definition.Categories.Should().HaveCount(1);
            definition.Ports.Should().HaveCount(2);
            definition.Description.Should().Be("ControlLogix® 5580 Controller");
        }
    }
}