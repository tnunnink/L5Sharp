using System;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class DataTypeMemberTests
    {
        [Test]
        public void New_ValidNameWithDataType_ShouldNotBeNull()
        {
            var member = new DataTypeMember<IDataType>("MemberName", new Bool());

            member.Should().NotBeNull();
        }

        [Test]
        public void New_ValidNameWithDataType_ShouldHaveExpectedDefaults()
        {
            var member = new DataTypeMember<IDataType>("Test", new Dint());

            member.Name.Should().Be("Test");
            member.DataType.Should().Be(new Dint());
            member.Dimensions.Length.Should().Be(0);
            member.Radix.Should().Be(Radix.Decimal);
            member.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            member.Description.Should().BeNull();
        }

        [Test]
        public void New_InvalidName_ShouldThrowInvalidNameException()
        {
            FluentActions.Invoking(() => new DataTypeMember<IDataType>("1!@#$%#$!", new Int()))
                .Should().Throw<ComponentNameInvalidException>();
        }


        [Test]
        public void New_NullDataType_ShouldHaveNullTypeDataType()
        {
            var member = new DataTypeMember<IDataType>("Test", null);

            member.DataType.Should().Be(new Undefined());
        }

        [Test]
        public void New_NullType_ShouldNotBeNull()
        {
            var type = new DataTypeMember<IDataType>("Test", null);

            type.DataType.Should().Be(new Undefined());
        }

        [Test]
        public void Name_ValidName_ShouldUpdateName()
        {
            var member = new DataTypeMember<IDataType>("MemberName", new Bool());

            member.SetName("Test");

            member.Name.Should().Be("Test");
        }
        
        [Test]
        public void Name_InvalidName_ShouldThrowInvalidNameException()
        {
            var member = new DataTypeMember<IDataType>("MemberName", new Bool());

            FluentActions.Invoking(() => member.SetName("09_#$Test")).Should().Throw<ComponentNameInvalidException>();
        }

        [Test]
        public void Name_Null_ShouldThrowInvalidNameException()
        {
            var member = new DataTypeMember<IDataType>("MemberName", new Bool());

            FluentActions.Invoking(() => member.SetName(null)).Should().Throw<ArgumentException>();
        }

        /*[Test]
        public void SetDataType_ValidDataType_ShouldUpdateDataType()
        {
            var member = new DataTypeMember<IDataType>("MemberName", new Bool());

            member.SetDataType(Logix.DataType.Lint);

            member.DataType.Should().Be(Logix.DataType.Lint);
        }*/
        
        [Test]
        public void SetDimension_ValidNumber_ShouldHaveExpectedDimensions()
        {
            var member = new DataTypeMember<IDataType>("MemberName", new Int());

            member.SetDimensions(new Dimensions(10));

            member.Dimensions.Length.Should().Be(10);
        }
        
        [Test]
        public void SetRadix_ValidRadix_ShouldUpdateRadix()
        {
            var member = new DataTypeMember<IDataType>("MemberName", new Int());

            member.SetRadix(Radix.Ascii);

            member.Radix.Should().Be(Radix.Ascii);
        }

        [Test]
        public void SetRadix_InvalidRadix_ShouldThrowRadixNotSupportedException()
        {
            var member = new DataTypeMember<IDataType>("MemberName", new Int());

            FluentActions.Invoking(() => member.SetRadix(Radix.Float)).Should().Throw<RadixNotSupportedException>();
        }

        [Test]
        public void SetRadix_Null_ShouldThrowRadixNotSupportedException()
        {
            var member = new DataTypeMember<IDataType>("MemberName", new Int());

            FluentActions.Invoking(() => member.SetRadix(null)).Should().Throw<RadixNotSupportedException>();
        }
        
        [Test]
        public void SetExternalAccess_ReadWrite_ShouldUpdateExternalAccess()
        {
            var member = new DataTypeMember<IDataType>("MemberName", new Int());

            member.SetExternalAccess(ExternalAccess.ReadWrite);

            member.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
        }

        [Test]
        public void SetExternalAccess_None_ShouldUpdateExternalAccess()
        {
            var member = new DataTypeMember<IDataType>("MemberName", new Int());

            member.SetExternalAccess(ExternalAccess.None);

            member.ExternalAccess.Should().Be(ExternalAccess.None);
        }
        
        [Test]
        public void SetExternalAccess_ReadOnly_ShouldUpdateExternalAccess()
        {
            var member = new DataTypeMember<IDataType>("MemberName", new Int());

            member.SetExternalAccess(ExternalAccess.ReadOnly);

            member.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
        }
        
        [Test]
        public void SetExternalAccess_Null_ShouldThrowArgumentNullException()
        {
            var member = new DataTypeMember<IDataType>("MemberName", new Int());

            FluentActions.Invoking(() => member.SetExternalAccess(null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Description_ValidString_ShouldUpdateString()
        {
            var member = new DataTypeMember<IDataType>("MemberName", new Int());

            member.SetDescription("This is a test description");

            member.Description.Should().Be("This is a test description");
        }

        [Test]
        public void Equals_TypeOverloadAreEqual_ShouldBeTrue()
        {
            var member1 = new DataTypeMember<IDataType>("Member", new Bool());
            var member2 = new DataTypeMember<IDataType>("Member", new Bool());

            var result = member1.Equals(member2);

            result.Should().BeTrue();
        }

        [Test]
        public void Equals_TypeOverloadAreNotEqual_ShouldBeFalse()
        {
            var member1 = new DataTypeMember<IDataType>("Member", new Bool());
            var member2 = new DataTypeMember<IDataType>("Member", new Int());

            var result = member1.Equals(member2);

            result.Should().BeFalse();
        }

        [Test]
        public void Equals_TypeOverloadSameReference_ShouldBeTrue()
        {
            var member = new DataTypeMember<IDataType>("Member", new Bool());

            var result = member.Equals(member);

            result.Should().BeTrue();
        }

        [Test]
        public void Equals_TypeOverloadNull_ShouldBeFalse()
        {
            var member = new DataTypeMember<IDataType>("Member", new Bool());

            var result = member.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void Equals_ObjectOverloadAreEqual_ShouldBeTrue()
        {
            var member1 = new DataTypeMember<IDataType>("Member", new Bool());
            var member2 = new DataTypeMember<IDataType>("Member", new Bool());

            var result = member1.Equals((object)member2);

            result.Should().BeTrue();
        }

        [Test]
        public void Equals_ObjectOverloadAreNotEqual_ShouldBeFalse()
        {
            var member1 = new DataTypeMember<IDataType>("Member", new Bool());
            var member2 = new DataTypeMember<IDataType>("Member", new Int());

            var result = member1.Equals((object)member2);

            result.Should().BeFalse();
        }

        [Test]
        public void Equals_ObjectOverloadSameReference_ShouldBeTrue()
        {
            var member = new DataTypeMember<IDataType>("Member", new Bool());

            var result = member.Equals((object)member);

            result.Should().BeTrue();
        }

        [Test]
        public void Equals_ObjectOverloadNull_ShouldBeFalse()
        {
            var member = new DataTypeMember<IDataType>("Member", new Bool());

            var result = member.Equals((object)null);

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeNull()
        {
            var member = new DataTypeMember<IDataType>("Member", new Bool());

            var hash = member.GetHashCode();

            hash.Should().NotBe(0);
        }

        [Test]
        public void Equals_OperatorAreEqual_ShouldBeTrue()
        {
            var member1 = new DataTypeMember<IDataType>("Member", new Bool());
            var member2 = new DataTypeMember<IDataType>("Member", new Bool());

            var result = member1 == member2;

            result.Should().BeTrue();
        }

        [Test]
        public void NotEquals_OperatorAreEqual_ShouldBeFalse()
        {
            var member1 = new DataTypeMember<IDataType>("Member", new Bool());
            var member2 = new DataTypeMember<IDataType>("Member", new Bool());

            var result = member1 != member2;

            result.Should().BeFalse();
        }
    }
}