using System;
using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Tests.Enums
{
    [TestFixture]
    public class RadixNullTests
    {
        [Test]
        public void Null_WhenCalled_ShouldBeExpected()
        {
            var radix = Radix.Null;

            radix.Should().NotBeNull();
            radix.Should().Be(Radix.Null);
        }

        [Test]
        public void Format_WhenCalled_ReturnsNull()
        {
            FluentActions.Invoking(() => Radix.Null.FormatValue(new DINT())).Should().Throw<NotSupportedException>();
        }

        [Test]
        public void Parse_WhenCalled_ReturnsNull()
        {
            FluentActions.Invoking(() => Radix.Null.ParseValue("Doesn't matter")).Should().Throw<NotSupportedException>();
        }
    }
}