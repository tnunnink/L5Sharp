using FluentAssertions;

namespace L5Sharp.Tests.Core.Enums;

[TestFixture]
public class TokenTypeTests
{
    [Test]
    public void Unknown_WhenCalled_ShouldNotBeNull()
    {
        TokenType.Unknown.Should().NotBeNull();
    }

    [Test]
    public void Identifier_WhenCalled_ShouldNotBeNull()
    {
        TokenType.Identifier.Should().NotBeNull();
    }

    [Test]
    public void Literal_WhenCalled_ShouldNotBeNull()
    {
        TokenType.Literal.Should().NotBeNull();
    }

    [Test]
    public void Operator_WhenCalled_ShouldNotBeNull()
    {
        TokenType.Operator.Should().NotBeNull();
    }

    [Test]
    public void OpenParen_WhenCalled_ShouldNotBeNull()
    {
        TokenType.OpenParen.Should().NotBeNull();
    }

    [Test]
    public void CloseParen_WhenCalled_ShouldNotBeNull()
    {
        TokenType.CloseParen.Should().NotBeNull();
    }

    [Test]
    public void OpenBracket_WhenCalled_ShouldNotBeNull()
    {
        TokenType.OpenBracket.Should().NotBeNull();
    }

    [Test]
    public void CloseBracket_WhenCalled_ShouldNotBeNull()
    {
        TokenType.CloseBracket.Should().NotBeNull();
    }

    [Test]
    public void Comma_WhenCalled_ShouldNotBeNull()
    {
        TokenType.Comma.Should().NotBeNull();
    }

    [Test]
    public void Dot_WhenCalled_ShouldNotBeNull()
    {
        TokenType.Dot.Should().NotBeNull();
    }

    [Test]
    public void SemiColon_WhenCalled_ShouldNotBeNull()
    {
        TokenType.SemiColon.Should().NotBeNull();
    }

    [Test]
    public void EOF_WhenCalled_ShouldNotBeNull()
    {
        TokenType.EOF.Should().NotBeNull();
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    public void FromToken_NullOrEmpty_ShouldBeEOF(string? token)
    {
        var result = TokenType.FromToken(token!);

        result.Should().Be(TokenType.EOF);
    }

    [Test]
    [TestCase("+")]
    [TestCase("-")]
    [TestCase("*")]
    [TestCase("/")]
    [TestCase("**")]
    [TestCase("MOD")]
    [TestCase("AND")]
    [TestCase("OR")]
    [TestCase("XOR")]
    [TestCase("NOT")]
    [TestCase(":=")]
    [TestCase("=")]
    [TestCase("<>")]
    [TestCase(">")]
    [TestCase(">=")]
    [TestCase("<")]
    [TestCase("<=")]
    public void FromToken_OperatorValue_ShouldBeOperator(string token)
    {
        var result = TokenType.FromToken(token);

        result.Should().Be(TokenType.Operator);
    }

    [Test]
    public void FromToken_OpenParen_ShouldBeOpenParen()
    {
        var result = TokenType.FromToken("(");

        result.Should().Be(TokenType.OpenParen);
    }

    [Test]
    public void FromToken_CloseParen_ShouldBeCloseParen()
    {
        var result = TokenType.FromToken(")");

        result.Should().Be(TokenType.CloseParen);
    }

    [Test]
    public void FromToken_OpenBracket_ShouldBeOpenBracket()
    {
        var result = TokenType.FromToken("[");

        result.Should().Be(TokenType.OpenBracket);
    }

    [Test]
    public void FromToken_CloseBracket_ShouldBeCloseBracket()
    {
        var result = TokenType.FromToken("]");

        result.Should().Be(TokenType.CloseBracket);
    }

    [Test]
    public void FromToken_Comma_ShouldBeComma()
    {
        var result = TokenType.FromToken(",");

        result.Should().Be(TokenType.Comma);
    }

    [Test]
    public void FromToken_Dot_ShouldBeDot()
    {
        var result = TokenType.FromToken(".");

        result.Should().Be(TokenType.Dot);
    }

    [Test]
    public void FromToken_SemiColon_ShouldBeSemiColon()
    {
        var result = TokenType.FromToken(";");

        result.Should().Be(TokenType.SemiColon);
    }

    [Test]
    [TestCase("123")]
    [TestCase("16#FF")]
    [TestCase("2#1011")]
    [TestCase("'String'")]
    [TestCase("'Another string'")]
    public void FromToken_LiteralValue_ShouldBeLiteral(string token)
    {
        var result = TokenType.FromToken(token);

        result.Should().Be(TokenType.Literal);
    }

    [Test]
    [TestCase("MyTag")]
    [TestCase("XIC")]
    [TestCase("ADD")]
    [TestCase("_MyTag")]
    [TestCase("Tag_Name")]
    [TestCase("tag123")]
    public void FromToken_IdentifierValue_ShouldBeIdentifier(string token)
    {
        var result = TokenType.FromToken(token);

        result.Should().Be(TokenType.Identifier);
    }

    [Test]
    public void FromToken_UnknownValue_ShouldBeUnknown()
    {
        var result = TokenType.FromToken("!@#");

        result.Should().Be(TokenType.Unknown);
    }
}