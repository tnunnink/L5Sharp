using FluentAssertions;

namespace L5Sharp.Tests.Core.Data;

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

    [Test]
    [TestCase("BOOL")]
    [TestCase("BIT")]
    [TestCase("SINT")]
    [TestCase("INT")]
    [TestCase("DINT")]
    [TestCase("LINT")]
    [TestCase("REAL")]
    [TestCase("USINT")]
    [TestCase("UINT")]
    [TestCase("UDINT")]
    [TestCase("ULINT")]
    [TestCase("LREAL")]
    public void IsAtomic_ValidNames_ShouldBeTrue(string name)
    {
        var result = AtomicData.IsAtomic(name);

        result.Should().BeTrue();
    }

    [Test]
    public void IsAtomic_InvalidName_ShouldBeFalse()
    {
        var result = AtomicData.IsAtomic("Test");

        result.Should().BeFalse();
    }
}