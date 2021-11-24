using System;
using System.Linq.Expressions;
using System.Xml.Linq;
using FluentAssertions;
using L5Sharp;
using L5Sharp.Enums;
using L5Sharp.Utilities;
using NUnit.Framework;

namespace L5SharpTests.Util
{
    [TestFixture]
    public class XElementVisitorTests
    {
        [Test]
        public void SingleExpression_NoAttributes_ShouldReturnSingleName()
        {
            Expression<Func<IDataType, bool>> expression = t => t.Class == DataTypeClass.User && t.Description == "Test";
            var visitor = new XElementVisitor();

            var result = (Expression<Func<XElement, bool>>) visitor.Visit(expression);
            
            var str = result?.ToString();
            str.Should().NotBeNull();
        }
    }
}