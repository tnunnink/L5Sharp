using FluentAssertions;
using L5Sharp.Types;
using L5Sharp.Types.Atomic;
using NUnit.Framework;

namespace L5Sharp.Enums.Tests
{
    [TestFixture]
    public class RadixDateTimeTests
    {
        [Test]
        public void DateTime_WhenCalled_ShouldBeExpected()
        {
            var radix = Radix.DateTime;

            radix.Should().NotBeNull();
            radix.Should().Be(Radix.DateTime);
        }
        
        [Test]
        public void Format_DateTimeValidLint_ShouldBeExpectedFormat()
        {
            var radix = Radix.DateTime;

            var result = radix.Convert(new Lint(1638277952000000));

            result.Should().Be("DT#2021-11-30-07:12:32.000000(UTC-06:00)");
        }

    }
}