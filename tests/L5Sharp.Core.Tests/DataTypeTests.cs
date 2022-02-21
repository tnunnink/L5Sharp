using System;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class DataTypeTests
    {
        [Test]
        public void List_WhenCalled_ShouldNotBeEmpty()
        {
            var list = DataType.All;

            list.Should().NotBeEmpty();
        }

        [Test]
        public void Atomics_WhenCalled_ShouldNotBeEmpty()
        {
            var list = DataType.Atomics;

            list.Should().NotBeEmpty();
        }

        [Test]
        public void Predefined_WhenCalled_ShouldNotBeEmpty()
        {
            var list = DataType.Predefined;

            list.Should().NotBeEmpty();
        }

        [Test]
        public void Exists_ExistingName_ShouldBeTrue()
        {
            var exists = DataType.Exists("bool");

            exists.Should().BeTrue();
        }

        [Test]
        public void Exists_NonExistingName_ShouldBeFalse()
        {
            var exists = DataType.Exists("SomeType");

            exists.Should().BeFalse();
        }

        [Test]
        public void Create_Null_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => DataType.Create(null!)).Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void Create_Empty_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => DataType.Create(string.Empty)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Create_ExistingType_ShouldNotBeNull()
        {
            var dataType = DataType.Create("Bool");

            dataType.Should().NotBeNull();
        }

        [Test]
        public void Create_ExistingType_ShouldBeExpected()
        {
            var dataType = DataType.Create("Bool");

            dataType.Should().BeEquivalentTo(new Bool());
        }

        [Test]
        public void Create_NonExistingType_ShouldBeUndefined()
        {
            var dataType = DataType.Create("SomeType");

            dataType.Should().BeEquivalentTo(new Undefined("SomeType"));
        }

        [Test]
        public void Atomic_ValidName_ShouldNotBeNull()
        {
            var type = DataType.Atomic("bool", Radix.Binary, true);

            type.Name.Should().Be("BOOL");
            type.Value.Should().Be(true);
        }
    }
}