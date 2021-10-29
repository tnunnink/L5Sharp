using System;
using FluentAssertions;
using L5Sharp.Abstractions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    internal class TestComponent : Component
    {
        public TestComponent(string name, string description) : base(name, description)
        {
        }
    }

    [TestFixture]
    public class ComponentTests
    {

        [Test]
        public void New_ValidName_ShouldNotBeNull()
        {
            var sut = new TestComponent("Test", "This is a tes");

            sut.Should().NotBeNull();
        }
        
        [Test]
        public void New_NullName_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => new TestComponent(null, "This is a tes")).Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void New_NullDescription_ShouldNotBeNull()
        {
            var sut = new TestComponent("Test", null);

            sut.Should().NotBeNull();
            sut.Description.Should().BeNull();
        }

        [Test]
        public void TypeEquals_Equal_ShouldBeTrue()
        {
            var first = new TestComponent("Test", "This is a test");
            var second = new TestComponent("Test", "This is a test");

            var result = first.Equals(second);

            result.Should().BeTrue();
        }
        
        [Test]
        public void TypeEquals_NotEqual_ShouldBeFalse()
        {
            var first = new TestComponent("First", "This is a test");
            var second = new TestComponent("Second", "This is a test");

            var result = first.Equals(second);

            result.Should().BeFalse();
        }
        
        [Test]
        public void TypeEquals_SameReference_ShouldBeTure()
        {
            var first = new TestComponent("Test", "This is a test");

            var result = first.Equals(first);

            result.Should().BeTrue();
        }

        [Test]
        public void TypeEquals_Null_ShouldBeFalse()
        {
            var first = new TestComponent("Test", "This is a test");

            var result = first.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void ObjectEquals_Equal_ShouldBeTrue()
        {
            var first = new TestComponent("Test", "This is a test");
            var second = (object) new TestComponent("Test", "This is a test");

            var result = first.Equals(second);

            result.Should().BeTrue();
        }
        
        [Test]
        public void ObjectEquals_SameReference_ShouldBeTure()
        {
            var first = new TestComponent("Test", "This is a test");

            var result = first.Equals((object) first);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_Null_ShouldBeFalse()
        {
            var first = new TestComponent("Test", "This is a test");

            var result = first.Equals((object) null);

            result.Should().BeFalse();
        }
        
        [Test]
        public void OperatorEquals_Equal_ShouldBeTrue()
        {
            var first = new TestComponent("Test", "This is a test");
            var second = new TestComponent("Test", "This is a test");

            var result = first == second;

            result.Should().BeTrue();
        }
        
        [Test]
        public void OperatorNotEquals_Equal_ShouldBeFalse()
        {
            var first = new TestComponent("Test", "This is a test");
            var second = new TestComponent("Test", "This is a test");

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var sut = new TestComponent("Test", "This is a tes");

            var result = sut.GetHashCode();

            result.Should().NotBe(0);
        }
    }
}