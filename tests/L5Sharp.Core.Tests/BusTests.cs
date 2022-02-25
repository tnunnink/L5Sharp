using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            module?.Name.Should().Be("Test");
        }

        [Test]
        public void IndexGetter_InvalidAddress_ShouldBeNull()
        {
            var bus = CreateChassisBus();

            var module = bus["1"];

            module.Should().BeNull();
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
        public void AddressOf_ValidModule_ShouldBeExpectedValue()
        {
            var bus = CreateChassisBus();

            var address = bus.AddressOf("Test");

            address.Should().Be("0");
        }

        [Test]
        public void AddressOf_NonExisting_ShouldBeNull()
        {
            var bus = CreateChassisBus();

            var address = bus.AddressOf("Fake");

            address.Should().BeNull();
        }

        [Test]
        public void ContainsAddress_Existing_ShouldBeTrue()
        {
            var bus = CreateChassisBus();

            var result = bus.ContainsAddress("0");

            result.Should().BeTrue();
        }

        [Test]
        public void ContainsAddress_NonExisting_ShouldBeFalse()
        {
            var bus = CreateChassisBus();

            var result = bus.ContainsAddress("11");

            result.Should().BeFalse();
        }

        [Test]
        public void ContainsName_Existing_ShouldBeTrue()
        {
            var bus = CreateChassisBus();

            var result = bus.ContainsName("Test");

            result.Should().BeTrue();
        }

        [Test]
        public void ContainsName_NonExisting_ShouldBeFalse()
        {
            var bus = CreateChassisBus();

            var result = bus.ContainsName("Fake");

            result.Should().BeFalse();
        }

        [Test]
        public void Clear_NoModules_ShouldStillHaveParentModule()
        {
            var bus = CreateChassisBus();

            bus.Clear();

            bus.Should().HaveCount(1);
            bus.Should().Contain(m => m.Name == "Test");
        }

        [Test]
        public void Clear_Modules_ShouldHaveExpectedCount()
        {
            var bus = CreateChassisBus();
            bus.New("Child1", "1756-IF8", 1);
            bus.New("Child2", "1756-IF8", 2);

            bus.Clear();

            bus.Should().HaveCount(1);
            bus.Should().Contain(m => m.Name == "Test");
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
            module?.IP.Should().Be(IPAddress.Parse("0.0.0.0"));
            module?.State.Should().Be(KeyingState.CompatibleModule);
            module?.ParentModule.Should().Be("Test");
            module?.ParentPortId.Should().Be(1);
            module?.Ports.Should().HaveCount(1);
        }

        [Test]
        public void New_EthernetBus_Valid_ShouldNotBeNull()
        {
            var bus = CreateEthernetBus();
            
            var module = bus?.New("Child", "1783-ETAP", 0, IPAddress.Parse("2.2.2.2"));

            module.Should().NotBeNull();
        }
        
        [Test]
        public void New_EthernetBus_Valid_ShouldHaveExpectedProperties()
        {
            var bus = CreateEthernetBus();
            
            var module = bus?.New("Child", "1783-ETAP", 0, IPAddress.Parse("2.2.2.2"));

            module?.Name.Should().Be("Child");
            module?.Description.Should().BeEmpty();
            module?.CatalogNumber.Should().Be(new CatalogNumber("1783-ETAP"));
            module?.Vendor.Should().Be(Vendor.Rockwell);
            module?.ProductType.Should().Be(ProductType.Communications);
            module?.ProductCode.Should().NotBe(0);
            module?.Inhibited.Should().BeFalse();
            module?.MajorFault.Should().BeFalse();
            module?.SafetyEnabled.Should().BeFalse();
            module?.Slot.Should().Be(0);
            module?.IP.Should().Be(IPAddress.Parse("2.2.2.2"));
            module?.State.Should().Be(KeyingState.CompatibleModule);
            module?.ParentModule.Should().Be("Test");
            module?.ParentPortId.Should().Be(2);
            module?.Ports.Should().HaveCount(1);
        }
        
        [Test]
        public void New_ValidModule_SlotAndIPPortModule_ShouldNotBeNull()
        {
            var bus = CreateChassisBus();

            var module = bus?.New("Child", "1756-EN2T", 1, IPAddress.Parse("1.1.1.1"));

            module.Should().NotBeNull();
            module?.Ports.Should().HaveCount(2);
        }

        [Test]
        public void New_ValidModule_BusShouldContainChild()
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
        public void New_InvalidModuleType_ShouldThrowArgumentException()
        {
            var module = new Module("Test", "1756-EN2T", IPAddress.Any);

            var bus = module.Ports.Local()?.Bus!;

            FluentActions.Invoking(() => bus?.New("Test", "5094-IY8")).Should().Throw<ArgumentException>();
        }

        [Test]
        public void New_IsFull_ShouldThrowInvalidArgumentException()
        {
            var module = new Module("Test", "1756-EN2T", IPAddress.Any);
        }

        [Test]
        public void New_ModuleDefinitionOverload_NullDefinition_ShouldThrowArgumentNullException()
        {
            var bus = CreateChassisBus();

            FluentActions.Invoking(() => bus.New("Test", ((ModuleDefinition)null)!)).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void New_ModuleDefinitionOverload_ExistingName_ShouldThrowComponentNameCollisionException()
        {
            var catalog = new ModuleCatalog();
            var definition = catalog.Lookup("1756-IF8");
            var bus = CreateChassisBus();

            FluentActions.Invoking(() => bus.New("Test", definition)).Should().Throw<ComponentNameCollisionException>();
        }

        [Test]
        public void New_ModuleDefinitionOverload_NotUpstreamPort_ShouldThrowArgumentException()
        {
            var catalog = new ModuleCatalog();
            var definition = catalog.Lookup("1756-IF8");
            var bus = CreateChassisBus();

            FluentActions.Invoking(() => bus.New("Child", definition)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void New_ModuleDefinitionOverload_InvalidModuleType_ShouldThrowArgumentException()
        {
            var catalog = new ModuleCatalog();
            var definition = catalog.Lookup("5094-IY8");
            var connecting = definition.Ports.First();
            connecting.Upstream = true;
            connecting.Address = "1";
            var bus = CreateChassisBus();

            FluentActions.Invoking(() => bus.New("Child", definition)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void New_ModuleDefinitionOverload_UnavailableAddress_ShouldThrowArgumentException()
        {
            var catalog = new ModuleCatalog();
            var definition = catalog.Lookup("1756-IF8");
            var connecting = definition.Ports.First();
            connecting.Upstream = true;
            connecting.Address = "0";
            var bus = CreateChassisBus();

            FluentActions.Invoking(() => bus.New("Child", definition)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void New_ModuleDefinitionOverload_ValidDefinition_ShouldNotBeNull()
        {
            var catalog = new ModuleCatalog();
            var definition = catalog.Lookup("1756-IF8");
            var connecting = definition.Ports.First();
            connecting.Upstream = true;
            connecting.Address = "1";
            var bus = CreateChassisBus();

            var module = bus.New("Child", definition);

            module.Should().NotBeNull();
        }

        [Test]
        public void RemoveAt_ParentAddress_ShouldBeFalse()
        {
            var bus = CreateChassisBus();

            var result = bus.RemoveAt("0");

            result.Should().BeFalse();
        }

        [Test]
        public void RemoveAt_InValidAddress_ShouldBeFalse()
        {
            var bus = CreateChassisBus();
            bus.New("Child1", "1756-IF8", 1);
            bus.New("Child2", "1756-IF8", 2);

            var result = bus.RemoveAt("3");

            result.Should().BeFalse();
        }

        [Test]
        public void RemoveAt_ValidAddress_ShouldBeTrue()
        {
            var bus = CreateChassisBus();
            bus.New("Child1", "1756-IF8", 1);
            bus.New("Child2", "1756-IF8", 2);

            var result = bus.RemoveAt("1");

            result.Should().BeTrue();
        }

        [Test]
        public void Remove_ParentName_ShouldBeFalse()
        {
            var bus = CreateChassisBus();

            var result = bus.Remove("Test");

            result.Should().BeFalse();
        }

        [Test]
        public void Remove_InValidName_ShouldBeFalse()
        {
            var bus = CreateChassisBus();
            bus.New("Child1", "1756-IF8", 1);
            bus.New("Child2", "1756-IF8", 2);

            var result = bus.Remove("Child3");

            result.Should().BeFalse();
        }

        [Test]
        public void Remove_ValidName_ShouldBeTrue()
        {
            var bus = CreateChassisBus();
            bus.New("Child1", "1756-IF8", 1);
            bus.New("Child2", "1756-IF8", 2);

            var result = bus.Remove("Child1");

            result.Should().BeTrue();
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
        
        private static Bus CreateEthernetBus()
        {
            var module = new Module("Test", "1756-EN2T", 1, IPAddress.Parse("1.1.1.1"));
            return module.Ports.Local()?.Bus;
        }
    }
}