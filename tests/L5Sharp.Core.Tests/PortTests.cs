using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class PortTests
    {
        [Test]
        public void New_ValidArguments_ShouldNotBeNull()
        {
            var port = new Port(1, "0", "ICP");

            port.Should().NotBeNull();
        }
        
        [Test]
        public void New_ValidArguments_ShouldHaveExpectedProperties()
        {
            var port = new Port(1, "0", "ICP");

            port.Id.Should().Be(1);
            port.Address.Should().Be("0");
            port.Type.Should().Be("ICP");
            port.Upstream.Should().Be(false);
            port.Bus.Should().BeNull();
        }
        
        [Test]
        public void New_Overrides_ShouldHaveExpectedProperties()
        {
            var port = new Port(1, "0", "ICP", true, new Bus(1));

            port.Id.Should().Be(1);
            port.Address.Should().Be("0");
            port.Type.Should().Be("ICP");
            port.Upstream.Should().Be(true);
            port.Bus?.Size.Should().Be(1);
        }
    }
}