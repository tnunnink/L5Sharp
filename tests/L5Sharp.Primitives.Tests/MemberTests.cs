using System;
using FluentAssertions;
using L5Sharp.Enumerations;
using L5Sharp.Exceptions;
using NUnit.Framework;

namespace L5Sharp.Primitives.Tests
{
    [TestFixture]
    public class MemberTests
    {
        [Test]
        public void New_ValidNameWithDataType_ShouldNotBeNull()
        {
            var member = new Member("MemberName", DataType.Bool);

            member.Should().NotBeNull();
        }

        [Test]
        public void New_ValidNameWithDataType_ShouldHaveExpectedDefaults()
        {
            var member = new Member("Test", DataType.Dint);

            member.Name.Should().Be("Test");
            member.DataType.Should().Be(DataType.Dint);
            member.Dimension.Should().Be(0);
            member.Radix.Should().Be(Radix.Decimal);
            member.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            member.Description.Should().BeEmpty();
        }

        [Test]
        public void New_InvalidName_ShouldThrowInvalidNameException()
        {
            FluentActions.Invoking(() => new Member("1!@#$%#$!", DataType.Int))
                .Should().Throw<InvalidNameException>();
        }


        [Test]
        public void New_NullDataType_ShouldHaveNullTypeDataType()
        {
            var member = new Member("1!@#$%#$!", DataType.Int);

            member.DataType.Should().Be(DataType.Null);
        }

        [Test]
        public void New_NullType_ShouldNotBeNull()
        {
            var type = new Member("Test", null);

            type.DataType.Should().Be(DataType.Null);
        }

        [Test]
        public void SetName_ValidName_ShouldUpdateName()
        {
            var member = new Member("MemberName", DataType.Bool);

            member.Name = "Test";

            member.Name.Should().Be("Test");
        }

        [Test]
        public void SetName_InvalidName_ShouldThrowInvalidNameException()
        {
            var member = new Member("MemberName", DataType.Bool);

            FluentActions.Invoking(() => member.Name = "09_#$Test").Should().Throw<InvalidNameException>();
        }

        [Test]
        public void SetDataType_ValidDataType_ShouldUpdateDataType()
        {
            var member = new Member("MemberName", DataType.Bool);

            member.DataType = DataType.Lint;

            member.DataType.Should().Be(DataType.Lint);
        }

        [Test]
        public void SetDataType_Null_ShouldUpdateDataTypeToNullType()
        {
            var member = new Member("MemberName", DataType.Bool);

            member.DataType = null;

            member.DataType.Should().Be(DataType.Null);
        }
        
        [Test]
        public void SetDimension_ValidSingleDimension_ShouldHaveExpectedDimensions()
        {
            var member = new Member("MemberName", DataType.Int);

            member.Dimension = 10;

            member.Dimension.Should().Be(10);
        }
    }
}