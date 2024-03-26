using AutoFixture;
using FluentAssertions;

namespace L5Sharp.Tests.Types.Predefined
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
            type.LEN.Should().NotBeNull();
            type.LEN.Should().BeOfType<DINT>();
            type.DATA.Should().NotBeNull();
            type.DATA.Should().BeOfType<ArrayType<SINT>>();
            type.Members.Should().HaveCount(2);

            var data = type.Members.FirstOrDefault(m => m.Name == "DATA");
            data.Should().NotBeNull();
            data?.Value.Should().NotBeNull();
            data?.Value.Name.Should().NotBeNull();

            var len = type.Members.FirstOrDefault(m => m.Name == "LEN");
            len.Should().NotBeNull();
            len?.Value.Should().NotBeNull();
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
            const string text =
                "'$0B$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00'";

            var type = new STRING(text);

            type.ToString().Should().Be("$0B");
        }

        [Test]
        public void SetValue_OutOfRangeString_ShouldThrowArgumentOutOfRangeException()
        {
            STRING type = "";

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
        public void TypeEquals_AreEqual_ShouldBeTrue()
        {
            var first = new STRING();
            var second = new STRING();

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void TypeEquals_AreNotEqual_ShouldBeFalse()
        {
            var first = new STRING("This is first");
            var second = new STRING("This is second");

            var result = first.Equals(second);

            result.Should().BeFalse();
        }

        [Test]
        public void TypeEquals_AreSame_ShouldBeTrue()
        {
            var first = new STRING();

            var result = first.Equals(first);

            result.Should().BeTrue();
        }


        [Test]
        public void TypeEquals_Null_ShouldBeFalse()
        {
            var first = new STRING();

            var result = first.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void ObjectEquals_AreEqual_ShouldBeTrue()
        {
            var first = new STRING();
            var second = new STRING();

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_AreSame_ShouldBeTrue()
        {
            var first = new STRING();

            var result = first.Equals((object)first);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_Null_ShouldBeFalse()
        {
            var first = new STRING();

            var result = first.Equals((object)null);

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