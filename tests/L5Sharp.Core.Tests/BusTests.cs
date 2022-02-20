using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using FluentAssertions;
using L5Sharp.Enums;
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

            //by default no upstream ports are created, so adding this should throw and argument exception.
            var module = new Module("Child", "1756-EN2T");

            FluentActions.Invoking(() => bus.Add(module)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Add_ValidModule_ShouldHaveExpectedCount()
        {
        }

        [Test]
        public void New_ValidModule_ShouldNotBeNull()
        {
            var parent = new Module("Test", "1756-EN2T", IPAddress.Any);

            var bus = parent.Ports.Local()?.Bus;

            var module = bus?.New("Child", "1756-IF8", 1);

            module.Should().NotBeNull();
        }

        [Test]
        public void New_ValidModule_ModuleShouldHaveExpectedProperties()
        {
            var parent = new Module("Test", "1756-EN2T", IPAddress.Any);

            var bus = parent.Ports.Local()?.Bus!;

            var module = bus?.New("Child", "1756-IF8", 1);

            module?.Name.Should().Be("Child");
            module?.Description.Should().BeEmpty();
            module?.CatalogNumber.Should().Be(new CatalogNumber("1756-IF8"));
            module?.Vendor.Should().Be(Vendor.Rockwell);
            module?.ProductType.Should().Be(ProductType.Analog);
            module?.ProductCode.Should().NotBe(0);
            module?.Inhibited.Should().BeFalse();
            module?.MajorFault.Should().BeFalse();
            module?.SafetyEnabled.Should().BeFalse();
            module?.Slot.Should().Be(1);
            module?.IP.Should().BeNull();
            module?.State.Should().Be(KeyingState.CompatibleModule);
            module?.ParentModule.Should().Be("Test");
            module?.ParentPortId.Should().Be(1);
            module?.Ports.Should().HaveCount(1);
        }

        [Test]
        public void New_ValidModuleForChassisTypePort_BusShouldContainChild()
        {
            var parent = new Module("Test", "1756-EN2T", IPAddress.Any);

            var bus = parent.Ports.Local()?.Bus!;

            var module = bus?.New("Child", "1756-IF8", 1);

            var child = bus?["1"];

            child.Should().BeSameAs(module);
        }

        [Test]
        public void New_ExistingModuleName_ShouldThrowComponentNameCollisionException()
        {
            var module = new Module("Test", "1756-EN2T", IPAddress.Any);
            var bus = module.Ports.Local()?.Bus!;

            FluentActions.Invoking(() => bus?.New("Test", "1756-IF8")).Should()
                .Throw<ComponentNameCollisionException>();
        }
        
        [Test]
        public void New_NonExistingModule_ShouldThrowModuleNotFoundException()
        {
            var module = new Module("Test", "1756-EN2T", IPAddress.Any);
            
            var bus = module.Ports.Local()?.Bus!;

            FluentActions.Invoking(() => bus?.New("Test", "1756-ABCD")).Should().Throw<ModuleNotFoundException>();
        }
        
        [Test]
        public void New_InvalidModuleType_ShouldThrowModuleNotFoundException()
        {
            var module = new Module("Test", "1756-EN2T", IPAddress.Any);
            
            var bus = module.Ports.Local()?.Bus!;

            FluentActions.Invoking(() => bus?.New("Test", "17-ABCD")).Should().Throw<ModuleNotFoundException>();
        }

        [Test]
        public void New_IsFull_ShouldThrowInvalidArgumentException()
        {
            //right now there is now way to set chassis size. Would need this to have IsFull be true and test exception...
            var module = new Module("Test", "1756-EN2T", IPAddress.Any);
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