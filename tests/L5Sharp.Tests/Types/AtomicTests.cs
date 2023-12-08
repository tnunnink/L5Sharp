using FluentAssertions;

namespace L5Sharp.Tests.Types;

[TestFixture]
public class AtomicTests
{
    [Test]
    [TestCase("123")]
    public void Parse_SintValues_ShouldReturnExpected(string value)
    {
        var result = Atomic.Parse(value);

        result.Should().BeOfType<SINT>();
        result.As<SINT>().Should().Be(123);
    }
    
    [Test]
    [TestCase("1234")]
    public void Parse_IntValues_ShouldReturnExpected(string value)
    {
        var result = Atomic.Parse(value);

        result.Should().BeOfType<INT>();
        result.As<INT>().Should().Be(1234);
    }
    
    [Test]
    [TestCase("123456")]
    public void Parse_DintValues_ShouldReturnExpected(string value)
    {
        var result = Atomic.Parse(value);

        result.Should().BeOfType<DINT>();
        result.As<DINT>().Should().Be(123456);
    }

    [Test]
    public void Parse_NameAndValue_ShouldReturnExpected()
    {
        var result = Atomic.Parse("INT", "1234");

        result.Should().BeOfType<INT>();
        result.As<INT>().Should().Be(1234);
    }

    [Test]
    public void Parse_InvalidName_ShouldThrowArgumentException()
    {
        FluentActions.Invoking(() => Atomic.Parse("TIMER", "123")).Should().Throw<ArgumentException>();
    }

    [Test]
    public void IsAtomic_ValidAtomic_ShouldBeTrue()
    {
        var result = Atomic.IsAtomic("REAL");

        result.Should().BeTrue();
    }
    
    [Test]
    public void IsAtomic_NonAtomic_ShouldBeFalse()
    {
        var result = Atomic.IsAtomic("TIMER");

        result.Should().BeFalse();
    }
}