using System;
using FluentAssertions;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class TaskTests
    {
        [Test]
        public void New_NullName_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new Task(null!)).Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void New_ValidName_ShouldNotBeNull()
        {
            var task = new Task("Test");

            task.Should().NotBeNull();
        }

        [Test]
        public void New_ValidName_ShouldHaveExpectDefaults()
        {
            var task = new Task("Test");

            task.Name.Should().Be("Test");
            task.Type.Should().Be(TaskType.Periodic);
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
            var task = new Task("Test", TaskType.Continuous, "This is a test task");

            task.Name.Should().Be("TaskName");
            task.Type.Should().Be(TaskType.Continuous);
            task.Description.Should().Be("This is a test task");
        }

        [Test]
        public void AddProgram_NonExistingProgram_ShouldAddProgram()
        {
            var task = new Task("Test");

            task.ScheduleProgram("Program");

            task.ScheduledPrograms.Should().Contain("Program");
        }

        [Test]
        public void AddProgram_ExistingProgram_ShouldNotThrow()
        {
            var task = new Task("Test");

            task.ScheduleProgram("Program");

            FluentActions.Invoking(() => task.ScheduleProgram("Program")).Should().NotThrow();
        }

        [Test]
        public void AddProgram_Null_ShouldThrowArgumentException()
        {
            var task = new Task("Test");

            FluentActions.Invoking(() => task.ScheduleProgram(null!)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void RemoveProgram_ExistingProgram_ProgramsShouldBeEmpty()
        {
            var task = new Task("Test");
            task.ScheduleProgram("Program");

            task.RemoveProgram("Program");

            task.ScheduledPrograms.Should().BeEmpty();
        }

        [Test]
        public void RemoveProgram_NonExistingProgram_ProgramsShouldBeEmpty()
        {
            var task = new Task("Test");

            task.RemoveProgram("Test");

            task.ScheduledPrograms.Should().BeEmpty();
        }


        [Test]
        public void RemoveProgram_Null_ShouldThrowArgumentException()
        {
            var task = new Task("Test");

            FluentActions.Invoking(() => task.RemoveProgram(null)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void TypedEquals_AreEqual_ShouldBeTrue()
        {
            var first = new Task("Test");
            var second = new Task("Test");

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void TypedEquals_AreSame_ShouldBeTrue()
        {
            var first = new Task("Test");

            var result = first.Equals(first);

            result.Should().BeTrue();
        }


        [Test]
        public void TypedEquals_Null_ShouldBeFalse()
        {
            var first = new Task("Test");

            var result = first.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void ObjectEquals_AreEqual_ShouldBeTrue()
        {
            var first = new Task("Test");
            var second = new Task("Test");

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_AreSame_ShouldBeTrue()
        {
            var first = new Task("Test");

            var result = first.Equals((object)first);

            result.Should().BeTrue();
        }


        [Test]
        public void ObjectEquals_Null_ShouldBeFalse()
        {
            var first = new Task("Test");

            var result = first.Equals((object)null);

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var first = new Task("Test");
            var second = new Task("Test");

            var result = first == second;

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = new Task("Test");
            var second = new Task("Test");

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var first = new Task("Test");

            var hash = first.GetHashCode();

            hash.Should().NotBe(0);
        }
    }
}