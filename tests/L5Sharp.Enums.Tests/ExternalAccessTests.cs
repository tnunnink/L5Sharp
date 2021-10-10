using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Enums.Tests
{
    [TestFixture]
    public class ExternalAccessTests
    {
        [Test]
        public void None_WhenCalled_ShouldNotBeNull()
        {
            ExternalAccess.None.Should().NotBeNull();
        }
        
        [Test]
        public void ReadOnly_WhenCalled_ShouldNotBeNull()
        {
            ExternalAccess.ReadOnly.Should().NotBeNull();
        }
        
        [Test]
        public void ReadWrite_WhenCalled_ShouldNotBeNull()
        {
            ExternalAccess.ReadWrite.Should().NotBeNull();
        }
    }
}