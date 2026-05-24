using FluentAssertions;

namespace L5Sharp.Tests.Core.Common;

[TestFixture]
public class NeutralStreamTests
{
    [Test]
    public void Constructor_ValidText_ShouldNotBeNull()
    {
        var stream = new NeutralStream("Test");
        stream.Should().NotBeNull();
    }

    [Test]
    public void Read_WhenCalled_ShouldReturnTrueAndFirstToken()
    {
        var stream = new NeutralStream("Tag1");

        var result = stream.Read(out var token);

        result.Should().BeTrue();
        token.Value.Should().Be("Tag1");
        token.Type.Should().Be(TokenType.Identifier);
    }

    [Test]
    public void Read_WhenAtEOF_ShouldReturnFalseAndEOFToken()
    {
        var stream = new NeutralStream("Tag1");
        stream.Read(out _); // Consume Tag1
        stream.Read(out _); // Consume EOF

        var result = stream.Read(out var token);

        result.Should().BeFalse();
        token.Type.Should().Be(TokenType.EOF);
    }

    [Test]
    public void Peek_WhenCalled_ShouldReturnCurrentToken()
    {
        var stream = new NeutralStream("Tag1");

        var token = stream.Peek();

        token.Value.Should().Be("Tag1");
    }

    [Test]
    public void Advance_PositiveCount_ShouldMovePosition()
    {
        var stream = new NeutralStream("Tag1.Tag2.Tag3");

        var result = stream.Advance(2);

        result.Should().BeTrue();
        stream.Read(out var token);
        token.Value.Should().Be("Tag2"); // Tag1, ., Tag2 -> Advance(2) moves past Tag1 and .
    }

    [Test]
    public void Advance_NegativeCount_ShouldThrowArgumentException()
    {
        var stream = new NeutralStream("Tag1");

        Action act = () => stream.Advance(-1);

        act.Should().Throw<ArgumentException>();
    }

    [Test]
    public void Seek_ConditionMet_ShouldReturnTrueAndAdvance()
    {
        var stream = new NeutralStream("Tag1.Tag2");

        var result = stream.Seek(t => t.Value == "Tag2");

        result.Should().BeTrue();
        stream.Peek().Value.Should().Be("Tag2");
    }

    [Test]
    public void Seek_ConditionNotMet_ShouldReturnFalse()
    {
        var stream = new NeutralStream("Tag1.Tag2");

        var result = stream.Seek(t => t.Value == "NonExistent");

        result.Should().BeFalse();
        stream.Peek().Type.Should().Be(TokenType.EOF);
    }

    [Test]
    public void Match_TypeMatches_ShouldReturnTrue()
    {
        var stream = new NeutralStream("Tag1");
        stream.Read(out _);

        var result = stream.Match(TokenType.EOF);

        result.Should().BeTrue();
    }

    [Test]
    public void Match_TypeDoesNotMatch_ShouldReturnFalse()
    {
        var stream = new NeutralStream("Tag1");
        stream.Read(out _);

        var result = stream.Match(TokenType.Colon);

        result.Should().BeFalse();
    }

    [Test]
    public void Advance_CountExceedsLength_ShouldReturnFalseAndBeAtEOF()
    {
        var stream = new NeutralStream("Tag1");

        var result = stream.Advance(5);

        result.Should().BeFalse();
        stream.Peek().Type.Should().Be(TokenType.EOF);
    }

    [Test]
    public void Dispose_WhenCalled_ShouldNotThrow()
    {
        var stream = new NeutralStream("Tag1");

        var act = stream.Dispose;

        act.Should().NotThrow();
    }
}