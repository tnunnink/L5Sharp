using FluentAssertions;
using L5Sharp.Common;

namespace L5Sharp.Tests.Common
{
    [TestFixture]
    public class AddressTests
    {
        [Test]
        public void New_Empty_ShouldNotBeNull()
        {
            var address = new Address(string.Empty);

            address.Should().NotBeNull();
        }

        [Test]
        public void New_SlotNumber_IsSlotShouldBeTrue()
        {
            var address = new Address("1");

            address.IsSlot.Should().BeTrue();
        }
        
        [Test]
        public void New_IP_IsIPv4ShouldBeTrue()
        {
            var address = new Address("192.168.0.1");

            address.IsIPv4.Should().BeTrue();
        }

        [Test]
        public void Slot_WithValue_ShouldHAveExpectedValue()
        {
            var address = Address.Slot(4);

            address.ToSlot().Should().Be(4);
        }
    }
}