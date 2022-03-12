using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using L5Sharp.Atomics;
using L5Sharp.Core;
using L5Sharp.Creators;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Internal.Tests.Core
{
    [TestFixture]
    public class StructureTypeTests
    {
        [Test]
        public void New_NullName_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new StructureType(null!)).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var type = new StructureType("Test");

            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_Valid_shouldHaveExpectedValues()
        {
            var type = new StructureType("AB:5000_AI8:O:0");

            type.Name.Should().Be("AB:5000_AI8:O:0");
            type.Description.Should().BeEmpty();
            type.Class.Should().Be(DataTypeClass.Unknown);
            type.Family.Should().Be(DataTypeFamily.None);
            type.Members.Should().BeEmpty();
        }

        [Test]
        public void Instantiate_WhenCalled_ShouldNotBeNull()
        {
            var type = new StructureType("AB:5000_AI8:O:0");

            var instance = type.Instantiate();

            instance.Should().NotBeNull();
            instance.Should().NotBeSameAs(type);
        }
        
        [Test]
        public void Instantiate_HasMembers_ShouldHaveDifferentInstances()
        {
            var type = new StructureType("AB:5000_AI8:O:0", new List<IMember<IDataType>>
            {
                Member.Create<Bool>("DATA"),
                Member.Create<Bool>("FAULTS")
            });

            var instance = (IComplexType) type.Instantiate();

            instance.Should().NotBeSameAs(type);

            foreach (var instanceMember in instance.Members)
            {
                var typeMember = type.Members.Single(m => m.Name == instanceMember.Name);
                instanceMember.Should().NotBeSameAs(typeMember);
            }
        }
    }
}