using System;
using System.Linq.Expressions;
using L5Sharp.Utilities;
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

            var visitor = new XElementVisitor();

            var result = visitor.Visit(expression);
        }
    }
}