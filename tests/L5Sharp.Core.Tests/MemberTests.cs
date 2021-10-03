using System;
using FluentAssertions;
using L5Sharp.Enumerations;
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
            var member = new Member("Test", null);

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
        public void SetDimension_ValidNumber_ShouldHaveExpectedDimensions()
        {
            var member = new Member("MemberName", DataType.Int);

            member.Dimension = 10;

            member.Dimension.Should().Be(10);
        }

        [Test]
        public void SetRadix_ValidRadix_ShouldUpdateRadix()
        {
            var member = new Member("MemberName", DataType.Int);

            member.Radix = Radix.Ascii;

            member.Radix.Should().Be(Radix.Ascii);
        }
        
        
        [Test]
        public void SetRadix_InvalidRadix_ShouldThrowRadixNotSupportedException()
        {
            var member = new Member("MemberName", DataType.Int);

            FluentActions.Invoking(() => member.Radix = Radix.Float).Should().Throw<RadixNotSupportedException>();
        }
        
                
        [Test]
        public void SetRadix_Null_ShouldThrowArgumentNullException()
        {
            var member = new Member("MemberName", DataType.Int);

            FluentActions.Invoking(() => member.Radix = null).Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void SetExternalAccess_None_ShouldUpdateExternalAccess()
        {
            var member = new Member("MemberName", DataType.Int);

            member.ExternalAccess = ExternalAccess.None;

            member.ExternalAccess.Should().Be(ExternalAccess.None);
        }
        
        [Test]
        public void SetExternalAccess_ReadOnly_ShouldUpdateExternalAccess()
        {
            var member = new Member("MemberName", DataType.Int);

            member.ExternalAccess = ExternalAccess.ReadOnly;

            member.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
        }
        
        [Test]
        public void SetExternalAccess_Null_ShouldThrowArgumentNullException()
        {
            var member = new Member("MemberName", DataType.Int);

            FluentActions.Invoking(() => member.ExternalAccess = null).Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void SetDescription_ValidString_ShouldUpdateString()
        {
            var member = new Member("MemberName", DataType.Int);

            member.Description = "This is a test description";

            member.Description.Should().Be("This is a test description");
        }
        
        [Test]
        public void SetDescription_Null_ShouldBeEmpty()
        {
            var member = new Member("MemberName", DataType.Int);

            member.Description = null;

            member.Description.Should().BeEmpty();
        }
    }
}