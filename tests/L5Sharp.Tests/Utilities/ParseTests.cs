using FluentAssertions;

namespace L5Sharp.Tests.Utilities;

[TestFixture]
public class ParseTests
{
    [Test]
    public void ParseType_Radix_ShouldBeExpected()
    {
        var value = "Decimal".Parse(typeof(Radix));

        value.Should().Be(Radix.Decimal);
    }
}