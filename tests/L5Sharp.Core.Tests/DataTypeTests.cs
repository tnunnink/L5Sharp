using System;
using FluentAssertions;
using L5Sharp.Exceptions;
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
        public void IsRegistered_RegisteredName_ShouldBeTrue()
        {
            var exists = DataType.Exists("bool");

            exists.Should().BeTrue();
        }

        [Test]
        public void IsRegistered_NonRegisteredName_ShouldBeFalse()
        {
            var exists = DataType.Exists("SomeType");

            exists.Should().BeFalse();
        }

        [Test]
        public void Create_ExistingType_ShouldNotBeNull()
        {
            var dataType = DataType.Create("Bool");

            dataType.Should().NotBeNull();
        }

        [Test]
        public void Create_ExistingType_ShouldNotBeExpected()
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
    }
}