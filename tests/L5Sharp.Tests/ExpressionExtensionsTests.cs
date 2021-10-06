using System;
using System.Linq.Expressions;
using L5Sharp.Abstractions;
using L5Sharp.Extensions;
using L5Sharp.Utilities;
using NUnit.Framework;

namespace L5Sharp.Tests
{
    [TestFixture]
    public class ExpressionExtensionsTests
    {
        [Test]
        public void Convert_WhenCalled_SeeHowItWorks()
        {
            Expression<Func<IDataType, bool>> expression = d => d.Name == string.Empty;

            var result = expression.Convert();
        }
    }
}