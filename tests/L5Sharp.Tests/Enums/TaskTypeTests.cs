using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Tests.Enums
{
    [TestFixture]
    public class TaskTypeTests
    {
        [Test]
        public void New_Continuous_ShouldNotBeNull()
        {
            var type = TaskType.Continuous;

            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_Periodic_ShouldNotBeNull()
        {
            var type = TaskType.Periodic;

            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_Event_ShouldNotBeNull()
        {
            var type = TaskType.Event;

            type.Should().NotBeNull();
        }
    }
}