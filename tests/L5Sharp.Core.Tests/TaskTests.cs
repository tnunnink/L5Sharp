using System;
using System.Globalization;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Enumerations;
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
            var task = new Task("Test");
            
            task.Should().NotBeNull();
        }
        
        [Test]
        public void New_InvalidName_ShouldThrowInvalidNameException()
        {
            FluentActions.Invoking(() => new Task("Test_Task_#!_001")).Should().Throw<InvalidNameException>();
        }
        
        [Test]
        public void New_ValidName_ShouldHaveExpectDefaults()
        {
            var task = new Task("TaskName");
            
            task.Name.Should().Be("TaskName");
            task.Type.Should().Be(TaskType.Periodic);
            task.Description.Should().BeEmpty();
            task.Rate.Should().Be(10);
            task.Priority.Should().Be(10);
            task.Watchdog.Should().Be(500);
            task.InhibitTask.Should().Be(false);
            task.DisableUpdateOutputs.Should().Be(false);
        }
        
        [Test]
        public void SetName_ValidType_ShouldBeExpectedValue()
        {
            var task = new Task("TestTask");
            
            task.Name = "NewTask";

            task.Name.Should().Be("NewTask");
        }
        
         
        [Test]
        public void SetName_InvalidType_ShouldThrowInvalidNameException()
        {
            var task = new Task("TestTask");
            
            FluentActions.Invoking(() => task.Name = "Invalid Name 01").Should().Throw<InvalidNameException>();
        }

        [Test]
        public void SetType_Continuous_ShouldBeExpectedValue()
        {
            var task = new Task("TestTask");
            
            task.Type = TaskType.Continuous;

            task.Type.Should().Be(TaskType.Continuous);
        }
        
        [Test]
        public void SetType_Event_ShouldBeExpectedValue()
        {
            var task = new Task("TestTask");
            
            task.Type = TaskType.Event;

            task.Type.Should().Be(TaskType.Event);
        }
        
        [Test]
        public void SetType_Null_ShouldThrowArgumentNullException()
        {
            var task = new Task("TestTask");
            
            FluentActions.Invoking(() => task.Type = null).Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void SetRate_ValidRange_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var rate = fixture.Create<int>();
            var task = new Task("TestTask");
            
            task.Rate = rate;

            task.Rate.Should().Be(rate);
        }
        
        [Test]
        public void SetRate_InvalidRange_ShouldThrowArgumentOutOfRangeException()
        {
            var task = new Task("TestTask");
            
            FluentActions.Invoking(() => task.Rate = 5000000).Should().Throw<ArgumentOutOfRangeException>();
        }
        
        [Test]
        public void SetPriority_ValidRange_ShouldBeExpectedValue()
        {
            var task = new Task("TestTask");
            
            task.Priority = 5;

            task.Priority.Should().Be(5);
        }
        
        [Test]
        public void SetPriority_InvalidRange_ShouldThrowArgumentOutOfRangeException()
        {
            var task = new Task("TestTask");
            
            FluentActions.Invoking(() => task.Priority = 20).Should().Throw<ArgumentOutOfRangeException>();
        }
        
        [Test]
        public void SetWatchdog_ValidRange_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var watchdog = fixture.Create<int>();
            var task = new Task("TestTask");
            
            task.Watchdog = watchdog;

            task.Watchdog.Should().Be(watchdog);
        }
        
        [Test]
        public void SetWatchdog_InvalidRange_ShouldThrowArgumentOutOfRangeException()
        {
            var task = new Task("TestTask");
            
            FluentActions.Invoking(() => task.Watchdog = 5000000).Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void AddProgram_NonExistingProgram_ShouldAddProgram()
        {
            var task = new Task("TestTask");
            
            task.NewProgram("Program");

            task.ScheduledPrograms.Should().Contain("Program");
            task.Programs.Should().Contain(p => p.Name == "Program");
        }
        
        [Test]
        public void AddProgram_NonExistingProgramProgramOverload_ShouldAddProgram()
        {
            var task = new Task("TestTask");
            
            task.AddProgram(new Program("TestProgram", ProgramType.EquipmentPhase, "This is a test"));

            task.ScheduledPrograms.Should().Contain("TestProgram");
            task.Programs.Should().Contain(p => p.Name == "TestProgram");
        }
        
        [Test]
        public void AddProgram_ExistingProgram_ShouldThrowNameCollisionException()
        {
            var task = new Task("TestTask");
            
            task.NewProgram("Program");

            FluentActions.Invoking(() => task.NewProgram("Program")).Should().Throw<ComponentNameCollisionException>();
        }
        
        [Test]
        public void RemoveProgram_ExistingProgram_ProgramsShouldBeEmpty()
        {
            var task = new Task("TestTask");
            task.NewProgram("Program");
            
            task.RemoveProgram("Program");
            
            task.ScheduledPrograms.Should().BeEmpty();
            task.Programs.Should().BeEmpty();
        }

        [Test]
        public void RemoveProgram_NonExistingProgram_ProgramsShouldBeEmpty()
        {
            var task = new Task("TestTask");
            task.NewProgram("Program");
            
            task.RemoveProgram("Program");

            task.ScheduledPrograms.Should().BeEmpty();
            task.Programs.Should().BeEmpty();
        }
    }
}