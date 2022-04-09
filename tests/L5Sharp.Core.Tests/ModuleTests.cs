using System;
using System.Collections.Generic;
using System.Net;
using FluentAssertions;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class ModuleTests
    {
        [Test]
        public void New_NullName_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new Module(null!, "1756-EN2T")).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void New_NullCatalogNumber_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new Module("Test", ((CatalogNumber)null)!)).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void New_DefaultConstructor_ShouldNotBeNull()
        {
            var module = new Module("Test", "1756-EN2T");

            module.Should().NotBeNull();
        }

        [Test]
        public void New_DefaultConstructor_ShouldHaveExpectedProperties()
        {
            var module = new Module("Test", "1756-EN2T");

            module.Name.Should().Be("Test");
            module.Description.Should().BeEmpty();
            module.CatalogNumber.Should().Be(new CatalogNumber("1756-EN2T"));
            module.Vendor.Should().Be(Vendor.Unknown);
            module.ProductType.Should().Be(ProductType.Unknown);
            module.ProductCode.Should().Be(0);
            module.Revision.Should().Be(new Revision());
            module.ParentModule.Should().BeEmpty();
            module.ParentPortId.Should().Be(0);
            module.Keying.Should().Be(ElectronicKeying.CompatibleModule);
            module.Inhibited.Should().BeFalse();
            module.MajorFault.Should().BeFalse();
            module.SafetyEnabled.Should().BeFalse();
            module.Config.Should().BeNull();
            module.Slot.Should().Be(0);
            module.IP.Should().Be(IPAddress.None);
            module.Ports.Should().HaveCount(1);
            module.Ports[1].Type.Should().Be("ICP");
            module.Ports[1].Address.Should().Be(PortAddress.None);
            module.Connections.Should().BeEmpty();
            module.Backplane.Should().BeEmpty();
            module.Ethernet.Should().BeNull();
            module.Tags.Should().BeEmpty();
            module.Modules.Should().BeEmpty();
        }

        [Test]
        public void New_DefinitionWithNullName_ShouldThrowArgumentNullException()
        {
            var definition = new ModuleDefinition("1756-EN2T", Vendor.Rockwell, ProductType.Communications, 12
                , new List<Revision>
                {
                    new(1, 2),
                    new(1, 3)
                }, new List<ModuleCategory>(),
                new List<Port> { new(1, "ICP", "0"), new(2, "Ethernet", "192.168.1.1") },
                "This is a test definition");

            FluentActions.Invoking(() => new Module(null!, definition)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void New_DefinitionWithNullDefinition_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new Module("Test", ((ModuleDefinition)null)!)).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void New_Definition_ShouldNotBeNull()
        {
            var definition = new ModuleDefinition("1756-EN2T", Vendor.Rockwell, ProductType.Communications, 12
                , new List<Revision>
                {
                    new(1, 2),
                    new(1, 3)
                }, new List<ModuleCategory>(),
                new List<Port> { new(1, "ICP", "0"), new(2, "Ethernet", "192.168.1.1") },
                "This is a test definition");
            
            var module = new Module("Test", definition);

            module.Should().NotBeNull();
        }

        [Test]
        public void DefinitionConstructor_Default_ShouldHaveExpectedProperties()
        {
            var definition = new ModuleDefinition("1756-EN2T", Vendor.Rockwell, ProductType.Communications, 12
                , new List<Revision>
                {
                    new(1, 2),
                    new(1, 3)
                }, new List<ModuleCategory>(),
                new List<Port> { new(1, "ICP", "0"), new(2, "Ethernet", "192.168.1.1") },
                "This is a test definition");

            var module = new Module("Test", definition);

            module.Name.Should().Be("Test");
            module.Description.Should().Be("This is a test definition");
            module.CatalogNumber.Should().Be(new CatalogNumber("1756-EN2T"));
            module.Vendor.Should().Be(Vendor.Rockwell);
            module.ProductType.Should().Be(ProductType.Communications);
            module.ProductCode.Should().Be(12);
            module.Revision.Should().Be(new Revision(1, 3));
            module.ParentModule.Should().BeEmpty();
            module.ParentPortId.Should().Be(0);
            module.Keying.Should().Be(ElectronicKeying.CompatibleModule);
            module.Inhibited.Should().BeFalse();
            module.MajorFault.Should().BeFalse();
            module.SafetyEnabled.Should().BeFalse();
            module.Config.Should().BeNull();
            module.Slot.Should().Be(0);
            module.IP.Should().Be(IPAddress.Parse("192.168.1.1"));
            module.Ports.Should().HaveCount(2);
            module.Ports[1].Type.Should().Be("ICP");
            module.Ports[1].Address.Should().Be(new PortAddress("0"));
            module.Ports[1].Upstream.Should().BeFalse();
            module.Ports[2].Type.Should().Be("Ethernet");
            module.Ports[2].Address.Should().Be(new PortAddress("192.168.1.1"));
            module.Ports[1].Upstream.Should().BeFalse();
            module.Connections.Should().BeEmpty();
            module.Backplane.Should().HaveCount(1);
            module.Ethernet.Should().HaveCount(1);
            module.Tags.Should().BeEmpty();
            module.Modules.Should().HaveCount(2);
        }

        [Test]
        public void New_Overloaded_ShouldHaveExpectedProperties()
        {
            var definition = new ModuleDefinition("1756-EN2T", Vendor.Rockwell, ProductType.Communications, 12
                , new List<Revision>
                {
                    new(1, 2),
                    new(1, 3)
                }, new List<ModuleCategory>(),
                new List<Port> { new(1, "ICP", "0"), new(2, "Ethernet", "192.168.1.1") },
                "This is a test definition");

            var module = new Module("Test", definition, "Local", 1, ElectronicKeying.Disabled, true, true, true,
                "This is a test");

            module.ParentModule.Should().Be("Local");
            module.ParentPortId.Should().Be(1);
            module.Keying.Should().Be(ElectronicKeying.Disabled);
            module.Inhibited.Should().BeTrue();
            module.MajorFault.Should().BeTrue();
            module.SafetyEnabled.Should().BeTrue();
            module.Description.Should().Be("This is a test");
        }

    }
}