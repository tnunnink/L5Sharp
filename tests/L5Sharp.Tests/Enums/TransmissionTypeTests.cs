using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Tests.Enums
{
    [TestFixture]
    public class TransmissionTypeTests
    {
        [Test]
        public void New_Null_ShouldNotBeNull()
        {
            var type = TransmissionType.Null;

            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_Multicast_ShouldNotBeNull()
        {
            var type = TransmissionType.Multicast;

            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_Unicast_ShouldNotBeNull()
        {
            var type = TransmissionType.Unicast;

            type.Should().NotBeNull();
        }
    }
}