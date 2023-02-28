using FluentAssertions;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Tests.Enums
{
    [TestFixture]
    public class TaskTriggerTests
    {
        [Test]
        public void New_AxisHome_ShouldNotBeNull()
        {
            var type = TaskEventTrigger.AxisHome;

            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_AxisRegistration1_ShouldNotBeNull()
        {
            var type = TaskEventTrigger.AxisRegistration1;

            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_AxisRegistration2_ShouldNotBeNull()
        {
            var type = TaskEventTrigger.AxisRegistration2;

            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_AxisWatch_ShouldNotBeNull()
        {
            var type = TaskEventTrigger.AxisWatch;

            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_ConsumedTag_ShouldNotBeNull()
        {
            var type = TaskEventTrigger.ConsumedTag;

            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_WindowsEvent_ShouldNotBeNull()
        {
            var type = TaskEventTrigger.WindowsEvent;

            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_EventInstructionOnly_ShouldNotBeNull()
        {
            var type = TaskEventTrigger.EventInstructionOnly;

            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_MotionGroupExecution_ShouldNotBeNull()
        {
            var type = TaskEventTrigger.MotionGroupExecution;

            type.Should().NotBeNull();
        }

        [Test]
        public void New_ModuleInputDataStateChange_ShouldNotBeNull()
        {
            var type = TaskEventTrigger.ModuleInputDataStateChange;

            type.Should().NotBeNull();
        }
    }
}