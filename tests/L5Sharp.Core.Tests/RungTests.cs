using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class RungTests
    {
        [Test]
        public void New_InValidArguments_ShouldThrowArgumentException()
        {
            var rung = new Rung("XIC(SomeTag)OTU(AnotherTag);");

            rung.Should().NotBeNull();
        }
        
        [Test]
        public void New_ValidArguments_ShouldNotBeNull()
        {
            var rung = new Rung("XIC(SomeTag)OTU(AnotherTag);", "This is a test rung");

            rung.Should().NotBeNull();
        }

        [Test]
        public void New_ValidArguments_ShouldHaveExpectedProperties()
        {
            var rung = new Rung("NOP();", "This is a test");

            rung.Number.Should().Be(0);
            rung.Type.Should().Be(RungType.Normal);
            rung.Comment.Should().Be("This is a test");
            rung.Text.Should().Be(new NeutralText("NOP();"));
        }

        [Test]
        public void TypedEquals_AreEqual_ShouldBeTrue()
        {
            var first = new Rung("Test");
            var second = new Rung("Test");

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void TypedEquals_AreSame_ShouldBeTrue()
        {
            var first = new Rung("Test");

            var result = first.Equals(first);

            result.Should().BeTrue();
        }


        [Test]
        public void TypedEquals_Null_ShouldBeFalse()
        {
            var first = new Rung("Test");

            var result = first.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void ObjectEquals_AreEqual_ShouldBeTrue()
        {
            var first = new Rung("Test");
            var second = new Rung("Test");

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_AreSame_ShouldBeTrue()
        {
            var first = new Rung("Test");
            var second = first;

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }


        [Test]
        public void ObjectEquals_Null_ShouldBeFalse()
        {
            var first = new Rung("Test");

            var result = first.Equals((object)null);

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var first = new Rung("Test");
            var second = new Rung("Test");

            var result = first == second;

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = new Rung("Test");
            var second = new Rung("Test");

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var first = new Rung("Test");

            var hash = first.GetHashCode();

            hash.Should().NotBe(0);
        }
    }
}