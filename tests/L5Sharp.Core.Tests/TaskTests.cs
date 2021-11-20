using System;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class TaskTests
    {
        [Test]
        public void New_ValidName_ShouldNotBeNull()
        {
            var task = Task.Create("Test");

            task.Should().NotBeNull();
        }

        [Test]
        public void New_InvalidName_ShouldThrowInvalidNameException()
        {
            FluentActions.Invoking(() => Task.Create("Test_Task_#!_001")).Should()
                .Throw<ComponentNameInvalidException>();
        }

        [Test]
        public void New_ValidName_ShouldHaveExpectDefaults()
        {
            var task = Task.Create("TaskName");

            task.Name.ToString().Should().Be("TaskName");
            task.Type.Should().Be(TaskType.Periodic);
            task.Description.Should().BeNull();
            task.Rate.Should().Be(new ScanRate(10));
            task.Priority.Should().Be(new TaskPriority(10));
            task.Watchdog.Should().Be(new Watchdog(500));
            task.InhibitTask.Should().Be(false);
            task.DisableUpdateOutputs.Should().Be(false);
        }

        [Test]
        public void Type_GetValue_ShouldBePeriodic()
        {
            var task = Task.Create("Test");

            task.Type.Should().Be(TaskType.Periodic);
        }

        [Test]
        public void SetName_ValidName_ShouldBeExpectedValue()
        {
            var task = Task.Create("TestTask");

            task.SetName("NewTask");

            task.Name.Should().Be("NewTask");
        }

        [Test]
        public void SetName_Null_ShouldThrowArgumentNullException()
        {
            var task = Task.Create("TestTask");

            FluentActions.Invoking(() => task.SetName(null)).Should().Throw<ArgumentNullException>();
        }

        /*[Test]
        public void SetType_ValidType_ShouldBeExpectedType()
        {
            var task = Task.Create("Test");

            task.SetType(TaskType.Continuous);

            task.Type.Should().Be(TaskType.Continuous);
        }

        [Test]
        public void SetType_Null_ShouldThrowArgumentNullException()
        {
            var task = Task.Create("Test");

            FluentActions.Invoking(() => task.SetType(null)).Should().Throw<ArgumentNullException>();
        }*/

        [Test]
        public void SetRate_ValidRange_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var rate = fixture.Create<float>();
            var task = Task.Create("TestTask");

            task.SetRate(new ScanRate(rate));

            task.Rate.Equals(rate).Should().BeTrue();
        }

        [Test]
        public void SetRate_InvalidRange_ShouldThrowArgumentOutOfRangeException()
        {
            var task = Task.Create("TestTask");

            FluentActions.Invoking(() => task.SetRate(new ScanRate(5000000))).Should()
                .Throw<ArgumentOutOfRangeException>();
        }

        /*[Test]
        public void SetPriority_ValidRange_ShouldBeExpectedValue()
        {
            var task = Task.Create("TestTask");

            task.SetPriority(5);

            task.Priority.Should().Be(5);
        }

        [Test]
        public void SetPriority_InvalidRange_ShouldThrowArgumentOutOfRangeException()
        {
            var task = Task.Create("TestTask");

            FluentActions.Invoking(() => task.SetPriority(20)).Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void SetWatchdog_ValidRange_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var watchdog = fixture.Create<int>();
            var task = Task.Create("TestTask");

            task.SetWatchdog(watchdog);

            task.Watchdog.Should().Be(watchdog);
        }

        [Test]
        public void SetWatchdog_InvalidRange_ShouldThrowArgumentOutOfRangeException()
        {
            var task = Task.Create("TestTask");

            FluentActions.Invoking(() => task.SetWatchdog(5000000)).Should().Throw<ArgumentOutOfRangeException>();
        }
        */


        [Test]
        public void AddProgram_NonExistingProgram_ShouldAddProgram()
        {
            var task = Task.Create("TestTask");

            task.ScheduleProgram("Program");

            task.ScheduledPrograms.Should().Contain("Program");
        }

        [Test]
        public void AddProgram_ExistingProgram_ShouldNotThrow()
        {
            var task = Task.Create("TestTask");

            task.ScheduleProgram("Program");

            FluentActions.Invoking(() => task.ScheduleProgram("Program")).Should().NotThrow();
        }

        [Test]
        public void AddProgram_Null_ShouldThrowArgumentException()
        {
            var task = Task.Create("TestTask");

            FluentActions.Invoking(() => task.ScheduleProgram(null)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void RemoveProgram_ExistingProgram_ProgramsShouldBeEmpty()
        {
            var task = Task.Create("TestTask");
            task.ScheduleProgram("Program");

            task.RemoveProgram("Program");

            task.ScheduledPrograms.Should().BeEmpty();
        }

        [Test]
        public void RemoveProgram_NonExistingProgram_ProgramsShouldBeEmpty()
        {
            var task = Task.Create("TestTask");

            task.RemoveProgram("Test");

            task.ScheduledPrograms.Should().BeEmpty();
        }


        [Test]
        public void RemoveProgram_Null_ShouldThrowArgumentException()
        {
            var task = Task.Create("TestTask");

            FluentActions.Invoking(() => task.RemoveProgram(null)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Inhibit_WhenCalled_ShouldSetInhibitToTrue()
        {
            var task = Task.Create("TestTask");

            task.InhibitTask = true;

            task.InhibitTask.Should().BeTrue();
        }

        [Test]
        public void UnInhibit_WhenCalled_ShouldSetInhibitToFalse()
        {
            var task = Task.Create("TestTask");

            task.InhibitTask = false;

            task.InhibitTask.Should().BeFalse();
        }

        [Test]
        public void DisableUpdateOutputs_WhenCalled_ShouldBeTrue()
        {
            var task = Task.Create("TestTask");

            task.DisableUpdateOutputs = true;

            task.DisableUpdateOutputs.Should().BeTrue();
        }

        [Test]
        public void DisableUpdateOutputs_WhenCalled_ShouldBeFalse()
        {
            var task = Task.Create("TestTask");

            task.DisableUpdateOutputs = false;

            task.DisableUpdateOutputs.Should().BeFalse();
        }

        [Test]
        public void TypedEquals_AreEqual_ShouldBeTrue()
        {
            var first = (Task)Task.Create("Test");
            var second =(Task)Task.Create("Test");

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        /*[Test]
        public void TypedEquals_AreNotEqual_ShouldBeFalse()
        {
            var first = (Task)Task.Create("Test", TaskType.Periodic, 3, 500);
            var second = (Task)Task.Create("Test", TaskType.Periodic, 4, 500);

            var result = first.Equals(second);

            result.Should().BeFalse();
        }*/

        [Test]
        public void TypedEquals_AreSame_ShouldBeTrue()
        {
            var first = (Task)Task.Create("Test");
            var second = first;

            var result = first.Equals(second);

            result.Should().BeTrue();
        }


        [Test]
        public void TypedEquals_Null_ShouldBeFalse()
        {
            var first = (Task)Task.Create("Test");

            var result = first.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void ObjectEquals_AreEqual_ShouldBeTrue()
        {
            var first = (Task)Task.Create("Test");
            var second = (Task)Task.Create("Test");

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_AreSame_ShouldBeTrue()
        {
            var first = (Task)Task.Create("Test");
            var second = first;

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }


        [Test]
        public void ObjectEquals_Null_ShouldBeFalse()
        {
            var first = Task.Create("Test");

            var result = first.Equals((object)null);

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var first = (Task)Task.Create("Test");
            var second = (Task)Task.Create("Test");

            var result = first == second;

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = (Task)Task.Create("Test");
            var second = (Task)Task.Create("Test");

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var first = Task.Create("Test");

            var hash = first.GetHashCode();

            hash.Should().NotBe(0);
        }
    }
}