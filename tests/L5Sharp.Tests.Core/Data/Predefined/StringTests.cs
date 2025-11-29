using AutoFixture;
using FluentAssertions;

namespace L5Sharp.Tests.Core.Data.Predefined
{
    [TestFixture]
    public class StringTests
    {
        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var type = new STRING();

            type.Should().NotBeNull();
        }

        [Test]
        public void New_Default_ShouldHaveEmptyValue()
        {
            var type = new STRING();

            type.ToString().Should().BeEmpty();
        }

        [Test]
        public void New_Default_ShouldHaveExpectedDefaults()
        {
            var type = new STRING();

            type.Name.Should().Be("STRING");
            type.Members.Should().BeEmpty();
            type.ToString().Should().BeEmpty();
        }

        [Test]
        public void New_WithValue_ShouldHaveExpectedValue()
        {
            var type = new STRING("This is a test");

            type.ToString().Should().Be("This is a test");
        }

        [Test]
        public void SetValue_EmptyString_ShouldBeEmpty()
        {
            STRING type = string.Empty;

            type.ToString().Should().BeEmpty();
        }

        [Test]
        public void SetValue_ValidString_ShouldHaveExpectedValue()
        {
            var fixture = new Fixture();
            var value = fixture.Create<string>();

            STRING type = value;

            type.ToString().Should().Be(value);
        }

        [Test]
        public void ImplicitOperator_LogixType_ShouldBeExpected()
        {
            STRING type = "This is a test";

            type.Should().BeEquivalentTo("This is a test");
        }

        [Test]
        public void ImplicitOperator_string_ShouldBeExpected()
        {
            string test = new STRING("This is a test");

            test.Should().Be("This is a test");
        }

        [Test]
        public void Equals_AreEqual_ShouldBeTrue()
        {
            var first = new STRING();
            var second = new STRING();

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void Equals_AreNotEqual_ShouldBeFalse()
        {
            var first = new STRING("This is first");
            var second = new STRING("This is second");

            var result = first.Equals(second);

            result.Should().BeFalse();
        }

        [Test]
        public void Equals_AreSame_ShouldBeTrue()
        {
            var first = new STRING();

            // ReSharper disable once EqualExpressionComparison
            var result = first.Equals(first);

            result.Should().BeTrue();
        }


        [Test]
        public void Equals_Null_ShouldBeFalse()
        {
            var first = new STRING();

            var result = first.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var first = new STRING();
            var second = new STRING();

            var result = first == second;

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = new STRING();
            var second = new STRING();

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var type = new STRING();

            var hash = type.GetHashCode();

            hash.Should().NotBe(0);
        }
    }
}