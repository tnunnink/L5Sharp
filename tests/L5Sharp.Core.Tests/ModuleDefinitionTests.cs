using System;
using System.Collections.Generic;
using System.Linq;
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
                    new(11, 1)
                },
                new List<ModuleCategory>
                {
                    ModuleCategory.Communication
                }, new List<Port>
                {
                    new(1, "ICP"),
                    new(2, "Ethernet")
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
                }, new List<Port>
                {
                    new(1, "ICP"),
                    new(2, "Ethernet")
                }, "1756 10/100 Mbps Ethernet Bridge, Twisted-Pair Media");

            definition.CatalogNumber.Should().Be("1756-EN2T");
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

        [Test]
        public void ConfigurePorts_ICPAndSlotAndIp_ShouldHaveExpectedPortConfiguration()
        {
            var definition = new ModuleDefinition("1756-EN2T",
                Vendor.Rockwell,
                ProductType.Communications,
                166,
                new List<Revision> { new(1, 1) },
                new List<ModuleCategory> { ModuleCategory.Communication },
                new List<Port>
                {
                    new(1, "ICP"),
                    new(2, "Ethernet")
                }, "1756 10/100 Mbps Ethernet Bridge, Twisted-Pair Media");
            
            definition.ConfigurePorts("ICP", "3", "192.168.1.1");

            var icpPort = definition.Ports.First(p => p.Type == "ICP");
            icpPort.Upstream.Should().BeTrue();
            icpPort.Address.Should().Be(new Address("3"));
            
            var ethernetPort = definition.Ports.First(p => p.Type == "Ethernet");
            ethernetPort.Upstream.Should().BeFalse();
            ethernetPort.Address.Should().Be(new Address("192.168.1.1"));
        }

        [Test]
        public void ConfigurePorts_EthernetAndIPAndSlot_ShouldHaveExpectedPortProperties()
        {
            var definition = new ModuleDefinition("1756-EN2T",
                Vendor.Rockwell,
                ProductType.Communications,
                166,
                new List<Revision> { new(1, 1) },
                new List<ModuleCategory> { ModuleCategory.Communication },
                new List<Port>
                {
                    new(1, "ICP"),
                    new(2, "Ethernet")
                }, "1756 10/100 Mbps Ethernet Bridge, Twisted-Pair Media");
            
            definition.ConfigurePorts("Ethernet", "192.168.1.1", "3");

            var icpPort = definition.Ports.First(p => p.Type == "ICP");
            icpPort.Upstream.Should().BeFalse();
            icpPort.Address.Should().Be(new Address("3"));
            
            var ethernetPort = definition.Ports.First(p => p.Type == "Ethernet");
            ethernetPort.Upstream.Should().BeTrue();
            ethernetPort.Address.Should().Be(new Address("192.168.1.1"));
        }

        [Test]
        public void ConfigureRevision_ValidRevision_ShouldUpdateRevision()
        {
            var definition = new ModuleDefinition("1756-EN2T",
                Vendor.Rockwell,
                ProductType.Communications,
                166,
                new List<Revision> { new(1, 1), new(11, 1) },
                new List<ModuleCategory> { ModuleCategory.Communication },
                new List<Port>
                {
                    new(1, "ICP"),
                    new(2, "Ethernet")
                }, "1756 10/100 Mbps Ethernet Bridge, Twisted-Pair Media");
            
            definition.ConfigureRevision(new Revision(11, 1));

            definition.Revision.Should().Be(new Revision(11, 1));
        }
        
        [Test]
        public void ConfigureRevision_InvalidRevision_ShouldUpdateRevision()
        {
            var definition = new ModuleDefinition("1756-EN2T",
                Vendor.Rockwell,
                ProductType.Communications,
                166,
                new List<Revision> { new(1, 1), new(11, 1) },
                new List<ModuleCategory> { ModuleCategory.Communication },
                new List<Port>
                {
                    new(1, "ICP"),
                    new(2, "Ethernet")
                }, "1756 10/100 Mbps Ethernet Bridge, Twisted-Pair Media");
            
            FluentActions.Invoking(() => definition.ConfigureRevision(new Revision(10, 1)))
                .Should().Throw<InvalidOperationException>()
                .WithMessage($"The provided revision 10.1 is not a valid available revision for the definition.");
        }
    }
}