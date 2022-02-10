using System;
using System.Collections.Generic;
using System.Net;
using FluentAssertions;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class ModuleTests
    {
        private static ModuleDefinition CreateFakeDefinition()
        {
            return new ModuleDefinition("1234-ABCD", Vendor.Unknown, ProductType.Unknown, 1,
                new List<Revision>
                {
                    new(1, 1),
                    new(2, 1),
                    new(3, 1)
                }, new List<ModuleCategory>
                {
                    ModuleCategory.Communication,
                    ModuleCategory.Digital
                }, new List<PortDefinition>
                {
                    new(1, "ICP", true),
                    new(2, "Ethernet", false)
                }, "This is a fake module definition");
        }

        [Test]
        public void DefinitionConstructor_NullName_ShouldThrowArgumentNullException()
        {
            var definition = CreateFakeDefinition();

            FluentActions.Invoking(() => new Module(null!, definition)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void DefinitionConstructor_NullDefinition_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new Module("Test", ((ModuleDefinition)null)!)).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void DefinitionConstructor_Default_ShouldNotBeNull()
        {
            var module = new Module("Test", CreateFakeDefinition());

            module.Should().NotBeNull();
        }

        [Test]
        public void DefinitionConstructor_Default_ShouldHaveExpectedProperties()
        {
            var module = new Module("Test", CreateFakeDefinition());

            module.Name.Should().Be("Test");
            module.Description.Should().BeEmpty();
            module.CatalogNumber.Should().BeEquivalentTo(new CatalogNumber("1234-ABCD"));
            module.Vendor.Should().Be(Vendor.Unknown);
            module.ProductType.Should().Be(ProductType.Unknown);
            module.ProductCode.Should().Be(1);
            module.Revision.Should().Be(new Revision(3, 1));
            module.ParentModule.Should().BeEmpty();
            module.ParentPortId.Should().Be(0);
            module.State.Should().Be(KeyingState.CompatibleModule);
            module.Ports.Should().HaveCount(2);
        }

        [Test]
        public void DefinitionConstructor_Overloaded_ShouldHaveParentProperties()
        {
            var module = new Module("Test", CreateFakeDefinition(), "Local", 1);

            module.ParentModule.Should().Be("Local");
            module.ParentPortId.Should().Be(1);
        }

        [Test]
        public void DefinitionConstructor_Overloaded_ShouldHaveExpectedDescription()
        {
            var module = new Module("Test", CreateFakeDefinition(), "Local", 1, "This is a test");

            module.Description.Should().Be("This is a test");
        }

        [Test]
        public void CatalogConstructor_NullName_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new Module(null!, "1756-EN2T"))
                .Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void CatalogConstructor_NullCatalogNumber_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new Module("Test", ((CatalogNumber)null)!))
                .Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void CatalogConstructor_Default_ShouldNotBeNull()
        {
            var module = new Module("Test", "1756-EN2T");

            module.Should().NotBeNull();
        }

        [Test]
        public void CatalogConstructor_Overloaded_ShouldHaveExpectedProperties()
        {
            var module = new Module("Parent", "1756-EN2T", 1, IPAddress.Parse("1.1.1.1"), "This is a test module");

            module.Should().NotBeNull();
            module.Name.Should().Be("Parent");
            module.Description.Should().Be("This is a test module");
            module.CatalogNumber.Should().Be(new CatalogNumber("1756-EN2T"));
            module.Vendor.Should().Be(Vendor.Rockwell);
            module.Revision.Should().Be(new Revision(11, 1));
            module.ProductType.Should().Be(ProductType.Communications);
            module.ProductCode.Should().NotBe(0);
            module.ParentModule.Should().BeEmpty();
            module.ParentPortId.Should().Be(0);
            module.Inhibited.Should().BeFalse();
            module.MajorFault.Should().BeFalse();
            module.SafetyEnabled.Should().BeFalse();
            module.State.Should().Be(KeyingState.CompatibleModule);
            module.Slot.Should().Be(1);
            module.IP.Should().Be(IPAddress.Parse("1.1.1.1"));
            module.Ports.Should().HaveCount(2);
            module.Connections.Should().BeEmpty();
            module.Tags.Config.Should().BeNull();
        }

        [Test]
        public void CatalogConstructor_SlotOnly_ShouldHaveExpectedConnectingPort()
        {
            var module = new Module("Parent", "1756-EN2T", 1);

            var port = module.Ports.Connecting();

            port?.Id.Should().Be(1);
            port?.Type.Should().Be("ICP");
            port?.Upstream.Should().BeTrue();
            port?.Address.Should().Be("1");
            port?.Bus.Should().BeNull();
        }

        [Test]
        public void CatalogConstructor_SlotOnly_ShouldHaveExpectedLocalPort()
        {
            var module = new Module("Parent", "1756-EN2T", 1);

            var port = module.Ports.Local();

            port?.Id.Should().Be(2);
            port?.Type.Should().Be("Ethernet");
            port?.Upstream.Should().BeFalse();
            port?.Address.Should().Be("0.0.0.0");
            port?.Bus.Should().NotBeNull();
        }
        
        [Test]
        public void CatalogConstructor_IPOnly_ShouldHaveExpectedConnectingPort()
        {
            var module = new Module("Parent", "1756-EN2T", IPAddress.Parse("1.2.3.4"));

            var port = module.Ports.Connecting();

            port?.Id.Should().Be(2);
            port?.Type.Should().Be("Ethernet");
            port?.Upstream.Should().BeTrue();
            port?.Address.Should().Be("1.2.3.4");
            port?.Bus.Should().BeNull();
        }

        [Test]
        public void CatalogConstructor_IPOnly_ShouldHaveExpectedLocalPort()
        {
            var module = new Module("Parent", "1756-EN2T", IPAddress.Parse("1.2.3.4"));

            var port = module.Ports.Local();

            port?.Id.Should().Be(1);
            port?.Type.Should().Be("ICP");
            port?.Upstream.Should().BeFalse();
            port?.Address.Should().Be("0");
            port?.Bus.Should().NotBeNull();
        }
    }
}