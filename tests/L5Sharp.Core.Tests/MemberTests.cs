using System;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class MemberTests
    {
        [Test]
        public void New_ValidNameWithDataType_ShouldNotBeNull()
        {
            var member = new Member("MemberName", Predefined.Bool);

            member.Should().NotBeNull();
        }

        [Test]
        public void New_ValidNameWithDataType_ShouldHaveExpectedDefaults()
        {
            var member = new Member("Test", Predefined.Dint);

            member.Name.Should().Be("Test");
            member.DataType.Should().Be(Predefined.Dint);
            member.Dimension.Should().Be(0);
            member.Radix.Should().Be(Radix.Decimal);
            member.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            member.Description.Should().BeNull();
        }

        [Test]
        public void New_InvalidName_ShouldThrowInvalidNameException()
        {
            FluentActions.Invoking(() => new Member("1!@#$%#$!", Predefined.Int))
                .Should().Throw<InvalidNameException>();
        }


        [Test]
        public void New_NullDataType_ShouldHaveNullTypeDataType()
        {
            var member = new Member("Test", null);

            member.DataType.Should().Be(Predefined.Undefined);
        }

        [Test]
        public void New_NullType_ShouldNotBeNull()
        {
            var type = new Member("Test", null);

            type.DataType.Should().Be(Predefined.Undefined);
        }

        [Test]
        public void SetName_ValidName_ShouldUpdateName()
        {
            var member = new Member("MemberName", Predefined.Bool);

            member.Name = "Test";

            member.Name.Should().Be("Test");
        }

        [Test]
        public void SetName_InvalidName_ShouldThrowInvalidNameException()
        {
            var member = new Member("MemberName", Predefined.Bool);

            FluentActions.Invoking(() => member.Name = "09_#$Test").Should().Throw<InvalidNameException>();
        }

        [Test]
        public void SetName_Null_ShouldThrowInvalidNameException()
        {
            var member = new Member("MemberName", Predefined.Bool);

            FluentActions.Invoking(() => member.Name = null).Should().Throw<ArgumentException>();
        }

        [Test]
        public void SetDataType_ValidDataType_ShouldUpdateDataType()
        {
            var member = new Member("MemberName", Predefined.Bool);

            member.DataType = Predefined.Lint;

            member.DataType.Should().Be(Predefined.Lint);
        }

        [Test]
        public void SetDataType_Null_ShouldUpdateDataTypeToNullType()
        {
            var member = new Member("MemberName", Predefined.Bool);

            member.DataType = null;

            member.DataType.Should().Be(Predefined.Undefined);
        }

        [Test]
        public void SetDimension_ValidNumber_ShouldHaveExpectedDimensions()
        {
            var member = new Member("MemberName", Predefined.Int);

            member.Dimension = 10;

            member.Dimension.Should().Be(10);
        }

        [Test]
        public void SetRadix_ValidRadix_ShouldUpdateRadix()
        {
            var member = new Member("MemberName", Predefined.Int);

            member.Radix = Radix.Ascii;

            member.Radix.Should().Be(Radix.Ascii);
        }


        [Test]
        public void SetRadix_InvalidRadix_ShouldThrowRadixNotSupportedException()
        {
            var member = new Member("MemberName", Predefined.Int);

            FluentActions.Invoking(() => member.Radix = Radix.Float).Should().Throw<RadixNotSupportedException>();
        }


        [Test]
        public void SetRadix_Null_ShouldThrowArgumentNullException()
        {
            var member = new Member("MemberName", Predefined.Int);

            FluentActions.Invoking(() => member.Radix = null).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void SetExternalAccess_None_ShouldUpdateExternalAccess()
        {
            var member = new Member("MemberName", Predefined.Int);

            member.ExternalAccess = ExternalAccess.None;

            member.ExternalAccess.Should().Be(ExternalAccess.None);
        }

        [Test]
        public void SetExternalAccess_ReadOnly_ShouldUpdateExternalAccess()
        {
            var member = new Member("MemberName", Predefined.Int);

            member.ExternalAccess = ExternalAccess.ReadOnly;

            member.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
        }

        [Test]
        public void SetExternalAccess_Null_ShouldThrowArgumentNullException()
        {
            var member = new Member("MemberName", Predefined.Int);

            FluentActions.Invoking(() => member.ExternalAccess = null).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void SetDescription_ValidString_ShouldUpdateString()
        {
            var member = new Member("MemberName", Predefined.Int);

            member.Description = "This is a test description";

            member.Description.Should().Be("This is a test description");
        }

        [Test]
        public void Equals_TypeOverloadAreEqual_ShouldBeTrue()
        {
            var member1 = new Member("Member", Predefined.Bool);
            var member2 = new Member("Member", Predefined.Bool);

            var result = member1.Equals(member2);

            result.Should().BeTrue();
        }

        [Test]
        public void Equals_TypeOverloadAreNotEqual_ShouldBeFalse()
        {
            var member1 = new Member("Member", Predefined.Bool);
            var member2 = new Member("Member", Predefined.Int);

            var result = member1.Equals(member2);

            result.Should().BeFalse();
        }

        [Test]
        public void Equals_TypeOverloadSameReference_ShouldBeTrue()
        {
            var member = new Member("Member", Predefined.Bool);

            var result = member.Equals(member);

            result.Should().BeTrue();
        }

        [Test]
        public void Equals_TypeOverloadNull_ShouldBeFalse()
        {
            var member = new Member("Member", Predefined.Bool);

            var result = member.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void Equals_ObjectOverloadAreEqual_ShouldBeTrue()
        {
            var member1 = new Member("Member", Predefined.Bool);
            var member2 = new Member("Member", Predefined.Bool);

            var result = member1.Equals((object)member2);

            result.Should().BeTrue();
        }

        [Test]
        public void Equals_ObjectOverloadAreNotEqual_ShouldBeFalse()
        {
            var member1 = new Member("Member", Predefined.Bool);
            var member2 = new Member("Member", Predefined.Int);

            var result = member1.Equals((object)member2);

            result.Should().BeFalse();
        }

        [Test]
        public void Equals_ObjectOverloadSameReference_ShouldBeTrue()
        {
            var member = new Member("Member", Predefined.Bool);

            var result = member.Equals((object)member);

            result.Should().BeTrue();
        }

        [Test]
        public void Equals_ObjectOverloadNull_ShouldBeFalse()
        {
            var member = new Member("Member", Predefined.Bool);

            var result = member.Equals((object)null);

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeNull()
        {
            var member = new Member("Member", Predefined.Bool);

            var hash = member.GetHashCode();

            hash.Should().NotBe(0);
        }

        [Test]
        public void Equals_OperatorAreEqual_ShouldBeTrue()
        {
            var member1 = new Member("Member", Predefined.Bool);
            var member2 = new Member("Member", Predefined.Bool);

            var result = member1 == member2;

            result.Should().BeTrue();
        }

        [Test]
        public void NotEquals_OperatorAreEqual_ShouldBeFalse()
        {
            var member1 = new Member("Member", Predefined.Bool);
            var member2 = new Member("Member", Predefined.Bool);

            var result = member1 != member2;

            result.Should().BeFalse();
        }
    }
}