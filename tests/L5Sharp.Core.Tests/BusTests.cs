using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class BusTests
    {
        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var bus = new Bus();

            bus.Should().NotBeNull();
        }

        [Test]
        public void New_Overload_ShouldNotBeNull()
        {
            var bus = new Bus(10);

            bus.Should().NotBeNull();
        }
        
        [Test]
        public void New_Overload_ShouldBeExpectedValue()
        {
            var bus = new Bus(10);

            bus.Should().BeEquivalentTo(new Bus(10));
        }

        [Test]
        public void Empty_WhenCalled_ShouldBeEqualToDefault()
        {
            var bus = Bus.Empty;

            bus.Should().BeEquivalentTo(new Bus());
        }
        
        [Test]
        public void IsValidSlot_EmptyBus_ShouldBeFalse()
        {
            var bus = Bus.Empty;

            bus.IsValidSlot(1).Should().BeFalse();
        }

        [Test]
        public void IsValidSlot_ValidSlot_ShouldBeTrue()
        {
            var bus = new Bus(10);

            bus.IsValidSlot(1).Should().BeTrue();
        }
        
        [Test]
        public void IsValidSlot_InvalidSlot_ShouldBeFalse()
        {
            var bus = new Bus(10);

            bus.IsValidSlot(11).Should().BeFalse();
        }
        
        [Test]
        public void ImplicitOperator_Byte_ShouldBeExpected()
        {
            Bus bus = 10;

            bus.Equals(10).Should().BeTrue();
        }

        [Test]
        public void ImplicitOperator_Type_ShouldBeExpected()
        {
            byte bus = new Bus(10);

            bus.Equals(10).Should().BeTrue();
        }

        [Test]
        public void TypedEquals_AreEqual_ShouldBeTrue()
        {
            var first = new Bus(10);
            var second = new Bus(10);

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void TypedEquals_AreNotEqual_ShouldBeFalse()
        {
            var first = new Bus(10);
            var second = new Bus(9);

            var result = first.Equals(second);

            result.Should().BeFalse();
        }

        [Test]
        public void TypedEquals_AreSame_ShouldBeTrue()
        {
            var first = new Bus(10);

            var result = first.Equals(first);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_AreEqual_ShouldBeTrue()
        {
            var first = new Bus(10);
            var second = new Bus(10);

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_AreSame_ShouldBeTrue()
        {
            var first = new Bus(10);

            var result = first.Equals((object)first);

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var first = new Bus(10);
            var second = new Bus(10);

            var result = first == second;

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = new Bus(10);
            var second = new Bus(10);

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var first = new Bus(10);

            var hash = first.GetHashCode();

            hash.Should().NotBe(0);
        }

        [Test]
        public void ToString_WhenCalled_ShouldReturnExpectedValue()
        {
            var rate = new Bus(10);

            var value = rate.ToString();

            value.Should().Be("10");
        }
    }
}