using AutoFixture;
using FluentAssertions;

namespace L5Sharp.Tests.Data.Predefined
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
            type.ToString().Should().BeEmpty();
            type.LEN.Should().Be(0);
            type.DATA.Should().BeEmpty();
            type.Members.Should().BeEmpty();
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
        public void New_AsciiStringValue_ShouldBeExpectedValue()
        {
            //Sometimes Studio pads string with zero. Documentation says it does this but in my experience it is not consistent.
            //I have not done enough testing to figure how/when it decides to do this.
            //In any case, if we get input text like this I want to strip of the "empty" parts to just return the string value.
            //The length is not really important to us.
            //This tests validates the code that parses the string input as expected.
            const string text = "'$0B$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00'";

            var type = new STRING(text);

            type.ToString().Should().Be("$0B");
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