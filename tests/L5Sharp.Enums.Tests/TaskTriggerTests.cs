using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Enums.Tests
{
    [TestFixture]
    public class TaskTriggerTests
    {
        [Test]
        public void New_AxisHome_ShouldNotBeNull()
        {
            var type = TaskTrigger.AxisHome;

            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_AxisRegistration1_ShouldNotBeNull()
        {
            var type = TaskTrigger.AxisRegistration1;

            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_AxisRegistration2_ShouldNotBeNull()
        {
            var type = TaskTrigger.AxisRegistration2;

            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_AxisWatch_ShouldNotBeNull()
        {
            var type = TaskTrigger.AxisWatch;

            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_ConsumedTag_ShouldNotBeNull()
        {
            var type = TaskTrigger.ConsumedTag;

            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_WindowsEvent_ShouldNotBeNull()
        {
            var type = TaskTrigger.WindowsEvent;

            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_EventInstructionOnly_ShouldNotBeNull()
        {
            var type = TaskTrigger.EventInstructionOnly;

            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_MotionGroupExecution_ShouldNotBeNull()
        {
            var type = TaskTrigger.MotionGroupExecution;

            type.Should().NotBeNull();
        }

        [Test]
        public void New_ModuleInputDataStateChange_ShouldNotBeNull()
        {
            var type = TaskTrigger.ModuleInputDataStateChange;

            type.Should().NotBeNull();
        }
    }
}