using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Common;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Serialization;
using L5Sharp.Serialization.Core;
using NUnit.Framework;

namespace L5Sharp.Internal.Tests.Serialization
{
    [TestFixture]
    public class TaskSerializerTests
    {
        private readonly string _fileName = Path.Combine(Environment.CurrentDirectory, @"TestFiles\Test.L5X");

        [Test]
        public void TestFileExists()
        {
            FileAssert.Exists(_fileName);
        }

        [Test]
        public void GetContinuousTaskElement_ShouldNotBeNull()
        {
            var element = GetContinuousTaskElement();
            element.Should().NotBeNull();
        }

        [Test]
        public void Deserialize_ContinuousTask_ShouldNotBeNull()
        {
            var element = GetContinuousTaskElement();
            var serializer = new TaskSerializer();

            var task = serializer.Deserialize(element);

            task.Should().NotBeNull();
        }

        [Test]
        public void Deserialize_Null_ShouldThrowArgumentNullException()
        {
            var serializer = new TaskSerializer();

            FluentActions.Invoking(() => serializer.Deserialize(null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Deserialize_ContinuousTask_ShouldHaveExpectedProperties()
        {
            var element = GetContinuousTaskElement();
            var serializer = new TaskSerializer();

            var task = serializer.Deserialize(element);

            task.Name.Should().Be(new ComponentName("Continuous"));
            task.Description.Should().Be("Test continuous task");
            task.Type.Should().Be(TaskType.Continuous);
            task.Priority.Should().Be(new TaskPriority(10));
            task.Rate.Should().Be(new ScanRate());
            task.Watchdog.Should().Be(new Watchdog(500));
            task.InhibitTask.Should().Be(false);
            task.DisableUpdateOutputs.Should().Be(false);
        }
        
        [Test]
        public void GetPeriodicTaskElement_ShouldNotBeNull()
        {
            var element = GetPeriodicTaskElement();
            element.Should().NotBeNull();
        }

        [Test]
        public void Deserialize_PeriodicTask_ShouldNotBeNull()
        {
            var element = GetPeriodicTaskElement();
            var serializer = new TaskSerializer();

            var task = serializer.Deserialize(element);

            task.Should().NotBeNull();
        }
        
        [Test]
        public void Deserialize_PeriodicTask_ShouldHaveExpectedProperties()
        {
            var element = GetPeriodicTaskElement();
            var serializer = new TaskSerializer();

            var task = serializer.Deserialize(element);

            task.Name.Should().Be(new ComponentName("Periodic"));
            task.Description.Should().BeEmpty();
            task.Type.Should().Be(TaskType.Periodic);
            task.Priority.Should().Be(new TaskPriority(10));
            task.Rate.Should().Be(new ScanRate(10));
            task.Watchdog.Should().Be(new Watchdog(500));
            task.InhibitTask.Should().Be(false);
            task.DisableUpdateOutputs.Should().Be(false);
        }

        private XElement GetContinuousTaskElement()
        {
            return XDocument.Load(_fileName).Descendants(LogixNames.Task)
                .FirstOrDefault(e => e.GetComponentName() == "Continuous");
        }
        
        private XElement GetPeriodicTaskElement()
        {
            return XDocument.Load(_fileName).Descendants(LogixNames.Task)
                .FirstOrDefault(e => e.GetComponentName() == "Periodic");
        }
    }
}