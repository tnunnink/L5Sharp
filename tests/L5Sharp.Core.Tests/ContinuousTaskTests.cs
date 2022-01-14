using System;
using FluentAssertions;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class ContinuousTaskTests
    {
        [Test]
        public void New_NullName_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new ContinuousTask(null!)).Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void New_ValidName_ShouldNotBeNull()
        {
            var task = new ContinuousTask("Test");

            task.Should().NotBeNull();
        }

        [Test]
        public void New_ValidName_ShouldHaveExpectDefaults()
        {
            var task = new ContinuousTask("Test");

            task.Name.Should().Be("Test");
            task.Type.Should().Be(TaskType.Continuous);
            task.Description.Should().BeEmpty();
            task.Rate.Should().Be(new ScanRate(10));
            task.Priority.Should().Be(new TaskPriority(10));
            task.Watchdog.Should().Be(new Watchdog(500));
            task.InhibitTask.Should().Be(false);
            task.DisableUpdateOutputs.Should().Be(false);
        }

        [Test]
        public void New_Overloaded_ShouldHaveExpectedValues()
        {
            var task = new ContinuousTask("Test", "This is a test task");

            task.Name.Should().Be("Test");
            task.Description.Should().Be("This is a test task");
        }
    }
}