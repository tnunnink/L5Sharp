using FluentAssertions;

namespace L5Sharp.Tests.Core.Common;

[TestFixture]
public class NeutralTextTests
{
    [Test]
    public void Constructor_NullText_ShouldThrowArgumentNullException()
    {
        Action act = () => _ = new NeutralText(null!);

        act.Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void Tokenize_SimpleIdentifier_ShouldReturnIdentifierAndEOF()
    {
        var text = new NeutralText("MyTag");

        var tokens = text.Tokenize().ToList();

        tokens.Should().HaveCount(2);
        tokens[0].Type.Should().Be(TokenType.Identifier);
        tokens[0].Value.Should().Be("MyTag");
        tokens[0].Index.Should().Be(0);
        tokens[1].Type.Should().Be(TokenType.EOF);
        tokens[1].Index.Should().Be(5);
    }

    [Test]
    public void Tokenize_Literal_ShouldReturnLiteralAndEOF()
    {
        var text = new NeutralText("123.4");

        var tokens = text.Tokenize().ToList();

        tokens.Should().HaveCount(2);
        tokens[0].Type.Should().Be(TokenType.Literal);
        tokens[0].Value.Should().Be("123.4");
        tokens[1].Type.Should().Be(TokenType.EOF);
    }

    [Test]
    public void Tokenize_Operator_ShouldReturnOperatorAndEOF()
    {
        var text = new NeutralText("+");

        var tokens = text.Tokenize().ToList();

        tokens.Should().HaveCount(2);
        tokens[0].Type.Should().Be(TokenType.Operator);
        tokens[0].Value.Should().Be("+");
        tokens[1].Type.Should().Be(TokenType.EOF);
    }

    [Test]
    public void Tokenize_MultiCharOperator_ShouldReturnOperatorAndEOF()
    {
        var text = new NeutralText(":=");

        var tokens = text.Tokenize().ToList();

        tokens.Should().HaveCount(2);
        tokens[0].Type.Should().Be(TokenType.Operator);
        tokens[0].Value.Should().Be(":=");
        tokens[1].Type.Should().Be(TokenType.EOF);
    }

    [Test]
    public void Tokenize_Structural_ShouldReturnStructuralAndEOF()
    {
        var text = new NeutralText("(");

        var tokens = text.Tokenize().ToList();

        tokens.Should().HaveCount(2);
        tokens[0].Type.Should().Be(TokenType.OpenParen);
        tokens[0].Value.Should().Be("(");
        tokens[1].Type.Should().Be(TokenType.EOF);
    }

    [Test]
    public void Tokenize_StringLiteral_ShouldReturnLiteralAndEOF()
    {
        var text = new NeutralText("'Test String'");

        var tokens = text.Tokenize().ToList();

        tokens.Should().HaveCount(2);
        tokens[0].Type.Should().Be(TokenType.Literal);
        tokens[0].Value.Should().Be("'Test String'");
        tokens[1].Type.Should().Be(TokenType.EOF);
    }

    [Test]
    public void Tokenize_StringLiteralWithEscape_ShouldReturnLiteralAndEOF()
    {
        var text = new NeutralText("'String with $'quote$' inside'");

        var tokens = text.Tokenize().ToList();

        tokens.Should().HaveCount(2);
        tokens[0].Type.Should().Be(TokenType.Literal);
        tokens[0].Value.Should().Be("'String with $'quote$' inside'");
        tokens[1].Type.Should().Be(TokenType.EOF);
    }

    [Test]
    public void Tokenize_MixedText_ShouldReturnExpectedTokens()
    {
        var text = new NeutralText("XIC(MyTag) ADD(1, 2, Result);");

        var tokens = text.Tokenize().ToList();

        // XIC, (, MyTag, ), ADD, (, 1, ,, 2, ,, Result, ), ;, EOF
        tokens.Should().HaveCount(14);
        
        tokens[0].Value.Should().Be("XIC");
        tokens[1].Value.Should().Be("(");
        tokens[2].Value.Should().Be("MyTag");
        tokens[3].Value.Should().Be(")");
        tokens[4].Value.Should().Be("ADD");
        tokens[5].Value.Should().Be("(");
        tokens[6].Value.Should().Be("1");
        tokens[7].Value.Should().Be(",");
        tokens[8].Value.Should().Be("2");
        tokens[9].Value.Should().Be(",");
        tokens[10].Value.Should().Be("Result");
        tokens[11].Value.Should().Be(")");
        tokens[12].Value.Should().Be(";");
        tokens[13].Type.Should().Be(TokenType.EOF);
    }

    [Test]
    [TestCase("MOD")]
    [TestCase("AND")]
    [TestCase("OR")]
    [TestCase("XOR")]
    [TestCase("NOT")]
    public void Tokenize_KeywordOperators_ShouldReturnOperator(string op)
    {
        var text = new NeutralText(op);

        var tokens = text.Tokenize().ToList();

        tokens.Should().HaveCount(2);
        tokens[0].Type.Should().Be(TokenType.Operator);
        tokens[0].Value.Should().Be(op);
    }

    [Test]
    [TestCase("<=")]
    [TestCase(">=")]
    [TestCase("<>")]
    [TestCase("**")]
    public void Tokenize_OtherMultiCharOperators_ShouldReturnOperator(string op)
    {
        var text = new NeutralText(op);

        var tokens = text.Tokenize().ToList();

        tokens.Should().HaveCount(2);
        tokens[0].Type.Should().Be(TokenType.Operator);
        tokens[0].Value.Should().Be(op);
    }

    [Test]
    public void Tokenize_Whitespace_ShouldBeIgnored()
    {
        var text = new NeutralText("  MyTag  ");

        var tokens = text.Tokenize().ToList();

        tokens.Should().HaveCount(2);
        tokens[0].Value.Should().Be("MyTag");
        tokens[0].Index.Should().Be(2);
    }

    [Test]
    public void Tokenize_UnexpectedCharacter_ShouldThrowArgumentException()
    {
        var text = new NeutralText("MyTag #"); // # alone is not valid at start of token

        // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
        Action act = () => text.Tokenize().ToList();

        act.Should().Throw<ArgumentException>().WithMessage("*Unexpected character '#'*");
    }

    [Test]
    public void Equals_SameText_ShouldBeTrue()
    {
        var text1 = new NeutralText("MyTag");
        var text2 = new NeutralText("mytag");

        text1.Equals(text2).Should().BeTrue();
    }

    [Test]
    public void Equals_String_ShouldBeTrue()
    {
        var text = new NeutralText("MyTag");

        // ReSharper disable once SuspiciousTypeConversion.Global
        text.Equals("mytag").Should().BeTrue();
    }

    [Test]
    public void ToString_WhenCalled_ShouldReturnOriginalText()
    {
        var text = new NeutralText("MyTag");

        text.ToString().Should().Be("MyTag");
    }

    [Test]
    public void GetHashCode_SameText_ShouldBeEqual()
    {
        var text1 = new NeutralText("MyTag");
        var text2 = new NeutralText("mytag");

        text1.GetHashCode().Should().Be(text2.GetHashCode());
    }

    [Test]
    public void ImplicitOperator_ToString_ShouldReturnExpectedValue()
    {
        NeutralText text = "MyTag";
        string value = text;

        value.Should().Be("MyTag");
    }

    [Test]
    public void ImplicitOperator_FromString_ShouldReturnExpectedValue()
    {
        const string value = "MyTag";

        NeutralText text = value;

        text.Should().Be(new NeutralText("MyTag"));
    }
}