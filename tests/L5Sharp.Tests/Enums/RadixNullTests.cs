using System;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics;
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
            FluentActions.Invoking(() => Radix.Null.Format(new DINT())).Should().Throw<NotSupportedException>();
        }

        [Test]
        public void Parse_WhenCalled_ReturnsNull()
        {
            FluentActions.Invoking(() => Radix.Null.Parse("Doesn't matter")).Should().Throw<NotSupportedException>();
        }
    }
}