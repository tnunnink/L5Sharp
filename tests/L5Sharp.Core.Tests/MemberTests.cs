using System;
using FluentAssertions;
using L5Sharp.Atomics;
using L5Sharp.Enums;
using L5Sharp.Factories;
using L5Sharp.Predefined;
using NUnit.Framework;
using String = L5Sharp.Predefined.String; 

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
        public void New_NullName_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Member.Create<Dint>(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void New_NullType_ShouldHaveNullType()
        {
            FluentActions.Invoking(() => Member.Create("Name", null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void New_Overloaded_ShouldHaveExpectedValues()
        {
            var member = Member.Create("Member", (IDataType)new Real(), Radix.Exponential, ExternalAccess.ReadOnly, "Test");

            member.Should().NotBeNull();
            member.Name.Should().Be("Member");
            member.DataType.Should().BeOfType<Real>();
            member.Dimensions.Should().Be(Dimensions.Empty);
            member.Radix.Should().Be(Radix.Exponential);
            member.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
            member.Description.Should().Be("Test");
        }

        [Test]
        public void New_ArrayMember_ShouldHaveExpectedProperties()
        {
            var member = Member.Create<Bool>("Test", new Dimensions(5));

            member.Should().NotBeNull();
            member.DataType.Should().BeOfType<ArrayType<Bool>>();
            member.Dimensions.Should().BeEquivalentTo(new Dimensions(5));
            member.Radix.Should().Be(Radix.Decimal);
            member.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            member.Description.Should().BeEmpty();
        }

        [Test]
        public void Name_GetValue_ShouldBeExpected()
        {
            var member = Member.Create<Real>("Member");

            var name = member.Name;

            name.Should().Be("Member");
        }

        [Test]
        public void Description_GetValue_ShouldBeExpected()
        {
            var member = Member.Create<Real>("Member");

            var description = member.Description;

            description.Should().BeEmpty();
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

            var dimension = member.Dimensions;

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
        public void IsValueMember_IsAtomicMember_ShouldBeTrue()
        {
            var member = Member.Create<Bool>("Test");

            member.IsValueMember.Should().BeTrue();
        }

        [Test]
        public void IsValueMember_IsComplexMember_ShouldBeFalse()
        {
            var member = Member.Create<String>("Test");

            member.IsValueMember.Should().BeFalse();
        }

        [Test]
        public void IsStructureMember_IsAtomicMember_ShouldBeFalse()
        {
            var member = Member.Create<Bool>("Test");

            member.IsStructureMember.Should().BeFalse();
        }

        [Test]
        public void IsStructureMember_IsComplexMember_ShouldBeTrue()
        {
            var member = Member.Create<String>("Test");

            member.IsStructureMember.Should().BeTrue();
        }

        [Test]
        public void IsArrayMember_DimensionsEmpty_ShouldBeFalse()
        {
            var member = Member.Create<Bool>("Test");

            member.IsArrayMember.Should().BeFalse();
        }

        [Test]
        public void IsArrayMember_DimensionsNotEmpty_ShouldBeTrue()
        {
            var member = Member.Create<Bool>("Test", new Dimensions(5));

            member.IsArrayMember.Should().BeTrue();
        }

        [Test]
        public void IsArrayMember_ComplexTypeArray_ShouldBeTrue()
        {
            var member = Member.Create<Timer>("Test", new Dimensions(5));

            member.IsArrayMember.Should().BeTrue();
        }
    }
}