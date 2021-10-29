using System;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class UserDefinedMemberTests
    {
        [Test]
        public void New_ValidNameWithDataType_ShouldNotBeNull()
        {
            var member = new DataTypeMember("MemberName", Predefined.Bool);

            member.Should().NotBeNull();
        }

        [Test]
        public void New_ValidNameWithDataType_ShouldHaveExpectedDefaults()
        {
            var member = new DataTypeMember("Test", Predefined.Dint);

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
            FluentActions.Invoking(() => new DataTypeMember("1!@#$%#$!", Predefined.Int))
                .Should().Throw<InvalidNameException>();
        }


        [Test]
        public void New_NullDataType_ShouldHaveNullTypeDataType()
        {
            var member = new DataTypeMember("Test", null);

            member.DataType.Should().Be(Predefined.Undefined);
        }

        [Test]
        public void New_NullType_ShouldNotBeNull()
        {
            var type = new DataTypeMember("Test", null);

            type.DataType.Should().Be(Predefined.Undefined);
        }

        [Test]
        public void Name_ValidName_ShouldUpdateName()
        {
            var member = new DataTypeMember("MemberName", Predefined.Bool);

            member.SetName("Test");

            member.Name.Should().Be("Test");
        }
        
        [Test]
        public void Name_ValidName_ShouldRaisePropertyChangedEvent()
        {
            var member = new DataTypeMember("MemberName", Predefined.Bool);
            var monitor = member.Monitor();

            member.SetName("Test");

            monitor.Should().RaisePropertyChangeFor(m => m.Name);
        }

        [Test]
        public void Name_InvalidName_ShouldThrowInvalidNameException()
        {
            var member = new DataTypeMember("MemberName", Predefined.Bool);

            FluentActions.Invoking(() => member.SetName("09_#$Test")).Should().Throw<InvalidNameException>();
        }

        [Test]
        public void Name_Null_ShouldThrowInvalidNameException()
        {
            var member = new DataTypeMember("MemberName", Predefined.Bool);

            FluentActions.Invoking(() => member.SetName(null)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void SetDataType_ValidDataType_ShouldUpdateDataType()
        {
            var member = new DataTypeMember("MemberName", Predefined.Bool);

            member.SetDataType(Predefined.Lint);

            member.DataType.Should().Be(Predefined.Lint);
        }
        
        [Test]
        public void SetDataType_ValidName_ShouldRaisePropertyChangeEvent()
        {
            var member = new DataTypeMember("MemberName", Predefined.Bool);
            var monitor = member.Monitor();

            member.SetDataType(Predefined.Lint);

            monitor.Should().RaisePropertyChangeFor(m => m.DataType);
        }

        [Test]
        public void SetDataType_Null_ShouldUpdateDataTypeToNullType()
        {
            var member = new DataTypeMember("MemberName", Predefined.Bool);

            member.SetDataType(null);

            member.DataType.Should().Be(Predefined.Undefined);
        }

        [Test]
        public void SetDimension_ValidNumber_ShouldHaveExpectedDimensions()
        {
            var member = new DataTypeMember("MemberName", Predefined.Int);

            member.SetDimensions(new Dimensions(10));

            member.Dimension.Should().Be(10);
        }
        
        [Test]
        public void SetDimension_ValidNumber_ShouldRaisePropertyChangeEvent()
        {
            var member = new DataTypeMember("MemberName", Predefined.Bool);
            var monitor = member.Monitor();

            member.SetDimensions(new Dimensions(10));

            monitor.Should().RaisePropertyChangeFor(m => m.Dimension);
        }

        [Test]
        public void SetRadix_ValidRadix_ShouldUpdateRadix()
        {
            var member = new DataTypeMember("MemberName", Predefined.Int);

            member.SetRadix(Radix.Ascii);

            member.Radix.Should().Be(Radix.Ascii);
        }
        
        [Test]
        public void SetRadix_ValidRadix_ShouldRaisePropertyChangeEvent()
        {
            var member = new DataTypeMember("MemberName", Predefined.Int);
            var monitor = member.Monitor();

            member.SetRadix(Radix.Ascii);

            monitor.Should().RaisePropertyChangeFor(m => m.Radix);
        }


        [Test]
        public void SetRadix_InvalidRadix_ShouldThrowRadixNotSupportedException()
        {
            var member = new DataTypeMember("MemberName", Predefined.Int);

            FluentActions.Invoking(() => member.SetRadix(Radix.Float)).Should().Throw<RadixNotSupportedException>();
        }


        [Test]
        public void SetRadix_Null_ShouldThrowArgumentNullException()
        {
            var member = new DataTypeMember("MemberName", Predefined.Int);

            FluentActions.Invoking(() => member.SetRadix(null)).Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void SetExternalAccess_ReadWrite_ShouldUpdateExternalAccess()
        {
            var member = new DataTypeMember("MemberName", Predefined.Int);

            member.SetExternalAccess(ExternalAccess.ReadWrite);

            member.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
        }
        
        [Test]
        public void SetExternalAccess_ReadWrite_ShouldRaisePropertyChangeEvent()
        {
            var member = new DataTypeMember("MemberName", Predefined.Bool);
            var monitor = member.Monitor();

            member.SetExternalAccess(ExternalAccess.ReadWrite);

            monitor.Should().NotRaisePropertyChangeFor(m => m.ExternalAccess);
        }

        [Test]
        public void SetExternalAccess_None_ShouldUpdateExternalAccess()
        {
            var member = new DataTypeMember("MemberName", Predefined.Int);

            member.SetExternalAccess(ExternalAccess.None);

            member.ExternalAccess.Should().Be(ExternalAccess.None);
        }
        
        [Test]
        public void SetExternalAccess_None_ShouldRaisePropertyChangeEvent()
        {
            var member = new DataTypeMember("MemberName", Predefined.Bool);
            var monitor = member.Monitor();

            member.SetExternalAccess(ExternalAccess.None);

            monitor.Should().RaisePropertyChangeFor(m => m.ExternalAccess);
        }

        [Test]
        public void SetExternalAccess_ReadOnly_ShouldUpdateExternalAccess()
        {
            var member = new DataTypeMember("MemberName", Predefined.Int);

            member.SetExternalAccess(ExternalAccess.ReadOnly);

            member.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
        }
        
        [Test]
        public void SetExternalAccess_ReadOnly_ShouldRaisePropertyChangeEvent()
        {
            var member = new DataTypeMember("MemberName", Predefined.Bool);
            var monitor = member.Monitor();

            member.SetExternalAccess(ExternalAccess.ReadOnly);

            monitor.Should().RaisePropertyChangeFor(m => m.ExternalAccess);
        }

        [Test]
        public void SetExternalAccess_Null_ShouldThrowArgumentNullException()
        {
            var member = new DataTypeMember("MemberName", Predefined.Int);

            FluentActions.Invoking(() => member.SetExternalAccess(null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Description_ValidString_ShouldUpdateString()
        {
            var member = new DataTypeMember("MemberName", Predefined.Int);

            member.SetDescription("This is a test description");

            member.Description.Should().Be("This is a test description");
        }
        
        [Test]
        public void Description_ValidString_ShouldRaisePropertyChangeEvent()
        {
            var member = new DataTypeMember("MemberName", Predefined.Bool);
            var monitor = member.Monitor();

            member.SetDescription("This is a test description");

            monitor.Should().RaisePropertyChangeFor(m => m.Description);
        }

        [Test]
        public void Equals_TypeOverloadAreEqual_ShouldBeTrue()
        {
            var member1 = new DataTypeMember("Member", Predefined.Bool);
            var member2 = new DataTypeMember("Member", Predefined.Bool);

            var result = member1.Equals(member2);

            result.Should().BeTrue();
        }

        [Test]
        public void Equals_TypeOverloadAreNotEqual_ShouldBeFalse()
        {
            var member1 = new DataTypeMember("Member", Predefined.Bool);
            var member2 = new DataTypeMember("Member", Predefined.Int);

            var result = member1.Equals(member2);

            result.Should().BeFalse();
        }

        [Test]
        public void Equals_TypeOverloadSameReference_ShouldBeTrue()
        {
            var member = new DataTypeMember("Member", Predefined.Bool);

            var result = member.Equals(member);

            result.Should().BeTrue();
        }

        [Test]
        public void Equals_TypeOverloadNull_ShouldBeFalse()
        {
            var member = new DataTypeMember("Member", Predefined.Bool);

            var result = member.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void Equals_ObjectOverloadAreEqual_ShouldBeTrue()
        {
            var member1 = new DataTypeMember("Member", Predefined.Bool);
            var member2 = new DataTypeMember("Member", Predefined.Bool);

            var result = member1.Equals((object)member2);

            result.Should().BeTrue();
        }

        [Test]
        public void Equals_ObjectOverloadAreNotEqual_ShouldBeFalse()
        {
            var member1 = new DataTypeMember("Member", Predefined.Bool);
            var member2 = new DataTypeMember("Member", Predefined.Int);

            var result = member1.Equals((object)member2);

            result.Should().BeFalse();
        }

        [Test]
        public void Equals_ObjectOverloadSameReference_ShouldBeTrue()
        {
            var member = new DataTypeMember("Member", Predefined.Bool);

            var result = member.Equals((object)member);

            result.Should().BeTrue();
        }

        [Test]
        public void Equals_ObjectOverloadNull_ShouldBeFalse()
        {
            var member = new DataTypeMember("Member", Predefined.Bool);

            var result = member.Equals((object)null);

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeNull()
        {
            var member = new DataTypeMember("Member", Predefined.Bool);

            var hash = member.GetHashCode();

            hash.Should().NotBe(0);
        }

        [Test]
        public void Equals_OperatorAreEqual_ShouldBeTrue()
        {
            var member1 = new DataTypeMember("Member", Predefined.Bool);
            var member2 = new DataTypeMember("Member", Predefined.Bool);

            var result = member1 == member2;

            result.Should().BeTrue();
        }

        [Test]
        public void NotEquals_OperatorAreEqual_ShouldBeFalse()
        {
            var member1 = new DataTypeMember("Member", Predefined.Bool);
            var member2 = new DataTypeMember("Member", Predefined.Bool);

            var result = member1 != member2;

            result.Should().BeFalse();
        }
    }
}