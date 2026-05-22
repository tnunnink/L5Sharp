using FluentAssertions;

namespace L5Sharp.Tests.Core.Common;

[TestFixture]
public class NeutralTokenTests
{
    [Test]
    public void Constructor_ValidArguments_ShouldSetProperties()
    {
        var token = new NeutralToken(TokenType.Identifier, "MyTag", 10);

        token.Type.Should().Be(TokenType.Identifier);
        token.Value.Should().Be("MyTag");
        token.Index.Should().Be(10);
    }

    [Test]
    public void Constructor_NullType_ShouldThrowArgumentNullException()
    {
        Action act = () => _ = new NeutralToken(null!, "value", 0);

        act.Should().Throw<ArgumentNullException>().WithParameterName("type");
    }

    [Test]
    public void Constructor_NullValue_ShouldThrowArgumentNullException()
    {
        Action act = () => _ = new NeutralToken(TokenType.Identifier, null!, 0);

        act.Should().Throw<ArgumentNullException>().WithParameterName("value");
    }

    [Test]
    public void Length_WhenCalled_ShouldReturnValueLength()
    {
        var token = new NeutralToken(TokenType.Literal, "123", 5);

        token.Length.Should().Be(3);
    }

    [Test]
    public void ToString_WhenCalled_ShouldReturnExpectedFormat()
    {
        var token = new NeutralToken(TokenType.Operator, "+", 15);

        var result = token.ToString();

        result.Should().Be("[Operator] + (at 15)");
    }

    [Test]
    public void None_WhenCalled_ShouldHaveExpectedProperties()
    {
        var token = NeutralToken.None;

        token.Type.Should().Be(TokenType.None);
        token.Value.Should().BeEmpty();
        token.Index.Should().Be(-1);
    }
}