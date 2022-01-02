using System;
using FluentAssertions;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class PeriodicTaskTests
    {
        [Test]
        public void New_NullName_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new PeriodicTask(null!)).Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void New_ValidName_ShouldNotBeNull()
        {
            var task = new PeriodicTask("Test");

            task.Should().NotBeNull();
        }

        [Test]
        public void New_ValidName_ShouldHaveExpectDefaults()
        {
            var task = new PeriodicTask("Test");

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
            var task = new PeriodicTask("Test", "This is a test task");

            task.Name.Should().Be("Test");
            task.Description.Should().Be("This is a test task");
        }

        [Test]
        public void TypedEquals_AreEqual_ShouldBeTrue()
        {
            var first = new PeriodicTask("Test");
            var second = new PeriodicTask("Test");

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void TypedEquals_AreSame_ShouldBeTrue()
        {
            var first = new PeriodicTask("Test");

            var result = first.Equals(first);

            result.Should().BeTrue();
        }


        [Test]
        public void TypedEquals_Null_ShouldBeFalse()
        {
            var first = new PeriodicTask("Test");

            var result = first.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void ObjectEquals_AreEqual_ShouldBeTrue()
        {
            var first = new PeriodicTask("Test");
            var second = new PeriodicTask("Test");

            var result = first.Equals((object)second);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_AreSame_ShouldBeTrue()
        {
            var first = new PeriodicTask("Test");

            var result = first.Equals((object)first);

            result.Should().BeTrue();
        }


        [Test]
        public void ObjectEquals_Null_ShouldBeFalse()
        {
            var first = new PeriodicTask("Test");

            var result = first.Equals((object)null);

            result.Should().BeFalse();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var first = new PeriodicTask("Test");
            var second = new PeriodicTask("Test");

            var result = first == second;

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = new PeriodicTask("Test");
            var second = new PeriodicTask("Test");

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var first = new PeriodicTask("Test");

            var hash = first.GetHashCode();

            hash.Should().NotBe(0);
        }
    }
}