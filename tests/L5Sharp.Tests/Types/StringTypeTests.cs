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

        type.Should<StringType>().NotBeNull();
    }

    [Test]
    public void New_NullName_ShouldThrowException()
    {
        FluentActions.Invoking(() => new StringType(null!, "")).Should().Throw<ArgumentNullException>();
    }
    
    [Test]
    public void New_NullValue_ShouldThrowException()
    {
        FluentActions.Invoking(() => new StringType("Test", null!)).Should().Throw<ArgumentNullException>();
    }
}