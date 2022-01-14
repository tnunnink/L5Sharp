using System;
using System.Collections.Generic;
using System.Xml.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Serialization.Tests
{
    [TestFixture]
    public class TaskSerializerTests
    {
        private TaskSerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new TaskSerializer();
        }

        [Test]
        public void Serialize_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _serializer.Serialize(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Serialize_Task_ShouldNotBeNull()
        {
            var task = new ContinuousTask("Test");

            var xml = _serializer.Serialize(task);

            xml.Should().NotBeNull();
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_ContinuousTask_ShouldBeApproved()
        {
            var task = new ContinuousTask("Test", new ScanRate(100), new TaskPriority(10), new Watchdog(5000), true,
                false, new List<string>
                {
                    "Test_Program_01",
                    "Test_Program_02",
                    "Test_Program_03"
                },
                "This is a test continuous task");

            var xml = _serializer.Serialize(task);

            Approvals.VerifyXml(xml.ToString());
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_PeriodicTask_ShouldBeApproved()
        {
            var task = new PeriodicTask("Test", new ScanRate(100), new TaskPriority(10), new Watchdog(5000), true,
                false, new List<string>
                {
                    "Test_Program_01",
                    "Test_Program_02",
                    "Test_Program_03"
                },
                "This is a test periodic task");

            var xml = _serializer.Serialize(task);

            Approvals.VerifyXml(xml.ToString());
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_EventTask_ShouldBeApproved()
        {
            var task = new PeriodicTask("Test", new ScanRate(100), new TaskPriority(10), new Watchdog(5000), true,
                false, new List<string>
                {
                    "Test_Program_01",
                    "Test_Program_02",
                    "Test_Program_03"
                },
                "This is a test event task");

            var xml = _serializer.Serialize(task);

            Approvals.VerifyXml(xml.ToString());
        }

        [Test]
        public void Deserialize_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _serializer.Deserialize(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Deserialize_NoTaskType_ShouldThrowArgumentException()
        {
            var element = XElement.Parse(GetNoTypeTaskData());

            FluentActions.Invoking(() => _serializer.Deserialize(element)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Deserialize_ContinuousTask_ShouldNotBeNull()
        {
            var element = XElement.Parse(GetContinuousTaskData());

            var task = _serializer.Deserialize(element);

            task.Should().NotBeNull();
        }

        [Test]
        public void Deserialize_ContinuousTask_ShouldHaveExpectedProperties()
        {
            var element = XElement.Parse(GetContinuousTaskData());

            var task = _serializer.Deserialize(element);

            task.Name.Should().Be(new ComponentName("Continuous"));
            task.Description.Should().Be("Test Continuous task");
            task.Type.Should().Be(TaskType.Continuous);
            task.Priority.Should().Be(new TaskPriority(10));
            task.Rate.Should().Be(new ScanRate());
            task.Watchdog.Should().Be(new Watchdog(500));
            task.InhibitTask.Should().Be(false);
            task.DisableUpdateOutputs.Should().Be(false);
        }

        [Test]
        public void Deserialize_PeriodicTask_ShouldNotBeNull()
        {
            var element = XElement.Parse(GetPeriodicTaskData());

            var task = _serializer.Deserialize(element);

            task.Should().NotBeNull();
        }

        [Test]
        public void Deserialize_PeriodicTask_ShouldHaveExpectedProperties()
        {
            var element = XElement.Parse(GetPeriodicTaskData());

            var task = _serializer.Deserialize(element);

            task.Name.Should().Be(new ComponentName("Periodic"));
            task.Description.Should().Be("Test Periodic task");
            task.Type.Should().Be(TaskType.Periodic);
            task.Priority.Should().Be(new TaskPriority(5));
            task.Rate.Should().Be(new ScanRate(1000));
            task.Watchdog.Should().Be(new Watchdog(5000));
            task.InhibitTask.Should().Be(true);
            task.DisableUpdateOutputs.Should().Be(true);
        }

        [Test]
        public void Deserialize_EventTask_ShouldNotBeNull()
        {
            var element = XElement.Parse(GetEventTaskData());

            var task = _serializer.Deserialize(element);

            task.Should().NotBeNull();
        }

        [Test]
        public void Deserialize_EventTask_ShouldHaveExpectedProperties()
        {
            var element = XElement.Parse(GetEventTaskData());

            var task = _serializer.Deserialize(element);

            task.Name.Should().Be(new ComponentName("Event"));
            task.Description.Should().Be("Test Event task");
            task.Type.Should().Be(TaskType.Event);
            task.Priority.Should().Be(new TaskPriority(10));
            task.Rate.Should().Be(new ScanRate(10));
            task.Watchdog.Should().Be(new Watchdog(500));
            task.InhibitTask.Should().Be(false);
            task.DisableUpdateOutputs.Should().Be(true);
        }

        [Test]
        public void Deserialize_TaskWithPrograms_ShouldHaveExpectedPrograms()
        {
            var element = XElement.Parse(GetTaskWithPrograms());

            var task = _serializer.Deserialize(element);

            task.Should().NotBeNull();
            task.ScheduledPrograms.Should().HaveCount(2);
            task.ScheduledPrograms.Should().Contain(p => p == "NProgram");
            task.ScheduledPrograms.Should().Contain(p => p == "EPProgram");
        }

        private static string GetNoTypeTaskData()
        {
            return
                @"<Task Name=""Continuous"" Priority=""10"" Watchdog=""500"" DisableUpdateOutputs=""false"" InhibitTask=""false"">
                <Description>
                <![CDATA[Test continuous task]]>
                </Description>
                <ScheduledPrograms>
                <ScheduledProgram Name=""MainProgram""/>
                </ScheduledPrograms>
                </Task>";
        }

        private static string GetContinuousTaskData()
        {
            return
                @"<Task Name=""Continuous"" Type=""CONTINUOUS"" Priority=""10"" Watchdog=""500"" DisableUpdateOutputs=""false"" InhibitTask=""false"">
                <Description>
                <![CDATA[Test Continuous task]]>
                </Description>
                <ScheduledPrograms>
                <ScheduledProgram Name=""MainProgram""/>
                </ScheduledPrograms>
                </Task>";
        }

        private static string GetPeriodicTaskData()
        {
            return
                @"<Task Name=""Periodic"" Type=""PERIODIC"" Rate=""1000"" Priority=""5"" Watchdog=""5000"" DisableUpdateOutputs=""true"" InhibitTask=""true"">
                <Description>
                <![CDATA[Test Periodic task]]>
                </Description>
                </Task>";
        }

        private static string GetEventTaskData()
        {
            return
                @"<Task Name=""Event"" Type=""EVENT"" Rate=""10"" Priority=""10"" Watchdog=""500"" DisableUpdateOutputs=""true"" InhibitTask=""false"">
                <Description>
                <![CDATA[Test Event task]]>
                </Description>
                <EventInfo EventTrigger=""EVENT Instruction Only"" EnableTimeout=""false""/>
                <ScheduledPrograms>
                <ScheduledProgram Name=""EventProgram01""/>
                </ScheduledPrograms>
                </Task>";
        }

        private static string GetTaskWithPrograms()
        {
            return
                @"<Task Name=""Periodic"" Type=""PERIODIC"" Rate=""10"" Priority=""10"" Watchdog=""500"" DisableUpdateOutputs=""false"" InhibitTask=""false"">
                <ScheduledPrograms>
                <ScheduledProgram Name=""NProgram""/>
                <ScheduledProgram Name=""EPProgram""/>
                </ScheduledPrograms>
                </Task>";
        }
    }
}