using System.Linq;
using FluentAssertions;
using L5Sharp.L5X;
using L5SharpTests;
using NUnit.Framework;

namespace L5Sharp.Querying.Tests
{
    [TestFixture]
    public class InstructionQueryTests
    {
        [Test]
        public void IsReferences_WhenCalled_ShouldNotBeEmpty()
        {
            var context = LogixContext.Load(Known.Test);

            var results = context.Instructions(q => q.IsReferenced()).ToList();

            results.Should().NotBeEmpty();
        }
    }
}