using System;
using FluentAssertions;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class MemberTests
    {
        [Test]
        public void New_ValidNameAndType_ShouldNotBeNull()
        {
            var member = new Member("Member", Logix.DataType.Real);

            member.Should().NotBeNull();
        }

        [Test]
        public void New_NullName_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new Member(null, Logix.DataType.Dint)).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void New_NullType_ShouldHaveUndefinedDataType()
        {
            var member = new Member("Name", null);
            member.DataType.Should().Be(Logix.DataType.Undefined);
        }

        [Test]
        public void New_OverrideProperties_ShouldNotBeNull()
        {
            var member = new Member("Member", Logix.DataType.Real, new Dimensions(35), Radix.General,
                ExternalAccess.ReadOnly, "Test");

            member.Should().NotBeNull();
            member.Name.Should().Be("Member");
            member.DataType.Should().Be(Logix.DataType.Real);
            member.Dimensions.Length.Should().Be(35);
            member.Radix.Should().Be(Radix.General);
            member.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
            member.Description.Should().Be("Test");
        }

        [Test]
        public void Name_GetValue_ShouldBeExpected()
        {
            var member = new Member("Member", Logix.DataType.Real);

            var name = member.Name;

            name.Should().Be("Member");
        }

        [Test]
        public void DataType_GetValue_ShouldBeExpected()
        {
            var member = new Member("Member", Logix.DataType.Real);

            var dataType = member.DataType;

            dataType.Should().Be(Logix.DataType.Real);
        }

        [Test]
        public void Dimension_GetValue_ShouldBeExpected()
        {
            var member = new Member("Member", Logix.DataType.Real);

            var dimension = member.Dimensions;

            dimension.Length.Should().Be(0);
        }

        [Test]
        public void Radix_GetValue_ShouldBeExpected()
        {
            var member = new Member("Member", Logix.DataType.Real);

            var radix = member.Radix;

            radix.Should().Be(Radix.Float);
        }

        [Test]
        public void ExternalAccess_GetValue_ShouldBeExpected()
        {
            var member = new Member("Member", Logix.DataType.Real);

            var access = member.ExternalAccess;

            access.Should().Be(ExternalAccess.ReadWrite);
        }

        [Test]
        public void Description_GetValue_ShouldBeExpected()
        {
            var member = new Member("Member", Logix.DataType.Real);

            var description = member.Description;

            description.Should().BeNull();
        }
    }
}