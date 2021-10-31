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
            var task = new Task("Test");
            
            task.Should().NotBeNull();
        }
        
        [Test]
        public void New_InvalidName_ShouldThrowInvalidNameException()
        {
            FluentActions.Invoking(() => new Task("Test_Task_#!_001")).Should().Throw<ComponentNameInvalidException>();
        }
        
        [Test]
        public void New_ValidName_ShouldHaveExpectDefaults()
        {
            var task = new Task("TaskName");
            
            task.Name.Should().Be("TaskName");
            task.Type.Should().Be(TaskType.Periodic);
            task.Description.Should().BeNull();
            task.Rate.Should().Be(10);
            task.Priority.Should().Be(10);
            task.Watchdog.Should().Be(500);
            task.InhibitTask.Should().Be(false);
            task.DisableUpdateOutputs.Should().Be(false);
        }

        [Test]
        public void Type_GetValue_ShouldBePeriodic()
        {
            var task = new Task("Test");

            task.Type.Should().Be(TaskType.Periodic);
        }
        
        [Test]
        public void SetName_ValidName_ShouldBeExpectedValue()
        {
            var task = new Task("TestTask");
            
            task.SetName("NewTask");

            task.Name.Should().Be("NewTask");
        }
        
         
        [Test]
        public void SetName_InvalidType_ShouldThrowInvalidNameException()
        {
            var task = new Task("TestTask");
            
            FluentActions.Invoking(() => task.SetName("Invalid Name 01")).Should().Throw<ComponentNameInvalidException>();
        }

        [Test]
        public void SetRate_ValidRange_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var rate = fixture.Create<float>();
            var task = new Task("TestTask");
            
            task.SetRate(rate);

            task.Rate.Should().Be(rate);
        }
        
        [Test]
        public void SetRate_InvalidRange_ShouldThrowArgumentOutOfRangeException()
        {
            var task = new Task("TestTask");
            
            FluentActions.Invoking(() => task.SetRate(5000000)).Should().Throw<ArgumentOutOfRangeException>();
        }
        
        [Test]
        public void SetPriority_ValidRange_ShouldBeExpectedValue()
        {
            var task = new Task("TestTask");
            
            task.SetPriority(5);

            task.Priority.Should().Be(5);
        }
        
        [Test]
        public void SetPriority_InvalidRange_ShouldThrowArgumentOutOfRangeException()
        {
            var task = new Task("TestTask");
            
            FluentActions.Invoking(() => task.SetPriority(20)).Should().Throw<ArgumentOutOfRangeException>();
        }
        
        [Test]
        public void SetWatchdog_ValidRange_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var watchdog = fixture.Create<int>();
            var task = new Task("TestTask");
            
            task.SetWatchdog(watchdog);

            task.Watchdog.Should().Be(watchdog);
        }
        
        [Test]
        public void SetWatchdog_InvalidRange_ShouldThrowArgumentOutOfRangeException()
        {
            var task = new Task("TestTask");
            
            FluentActions.Invoking(() => task.SetWatchdog(5000000)).Should().Throw<ArgumentOutOfRangeException>();
        }
        
        
        [Test]
        public void AddProgram_NonExistingProgram_ShouldAddProgram()
        {
            var task = new Task("TestTask");
            
            task.AddProgram("Program");

            task.ScheduledPrograms.Should().Contain("Program");
        }

        [Test]
        public void AddProgram_ExistingProgram_ShouldNotThrow()
        {
            var task = new Task("TestTask");
            
            task.AddProgram("Program");

            FluentActions.Invoking(() => task.AddProgram("Program")).Should().NotThrow();
        }
        
        [Test]
        public void AddProgram_Null_ShouldThrowArgumentException()
        {
            var task = new Task("TestTask");
            
            FluentActions.Invoking(() => task.AddProgram(null)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void NewProgram_NonExistingProgram_ShouldAddProgram()
        {
            var task = new Task("TestTask");
            
            var program = task.NewProgram("Program");

            task.ScheduledPrograms.Should().Contain("Program");
            program.Should().NotBeNull();
        }

        [Test]
        public void NewProgram_ExistingProgram_ShouldThrowNameCollisionException()
        {
            var task = new Task("TestTask");
            
            task.AddProgram("Program");

            FluentActions.Invoking(() => task.NewProgram("Program")).Should().Throw<ComponentNameCollisionException>();
        }
        
        [Test]
        public void NewProgram_Null_ShouldThrowArgumentNullException()
        {
            var task = new Task("TestTask");
            
            FluentActions.Invoking(() => task.NewProgram(null)).Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void RemoveProgram_ExistingProgram_ProgramsShouldBeEmpty()
        {
            var task = new Task("TestTask");
            task.AddProgram("Program");
            
            task.RemoveProgram("Program");
            
            task.ScheduledPrograms.Should().BeEmpty();
        }

        [Test]
        public void RemoveProgram_NonExistingProgram_ProgramsShouldBeEmpty()
        {
            var task = new Task("TestTask");

            task.RemoveProgram("Test");

            task.ScheduledPrograms.Should().BeEmpty();
        }
        
        
        [Test]
        public void RemoveProgram_Null_ShouldThrowArgumentException()
        {
            var task = new Task("TestTask");
            
            FluentActions.Invoking(() => task.RemoveProgram(null)).Should().Throw<ArgumentException>();
        }
    }
}