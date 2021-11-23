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
        public void Create_ValidName_ShouldNotBeNull()
        {
            var task = Task.Create("Test");

            task.Should().NotBeNull();
        }

        [Test]
        public void Create_InvalidName_ShouldThrowInvalidNameException()
        {
            FluentActions.Invoking(() => Task.Create("Test_Task_#!_001")).Should()
                .Throw<ComponentNameInvalidException>();
        }

        [Test]
        public void Create_ValidName_ShouldHaveExpectDefaults()
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
        public void Create_GetValue_ShouldBePeriodic()
        {
            var task = Task.Create("Test");

            task.Type.Should().Be(TaskType.Periodic);
        }

        [Test]
        public void Build_NoOverloads_ShouldNotBeNull()
        {
            var task = Task.Build("Test").Create();

            task.Should().NotBeNull();
        }
        
        [Test]
        public void Build_Overloads_ShouldHaveExpected()
        {
            var task = Task.Build("Test")
                .OfType(TaskType.Periodic)
                .WithDescription("This is a test")
                .WithRate(new ScanRate(1000))
                .Create();

            task.Name.ToString().Should().Be("Test");
            task.Description.Should().Be("This is a test");
            task.Type.Should().Be(TaskType.Periodic);
            task.Rate.Should().Be(new ScanRate(1000));
        }

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
        public void TypedEquals_AreEqual_ShouldBeTrue()
        {
            var first = (Task)Task.Create("Test");
            var second =(Task)Task.Create("Test");

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

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