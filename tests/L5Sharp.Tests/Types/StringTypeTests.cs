using FluentAssertions;
using L5Sharp.Types;

namespace L5Sharp.Tests.Types;

[TestFixture]
public class StringTypeTests
{
    [Test]
    public void New_EmptyString_ShouldNotBeNull()
    {
        var type = new StringType("Test", "");

        type.Should().NotBeNull();
    }

    [Test]
    public void New_NullName_ShouldThrowException()
    {
        FluentActions.Invoking(() => new StringType(null!, "")).Should().Throw<ArgumentException>();
    }
    
    [Test]
    public void New_NullValue_ShouldThrowException()
    {
        var type = new StringType("Test", null!);

        type.Should().BeEmpty(string.Empty);
    }
}