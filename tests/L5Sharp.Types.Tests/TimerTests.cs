using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Types.Tests
{
    [TestFixture]
    public class TimerTests
    {
        [Test]
        public void New_WhenCalled_ShouldNotBeNull()
        {
            var type = new Timer();
            type.Should().NotBeNull();
        }

        [Test]
        public void Members_ShouldNotBeNull()
        {
            var type = new Timer();
            type.DN.Should().NotBeNull();
            type.EN.Should().NotBeNull();
            type.TT.Should().NotBeNull();
            type.ACC.Should().NotBeNull();
            type.PRE.Should().NotBeNull();
        }
    }
}