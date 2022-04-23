using System;
using System.Linq;
using FluentAssertions;
using L5Sharp.L5X;
using L5SharpTests;
using NUnit.Framework;

namespace L5Sharp.Querying.Tests
{
    [TestFixture]
    public class ProgramQueryTests
    {
        [Test]
        public void InTask_NullTask_ShouldThrowArgumentNullException()
        {
            var context = LogixContext.Load(Known.Test);

            FluentActions.Invoking(() => context.Programs(q => q.InTask(null!))).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void InTask_ValidTask_ShouldHaveExpectedCount()
        {
            var context = LogixContext.Load(Known.Test);

            var results = context.Programs(q => q.InTask("Periodic")).ToList();

            results.Should().HaveCount(2);
        }

        [Test]
        public void InTask_InvalidTask_ShouldBeEmpty()
        {
            var context = LogixContext.Load(Known.Test);

            var results = context.Programs(q => q.InTask("Fake"));

            results.Should().BeEmpty();
        }
    }
}