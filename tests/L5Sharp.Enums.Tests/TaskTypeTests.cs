using FluentAssertions;
using L5Sharp.Core;
using NUnit.Framework;

namespace L5Sharp.Enums.Tests
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
        
        [Test]
        public void Create_ValidNameContinuous_ShouldNotBeNull()
        {
            var type = TaskType.Continuous;

            var task = type.Create("Test"); 

            task.Should().NotBeNull();
        }
        
        [Test]
        public void Create_ValidNamePeriodic_ShouldNotBeNull()
        {
            var type = TaskType.Periodic;

            var task = type.Create("Test"); 

            task.Should().NotBeNull();
        }
        
        [Test]
        public void Create_ValidNameEvent_ShouldNotBeNull()
        {
            var type = TaskType.Event;

            var task = type.Create("Test"); 

            task.Should().NotBeNull();
        }

        [Test]
        public void CreateGeneric_ValidNameContinuous_ShouldNotBeNull()
        {
            var type = TaskType.Create<ContinuousTask>("Test");

            type.Should().NotBeNull();
        }
        
        [Test]
        public void CreateGeneric_ValidNamePeriodic_ShouldNotBeNull()
        {
            var type = TaskType.Create<PeriodicTask>("Test");

            type.Should().NotBeNull();
        }
        
        [Test]
        public void CreateGeneric_ValidNameEvent_ShouldNotBeNull()
        {
            var type = TaskType.Create<EventTask>("Test");
            
            type.Should().NotBeNull();
        }
    }
}