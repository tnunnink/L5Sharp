using FluentAssertions;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Tests.Enums
{
    [TestFixture]
    public class ConnectionPriorityTests
    {
        [Test]
        public void Low_WhenCalled_ShouldNotBeNull()
        {
            ConnectionPriority.Low.Should().NotBeNull();
        }
        
        [Test]
        public void High_WhenCalled_ShouldNotBeNull()
        {
            ConnectionPriority.High.Should().NotBeNull();
        }
        
        [Test]
        public void Scheduled_WhenCalled_ShouldNotBeNull()
        {
            ConnectionPriority.Scheduled.Should().NotBeNull();
        }
        
        [Test]
        public void Urgent_WhenCalled_ShouldNotBeNull()
        {
            ConnectionPriority.Urgent.Should().NotBeNull();
        }
    }
}