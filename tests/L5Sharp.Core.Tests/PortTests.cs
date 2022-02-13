using System.Net;
using FluentAssertions;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class PortTests
    {
        [Test]
        public void New_KnownModule_ShouldHaveExpectedPortCount()
        {
            var module = new Module("Test", "1756-EN2T");

            module.Ports.Should().HaveCount(2);
        }

        [Test]
        public void Primary_KnownModule_ShouldNotBeNull()
        {
            var module = new Module("Test", "1756-EN2T");

            var port = module.Ports.Primary();

            port.Should().NotBeNull();
        }
        
        [Test]
        public void Secondary_KnownModuleWithTwoPorts_ShouldNotBeNull()
        {
            var module = new Module("Test", "1756-EN2T");

            var port = module.Ports.Secondary();

            port.Should().NotBeNull();
        }
        
        [Test]
        public void Secondary_KnownModuleWithSinglePort_ShouldNotNull()
        {
            var module = new Module("Test", "1756-L74");

            var port = module.Ports.Secondary();

            port.Should().BeNull();
        }
        
        [Test]
        public void Primary_WhenCalled_PortShouldHaveExpectedProperties()
        {
            var module = new Module("Test", "1756-EN2T");
            
            var port = module.Ports.Primary();

            port.Id.Should().Be(1);
            port.Type.Should().Be("ICP");
            port.Upstream.Should().BeTrue();
            port.Address.Should().Be("0");
            port.Bus.Should().BeNull();
            port.Module.Should().BeSameAs(module);
        }

        [Test]
        public void New_SecondPort_ShouldHaveExpectedProperties()
        {
            var module = new Module("Test", "1756-EN2T", 1, IPAddress.Any);
            
            var port = module.Ports.Secondary();

            port.Should().NotBeNull();
            port?.Id.Should().Be(2);
            port?.Type.Should().Be("Ethernet");
            port?.Upstream.Should().BeFalse();
            port?.Address.Should().Be("0.0.0.0");
            port?.Bus.Should().NotBeNull();
            port?.Module.Should().BeSameAs(module);
        }

        [Test]
        public void NewModule_ValidModuleForChassisTypePort_ShouldNotBeNull()
        {
            var parent = new Module("Test", "1756-EN2T", IPAddress.Any);

            var port = parent.Ports.Primary();

            var module = port.NewModule("Child", "1756-IF8", 1);

            module.Should().NotBeNull();
        }
        
        [Test]
        public void NewModule_ValidModuleForChassisTypePort_ModuleShouldHaveExpectedProperties()
        {
            var parent = new Module("Test", "1756-EN2T", IPAddress.Any);

            var port = parent.Ports.Primary();

            var module = port.NewModule("Child", "1756-IF8", 1);

            module.Name.Should().Be("Child");
            module.Description.Should().BeEmpty();
            module.CatalogNumber.Should().Be(new CatalogNumber("1756-IF8"));
            module.Vendor.Should().Be(Vendor.Rockwell);
            module.ProductType.Should().Be(ProductType.Analog);
            module.ProductCode.Should().NotBe(0);
            module.Inhibited.Should().BeFalse();
            module.MajorFault.Should().BeFalse();
            module.SafetyEnabled.Should().BeFalse();
            module.Slot.Should().Be(1);
            module.IP.Should().BeNull();
            module.State.Should().Be(KeyingState.CompatibleModule);
            module.ParentModule.Should().Be("Test");
            module.ParentPortId.Should().Be(1);
            module.Ports.Should().HaveCount(1);
        }
        
        [Test]
        public void NewModule_ValidModuleForChassisTypePort_ParentLocalBusShouldContainChild()
        {
            var parent = new Module("Test", "1756-EN2T", IPAddress.Any);

            var port = parent.Ports.Primary();

            var module = port.NewModule("Child", "1756-IF8", 1);

            var child = parent.Ports.Local()?.Bus?["1"];

            child.Should().BeSameAs(module);
        }
    }
}