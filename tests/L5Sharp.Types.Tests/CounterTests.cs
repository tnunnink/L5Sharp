using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Types.Tests
{
    [TestFixture]
    public class CounterTests
    {
        [Test]
        public void New_WhenCalled_ShouldNotBeNull()
        {
            var type = new Counter();
            type.Should().NotBeNull();
        }

        [Test]
        public void Members_ShouldNotBeNull()
        {
            var type = new Counter();
            type.DN.Should().NotBeNull();
            type.CD.Should().NotBeNull();
            type.CU.Should().NotBeNull();
            type.OV.Should().NotBeNull();
            type.UN.Should().NotBeNull();
            type.ACC.Should().NotBeNull();
            type.PRE.Should().NotBeNull();
        }
    }
}