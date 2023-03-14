using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;

namespace L5Sharp.Tests.Components
{
    [TestFixture]
    public class TaskTests
    {
        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var task = new LogixTask();

            task.Should().NotBeNull();
        }
        
        [Test]
        public void New_Default_ShouldHaveDefaults()
        {
            var task = new LogixTask();

            task.Name.Should().BeEmpty();
            task.Type.Should().Be(TaskType.Periodic);
            task.Description.Should().BeEmpty();
            task.Priority.Should().Be(new TaskPriority(10));
            task.Rate.Should().Be(new ScanRate(10));
            task.Watchdog.Should().Be(new Watchdog(500));
            task.InhibitTask.Should().BeFalse();
            task.DisableUpdateOutputs.Should().BeFalse();
            task.ScheduledPrograms.Should().BeEmpty();
        }
        
        [Test]
        public void New_WithValues_ShouldHaveExpectedValues()
        {
            var task = new LogixTask
            {
                Name = "Test",
                Type = TaskType.Continuous,
                Description = "This is a test",
                Priority = new TaskPriority(13),
                Rate = new ScanRate(300),
                Watchdog = new Watchdog(501),
                InhibitTask = true,
                DisableUpdateOutputs = true,
                ScheduledPrograms = new List<string>
                {
                    "MainProgram",
                    "TestProgram",
                    "Another"
                }
            };

            task.Name.Should().Be("Test");
            task.Description.Should().Be("This is a test");
            task.Type.Should().Be(TaskType.Continuous);
            task.Priority.Should().Be(new TaskPriority(13));
            task.Rate.Should().Be(new ScanRate(300));
            task.Watchdog.Should().Be(new Watchdog(501));
            task.InhibitTask.Should().BeTrue();
            task.DisableUpdateOutputs.Should().BeTrue();
            task.ScheduledPrograms.Should().HaveCount(3);
        }

        [Test]
        public void Clone_WhenCalled_ShouldBeAllNewReferences()
        {
            var task = Logix.Task("Test");

            var clone = task.Clone();

            clone.Should().NotBeSameAs(task);
            clone.Name.Should().Be(task.Name);

            clone.Name = "NewName";
            clone.Name.Should().NotBeSameAs(task.Name);
            task.Name.Should().Be("Test");
            
            clone.Type = TaskType.Event;
            task.Type.Should().Be(TaskType.Periodic);
            
            clone.ScheduledPrograms.Add("New");
            clone.ScheduledPrograms.Should().HaveCount(1);
            task.ScheduledPrograms.Should().BeEmpty();
        }
    }
}