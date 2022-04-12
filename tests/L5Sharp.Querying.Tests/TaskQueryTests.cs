using System;
using System.Linq;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.L5X;
using L5SharpTests;
using NUnit.Framework;

namespace L5Sharp.Querying.Tests
{
    [TestFixture]
    public class TaskQueryTests
    {
        [Test]
        public void OfType_NullType_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.Tasks(q => q.OfType(null!)).ToList()).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void OfType_ValidType_ShouldHaveTasksWithTypeOnly()
        {
            var context = L5XContext.Load(Known.Test);

            var tasks = context.Tasks(q => q.OfType(TaskType.Periodic)).ToList();

            tasks.Should().NotBeEmpty();
            tasks.Should().AllSatisfy(t => t.Type.Should().Be(TaskType.Periodic));
        }

        [Test]
        public void WithRate_NullPredicate_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.Tasks(q => q.WithRate(null!))).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void WithRate_RateLessThan100_ShouldNotBeEmptyAndSatisfyQuery()
        {
            var context = L5XContext.Load(Known.Test);

            var tasks = context.Tasks(q => q.WithRate(r => r < 100)).ToList();

            tasks.Should().NotBeEmpty();
            tasks.Should().AllSatisfy(t => t.Rate.As<float>().Should().BeLessThan(100));
        }

        [Test]
        public void WithRate_RateGreaterThan100_ShouldBeEmpty()
        {
            var context = L5XContext.Load(Known.Test);

            var tasks = context.Tasks(q => q.WithRate(r => r > 100)).ToList();

            tasks.Should().BeEmpty();
        }

        [Test]
        public void ForProgram_NullProgram_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.Tasks(q => q.ForProgram(null!)).ToList()).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void ForProgram_ValidProgram_ShouldReturnTaskWithProgram()
        {
            var context = L5XContext.Load(Known.Test);

            var tasks = context.Tasks(q => q.ForProgram("MainProgram")).ToList();

            tasks.Should().NotBeEmpty();
            tasks.Should().HaveCount(1);
            tasks.First().ScheduledPrograms.Should().Contain("MainProgram");
        }
    }
}