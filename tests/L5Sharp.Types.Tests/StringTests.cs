using System;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Types.Tests
{
    [TestFixture]
    public class StringTests
    {
        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var type = new String();

            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_DefaultShouldHaveEmptyValue()
        {
            var type = new String();
            
            type.Value.Should().BeEmpty();
        }

        [Test]
        public void New_Default_ShouldHaveExpectedDefaults()
        {
            var type = new String();

            type.Name.Should().Be("STRING");
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Family.Should().Be(DataTypeFamily.String);
            type.Description.Should().Be("Logix representation of a System.String");
            type.Value.Should().BeEmpty();
            type.LEN.Should().NotBeNull();
            type.LEN.DataType.Should().BeOfType<Dint>();
            type.DATA.Should().NotBeNull();
            type.DATA.DataType.Should().BeOfType<Sint>();
            type.DATA.Dimension.Should().Be(new Dimensions(82));
            type.DATA.Should().HaveCount(82);
        }

        [Test]
        public void New_WithValue_ShouldHaveExpectedValue()
        {
            var type = new String("This is a test");

            type.Value.Should().Be("This is a test");
        }
        
        [Test]
        public void SetValue_Null_ShouldThrowArgumentNullException()
        {
            var type = new String();
            
            FluentActions.Invoking(() => type.SetValue(null!)).Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void SetValue_EmptyString_ShouldBeEmpty()
        {
            String type = string.Empty;

            type.Value.Should().BeEmpty();
        }
        
        [Test]
        public void SetValue_OutOfRangeString_ShouldThrowArgumentOutOfRangeException()
        {
            String type = "";

            FluentActions.Invoking(() =>
            type =
                "This is a really long test string to see if the argument out of range exception will work. The string length must be less than eighty two characters in length. Do you think this is long enough?")
                .Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void SetValue_ValidString_ShouldHaveExpectedValue()
        {
            var fixture = new Fixture();
            var value = fixture.Create<string>();
            var type = new String();
            
            type.SetValue(value);

            type.Value.Should().Be(value);
        }

        [Test]
        public void ImplicitOperator_LogixType_ShouldBeExpected()
        {
            String type = "This is a test";

            type.Should().Be("This is a test");
        }
        
        [Test]
        public void ImplicitOperator_string_ShouldBeExpected()
        {
            string test = new String("This is a test");

            test.Should().Be("This is a test");
        }

        [Test]
        public void Instantiate_WhenCalled_shouldReturnDifferentInstance()
        {
            var type = new String();

            var instance = type.Instantiate();

            instance.Should().NotBeSameAs(type);
        }
        
        [Test]
        public void TypeEquals_AreEqual_ShouldBeTrue()
        {
            var first = new String();
            var second = new String();

            var result = first.Equals(second);

            result.Should().BeTrue();
        }
        
        [Test]
        public void TypeEquals_AreNotEqual_ShouldBeFalse()
        {
            var first = new String("This is first");
            var second = new String("This is second");

            var result = first.Equals(second);

            result.Should().BeFalse();
        }
        
        [Test]
        public void TypeEquals_AreSame_ShouldBeTrue()
        {
            var first = new String();

            var result = first.Equals(first);

            result.Should().BeTrue();
        }
        
        
        [Test]
        public void TypeEquals_Null_ShouldBeFalse()
        {
            var first = new String();

            var result = first.Equals(null);

            result.Should().BeFalse();
        }
        
        [Test]
        public void ObjectEquals_AreEqual_ShouldBeTrue()
        {
            var first = new String();
            var second = new String();

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }
        
        [Test]
        public void ObjectEquals_AreSame_ShouldBeTrue()
        {
            var first = new String();

            var result = first.Equals((object)first);

            result.Should().BeTrue();
        }
        
        [Test]
        public void ObjectEquals_Null_ShouldBeFalse()
        {
            var first = new String();

            var result = first.Equals((object)null);

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var first = new String();
            var second = new String();

            var result = first == second;

            result.Should().BeTrue();
        }
        
        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = new String();
            var second = new String();

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var type = new String();

            var hash = type.GetHashCode();

            hash.Should().NotBe(0);
        }
        
        [Test]
        public void CompareTo_Same_ShouldBeZero()
        {
            var type = new String();

            var compare = type.CompareTo(type);

            compare.Should().Be(0);
        }
        
        [Test]
        public void CompareTo_Null_ShouldBeOne()
        {
            var type = new String();

            var compare = type.CompareTo(null);

            compare.Should().Be(1);
        }

        [Test]
        public void CompareTo_ValidOther_ShouldBeZero()
        {
            var first = new String();
            var second = new String();

            var compare = first.CompareTo(second);

            compare.Should().Be(0);
        }
    }
}