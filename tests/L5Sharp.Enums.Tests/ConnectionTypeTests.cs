using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Enums.Tests
{
    [TestFixture]
    public class ConnectionTypeTests
    {
        [Test]
        public void Input_WhenCalled_ShouldNotBeNull()
        {
            ConnectionType.Input.Should().NotBeNull();
        }
        
        [Test]
        public void Output_WhenCalled_ShouldNotBeNull()
        {
            ConnectionType.Output.Should().NotBeNull();
        }
        
        [Test]
        public void MotionAsync_WhenCalled_ShouldNotBeNull()
        {
            ConnectionType.MotionAsync.Should().NotBeNull();
        }
        
        [Test]
        public void MotionSync_WhenCalled_ShouldNotBeNull()
        {
            ConnectionType.MotionSync.Should().NotBeNull();
        }
        
        [Test]
        public void SafetyInput_WhenCalled_ShouldNotBeNull()
        {
            ConnectionType.SafetyInput.Should().NotBeNull();
        }
        
        [Test]
        public void SafetyOutput_WhenCalled_ShouldNotBeNull()
        {
            ConnectionType.SafetyOutput.Should().NotBeNull();
        }
        
        [Test]
        public void Unknown_WhenCalled_ShouldNotBeNull()
        {
            ConnectionType.Unknown.Should().NotBeNull();
        }
    }
}