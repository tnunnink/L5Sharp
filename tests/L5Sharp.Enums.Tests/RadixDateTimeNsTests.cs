using FluentAssertions;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Enums.Tests
{
    [TestFixture]
    public class RadixDateTimeNsTests
    {
        [Test]
        public void DateTimeNs_WhenCalled_ShouldBeExpected()
        {
            var radix = Radix.DateTimeNs;

            radix.Should().NotBeNull();
            radix.Should().Be(Radix.DateTimeNs);
        }
        
        [Test]
        public void Format_DateTimeNsValidLint_ShouldBeExpectedFormat()
        {
            var radix = Radix.DateTimeNs;

            var result = radix.Convert(new Lint(1638277952000000));

            result.Should().Be("LDT#1970-01-19-17:04:37.9520000(UTC-06:00)");
        }
    }
}