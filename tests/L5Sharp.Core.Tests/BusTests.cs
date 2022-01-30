using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using L5Sharp.Exceptions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class BusTests
    {
        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var bus = new Bus();

            bus.Should().NotBeNull();
        }

        [Test]
        public void New_Default_ShouldBeEmpty()
        {
            var bus = new Bus();

            bus.IsEmpty.Should().BeTrue();
        }

        [Test]
        public void New_Default_ShouldNotBeFull()
        {
            var bus = new Bus();

            bus.IsFull.Should().BeFalse();
        }

        [Test]
        public void New_Default_SizeShouldBeNull()
        {
            var bus = new Bus();

            bus.Size.Should().BeNull();
        }

        [Test]
        public void New_Default_CountShouldBeZero()
        {
            var bus = new Bus();

            bus.Count.Should().Be(0);
        }

        [Test]
        public void New_SizeOverload_SizeShouldBeExpected()
        {
            var bus = new Bus(10);

            bus.Size.Should().Be(10);
        }

        [Test]
        public void New_SizeOverload_CountShouldBeZero()
        {
            var bus = new Bus(10);

            bus.Count.Should().Be(0);
        }

        [Test]
        public void New_SizeOverload_ShouldBeEmpty()
        {
            var bus = new Bus(10);

            bus.IsEmpty.Should().BeTrue();
        }

        [Test]
        public void New_SizeOverload_ShouldNotBeFull()
        {
            var bus = new Bus(10);

            bus.IsFull.Should().BeFalse();
        }

        [Test]
        public void New_SizeOverload_ShouldContainAllNull()
        {
            var bus = new Bus(10);

            bus.Should().AllBeEquivalentTo<IModule>(null);
        }

        [Test]
        public void New_SizeOverLoad()
        {
            
        }

        [Test]
        public void New_SizeAndBaudOverload_BaudShouldBeExpected()
        {
            var bus = new Bus(10, 54.3f);

            bus.Baud.Should().Be(54.3f);
        }

        [Test]
        public void Add_NullModule_ShouldThrowArgumentNullException()
        {
            var bus = new Bus();

            FluentActions.Invoking(() => bus.Add(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Add_IsFull_ShouldThrowInvalidOperationException()
        {
            var bus = new Bus(0);
            var module = CreateValidModule();

            FluentActions.Invoking(() => bus.Add(module))
                .Should().Throw<InvalidOperationException>()
                .WithMessage(
                    "The current Bus is full and can not add new modules. Remove modules before adding further items.");
        }

        [Test]
        public void Add_NullPort_ShouldThrowInvalidOperationException()
        {
            var bus = new Bus();
            var definition = new ModuleDefinition("1756-AENT", Vendor.Unkown, ProductType.Unkown, new Revision(1, 1));
            var module = new Module("Test", definition, null!, "This is a test");

            FluentActions.Invoking(() => bus.Add(module))
                .Should().Throw<ArgumentException>()
                .WithMessage("The provided Module does not have an upstream port." +
                             " In order to add a Module to the current Bus, it must have an upstream port defined");
        }

        [Test]
        public void Add_NoUpstreamPort_ShouldThrowInvalidOperationException()
        {
            var bus = new Bus();
            var port = new Port(2, "192.168.1.1", "Ethernet", false);
            var definition = new ModuleDefinition("1756-AENT", Vendor.Unkown, ProductType.Unkown, new Revision(1, 1));
            var module = new Module("Test", definition, port, "This is a test");

            FluentActions.Invoking(() => bus.Add(module))
                .Should().Throw<ArgumentException>()
                .WithMessage("The provided Module does not have an upstream port." +
                             " In order to add a Module to the current Bus, it must have an upstream port defined");
        }

        [Test]
        public void Add_ValidEthernetPortModule_ShouldHaveExpectedCount()
        {
            var bus = new Bus();
            var port = new Port(2, "192.168.1.1", "Ethernet", true);
            var definition = new ModuleDefinition("1756-AENT", Vendor.Unkown, ProductType.Unkown, new Revision(1, 1));
            var module = new Module("Test", definition, port, "This is a test");

            bus.Add(module);

            bus.Should().HaveCount(1);
        }

        [Test]
        public void Add_NullSizeNonEthernet_ShouldFailIGuess()
        {
            var bus = new Bus();
            var module = CreateValidModule();

            bus.Add(module);
            
            
        }

        [Test]
        public void Add_NonParsableSlotNumberModule_ShouldThrowArgumentException()
        {
            var bus = new Bus();
            var port = new Port(1, "1.1", "ICP", true);
            var definition = new ModuleDefinition("1756-AENT", Vendor.Unkown, ProductType.Unkown, new Revision(1, 1));
            var module = new Module("Test", definition, port, "This is a test");

            FluentActions.Invoking(() => bus.Add(module))
                .Should().Throw<ArgumentException>()
                .WithMessage("Could not determine the slot number for the provided Module Address '1.1'");
        }

        [Test]
        public void Add_NegativeSlotNumberModule_ShouldThrowArgumentException()
        {
            var bus = new Bus();
            var port = new Port(1, "-1", "ICP", true);
            var definition = new ModuleDefinition("1756-AENT", Vendor.Unkown, ProductType.Unkown, new Revision(1, 1));
            var module = new Module("Test", definition, port, "This is a test");

            FluentActions.Invoking(() => bus.Add(module))
                .Should().Throw<ArgumentException>()
                .WithMessage(
                    "The provided Module slot number is out of range for the current Bus size. (Parameter 'slot')");
        }

        [Test]
        public void Add_LargerThanSizeSlotNumberModule_ShouldThrowArgumentException()
        {
            var bus = new Bus(10);
            var port = new Port(1, "11", "ICP", true);
            var definition = new ModuleDefinition("1756-AENT", Vendor.Unkown, ProductType.Unkown, new Revision(1, 1));
            var module = new Module("Test", definition, port, "This is a test");

            FluentActions.Invoking(() => bus.Add(module))
                .Should().Throw<ArgumentException>()
                .WithMessage(
                    "The provided Module slot number is out of range for the current Bus size. (Parameter 'slot')");
        }

        [Test]
        public void Add_ValidSlotNumber_ShouldHaveExpectedCount()
        {
            var bus = new Bus(10);
            var port = new Port(1, "1", "ICP", true);
            var definition = new ModuleDefinition("1756-AENT", Vendor.Unkown, ProductType.Unkown, new Revision(1, 1));
            var module = new Module("Test", definition, port, "This is a test");

            bus.Add(module);

            bus.Where(m => m is not null).Should().HaveCount(1);
        }

        [Test]
        public void Add_MultipleTimes_ShouldThrowComponentNameCollisionException()
        {
            var bus = new Bus(10);
            var module = CreateValidModule();

            bus.Add(module);

            FluentActions.Invoking(() => bus.Add(module)).Should().Throw<ComponentNameCollisionException>();
        }

        [Test]
        public void Add_ModuleWithSameSlotNumber_ShouldThrowInvalidOperationException()
        {
            var bus = new Bus(10);
            var module1 = CreateValidModule();
            bus.Add(module1);

            var port = new Port(1, "1", "ICP", true);
            var definition = new ModuleDefinition("1756-AENT", Vendor.Unkown, ProductType.Unkown, new Revision(1, 1));
            var module2 = new Module("Module2", definition, port, "This is a test");

            FluentActions.Invoking(() => bus.Add(module2)).Should().Throw<InvalidOperationException>()
                .WithMessage("The current Bus already has a module at the specified slot number 1");
        }

        [Test]
        public void AddMany_NullCollection_ShouldThrowArgumentNullException()
        {
            var bus = new Bus(10);

            FluentActions.Invoking(() => bus.AddMany(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void AddMany_ValidCollection_ShouldHaveExpectedCount()
        {
            var m1 = new Module("Test1",
                new ModuleDefinition("1756-AENT", Vendor.Unkown, ProductType.Unkown, new Revision(1, 1)),
                new Port(1, "1", "ICP", true));

            var m2 = new Module("Test2",
                new ModuleDefinition("1756-AENT", Vendor.Unkown, ProductType.Unkown, new Revision(1, 1)),
                new Port(1, "2", "ICP", true));

            var m3 = new Module("Test3",
                new ModuleDefinition("1756-AENT", Vendor.Unkown, ProductType.Unkown, new Revision(1, 1)),
                new Port(1, "3", "ICP", true));

            var modules = new List<IModule>()
            {
                m1, m2, m3
            };
            var bus = new Bus(7);

            bus.AddMany(modules);

            bus.Count.Should().Be(3);
        }

        [Test]
        public void Contains_NoModules_ShouldBeTrue()
        {
            var bus = new Bus(10);

            var contains = bus.Contains("Test");

            contains.Should().BeFalse();
        }

        [Test]
        public void Contains_ValidModule_ShouldBeTrue()
        {
            var bus = new Bus(10);
            var module = CreateValidModule();
            bus.Add(module);

            var contains = bus.Contains("Test");

            contains.Should().BeTrue();
        }

        [Test]
        public void Index_InvalidSlot_ShouldThrowArgumentOutOfRangeException()
        {
            var bus = new Bus(7);

            FluentActions.Invoking(() => bus[-1]).Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Index_ValidNullSlot_ShouldBeNull()
        {
            var bus = new Bus(7);

            var module = bus[0];

            module.Should().BeNull();
        }

        [Test]
        public void Index_ValidModuleSlot_ShouldNotBeNull()
        {
            var bus = new Bus(7);
            var module = CreateValidModule();
            bus.Add(module);

            var slot = bus[1];

            slot.Should().NotBeNull();
            slot.Should().BeSameAs(module);
        }

        [Test]
        public void Index_DoesNotHaveName_ShouldBeNull()
        {
            var bus = new Bus(7);

            var module = bus["ModuleName"];

            module.Should().BeNull();
        }

        [Test]
        public void Index_HasName_ShouldNotBeNull()
        {
            var bus = new Bus(7);
            var validModule = CreateValidModule();
            bus.Add(validModule);

            var module = bus["Test"];

            module.Should().NotBeNull();
        }

        [Test]
        public void Remove_ValidName_ShouldBeTrue()
        {
            var bus = new Bus(10);
            var module = CreateValidModule();
            bus.Add(module);

            var result = bus.Remove("Test");

            result.Should().BeTrue();
        }

        [Test]
        public void Remove_ValidName_ShouldNotHaveModuleWithName()
        {
            var bus = new Bus(10);
            var module = CreateValidModule();
            bus.Add(module);

            bus.Remove("Test");

            var result = bus["Test"];
            result.Should().BeNull();
            bus.Count.Should().Be(0);
        }

        [Test]
        public void Remove_InvalidName_ShouldBeFalse()
        {
            var bus = new Bus(10);
            var module = CreateValidModule();
            bus.Add(module);

            var result = bus.Remove("TestModule");

            result.Should().BeFalse();
        }

        [Test]
        public void Remove_InvalidName_ShouldStillHaveSameCount()
        {
            var bus = new Bus(10);
            var module = CreateValidModule();
            bus.Add(module);

            bus.Remove("TestModule");

            bus.Count.Should().Be(1);
        }

        [Test]
        public void RemoveAt_InvalidIndex_ShouldThrowArgumentOutOfRangeException()
        {
            var bus = new Bus(10);
            var module = CreateValidModule();
            bus.Add(module);

            FluentActions.Invoking(() => bus.RemoveAt(10)).Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void RemoveAt_ValidIndex_IndexShouldBeNull()
        {
            var bus = new Bus(10);
            var module = CreateValidModule();
            bus.Add(module);

            bus.RemoveAt(1);

            bus[1].Should().BeNull();
        }

        [Test]
        public void TryAdd_Null_ShouldThrowArgumentNullException()
        {
            var bus = new Bus(10);

            FluentActions.Invoking(() => bus.TryAdd(null!)).Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void TryAdd_BusAlreadyHasName_ShouldBeFalse()
        {
            var bus = new Bus(10);
            var module = CreateValidModule();
            bus.Add(module);

            var result = bus.TryAdd(module);

            result.Should().BeFalse();
        }
        
        [Test]
        public void TryAdd_SlotAlreadyHasModule_ShouldBeFalse()
        {
            var bus = new Bus(10);
            var port = new Port(1, "1", "ICP", true);
            var definition = new ModuleDefinition("1756-AENT", Vendor.Unkown, ProductType.Unkown, new Revision(1, 1));
            var first = new Module("First", definition, port, "This is a test");
            bus.Add(first);

            var result = bus.TryAdd(CreateValidModule());

            result.Should().BeFalse();
        }
        
        [Test]
        public void TryAdd_Valid_ShouldBeTrue()
        {
            var bus = new Bus(10);
            var module = CreateValidModule();

            var result = bus.TryAdd(module);

            result.Should().BeTrue();
        }
        
        [Test]
        public void TryAdd_Valid_SlotShouldContainModule()
        {
            var bus = new Bus(10);
            var module = CreateValidModule();

            bus.TryAdd(module);

            bus[1].Should().BeSameAs(module);
        }

        private static IModule CreateValidModule()
        {
            var port = new Port(1, "1", "ICP", true);
            var definition = new ModuleDefinition("1756-AENT", Vendor.Unkown, ProductType.Unkown, new Revision(1, 1));
            return new Module("Test", definition, port, "This is a test");
        }
    }
}