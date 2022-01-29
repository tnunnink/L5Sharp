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
    }
}