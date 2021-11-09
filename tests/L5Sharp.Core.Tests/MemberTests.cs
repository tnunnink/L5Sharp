using System;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class MemberTests
    {
        [Test]
        public void Constructor_ValidNameAndType_ShouldNotBeNull()
        {
            var member = new Member<IDataType>("Test", new Bool());

            member.Should().NotBeNull();
        }
        
        [Test]
        public void New_ValidNameAndType_ShouldNotBeNull()
        {
            var member = Member.New("Test", new Bool());

            member.Should().NotBeNull();
        }
        
        [Test]
        public void OfType_ValidNameAndType_ShouldNotBeNull()
        {
            var member = Member.OfType<Bool>("Test");

            member.Should().NotBeNull();
        }

        [Test]
        public void New_ArrayType_ShouldHaveExpectedLength()
        {
            var fixture = new Fixture();
            var length = fixture.Create<ushort>();
            var array = Member.OfType<Dint>("Test", new Dimensions(length));

            array.Elements.Length.Should().Be(length);
            array.Elements.Should().BeOfType<IMember<Dint>[]>();
        }

        [Test]
        public void New_NullName_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Member.OfType<Dint>(null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void New_NullType_ShouldHaveNullType()
        {
            var member = Member.New("Name", null);
            member.DataType.Should().BeNull();
        }

        [Test]
        public void New_OverrideProperties_ShouldHaveExpectedOverloads()
        {
            var member = Member.New("Member", new Real(), new Dimensions(35), Radix.Exponential,
                ExternalAccess.ReadOnly, "Test");

            member.Should().NotBeNull();
            member.Name.Should().Be("Member");
            member.DataType.Should().BeNull();
            member.Dimensions.Length.Should().Be(35);
            member.Radix.Should().Be(Radix.Exponential);
            member.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
            member.Description.Should().Be("Test");
            member.Elements.Length.Should().Be(35);
            member.Elements.Should().AllBeOfType<Member<IDataType>>();
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

        [Test]
        public void Elements_GetValue_ShouldBeEmptyArray()
        {
            var member = Member.OfType<Real>("Member");

            var elements = member.Elements;

            elements.Should().BeEmpty();
        }
    }
}