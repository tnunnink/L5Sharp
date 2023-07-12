using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Elements;
using NUnit.Framework;

namespace L5Sharp.Tests.Core
{
    [TestFixture]
    public class PortTests
    {
        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var port = new Port();

            port.Should().NotBeNull();
        }

        [Test]
        public void New_Default_ShouldHaveExpected()
        {
            var port = new Port();

            port.Id.Should().Be(0);
            port.Type.Should().Be(string.Empty);
            port.Upstream.Should().BeFalse();
            port.Address.Should().Be(Address.None);
            port.BusSize.Should().BeNull();
        }

        [Test]
        public void New_WithValues_ShouldHaveExpectedProperties()
        {
            var port = new Port
            {
                Id = 1,
                Type = "ICP",
                Address = "0",
                Upstream = true,
                BusSize = 17,
            };

            port.Id.Should().Be(1);
            port.Type.Should().Be("ICP");
            port.Upstream.Should().BeTrue();
            port.Address.Should().Be(new Address("0"));
            port.BusSize.Should().Be(17);
        }
    }
}