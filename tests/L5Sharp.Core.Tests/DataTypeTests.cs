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
            var list = LogixType.All;

            list.Should().NotBeEmpty();
        }

        [Test]
        public void Atomics_WhenCalled_ShouldNotBeEmpty()
        {
            var list = LogixType.Atomics;

            list.Should().NotBeEmpty();
        }

        [Test]
        public void Predefined_WhenCalled_ShouldNotBeEmpty()
        {
            var list = LogixType.Predefined;

            list.Should().NotBeEmpty();
        }

        [Test]
        public void Exists_ExistingName_ShouldBeTrue()
        {
            var exists = LogixType.IsDefined("bool");

            exists.Should().BeTrue();
        }

        [Test]
        public void Exists_NonExistingName_ShouldBeFalse()
        {
            var exists = LogixType.IsDefined("SomeType");

            exists.Should().BeFalse();
        }

        [Test]
        public void Create_Null_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => LogixType.Create(null!)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Create_Empty_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => LogixType.Create(string.Empty)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Create_ExistingType_ShouldNotBeNull()
        {
            var dataType = LogixType.Create("BOOL");

            dataType.Should().NotBeNull();
        }

        [Test]
        public void Create_ExistingType_ShouldBeExpected()
        {
            var dataType = LogixType.Create("BOOL");

            dataType.Should().BeEquivalentTo(new BOOL());
        }

        [Test]
        public void Create_NonExistingType_ShouldBeUndefined()
        {
            var dataType = LogixType.Create("SomeType");

            dataType.Should().BeEquivalentTo(new UNDEFINED("SomeType"));
        }

        [Test]
        public void Atomic_InvalidName_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => LogixType.Atomic("Timer", 1234)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Atomic_NullName_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => LogixType.Atomic(null!, 1234)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Atomic_EmptyName_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => LogixType.Atomic(string.Empty, 1234)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Atomic_ValidBool_ShouldBeExpected()
        {
            var type = LogixType.Atomic("bool", true);

            type.Should().BeOfType<BOOL>();
            type.Name.Should().Be("BOOL");
            type.Value.Should().Be(true);
        }

        [Test]
        public void Atomic_ValidSint_ShouldBeExpected()
        {
            var type = LogixType.Atomic("sint", "1");

            type.Should().BeOfType<SINT>();
            type.Name.Should().Be("SINT");
            type.Value.Should().Be(1);
        }
        
        [Test]
        public void Complex_NullName_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => LogixType.Complex(null!)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Complex_EmptyName_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => LogixType.Complex(string.Empty)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Complex_InvalidName_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => LogixType.Complex("BOOL")).Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void Complex_ValidTimer_ShouldBeExpected()
        {
            var type = LogixType.Complex("timer");

            type.Should().BeOfType<TIMER>();
            type.Name.Should().Be("TIMER");
        }

        [Test]
        public void Complex_ValidCounter_ShouldBeExpected()
        {
            var type = LogixType.Complex("counter");

            type.Should().BeOfType<COUNTER>();
            type.Name.Should().Be("COUNTER");
        }
    }
}