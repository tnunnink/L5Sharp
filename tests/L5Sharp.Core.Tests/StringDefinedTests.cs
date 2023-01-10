using System;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class StringDefinedTests
    {
        [Test]
        public void Create_ValidNameAndLength_ShouldNotBeNull()
        {
            var type = new StringDefined("Test", 100, "This is a test");

            type.Should<StringDefined>().NotBeNull();
        }

        [Test]
        public void Create_InvalidLength_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => new StringDefined("Test", 0)).Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void Create_NullName_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new StringDefined(null!, 0)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Name_GetValue_ShouldBeExpected()
        {
            var type = new StringDefined("Test", 100);
            type.Name.Should().Be("Test");
        }
        
        [Test]
        public void Class_GetValue_ShouldBeExpected()
        {
            var type = new StringDefined("Test", 100);
            type.Class.Should().Be(DataTypeClass.User);
        }
        
        [Test]
        public void Family_GetValue_ShouldBeExpected()
        {
            var type = new StringDefined("Test", 100);
            
            type.Family.Should().Be(DataTypeFamily.String);
        }
        
        [Test]
        public void Description_GetValue_ShouldBeExpected()
        {
            var type = new StringDefined("Test", 100);
            
            type.Description.Should().BeEmpty();
        }
        
        [Test]
        public void Value_GetValue_ShouldBeExpected()
        {
            var type = new StringDefined("Test", 100);
            
            type.Value.Should().BeEmpty();
        }

        [Test]
        public void Instantiate_WhenCalled_ShouldBeNotBeNull()
        {
            var type = new StringDefined("Test", 10);

            var instance = type.Instantiate();

            instance.Should().NotBeNull();
        }

        [Test]
        public void Len_GetValue_ShouldHaveExpectedProperties()
        {
            var type = new StringDefined("Test", 10);

            type.LEN.Should().NotBeNull();
            type.LEN.Name.Should().Be("LEN");
            type.LEN.DataType.Should().BeOfType<DINT>();
            type.LEN.DataType.Value.Should().Be(0);
            type.LEN.Dimensions.Length.Should().Be(0);
            type.LEN.Radix.Should().Be(Radix.Decimal);
            type.LEN.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            type.LEN.Description.Should().BeEmpty();
        }
        
        [Test]
        public void Data_GetValue_ShouldHaveExpectedProperties()
        {
            var type = new StringDefined("Test", 100);
            
            type.DATA.Should().NotBeNull();
            type.DATA.Name.Should().Be("DATA");
            type.DATA.DataType.Should().BeOfType<ArrayType<SINT>>();
            type.DATA.Dimensions.Length.Should().Be(100);
            type.DATA.Radix.Should().Be(Radix.Ascii);
            type.DATA.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            type.DATA.Description.Should().BeEmpty();
        }

        [Test]
        public void SetValue_Null_ShouldThrowArgumentNullException()
        {
            var type = new StringDefined("Test", 100);
            
            FluentActions.Invoking(() => type.SetValue(null!)).Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void SetValue_InvalidLength_ShouldThrowArgumentException()
        {
            var type = new StringDefined("Test", 10);
            
            FluentActions.Invoking(() => type.SetValue("This is a string more than 10 characters."))
                .Should().Throw<ArgumentException>();
        }

        [Test]
        public void SetValue_ValidString_ShouldBeExpected()
        {
            var type = new StringDefined("Test", 100);
            
            type.SetValue("This is a test string to set the value with.");

            type.Value.Should().Be("This is a test string to set the value with.");
        }
        
        [Test]
        public void SetValue_MultipleTimes_ShouldBeExpectedEachTime()
        {
            var type = new StringDefined("Test", 100);
            
            type.SetValue("This is a test string to set the value with.");
            type.Value.Should().Be("This is a test string to set the value with.");
            
            type.SetValue("This is a test");
            type.Value.Should().Be("This is a test");
            
            type.SetValue(string.Empty);
            type.Value.Should().BeEmpty();
        }

        [Test]
        public void ToString_WhenCalled_ShouldBeName()
        {
            var type = new StringDefined("Test", 10);

            type.ToString().Should().Be("Test");
        }

        [Test]
        public void TypedEquals_AreEqual_ShouldBeTrue()
        {
            var first = new StringDefined("Test", 10);
            var second = new StringDefined("Test", 10);

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void TypedEquals_AreSame_ShouldBeTrue()
        {
            var first = new StringDefined("Test", 10);

            var result = first.Equals(first);

            result.Should().BeTrue();
        }


        [Test]
        public void TypedEquals_Null_ShouldBeFalse()
        {
            var first = new StringDefined("Test", 10);

            var result = first.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void ObjectEquals_AreEqual_ShouldBeTrue()
        {
            var first = new StringDefined("Test", 10);
            var second = new StringDefined("Test", 10);

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_AreSame_ShouldBeTrue()
        {
            var first = new StringDefined("Test", 10);
            var second = first;

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }


        [Test]
        public void ObjectEquals_Null_ShouldBeFalse()
        {
            var first = new StringDefined("Test", 10);

            var result = first.Equals((object)null);

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var first = new StringDefined("Test", 10);
            var second = new StringDefined("Test", 10);

            var result = first == second;

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = new StringDefined("Test", 10);
            var second = new StringDefined("Test", 10);

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var first = new StringDefined("Test", 10);

            var hash = first.GetHashCode();

            hash.Should().NotBe(0);
        }
    }
}