using FluentAssertions;

namespace L5Sharp.Tests.Types;

[TestFixture]
public class AtomicDataTests
{
    [Test]
    [TestCase("123")]
    public void Parse_SintValues_ShouldReturnExpected(string value)
    {
        var result = AtomicData.Parse(value);

        result.Should().BeOfType<SINT>();
        result.As<SINT>().Should().Be(123);
    }
    
    [Test]
    [TestCase("1234")]
    public void Parse_IntValues_ShouldReturnExpected(string value)
    {
        var result = AtomicData.Parse(value);

        result.Should().BeOfType<INT>();
        result.As<INT>().Should().Be(1234);
    }
    
    [Test]
    [TestCase("123456")]
    public void Parse_DintValues_ShouldReturnExpected(string value)
    {
        var result = AtomicData.Parse(value);

        result.Should().BeOfType<DINT>();
        result.As<DINT>().Should().Be(123456);
    }
}