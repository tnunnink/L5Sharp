using System;
using System.Net;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Creators.Tests
{
    [TestFixture]
    public class ModuleBuilderTests
    {
        [Test]
        public void Build_ValidNameAndCatalog_ShouldNotBeNull()
        {
            var module = Module.Build("Test")
                .WithCatalog("1756-EN2T")
                .Create();

            module.Should().NotBeNull();
        }

        [Test]
        public void Build_ValidNameAndCatalog_ShouldHaveExpectedProperties()
        {
            var module = Module.Build("Test").WithCatalog("1756-EN2T").Create();

            module.Name.Should().Be("Test");
            module.CatalogNumber.Should().Be(new CatalogNumber("1756-EN2T"));
            module.Vendor.Should().Be(Vendor.Rockwell);
            module.ProductType.Should().Be(ProductType.Communications);
            module.ProductCode.Should().Be(166);
            module.Revision.Should().Be(new Revision(11, 1));
            module.Ports.Should().HaveCount(2);
            module.Keying.Should().Be(ElectronicKeying.CompatibleModule);
            module.ParentModule.Should().BeEmpty();
            module.ParentPortId.Should().Be(0);
            module.Inhibited.Should().BeFalse();
            module.MajorFault.Should().BeFalse();
            module.SafetyEnabled.Should().BeFalse();
            module.Slot.Should().Be(0);
            module.IP.Should().Be(IPAddress.None);
            module.Config.Should().BeNull();
            module.Connections.Should().BeEmpty();
            module.Backplane.Should().NotBeNull();
            module.Ethernet.Should().NotBeNull();
            module.Description.Should().BeEmpty();
        }

        [Test]
        public void Build_WithDescription_ShouldHaveExpectedDescription()
        {
            var module = Module.Build("Test")
                .WithCatalog("1756-EN2T")
                .WithDescription("This is a test module")
                .Create();

            module.Should().NotBeNull();
            module.Description.Should().Be("This is a test module");
        }

        [Test]
        public void Build_WithValidRevision_ShouldHaveExpectedRevision()
        {
            var module = Module.Build("Test")
                .WithCatalog("1756-EN2T")
                .WithRevision(new Revision(11, 1))
                .Create();

            module.Revision.Should().Be(new Revision(11, 1));
        }

        [Test]
        public void Build_WithInvalidRevision_ShouldThrowInvalidOperationException()
        {
            var builder = Module.Build("Test")
                .WithCatalog("1756-EN2T")
                .WithRevision(new Revision(123, 456));

            FluentActions.Invoking(() => builder.Create()).Should().Throw<InvalidOperationException>();
        }
    }
}