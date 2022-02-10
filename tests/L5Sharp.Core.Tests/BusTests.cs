using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using FluentAssertions;
using L5Sharp.Exceptions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class BusTests
    {
        [Test]
        public void New_ChassisTypeBus_ShouldHaveExpectedProperties()
        {
            var module = new Module("Test", "1756-EN2T", IPAddress.Any);
            var bus = module.Ports.Local()?.Bus;
            
            bus?.Type.Should().Be("ICP");
            bus?.Size.Should().Be(0);
            bus?.Count.Should().Be(1);
            bus?.IsEmpty.Should().BeFalse();
            bus?.IsFull.Should().BeFalse();
            bus?.IsFixed.Should().BeFalse();
            bus?.IsChassis.Should().BeTrue();
            bus?.IsEthernet.Should().BeFalse();
        }

        [Test]
        public void New_EthernetTypeBus_ShouldHaveExpectedProperties()
        {
            var bytes = new List<byte> { 1, 2, 3, 4 };
            var ip = new IPAddress(bytes.ToArray());
            var module = new Module("Test", "1756-EN2T", 0, ip);
            var bus = module.Ports[2].Bus;

            bus.Should().NotBeNull();
            bus?.Type.Should().Be("Ethernet");
            bus?.Size.Should().Be(0);
            bus?.Count.Should().Be(1);
            bus?.IsEmpty.Should().BeFalse();
            bus?.IsFull.Should().BeFalse();
            bus?.IsFixed.Should().BeFalse();
            bus?.IsChassis.Should().BeFalse();
            bus?.IsEthernet.Should().BeTrue();
        }

        [Test]
        public void IndexGetter_ValidAddress_ShouldBeExpected()
        {
            var bus = CreateChassisBus();

            var module = bus["0"];

            module.Should().NotBeNull();
            module.Name.Should().Be("Test");
        }
        
        [Test]
        public void IndexGetter_InvalidAddress_ShouldThrowKeyNotFoundException()
        {
            var bus = CreateChassisBus();

            FluentActions.Invoking(() => bus["1"]).Should().Throw<KeyNotFoundException>();
        }

        [Test]
        public void Add_Null_ShouldThrowArgumentNullException()
        {
            var bus = CreateChassisBus();

            FluentActions.Invoking(() => bus?.Add(null!)).Should()
                .Throw<ArgumentNullException>();
        }
        
        [Test]
        public void Add_DuplicateName_ShouldThrowArgumentNullException()
        {
            var bus = CreateChassisBus();
            var module = new Module("Test", "1756-EN2T");

            FluentActions.Invoking(() => bus?.Add(module)).Should()
                .Throw<ComponentNameCollisionException>();
        }

        [Test]
        public void Add_NoUpstreamPort_ShouldThrowArgumentException()
        {
            var bus = CreateChassisBus();
            
            //by default no upstream ports are created, so adding this should throw and argument exception with the message.
            var module = new Module("Child", "1756-EN2T");

            FluentActions.Invoking(() => bus.Add(module)).Should()
                .Throw<ArgumentException>();
        }

        [Test]
        public void Add_ValidModule_ShouldHaveExpectedCount()
        {
            
        }

        [Test]
        public void DetermineAddress_ValidAddress_ShouldBeProvided()
        {
            const string provided = "1";
            var bus = CreateChassisBus();

            var address = bus.GetAddress(provided);

            address.Should().Be(provided);
        }
        
        [Test]
        public void DetermineAddress_InvalidAddressType_ShouldBeNextAvailable()
        {
            const string provided = "192.168.1.2";
            var bus = CreateChassisBus();

            var address = bus.GetAddress(provided);

            address.Should().NotBe(provided);
            address.Should().Be("1");
        }
        
        [Test]
        public void DetermineAddress_ValidButNotAvailable_ShouldBeNextAvailable()
        {
            const string provided = "0";
            var bus = CreateChassisBus();

            var address = bus.GetAddress(provided);

            address.Should().NotBe(provided);
            address.Should().Be("1");
        }

        [Test]
        public void Iterate_WhenPerformed_ShouldWork()
        {
            var bus = CreateChassisBus();

            foreach (var module in bus)
            {
                module.Should().NotBeNull();
            }
        }

        [Test]
        public void Enumerate_AsEnumerable_ShouldWork()
        {
            var bus = (IEnumerable)CreateChassisBus();

            foreach (var module in bus)
            {
                module.Should().NotBeNull();
            }
        }

        private static Bus CreateChassisBus()
        {
            var module = new Module("Test", "1756-EN2T", IPAddress.Any);
            return module.Ports.Local()?.Bus;
        }
    }
}