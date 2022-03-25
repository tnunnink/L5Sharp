using System;
using System.Collections;
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
        public void New_NullPort_ShouldThrowNewArgumentNullException()
        {
            FluentActions.Invoking(() => new Bus(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void New_ValidPort_ShouldNotBeNull()
        {
            var bus = new Bus(new Port(1, "ICP"));

            bus.Should().NotBeNull();
        }

        [Test]
        public void New_ValidPort_ShouldHaveExpectedType()
        {
            var bus = new Bus(new Port(1, "ICP", "0"));

            bus.Type.Should().Be("ICP");
        }

        [Test]
        public void New_NullModule_ShouldNotBeNull()
        {
            FluentActions.Invoking(() => new Bus(new Port(1, "ICP", "0"), ((IModule)null)!)).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void New_ValidModule_ShouldNotBeNull()
        {
            var module = new Module("Test", "1756-EN2T", IPAddress.Any);
            var bus = new Bus(new Port(1, "ICP", "0"), module);

            bus.Should().NotBeNull();
        }

        [Test]
        public void Count_ValidPortNoModule_ShouldBeZero()
        {
            var bus = new Bus(new Port(1, "ICP", "0"));

            bus.Count.Should().Be(0);
        }

        [Test]
        public void Count_ValidWithModule_ShouldNotBeOne()
        {
            var module = new Module("Test", "1756-EN2T", IPAddress.Any);
            var bus = new Bus(new Port(1, "ICP", "0"), module);

            bus.Count.Should().Be(1);
        }

        [Test]
        public void Size_NoSizeSpecifiedPort_ShouldBeZero()
        {
            var bus = new Bus(new Port(1, "ICP", "0"));

            bus.Size.Should().Be(0);
        }
        
        [Test]
        public void Size_SizeSpecifiedPort_ShouldBeExpectedValue()
        {
            var bus = new Bus(new Port(1, "ICP", "0", false, 10));

            bus.Size.Should().Be(10);
        }

        [Test]
        public void IsEmpty_NoModules_ShouldBeTrue()
        {
            var bus = new Bus(new Port(1, "ICP", "0"));

            bus.IsEmpty.Should().BeTrue();
        }

        [Test]
        public void IsEmpty_OneModule_ShouldBeFalse()
        {
            var module = new Module("Test", "1756-EN2T", IPAddress.Any);
            var bus = new Bus(new Port(1, "ICP", "0"), module);

            bus.IsEmpty.Should().BeFalse();
        }
        
        [Test]
        public void IsFull_SizeIsOneAndModuleAdded_ShouldBeTrue()
        {
            var module = new Module("Test", "1756-EN2T", IPAddress.Any);
            var bus = new Bus(new Port(1, "ICP", "0", false, 1), module);

            bus.IsFull.Should().BeTrue();
        }
        
        [Test]
        public void IsFull_NoSizePort_ShouldBeFalse()
        {
            var bus = new Bus(new Port(1, "ICP", "0"));

            bus.IsFull.Should().BeFalse();
        }

        [Test]
        public void IsFixed_NoSizePort_ShouldBeTure()
        {
            var bus = new Bus(new Port(1, "ICP", "0", false, 10));

            bus.IsFixed.Should().BeTrue();
        }

        [Test]
        public void IsFixed_NoSizePort_ShouldBeFalse()
        {
            var bus = new Bus(new Port(1, "ICP", "0"));

            bus.IsFixed.Should().BeFalse();
        }

        [Test]
        public void IsBackplane_ICPPort_ShouldBeTrue()
        {
            var bus = new Bus(new Port(1, "ICP", "0"));

            bus.Should().NotBeNull();
            bus.IsBackplane.Should().BeTrue();
        }

        [Test]
        public void IsBackplane_EthernetPort_ShouldBeFalse()
        {
            var bus = new Bus(new Port(1, "Ethernet", "1"));

            bus.Should().NotBeNull();
            bus.IsBackplane.Should().BeFalse();
        }

        [Test]
        public void IsEthernet_EthernetTypePort_ShouldBeTrue()
        {
            var bus = new Bus(new Port(1, "Ethernet", "1.2.3.4"));

            bus.Should().NotBeNull();
            bus.IsEthernet.Should().BeTrue();
        }

        [Test]
        public void IsEthernet_ICPPort_ShouldBeFalse()
        {
            var bus = new Bus(new Port(1, "ICP", "1"));

            bus.Should().NotBeNull();
            bus.IsEthernet.Should().BeFalse();
        }

        [Test]
        public void Index_ValidAddress_ShouldBeExpected()
        {
            var bus = CreateBackplane();

            var module = bus["0"];

            module.Should().NotBeNull();
            module?.Name.Should().Be("Test");
        }

        [Test]
        public void Index_InvalidAddress_ShouldBeNull()
        {
            var bus = CreateBackplane();

            var module = bus["1"];

            module.Should().BeNull();
        }

        [Test]
        public void Add_Null_ShouldThrowArgumentNullException()
        {
            var bus = CreateBackplane();

            FluentActions.Invoking(() => bus?.Add(null!)).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void Add_DuplicateName_ShouldThrowArgumentNullException()
        {
            var bus = CreateBackplane();
            var module = new Module("Test", "1756-EN2T");

            FluentActions.Invoking(() => bus?.Add(module)).Should()
                .Throw<ComponentNameCollisionException>();
        }

        [Test]
        public void Add_NoUpstreamPort_ShouldThrowArgumentException()
        {
            var bus = CreateBackplane();
            
            var module = new Module("Child", "1756-EN2T");

            FluentActions.Invoking(() => bus.Add(module)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Add_ValidModule_ShouldHaveExpectedCount()
        {
            var bus = CreateBackplane();
            var module = new Module("Child", "1756-IF8", 1);

            bus.Add(module);

            bus.Should().HaveCount(2);
        }

        [Test]
        public void AddMany_ValidModules_ShouldHaveExpectedCount()
        {
            
        }

        [Test]
        public void AddressOf_ValidModule_ShouldBeExpectedValue()
        {
            var bus = CreateBackplane();

            var address = bus.AddressOf("Test");

            address.Should().Be("0");
        }

        [Test]
        public void AddressOf_NonExisting_ShouldBeNull()
        {
            var bus = CreateBackplane();

            var address = bus.AddressOf("Fake");

            address.Should().BeNull();
        }

        [Test]
        public void ContainsAddress_Existing_ShouldBeTrue()
        {
            var bus = CreateBackplane();

            var result = bus.ContainsAddress("0");

            result.Should().BeTrue();
        }

        [Test]
        public void ContainsAddress_NonExisting_ShouldBeFalse()
        {
            var bus = CreateBackplane();

            var result = bus.ContainsAddress("11");

            result.Should().BeFalse();
        }

        [Test]
        public void ContainsName_Existing_ShouldBeTrue()
        {
            var bus = CreateBackplane();

            var result = bus.ContainsName("Test");

            result.Should().BeTrue();
        }

        [Test]
        public void ContainsName_NonExisting_ShouldBeFalse()
        {
            var bus = CreateBackplane();

            var result = bus.ContainsName("Fake");

            result.Should().BeFalse();
        }

        [Test]
        public void Clear_NoModules_ShouldStillHaveParentModule()
        {
            var bus = CreateBackplane();

            bus.Clear();

            bus.Should().HaveCount(1);
            bus.Should().Contain(m => m.Name == "Test");
        }

        [Test]
        public void Clear_Modules_ShouldHaveExpectedCount()
        {
            var bus = CreateBackplane();
            bus.Create("Child1", "1756-IF8", 1);
            bus.Create("Child2", "1756-IF8", 2);

            bus.Clear();

            bus.Should().HaveCount(1);
            bus.Should().Contain(m => m.Name == "Test");
        }

        [Test]
        public void Create_ExistingModuleName_ShouldThrowComponentNameCollisionException()
        {
            var bus = CreateBackplane();

            FluentActions.Invoking(() => bus?.Create("Test", "1756-IF8")).Should()
                .Throw<ComponentNameCollisionException>();
        }

        [Test]
        public void Create_NonExistingModule_ShouldThrowModuleNotFoundException()
        {
            var bus = CreateBackplane();

            FluentActions.Invoking(() => bus?.Create("Test", "1756-ABCD")).Should().Throw<ModuleNotFoundException>();
        }

        [Test]
        public void Create_InvalidModuleType_ShouldThrowArgumentException()
        {
            var bus = CreateBackplane();

            FluentActions.Invoking(() => bus?.Create("Child", "5094-IY8")).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Create_IsFull_ShouldThrowInvalidOperationException()
        {
            var bus = new Bus(new Port(1, "ICP", "0", false, 1), new Module("Test", "1756-EN2T"));

            FluentActions.Invoking(() => bus.Create("Test", "1756-IF8", 1)).Should().Throw<InvalidOperationException>();
        }
        
        [Test]
        public void Create_ValidModule_ShouldNotBeNull()
        {
            var bus = CreateBackplane();

            var module = bus.Create("Child", "1756-IF8", 1);

            module.Should().NotBeNull();
        }

        [Test]
        public void Create_ValidModule_ModuleShouldHaveExpectedProperties()
        {
            var bus = CreateBackplane();

            var module = bus.Create("Child", "1756-IF8", 1);

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
            module.IP.Should().Be(IPAddress.Parse("0.0.0.0"));
            module.State.Should().Be(KeyingState.CompatibleModule);
            module.ParentModule.Should().Be("Test");
            module.ParentPortId.Should().Be(1);
            module.Ports.Should().HaveCount(1);
        }

        [Test]
        public void Create_SlotAndIpOverride_ShouldHaveExpectedSlotAndIp()
        {
            var bus = CreateBackplane();

            var module = bus.Create("Child", "1756-EN2T", 1, IPAddress.Parse("1.1.1.1"));

            module.Slot.Should().Be(1);
            module.IP.Should().Be(IPAddress.Parse("1.1.1.1"));
        }

        [Test]
        public void Create_ValidModule_BusShouldContainChild()
        {
            var bus = CreateBackplane();

            var module = bus.Create("Child", "1756-IF8", 1);

            var child = bus["1"];

            child.Should().BeSameAs(module);
        }
        
        [Test]
        public void Create_EthernetBus_ShouldNotBeNull()
        {
            var parent = new Module("Test", "1756-EN2T", 1, IPAddress.Parse("1.2.3.4"));
            
            var module = parent.Bus.Ethernet()?.Create("Child", "1783-ETAP", 0, IPAddress.Parse("2.2.2.2"));

            module.Should().NotBeNull();
        }
        
        [Test]
        public void Create_EthernetBus_ShouldHaveExpectedProperties()
        { 
            var parent = new Module("Test", "1756-EN2T", 1, IPAddress.Parse("1.2.3.4"));
            
            var module = parent.Bus.Ethernet()?.Create("Child", "1783-ETAP", 0, IPAddress.Parse("2.2.2.2"));

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
        public void Create_DuplicateIPAddress_ShouldAssignNextAvailable()
        {
            var parent = new Module("Test", "1756-EN2T", 1, IPAddress.Parse("1.2.3.4"));
            
            var module = parent.Bus.Ethernet()?.Create("Child", "1783-ETAP", 0, IPAddress.Parse("1.2.3.4"));

            module?.IP.Should().Be(IPAddress.Parse("192.168.1.1"));
        }
        
        [Test]
        public void Create_DuplicateIPAddressOnDefaultNetwork_ShouldAssignNextAvailable()
        {
            var parent = new Module("Test", "1756-EN2T", 1, IPAddress.Parse("192.168.1.1"));
            
            var module = parent.Bus.Ethernet()?.Create("Child", "1783-ETAP", 0, IPAddress.Parse("192.168.1.1"));

            module?.IP.Should().Be(IPAddress.Parse("192.168.1.2"));
        }

        [Test]
        public void Create_ModuleDefinitionOverloadWithNullDefinition_ShouldThrowArgumentNullException()
        {
            var bus = CreateBackplane();

            FluentActions.Invoking(() => bus.Create("Test", ((ModuleDefinition)null)!)).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void Create_ModuleDefinitionOverloadWithExistingName_ShouldThrowComponentNameCollisionException()
        {
            var catalog = new ModuleCatalog();
            var definition = catalog.Lookup("1756-IF8");
            var bus = CreateBackplane();

            FluentActions.Invoking(() => bus.Create("Test", definition)).Should().Throw<ComponentNameCollisionException>();
        }

        [Test]
        public void Create_ModuleDefinitionOverloadWithNotUpstreamPort_ShouldThrowArgumentException()
        {
            var catalog = new ModuleCatalog();
            var definition = catalog.Lookup("1756-IF8");
            var bus = CreateBackplane();

            FluentActions.Invoking(() => bus.Create("Child", definition)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Create_ModuleDefinitionOverloadWithInvalidModuleType_ShouldThrowArgumentException()
        {
            var catalog = new ModuleCatalog();
            var definition = catalog.Lookup("5094-IY8").ConfigurePorts("5094", "1", "0.0.0.0");
            var bus = CreateBackplane();

            FluentActions.Invoking(() => bus.Create("Child", definition)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Create_ModuleDefinitionOverloadWithUnavailableAddress_ShouldThrowArgumentException()
        {
            var catalog = new ModuleCatalog();
            var definition = catalog.Lookup("1756-IF8").ConfigurePorts("ICP", "0", "0.0.0.0");
            var bus = CreateBackplane();

            FluentActions.Invoking(() => bus.Create("Child", definition)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Create_ModuleDefinitionOverloadWithValidDefinition_ShouldNotBeNull()
        {
            var catalog = new ModuleCatalog();
            var definition = catalog.Lookup("1756-IF8").ConfigurePorts("ICP", "1", "0.0.0.0");
            var bus = CreateBackplane();

            var module = bus.Create("Child", definition);

            module.Should().NotBeNull();
        }
        
        [Test]
        public void Create_ModuleDefinitionOverloadWithValidDefinition_ShouldHaveExpectedProperties()
        {
            var catalog = new ModuleCatalog();
            var definition = catalog.Lookup("1756-IF8").ConfigurePorts("ICP", "1", "0.0.0.0");
            var bus = CreateBackplane();

            var module = bus.Create("Child", definition);

            module.Should().NotBeNull();
        }

        [Test]
        public void RemoveAt_ParentAddress_ShouldBeFalse()
        {
            var bus = CreateBackplane();

            var result = bus.RemoveAt("0");

            result.Should().BeFalse();
        }

        [Test]
        public void RemoveAt_InValidAddress_ShouldBeFalse()
        {
            var bus = CreateBackplane();
            bus.Create("Child1", "1756-IF8", 1);
            bus.Create("Child2", "1756-IF8", 2);

            var result = bus.RemoveAt("3");

            result.Should().BeFalse();
        }

        [Test]
        public void RemoveAt_ValidAddress_ShouldBeTrue()
        {
            var bus = CreateBackplane();
            bus.Create("Child1", "1756-IF8", 1);
            bus.Create("Child2", "1756-IF8", 2);

            var result = bus.RemoveAt("1");

            result.Should().BeTrue();
        }

        [Test]
        public void Remove_ParentName_ShouldBeFalse()
        {
            var bus = CreateBackplane();

            var result = bus.Remove("Test");

            result.Should().BeFalse();
        }

        [Test]
        public void Remove_InValidName_ShouldBeFalse()
        {
            var bus = CreateBackplane();
            bus.Create("Child1", "1756-IF8", 1);
            bus.Create("Child2", "1756-IF8", 2);

            var result = bus.Remove("Child3");

            result.Should().BeFalse();
        }

        [Test]
        public void Remove_ValidName_ShouldBeTrue()
        {
            var bus = CreateBackplane();
            bus.Create("Child1", "1756-IF8", 1);
            bus.Create("Child2", "1756-IF8", 2);

            var result = bus.Remove("Child1");

            result.Should().BeTrue();
        }

        [Test]
        public void Iterate_WhenPerformed_ShouldWork()
        {
            var bus = CreateBackplane();

            foreach (var module in bus)
            {
                module.Should().NotBeNull();
            }
        }

        [Test]
        public void Enumerate_AsEnumerable_ShouldWork()
        {
            var bus = (IEnumerable)CreateBackplane();

            foreach (var module in bus)
            {
                module.Should().NotBeNull();
            }
        }

        private static Bus CreateBackplane()
        {
            //One way to get a backplane port is get an EN2T with the IP set so that it is the upstream port, making the
            //ICP port the downstream backplane port.
            var module = new Module("Test", "1756-EN2T", IPAddress.Any);
            return module.Bus.Backplane();
        }
    }
}