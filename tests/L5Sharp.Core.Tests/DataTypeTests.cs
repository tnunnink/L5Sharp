using System;
using FluentAssertions;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;
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
        public void Atomic_InvalidName_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => DataType.Atomic("Timer", 1234)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Atomic_NullName_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => DataType.Atomic(null!, 1234)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Atomic_EmptyName_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => DataType.Atomic(string.Empty, 1234)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Atomic_ValidBool_ShouldBeExpected()
        {
            var type = DataType.Atomic("bool", true);

            type.Should().BeOfType<Bool>();
            type.Name.Should().Be("BOOL");
            type.Value.Should().Be(true);
        }

        [Test]
        public void Atomic_ValidSint_ShouldBeExpected()
        {
            var type = DataType.Atomic("sint", "1");

            type.Should().BeOfType<Sint>();
            type.Name.Should().Be("SINT");
            type.Value.Should().Be(1);
        }
        
        [Test]
        public void Complex_NullName_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => DataType.Complex(null!)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Complex_EmptyName_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => DataType.Complex(string.Empty)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Complex_InvalidName_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => DataType.Complex("Bool")).Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void Complex_ValidTimer_ShouldBeExpected()
        {
            var type = DataType.Complex("timer");

            type.Should().BeOfType<Timer>();
            type.Name.Should().Be("TIMER");
        }

        [Test]
        public void Complex_ValidCounter_ShouldBeExpected()
        {
            var type = DataType.Complex("counter");

            type.Should().BeOfType<Counter>();
            type.Name.Should().Be("COUNTER");
        }
    }
}