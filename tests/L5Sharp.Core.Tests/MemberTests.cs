using System;
using System.Collections.Generic;
using FluentAssertions;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class MemberTests
    {
        [Test]
        public void New_ValidNameAndType_ShouldNotBeNull()
        {
            var member = Member.OfType<Real>("Member");

            member.Should().NotBeNull();
        }

        [Test]
        public void New_ArrayType_ShouldNotBeNull()
        {
            var collection = new ComponentCollection<IMember<IDataType>>();
            
            var simple = new Member<IDataType>("Test", new Dint());
            var array = new Member<IDataType>("Test", new Dint(), new Dimensions(10));

            collection.Add(simple);
            collection.Add(array);
        }

        [Test]
        public void New_NullName_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Member.OfType<Dint>(null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void New_NullType_ShouldHaveUndefinedDataType()
        {
            var member = Member.New("Name", null);
            member.DataType.Should().BeNull();
        }

        [Test]
        public void New_OverrideProperties_ShouldNotBeNull()
        {
            var member = Member.New("Member", new Real(), new Dimensions(35), Radix.General,
                ExternalAccess.ReadOnly, "Test");

            member.Should().NotBeNull();
            member.Name.Should().Be("Member");
            member.DataType.Should().Be(new Real());
            member.Dimensions.Length.Should().Be(35);
            member.Radix.Should().Be(Radix.General);
            member.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
            member.Description.Should().Be("Test");
        }

        [Test]
        public void Name_GetValue_ShouldBeExpected()
        {
            var member = Member.OfType<Real>("Member");

            var name = member.Name;

            name.Should().Be("Member");
        }

        [Test]
        public void DataType_GetValue_ShouldBeExpected()
        {
            var member = Member.OfType<Real>("Member");

            var dataType = member.DataType;

            dataType.Should().Be(new Real());
        }

        [Test]
        public void Dimension_GetValue_ShouldBeExpected()
        {
            var member = Member.OfType<Real>("Member");

            var dimension = member.Dimensions;

            dimension.Length.Should().Be(0);
        }

        [Test]
        public void Radix_GetValue_ShouldBeExpected()
        {
            var member = Member.OfType<Real>("Member");

            var radix = member.Radix;

            radix.Should().Be(Radix.Float);
        }

        [Test]
        public void ExternalAccess_GetValue_ShouldBeExpected()
        {
            var member = Member.OfType<Real>("Member");

            var access = member.ExternalAccess;

            access.Should().Be(ExternalAccess.ReadWrite);
        }

        [Test]
        public void Description_GetValue_ShouldBeExpected()
        {
            var member = Member.OfType<Real>("Member");

            var description = member.Description;

            description.Should().BeNull();
        }
    }
}