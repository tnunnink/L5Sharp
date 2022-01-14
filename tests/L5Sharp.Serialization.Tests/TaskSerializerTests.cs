using System;
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
        [Test]
        public void Serialize_Null_ShouldThrowArgumentNullException()
        {
            var serializer = new TaskSerializer();

            FluentActions.Invoking(() => serializer.Serialize(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Serialize_ContinuousTask_ShouldNotBeNull()
        {
            var task = new ContinuousTask("Test");
            var serializer = new TaskSerializer();

            var xml = serializer.Serialize(task);

            xml.Should().NotBeNull();
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_ContinuousTask_ShouldBeApproved()
        {
            var task = new ContinuousTask("Test");
            var serializer = new TaskSerializer();

            var xml = serializer.Serialize(task);

            Approvals.VerifyXml(xml.ToString());
        }

        [Test]
        public void Deserialize_ContinuousTask_ShouldNotBeNull()
        {
            var element = XElement.Parse(GetContinuousTaskData());
            var serializer = new TaskSerializer();

            var task = serializer.Deserialize(element);

            task.Should().NotBeNull();
        }

        [Test]
        public void Deserialize_Null_ShouldThrowArgumentNullException()
        {
            var serializer = new TaskSerializer();

            FluentActions.Invoking(() => serializer.Deserialize(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Deserialize_ContinuousTask_ShouldHaveExpectedProperties()
        {
            var element = XElement.Parse(GetContinuousTaskData());
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
        public void Deserialize_PeriodicTask_ShouldNotBeNull()
        {
            var element = XElement.Parse(GetPeriodicTaskData());
            var serializer = new TaskSerializer();

            var task = serializer.Deserialize(element);

            task.Should().NotBeNull();
        }

        [Test]
        public void Deserialize_PeriodicTask_ShouldHaveExpectedProperties()
        {
            var element = XElement.Parse(GetPeriodicTaskData());
            var serializer = new TaskSerializer();

            var task = serializer.Deserialize(element);

            task.Name.Should().Be(new ComponentName("Periodic"));
            task.Description.Should().Be("This is a test task 01");
            task.Type.Should().Be(TaskType.Periodic);
            task.Priority.Should().Be(new TaskPriority(5));
            task.Rate.Should().Be(new ScanRate(1000));
            task.Watchdog.Should().Be(new Watchdog(5000));
            task.InhibitTask.Should().Be(true);
            task.DisableUpdateOutputs.Should().Be(true);
        }
        
        private static string GetContinuousTaskData()
        {
            return
                @"<Task Name=""Continuous"" Type=""CONTINUOUS"" Priority=""10"" Watchdog=""500"" DisableUpdateOutputs=""false"" InhibitTask=""false"">
                <Description>
                <![CDATA[Test continuous task]]>
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
                <![CDATA[This is a test task 01]]>
                </Description>
                </Task>";
        }
        
        private static string GetEventTaskData()
        {
            return
                @"<Task Name=""Event"" Type=""EVENT"" Rate=""10"" Priority=""10"" Watchdog=""500"" DisableUpdateOutputs=""true"" InhibitTask=""false"">
                <Description>
                <![CDATA[TestEvent Task]]>
                </Description>
                <EventInfo EventTrigger=""EVENT Instruction Only"" EnableTimeout=""false""/>
                <ScheduledPrograms>
                <ScheduledProgram Name=""EventProgram01""/>
                </ScheduledPrograms>
                </Task>";
        }
    }
}