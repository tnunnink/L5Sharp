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
        public void Create_ValidNameAndType_ShouldNotBeNull()
        {
            var type = new UserDefined("Test");
            var member = Member.Create("Test", type);

            member.Should().NotBeNull();
        }
        
        [Test]
        public void Create_TypedValidNameAndType_ShouldNotBeNull()
        {
            var member = Member.Create("Test", new Bool());

            member.Should().NotBeNull();
        }

        [Test]
        public void Create_TypedValidName_ShouldNotBeNull()
        {
            var member = Member.Create<Bool>("Test");

            member.Should().NotBeNull();
        }

        [Test]
        public void New_ArrayType_ShouldHaveExpectedLength()
        {
            var fixture = new Fixture();
            var length = fixture.Create<ushort>();
            var array = Member.Create<Dint>("Test", new Dimensions(length));

            array.Dimension.Length.Should().Be(length);
            array.Should().AllBeOfType<Member<Dint>>();
        }

        [Test]
        public void New_NullName_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Member.Create<Dint>(null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void New_NullType_ShouldHaveNullType()
        {
            var member = Member.Create("Name", null);
            member.DataType.Should().BeNull();
        }
        
        [Test]
        public void New_OverloadExceptDimensions_ShouldHaveExpectedValues()
        {
            var member = Member.Create("Member", (IDataType)new Real(), Radix.Exponential,
                ExternalAccess.ReadOnly, "Test");

            member.Should().NotBeNull();
            member.Name.Should().Be("Member");
            member.DataType.Should().BeOfType<Real>();
            member.Dimension.Should().Be(Dimensions.Empty);
            member.Radix.Should().Be(Radix.Exponential);
            member.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
            member.Description.Should().Be("Test");
        }

        [Test]
        public void New_OverrideProperties_ShouldHaveExpectedOverloads()
        {
            var member = Member.Create("Member", (IDataType)new Real(), new Dimensions(35), Radix.Exponential,
                ExternalAccess.ReadOnly, "Test");

            member.Should().NotBeNull();
            member.Name.Should().Be("Member");
            member.DataType.Should().Be(new Real(Radix.Exponential));
            member.Dimension.Length.Should().Be(35);
            member.Radix.Should().Be(Radix.Exponential);
            member.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
            member.Description.Should().Be("Test");
            member.Should().AllBeOfType<Member<IDataType>>();
        }

        [Test]
        public void Name_GetValue_ShouldBeExpected()
        {
            var member = Member.Create<Real>("Member");

            var name = member.Name;

            name.Should().Be("Member");
        }

        [Test]
        public void DataType_GetValue_ShouldBeExpected()
        {
            var member = Member.Create<Real>("Member");

            var dataType = member.DataType;

            dataType.Should().Be(new Real());
        }

        [Test]
        public void Dimension_GetValue_ShouldBeExpected()
        {
            var member = Member.Create<Real>("Member");

            var dimension = member.Dimension;

            dimension.Length.Should().Be(0);
        }

        [Test]
        public void Radix_GetValue_ShouldBeExpected()
        {
            var member = Member.Create<Real>("Member");

            var radix = member.Radix;

            radix.Should().Be(Radix.Float);
        }

        [Test]
        public void ExternalAccess_GetValue_ShouldBeExpected()
        {
            var member = Member.Create<Real>("Member");

            var access = member.ExternalAccess;

            access.Should().Be(ExternalAccess.ReadWrite);
        }

        [Test]
        public void Description_GetValue_ShouldBeExpected()
        {
            var member = Member.Create<Real>("Member");

            var description = member.Description;

            description.Should().BeNull();
        }

        [Test]
        public void Copy_WhenCalled_ShouldNotBeSameButEqual()
        {
            var member = Member.Create<Dint>("Test", Radix.Binary, ExternalAccess.ReadOnly, "This is a test");

            var copy = member.Copy();

            copy.Should().NotBeSameAs(member);
            copy.Name.Should().NotBeSameAs(member.Name);
            copy.DataType.Should().NotBeSameAs(member.DataType);
            copy.Dimension.Should().NotBeSameAs(member.Dimension);
            copy.Description.Should().NotBeSameAs(member.Description);
        }

        [Test]
        public void TypedEquals_AreEqual_ShouldBeTrue()
        {
            var first = (Member<Bool>)Member.Create("Test", new Bool());
            var second = (Member<Bool>)Member.Create("Test", new Bool());

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void TypedEquals_AreSame_ShouldBeTrue()
        {
            var first = (Member<Bool>)Member.Create("Test", new Bool());
            var second = first;

            var result = first.Equals(second);

            result.Should().BeTrue();
        }


        [Test]
        public void TypedEquals_Null_ShouldBeFalse()
        {
            var first = (Member<Bool>)Member.Create("Test", new Bool());

            var result = first.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void ObjectEquals_AreEqual_ShouldBeTrue()
        {
            var first = Member.Create("Test", new Bool());
            var second = Member.Create("Test", new Bool());

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_AreSame_ShouldBeTrue()
        {
            var first = Member.Create("Test", new Bool());
            var second = first;

            var result = first.Equals(second);

            result.Should().BeTrue();
        }


        [Test]
        public void ObjectEquals_Null_ShouldBeFalse()
        {
            var first = Member.Create("Test", new Bool());

            var result = first.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var first = (Member<Bool>)Member.Create("Test", new Bool());
            var second = (Member<Bool>)Member.Create("Test", new Bool());

            var result = first == second;

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = (Member<Bool>)Member.Create("Test", new Bool());
            var second = (Member<Bool>)Member.Create("Test", new Bool());

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var first = Member.Create("Test", new Bool());

            var hash = first.GetHashCode();

            hash.Should().NotBe(0);
        }
    }
}