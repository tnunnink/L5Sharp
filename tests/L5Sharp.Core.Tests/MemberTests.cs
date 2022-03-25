using System;
using FluentAssertions;
using L5Sharp.Creators;
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
            var member = Member.Create("Test", new BOOL());

            member.Should().NotBeNull();
        }

        [Test]
        public void Create_TypedValidName_ShouldNotBeNull()
        {
            var member = Member.Create<BOOL>("Test");

            member.Should().NotBeNull();
        }

        [Test]
        public void New_NullName_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Member.Create<DINT>(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void New_NullType_ShouldHaveNullType()
        {
            FluentActions.Invoking(() => Member.Create("Name", null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void New_Overloaded_ShouldHaveExpectedValues()
        {
            var member = Member.Create("Member", (IDataType)new REAL(), Radix.Exponential, ExternalAccess.ReadOnly, "Test");

            member.Should().NotBeNull();
            member.Name.Should().Be("Member");
            member.DataType.Should().BeOfType<REAL>();
            member.Dimensions.Should().Be(Dimensions.Empty);
            member.Radix.Should().Be(Radix.Exponential);
            member.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
            member.Description.Should().Be("Test");
        }

        [Test]
        public void New_ArrayMember_ShouldHaveExpectedProperties()
        {
            var member = Member.Create<BOOL>("Test", new Dimensions(5));

            member.Should().NotBeNull();
            member.DataType.Should().BeOfType<ArrayType<BOOL>>();
            member.Dimensions.Should().BeEquivalentTo(new Dimensions(5));
            member.Radix.Should().Be(Radix.Decimal);
            member.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            member.Description.Should().BeEmpty();
        }

        [Test]
        public void Name_GetValue_ShouldBeExpected()
        {
            var member = Member.Create<REAL>("Member");

            var name = member.Name;

            name.Should().Be("Member");
        }

        [Test]
        public void Description_GetValue_ShouldBeExpected()
        {
            var member = Member.Create<REAL>("Member");

            var description = member.Description;

            description.Should().BeEmpty();
        }

        [Test]
        public void DataType_GetValue_ShouldBeExpected()
        {
            var member = Member.Create<REAL>("Member");

            var dataType = member.DataType;

            dataType.Should().Be(new REAL());
        }

        [Test]
        public void Dimension_GetValue_ShouldBeExpected()
        {
            var member = Member.Create<REAL>("Member");

            var dimension = member.Dimensions;

            dimension.Length.Should().Be(0);
        }

        [Test]
        public void Radix_GetValue_ShouldBeExpected()
        {
            var member = Member.Create<REAL>("Member");

            var radix = member.Radix;

            radix.Should().Be(Radix.Float);
        }

        [Test]
        public void ExternalAccess_GetValue_ShouldBeExpected()
        {
            var member = Member.Create<REAL>("Member");

            var access = member.ExternalAccess;

            access.Should().Be(ExternalAccess.ReadWrite);
        }

        [Test]
        public void IsValueMember_IsAtomicMember_ShouldBeTrue()
        {
            var member = Member.Create<BOOL>("Test");

            member.IsValueMember.Should().BeTrue();
        }

        [Test]
        public void IsValueMember_IsComplexMember_ShouldBeFalse()
        {
            var member = Member.Create<STRING>("Test");

            member.IsValueMember.Should().BeFalse();
        }

        [Test]
        public void IsStructureMember_IsAtomicMember_ShouldBeFalse()
        {
            var member = Member.Create<BOOL>("Test");

            member.IsStructureMember.Should().BeFalse();
        }

        [Test]
        public void IsStructureMember_IsComplexMember_ShouldBeTrue()
        {
            var member = Member.Create<STRING>("Test");

            member.IsStructureMember.Should().BeTrue();
        }

        [Test]
        public void IsArrayMember_DimensionsEmpty_ShouldBeFalse()
        {
            var member = Member.Create<BOOL>("Test");

            member.IsArrayMember.Should().BeFalse();
        }

        [Test]
        public void IsArrayMember_DimensionsNotEmpty_ShouldBeTrue()
        {
            var member = Member.Create<BOOL>("Test", new Dimensions(5));

            member.IsArrayMember.Should().BeTrue();
        }

        [Test]
        public void IsArrayMember_ComplexTypeArray_ShouldBeTrue()
        {
            var member = Member.Create<TIMER>("Test", new Dimensions(5));

            member.IsArrayMember.Should().BeTrue();
        }
    }
}