using FluentAssertions;

namespace L5Sharp.Tests.Core.Enums
{
    [TestFixture]
    public class AccessTests
    {
        [Test]
        public void None_WhenCalled_ShouldNotBeNull()
        {
            Access.None.Should().NotBeNull();
        }

        [Test]
        public void ReadOnly_WhenCalled_ShouldNotBeNull()
        {
            Access.ReadOnly.Should().NotBeNull();
        }

        [Test]
        public void ReadWrite_WhenCalled_ShouldNotBeNull()
        {
            Access.ReadWrite.Should().NotBeNull();
        }
    }
}