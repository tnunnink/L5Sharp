using System.Collections.Generic;
using FluentAssertions;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class ModuleDefinitionTests
    {
        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var definition = new ModuleDefinition("1756-EN2T",
                Vendor.Rockwell,
                ProductType.Communications,
                166,
                new List<Revision>
                {
                    new(1, 1),
                    new(2, 1),
                    new(3, 1),
                    new(4, 1),
                    new(5, 1),
                    new(10, 1),
                    new(11, 1),
                },
                new List<ModuleCategory>
                {
                    ModuleCategory.Communication
                }, new List<PortDefinition>
                {
                    new(1, "ICP", false),
                    new(2, "Ethernet", false)
                }, "1756 10/100 Mbps Ethernet Bridge, Twisted-Pair Media");

            definition.Should().NotBeNull();
        }

        [Test]
        public void New_Default_ShouldHaveExpectedProperties()
        {
            var definition = new ModuleDefinition("1756-EN2T",
                Vendor.Rockwell,
                ProductType.Communications,
                166,
                new List<Revision>
                {
                    new(1, 1),
                    new(2, 1),
                    new(3, 1),
                    new(4, 1),
                    new(5, 1),
                    new(10, 1),
                    new(11, 1),
                },
                new List<ModuleCategory>
                {
                    ModuleCategory.Communication
                }, new List<PortDefinition>
                {
                    new(1, "ICP", false),
                    new(2, "Ethernet", false)
                }, "1756 10/100 Mbps Ethernet Bridge, Twisted-Pair Media");

            definition.CatalogNumber.Should().Be(new CatalogNumber("1756-EN2T"));
            definition.Vendor.Should().Be(Vendor.Rockwell);
            definition.ProductType.Should().Be(ProductType.Communications);
            definition.ProductCode.Should().Be(166);
            definition.Revisions.Should().Contain(new Revision(1, 1));
            definition.Revisions.Should().Contain(new Revision(2, 1));
            definition.Revisions.Should().Contain(new Revision(3, 1));
            definition.Revisions.Should().Contain(new Revision(4, 1));
            definition.Revisions.Should().Contain(new Revision(5, 1));
            definition.Revisions.Should().Contain(new Revision(10, 1));
            definition.Revisions.Should().Contain(new Revision(11, 1));
            definition.Categories.Should().Contain(ModuleCategory.Communication);
            definition.Ports.Should().Contain(p => p.Id == 1);
            definition.Ports.Should().Contain(p => p.Id == 2);
            definition.Description.Should().Be("1756 10/100 Mbps Ethernet Bridge, Twisted-Pair Media");
        }
    }
}