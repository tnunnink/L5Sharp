using System.Linq;
using FluentAssertions;
using L5Sharp.L5X;
using L5Sharp.Querying.Tests.Content;
using NUnit.Framework;

namespace L5Sharp.Querying.Tests
{
    [TestFixture]
    public class RungQueryTests
    {
        [Test]
        public void InProgram_WhenCalled_ShouldNotBeEmpty()
        {
            var context = L5XContext.Load(Known.Test);

            var rungs = context.Rungs().InProgram("MainProgram").All().ToList();

            rungs.Should().NotBeEmpty();
        }

        [Test]
        public void Flatten_WhenCalled_ShouldNotBeACompleteDisaster()
        {
            var context = L5XContext.Load(Known.Test);

            var rungs = context.Rungs().InProgram("MainProgram").Flatten().All().ToList();

            rungs.Should().NotBeEmpty();
        }
        
        [Test]
        public void Flatten_RungExample1_PleaseGod()
        {
            var context = L5XContext.Load(Known.RungExample1);

            var rungs = context.Rungs().Flatten().All().ToList();

            rungs.Should().NotBeEmpty();
        }
    }
}