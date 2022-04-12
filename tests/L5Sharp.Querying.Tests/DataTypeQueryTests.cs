using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.L5X;
using L5SharpTests;
using NUnit.Framework;

namespace L5Sharp.Querying.Tests
{
    [TestFixture]
    public class DataTypeQueryTests
    {
        [Test]
        public void DependingOn_ValidName_ShouldNotBeEmpty()
        {
            var context = L5XContext.Load(Known.Test);

            var results = context.DataTypes(q => q.DependingOn("SimpleType"));

            results.Should().NotBeEmpty();
        }

        [Test]
        public void OfFamily_ValidFamily_ShouldNotBeEmpty()
        {
            var context = L5XContext.Load(Known.Test);

            var results = context.DataTypes(q => q.OfFamily(DataTypeFamily.String));

            results.Should().NotBeEmpty();
        }
    }
}