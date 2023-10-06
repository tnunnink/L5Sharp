using FluentAssertions;
using L5Sharp.Common;

namespace L5Sharp.Tests.Common
{
    [TestFixture]
    public class ScanRateTests
    {
        [Test]
        public void New_ValidRange_ShouldNotBeNull()
        {
            var rate = new ScanRate(1000);

            rate.Should().NotBeNull();
        }

        [Test]
        public void New_Zero_ShouldThrowArgumentOutOfRangeException()
        {
            FluentActions.Invoking(() => new ScanRate(0)).Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void New_Over2M_ShouldThrowArgumentOutOfRangeException()
        {
            FluentActions.Invoking(() => new ScanRate(2000000.1f)).Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void New_ValidRange_ShouldBeExpectedValue()
        {
            var rate = new ScanRate(5000);

            rate.Should().Be(new ScanRate(5000));
        }

        [Test]
        public void ImplicitOperator_Float_ShouldBeExpected()
        {
            ScanRate rate = 10f;

            rate.Equals(10f).Should().BeTrue();
        }

        [Test]
        public void ImplicitOperator_ScanRate_ShouldBeExpected()
        {
            float rate = new ScanRate(5000);

            rate.Equals(5000).Should().BeTrue();
        }

        [Test]
        public void TypedEquals_AreEqual_ShouldBeTrue()
        {
            var first = new ScanRate(1000);
            var second = new ScanRate(1000);

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void TypedEquals_AreNotEqual_ShouldBeFalse()
        {
            var first = new ScanRate(1000);
            var second = new ScanRate(2000);

            var result = first.Equals(second);

            result.Should().BeFalse();
        }

        [Test]
        public void TypedEquals_AreSame_ShouldBeTrue()
        {
            var first = new ScanRate(1000);
            var second = first;

            var result = first.Equals(second);

            result.Should().BeTrue();
        }


        [Test]
        public void TypedEquals_Null_ShouldBeFalse()
        {
            var first = new ScanRate(1000);

            var result = first.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void ObjectEquals_AreEqual_ShouldBeTrue()
        {
            var first = new ScanRate(1000);
            var second = new ScanRate(1000);

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_AreSame_ShouldBeTrue()
        {
            var first = new ScanRate(1000);
            var second = first;

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }


        [Test]
        public void ObjectEquals_Null_ShouldBeFalse()
        {
            var first = new ScanRate(1000);

            var result = first.Equals((object)null);

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var first = new ScanRate(1000);
            var second = new ScanRate(1000);

            var result = first == second;

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = new ScanRate(1000);
            var second = new ScanRate(1000);

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var first = new ScanRate(1000);

            var hash = first.GetHashCode();

            hash.Should().NotBe(0);
        }

        [Test]
        public void ToString_WhenCalled_ShouldReturnExpectedValue()
        {
            var rate = new ScanRate(1000);

            var value = rate.ToString();

            value.Should().Be("1000");
        }
    }
}