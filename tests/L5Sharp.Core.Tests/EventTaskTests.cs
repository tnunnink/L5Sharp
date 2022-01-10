using FluentAssertions;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class EventTaskTests
    {
        [Test]
        public void New_ValidDefault_ShouldNotBeNull()
        {
            var task = new EventTask("Test");

            task.Should().NotBeNull();
        }

        [Test]
        public void New_Overloaded_ShouldHaveExpectedProperties()
        {
            var task = new EventTask("Test", "This is a test task");

            task.Name.Should().Be("Test");
            task.Description.Should().Be("This is a test task");
        }

        [Test]
        public void New_Valid_ShouldHaveExpectedProperties()
        {
            var task = new EventTask("Test");

            task.Name.Should().Be("Test");
            task.Description.Should().BeEmpty();
            task.Type.Should().Be(TaskType.Event);
            task.Rate.Should().Be(new ScanRate(10));
            task.Priority.Should().Be(new TaskPriority(10));
            task.Watchdog.Should().Be(new Watchdog(500));
            task.EventTrigger.Should().Be(TaskEventTrigger.EventInstructionOnly);
            task.EventTag.Should().BeNull();
            task.EnableTimeout.Should().BeFalse();
        }
    }
}