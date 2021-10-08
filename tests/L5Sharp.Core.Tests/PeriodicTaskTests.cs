using System;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class PeriodicTaskTests
    {
        [Test]
        public void New_ValidName_ShouldNotBeNull()
        {
            var task = new PeriodicTask("Test");
            
            task.Should().NotBeNull();
        }
        
        [Test]
        public void New_InvalidName_ShouldThrowInvalidNameException()
        {
            FluentActions.Invoking(() => new PeriodicTask("Test_Task_#!_001")).Should().Throw<InvalidNameException>();
        }
        
        [Test]
        public void New_ValidName_ShouldHaveExpectDefaults()
        {
            var task = new PeriodicTask("TaskName");
            
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
            var task = new PeriodicTask("TestTask");
            
            task.Name = "NewTask";

            task.Name.Should().Be("NewTask");
        }
        
         
        [Test]
        public void SetName_InvalidType_ShouldThrowInvalidNameException()
        {
            var task = new PeriodicTask("TestTask");
            
            FluentActions.Invoking(() => task.Name = "Invalid Name 01").Should().Throw<InvalidNameException>();
        }

        [Test]
        public void SetRate_ValidRange_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var rate = fixture.Create<float>();
            var task = new PeriodicTask("TestTask");
            
            task.Rate = rate;

            task.Rate.Should().Be(rate);
        }
        
        [Test]
        public void SetRate_InvalidRange_ShouldThrowArgumentOutOfRangeException()
        {
            var task = new PeriodicTask("TestTask");
            
            FluentActions.Invoking(() => task.Rate = 5000000).Should().Throw<ArgumentOutOfRangeException>();
        }
        
        [Test]
        public void SetPriority_ValidRange_ShouldBeExpectedValue()
        {
            var task = new PeriodicTask("TestTask");
            
            task.Priority = 5;

            task.Priority.Should().Be(5);
        }
        
        [Test]
        public void SetPriority_InvalidRange_ShouldThrowArgumentOutOfRangeException()
        {
            var task = new PeriodicTask("TestTask");
            
            FluentActions.Invoking(() => task.Priority = 20).Should().Throw<ArgumentOutOfRangeException>();
        }
        
        [Test]
        public void SetWatchdog_ValidRange_ShouldBeExpectedValue()
        {
            var fixture = new Fixture();
            var watchdog = fixture.Create<int>();
            var task = new PeriodicTask("TestTask");
            
            task.Watchdog = watchdog;

            task.Watchdog.Should().Be(watchdog);
        }
        
        [Test]
        public void SetWatchdog_InvalidRange_ShouldThrowArgumentOutOfRangeException()
        {
            var task = new PeriodicTask("TestTask");
            
            FluentActions.Invoking(() => task.Watchdog = 5000000).Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void AddProgram_NonExistingProgram_ShouldAddProgram()
        {
            var task = new PeriodicTask("TestTask");
            
            task.NewProgram("Program");

            task.ScheduledPrograms.Should().Contain("Program");
        }

        [Test]
        public void AddProgram_ExistingProgram_ShouldThrowNameCollisionException()
        {
            var task = new PeriodicTask("TestTask");
            
            task.NewProgram("Program");

            FluentActions.Invoking(() => task.NewProgram("Program")).Should().Throw<ComponentNameCollisionException>();
        }
        
        [Test]
        public void RemoveProgram_ExistingProgram_ProgramsShouldBeEmpty()
        {
            var task = new PeriodicTask("TestTask");
            task.NewProgram("Program");
            
            task.RemoveProgram("Program");
            
            task.ScheduledPrograms.Should().BeEmpty();
        }

        [Test]
        public void RemoveProgram_NonExistingProgram_ProgramsShouldBeEmpty()
        {
            var task = new PeriodicTask("TestTask");
            task.NewProgram("Program");
            
            task.RemoveProgram("Program");

            task.ScheduledPrograms.Should().BeEmpty();
        }
    }
}