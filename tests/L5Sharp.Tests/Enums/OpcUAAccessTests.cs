using FluentAssertions;

namespace L5Sharp.Tests.Enums
{
    [TestFixture]
    public class OpcUAAccessTests
    {
        [Test]
        public void None_WhenCalled_ShouldNotBeNull()
        {
            OpcUAAccess.None.Should().NotBeNull();
        }

        [Test]
        public void ReadOnly_WhenCalled_ShouldNotBeNull()
        {
            OpcUAAccess.ReadOnly.Should().NotBeNull();
        }

        [Test]
        public void ReadWrite_WhenCalled_ShouldNotBeNull()
        {
            OpcUAAccess.ReadWrite.Should().NotBeNull();
        }
    }
}
