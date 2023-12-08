using FluentAssertions;

namespace L5Sharp.Tests.Common
{
    [TestFixture]
    public class WatchdogTests
    {
        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var priority = new Watchdog();

            priority.Should().NotBeNull();
        }
        
        [Test]
        public void Default_WhenCalled_ShouldNotBeNull()
        {
            var priority = Watchdog.Default();

            priority.Should().NotBeNull();
        }
        
        [Test]
        public void New_ValidArgument_ShouldNotBeNull()
        {
            var priority = new Watchdog(1000);

            priority.Should().NotBeNull();
        }

        [Test]
        public void New_ValidArgument_ShouldHaveValue()
        {
            var priority = new Watchdog(5000);

            priority.Equals(5000).Should().BeTrue();
        }

        [Test]
        public void New_UnderRange_ShouldThrowException()
        {
            FluentActions.Invoking(() => new Watchdog(0)).Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void New_OverRange_ShouldThrowException()
        {
            FluentActions.Invoking(() => new Watchdog(10000000)).Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void ImplicitOperator_Byte_ShouldBeExpected()
        {
            Watchdog priority = 1000f;

            priority.Equals(1000f).Should().BeTrue();
        }

        [Test]
        public void ImplicitOperator_ScanRate_ShouldBeExpected()
        {
            float priority = new Watchdog(1000f);

            priority.Equals(1000f).Should().BeTrue();
        }

        [Test]
        public void TypedEquals_AreEqual_ShouldBeTrue()
        {
            var first = new Watchdog(1000);
            var second = new Watchdog(1000);

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void TypedEquals_AreNotEqual_ShouldBeFalse()
        {
            var first = new Watchdog(1000);
            var second = new Watchdog(1100);

            var result = first.Equals(second);

            result.Should().BeFalse();
        }

        [Test]
        public void TypedEquals_AreSame_ShouldBeTrue()
        {
            var first = new Watchdog(1000);
            var second = first;

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_AreEqual_ShouldBeTrue()
        {
            var first = new Watchdog(1000);
            var second = new Watchdog(1000);

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_AreSame_ShouldBeTrue()
        {
            var first = new Watchdog(1000);
            var second = first;

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var first = new Watchdog(1000);
            var second = new Watchdog(1000);

            var result = first == second;

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = new Watchdog(1000);
            var second = new Watchdog(1000);

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var first = new Watchdog(1000);

            var hash = first.GetHashCode();

            hash.Should().NotBe(0);
        }

        [Test]
        public void ToString_WhenCalled_ShouldReturnExpectedValue()
        {
            var rate = new Watchdog(1000);

            var value = rate.ToString();

            value.Should().Be("1000");
        }
    }
}