using System;
using FluentAssertions;
using L5Sharp.L5X;
using L5SharpTests;
using NUnit.Framework;

namespace L5Sharp.Querying.Tests
{
    [TestFixture]
    public class TagQueryTests
    {
        [Test]
        public void OfType_NullName_ShouldThrowArgumentNullException()
        {
            var context = L5XContext.Load(Known.Test);

            FluentActions.Invoking(() => context.Tags(q => q.OfType(null!))).Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void OfType_ValidType_ShouldNotBeEmpty()
        {
            var context = L5XContext.Load(Known.Test);

            var results = context.Tags(q => q.OfType("SimpleType"));

            results.Should().NotBeEmpty();
        }
    }
}