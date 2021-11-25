using System;
using System.Linq.Expressions;
using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Extensions.Tests
{
    [TestFixture]
    public class ExpressionExtensionsTests
    {
        [Test]
        public void Testing()
        {
            Expression<Func<ITask, bool>> expression = t => t.Priority == 1;

            var converted = expression.ToXExpression();

            converted.Should().NotBeNull();
            /*converted.ToString().Should()
                .Be("e => e.Attribute(Priority) != null && e.Attribute(Priority).Value == Convert(1, TaskPriority)");*/
        }
    }
}