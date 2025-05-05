using FluentAssertions;

namespace L5Sharp.Tests.Core.Common
{
    [TestFixture]
    public class AddressTests
    {
        [Test]
        public void New_Null_ShouldThrowException()
        {
            var action = () => new Address(null);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void New_Empty_ShouldThrowException()
        {
            var action = () => new Address(string.Empty);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void New_SlotNumber_IsSlotShouldBeTrue()
        {
            var address = new Address("1");

            address.IsSlot.Should().BeTrue();
        }

        [Test]
        public void New_IP_IsNetworkShouldBeTrue()
        {
            var address = new Address("192.168.0.1");

            address.IsNetwork.Should().BeTrue();
        }

        [Test]
        public void New_HostName_IsNetworkShouldBeTrue()
        {
            var address = new Address("MyHostName");

            address.IsNetwork.Should().BeTrue();
        }

        [Test]
        public void Slot_WithValue_ShouldHAveExpectedValue()
        {
            var address = Address.NewSlot(4);

            address.ToSlot().Should().Be(4);
        }
    }
}