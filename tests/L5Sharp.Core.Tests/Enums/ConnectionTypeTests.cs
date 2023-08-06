using FluentAssertions;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Core.Tests.Enums
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
        public void DiagnosticInput_WhenCalled_ShouldNotBeNull()
        {
            ConnectionType.DiagnosticInput.Should().NotBeNull();
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
        
        [Test]
        public void StandardDataDriven_WhenCalled_ShouldNotBeNull()
        {
            ConnectionType.StandardDataDriven.Should().NotBeNull();
        }
        
        [Test]
        public void SafetyInputDataDriven_WhenCalled_ShouldNotBeNull()
        {
            ConnectionType.SafetyInputDataDriven.Should().NotBeNull();
        }
        
        [Test]
        public void SafetyOutputDataDriven_WhenCalled_ShouldNotBeNull()
        {
            ConnectionType.SafetyOutputDataDriven.Should().NotBeNull();
        }
    }
}