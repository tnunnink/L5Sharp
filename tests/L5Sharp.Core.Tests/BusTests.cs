using System;
using System.Linq;
using System.Net;
using FluentAssertions;
using L5Sharp.Exceptions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class BusTests
    {
        private IModule _module;
        private LogixCatalog _catalog;

        [SetUp]
        public void Setup()
        {
            _catalog = new LogixCatalog();
            _module = _catalog.Create("Parent", "1756-EN2T");
        }
        
        [Test]
        public void New_DownstreamICP_BusShouldHaveExpectedProperties()
        {
            _module.Ports.ChassisPort?.Bus?.Type.Should().Be("ICP");
            _module.Ports.ChassisPort?.Bus?.Size.Should().Be(10);
            _module.Ports.ChassisPort?.Bus?.Count.Should().Be(0);
            _module.Ports.ChassisPort?.Bus?.IsEmpty.Should().BeTrue();
            _module.Ports.ChassisPort?.Bus?.IsFull.Should().BeFalse();
            _module.Ports.ChassisPort?.Bus?.IsFixed.Should().BeTrue();
            _module.Ports.ChassisPort?.Bus?.IsChassis.Should().BeTrue();
            _module.Ports.ChassisPort?.Bus?.IsNetwork.Should().BeFalse();
        }

        /*[Test]
        public void New_DownstreamEthernet_BusShouldHaveExpectedProperties()
        {
            var port = new Port(1, "Ethernet", false, "0");

            port.Bus?.Type.Should().Be("Ethernet");
            port.Bus?.Size.Should().Be(0);
            port.Bus?.Count.Should().Be(0);
            port.Bus?.IsEmpty.Should().BeTrue();
            port.Bus?.IsFull.Should().BeFalse();
            port.Bus?.IsFixed.Should().BeFalse();
            port.Bus?.IsChassis.Should().BeFalse();
            port.Bus?.IsNetwork.Should().BeTrue();
        }

        [Test]
        public void AddModule_ChassisPortNullName_ShouldThrowArgumentNullException()
        {
            var port = NewChassisPort();

            FluentActions.Invoking(() => port.Bus?.AddModule(null!, "1756-EN2T", 1)).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void AddModule_ChassisPortNullCatalogNumber_ShouldThrowArgumentNullException()
        {
            var port = NewChassisPort();

            FluentActions.Invoking(() => port.Bus?.AddModule("Test", null!, 1)).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void AddModule_ChassisPortInvalidCatalogNumber_ShouldThrowArgumentException()
        {
            var port = NewChassisPort();

            FluentActions.Invoking(() => port.Bus?.AddModule("Test", "1234-FAKE", 1)).Should()
                .Throw<ArgumentException>();
        }

        [Test]
        public void AddModule_ChassisPortInvalidSlotNumber_ShouldAddToNextAvailable()
        {
            var port = NewChassisPort();

            port.Bus?.AddModule("Test", "1756-EN2T", -1);

            port.Bus.Should().HaveCount(1);
            port.Bus?["0"].Should().NotBeNull();
        }

        [Test]
        public void AddModule_ChassisPortValidParameters_ShouldHaveExpectedCount()
        {
            var port = NewChassisPort();

            port.Bus?.AddModule("Test", "1756-EN2T", 1, "Test Module");

            port.Bus?.Count.Should().Be(1);
        }

        [Test]
        public void AddModule_NetworkPortValidParameters_ShouldHaveExpectedCount()
        {
            var port = NewNetworkPort();

            port.Bus?.AddModule("Test", "1756-EN2T", IPAddress.Any, "Test Module");

            port.Bus?.Count.Should().Be(1);
        }

        [Test]
        public void AddModule_NetworkPortNullName_ShouldThrowArgumentNullException()
        {
            var port = NewNetworkPort();

            FluentActions.Invoking(() => port.Bus?.AddModule(null!, "1756-EN2T", IPAddress.Any)).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void AddModule_NetworkPortInvalidCatalogNumber_ShouldThrowArgumentException()
        {
            var port = NewNetworkPort();

            FluentActions.Invoking(() => port.Bus?.AddModule("Test", "1234-FAKE", IPAddress.Any)).Should()
                .Throw<ArgumentException>();
        }

        [Test]
        public void AddModule_NetworkPortInvalidIP_ShouldThrowArgumentException()
        {
            var port = NewNetworkPort();

            FluentActions.Invoking(() => port.Bus?.AddModule("Test", "1756-EN2T", null!)).Should()
                .Throw<ArgumentException>();
        }

        [Test]
        public void AddModule_InvalidPortType_ShouldThrowArgumentException()
        {
            var port = NewNetworkPort();

            FluentActions.Invoking(() => port.Bus?.AddModule("Test", "1756-IF8", IPAddress.Any)).Should()
                .Throw<ArgumentException>()
                .WithMessage("No upstream port of type 'Ethernet' defined for module '1756-IF8'.");
        }

        [Test]
        public void AddModule_SameNameTwice_ShouldThrowComponentNameCollisionException()
        {
            var port = NewChassisPort();

            port.Bus?.AddModule("Test", "1756-EN2T", 1, "Test Module");

            FluentActions.Invoking(() => port.Bus?.AddModule("Test", "1756-EN2T", 1, "Test Module")).Should()
                .Throw<ComponentNameCollisionException>();
        }
        
        [Test]
        public void AddModule_TakenSLotNumber_ShouldAddAtNextAvailable()
        {
            var port = NewChassisPort();

            port.Bus?.AddModule("Test1", "1756-EN2T", 1, "Test Module");
            port.Bus?.AddModule("Test2", "1756-EN2T", 1, "Test Module");
            port.Bus?.AddModule("Test3", "1756-EN2T", 1, "Test Module");

            port.Bus?["1"].Name.Should().Be("Test1");
            port.Bus?["0"].Name.Should().Be("Test2"); //todo this should actually not happen
            port.Bus?["2"].Name.Should().Be("Test3");
        }


        private static Port NewChassisPort() => new(1, "ICP", false, "0", 10, "Parent");

        private static Port NewNetworkPort() => new(1, "Ethernet", false, "192.168.1.1", 0, "Parent");*/
    }
}