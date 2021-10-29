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
        public void New_ValidNameAndType_ShouldNotBeNull()
        {
            var member = new Member("Member", Predefined.Real);

            member.Should().NotBeNull();
        }

        [Test]
        public void New_InvalidName_ShouldThrowInvalidNameException()
        {
            FluentActions.Invoking(() => new Member("$Invalid", Predefined.Dint)).Should()
                .Throw<InvalidNameException>();
        }

        [Test]
        public void New_NullName_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new Member(null, Predefined.Dint)).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void New_NullType_ShouldHaveUndefinedDataType()
        {
            var member = new Member("Name", null);
            member.DataType.Should().Be(Predefined.Undefined);
        }

        [Test]
        public void New_OverrideProperties_ShouldNotBeNull()
        {
            var member = new Member("Member", Predefined.Real, new Dimensions(35), Radix.General,
                ExternalAccess.ReadOnly, "Test");

            member.Should().NotBeNull();
            member.Name.Should().Be("Member");
            member.DataType.Should().Be(Predefined.Real);
            member.Dimension.Should().Be(35);
            member.Radix.Should().Be(Radix.General);
            member.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
            member.Description.Should().Be("Test");
        }

        [Test]
        public void Name_GetValue_ShouldBeExpected()
        {
            var member = new Member("Member", Predefined.Real);

            var name = member.Name;

            name.Should().Be("Member");
        }

        [Test]
        public void DataType_GetValue_ShouldBeExpected()
        {
            var member = new Member("Member", Predefined.Real);

            var dataType = member.DataType;

            dataType.Should().Be(Predefined.Real);
        }

        [Test]
        public void Dimension_GetValue_ShouldBeExpected()
        {
            var member = new Member("Member", Predefined.Real);

            var dimension = member.Dimension;

            dimension.Should().Be(0);
        }

        [Test]
        public void Radix_GetValue_ShouldBeExpected()
        {
            var member = new Member("Member", Predefined.Real);

            var radix = member.Radix;

            radix.Should().Be(Radix.Float);
        }

        [Test]
        public void ExternalAccess_GetValue_ShouldBeExpected()
        {
            var member = new Member("Member", Predefined.Real);

            var access = member.ExternalAccess;

            access.Should().Be(ExternalAccess.ReadWrite);
        }

        [Test]
        public void Description_GetValue_ShouldBeExpected()
        {
            var member = new Member("Member", Predefined.Real);

            var description = member.Description;

            description.Should().BeNull();
        }
    }
}