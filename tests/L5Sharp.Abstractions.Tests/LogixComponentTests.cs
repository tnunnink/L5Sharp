using System;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Exceptions;
using NUnit.Framework;

namespace L5Sharp.Abstractions.Tests
{
    [TestFixture]
    public class LogixComponentTests
    {

        [Test]
        public void New_ValidName_ShouldNotBeNull()
        {
            var sut = new TestLogixComponent("Test", "This is a tes");

            sut.Should().NotBeNull();
        }
        
        [Test]
        public void New_NullName_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => new TestLogixComponent(null, "This is a tes")).Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void New_NullDescription_ShouldNotBeNull()
        {
            var sut = new TestLogixComponent("Test", null);

            sut.Should().NotBeNull();
            sut.Description.Should().BeNull();
        }
        
        [Test]
        public void SetName_Null_ShouldThrowArgumentException()
        {
            var sut = new TestLogixComponent("Test", "This is a test");
            
            FluentActions.Invoking(() => sut.SetName(null)).Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void SetName_Empty_ShouldThrowArgumentException()
        {
            var sut = new TestLogixComponent("Test", "This is a test");
            
            FluentActions.Invoking(() => sut.SetName(string.Empty)).Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void SetName_InvalidName_ShouldThrowInvalidNameException()
        {
            var fixture = new Fixture();
            var sut = new TestLogixComponent("Test", "This is a test");
            
            FluentActions.Invoking(() => sut.SetName(fixture.Create<string>())).Should().Throw<ComponentNameInvalidException>();
        }

        [Test]
        public void SetName_ValidName_ShouldBeExpectedValue()
        {
            var sut = new TestLogixComponent("Test", "This is a test");
            
            sut.SetName("New_Name");

            sut.Name.Should().Be("New_Name");
        }
        
        [Test]
        public void SetDescription_Null_ShouldBeExpected()
        {
            var sut = new TestLogixComponent("Test", "This is a test");
            
            sut.SetDescription(null);

            sut.Description.Should().Be(null);
        }
        
        [Test]
        public void SetDescription_Empty_ShouldBeExpected()
        {
            var sut = new TestLogixComponent("Test", "This is a test");
            
            sut.SetDescription(string.Empty);

            sut.Description.Should().Be(string.Empty);
        }

        [Test]
        public void SetDescription_ValidName_ShouldBeExpected()
        {
            var sut = new TestLogixComponent("Test", "This is a test");
            
            sut.SetDescription("This is a different description for testing");

            sut.Description.Should().Be("This is a different description for testing");
        }

        [Test]
        public void TypeEquals_Equal_ShouldBeTrue()
        {
            var first = new TestLogixComponent("Test", "This is a test");
            var second = new TestLogixComponent("Test", "This is a test");

            var result = first.Equals(second);

            result.Should().BeTrue();
        }
        
        [Test]
        public void TypeEquals_NotEqual_ShouldBeFalse()
        {
            var first = new TestLogixComponent("First", "This is a test");
            var second = new TestLogixComponent("Second", "This is a test");

            var result = first.Equals(second);

            result.Should().BeFalse();
        }
        
        [Test]
        public void TypeEquals_SameReference_ShouldBeTure()
        {
            var first = new TestLogixComponent("Test", "This is a test");

            var result = first.Equals(first);

            result.Should().BeTrue();
        }

        [Test]
        public void TypeEquals_Null_ShouldBeFalse()
        {
            var first = new TestLogixComponent("Test", "This is a test");

            var result = first.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void ObjectEquals_Equal_ShouldBeTrue()
        {
            var first = new TestLogixComponent("Test", "This is a test");
            var second = (object) new TestLogixComponent("Test", "This is a test");

            var result = first.Equals(second);

            result.Should().BeTrue();
        }
        
        [Test]
        public void ObjectEquals_SameReference_ShouldBeTure()
        {
            var first = new TestLogixComponent("Test", "This is a test");

            var result = first.Equals((object) first);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_Null_ShouldBeFalse()
        {
            var first = new TestLogixComponent("Test", "This is a test");

            var result = first.Equals((object) null);

            result.Should().BeFalse();
        }
        
        [Test]
        public void OperatorEquals_Equal_ShouldBeTrue()
        {
            var first = new TestLogixComponent("Test", "This is a test");
            var second = new TestLogixComponent("Test", "This is a test");

            var result = first == second;

            result.Should().BeTrue();
        }
        
        [Test]
        public void OperatorNotEquals_Equal_ShouldBeFalse()
        {
            var first = new TestLogixComponent("Test", "This is a test");
            var second = new TestLogixComponent("Test", "This is a test");

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var sut = new TestLogixComponent("Test", "This is a tes");

            var result = sut.GetHashCode();

            result.Should().NotBe(0);
        }
    }
}