using FluentAssertions;

namespace L5Sharp.Tests.Core.Elements;

[TestFixture]
public class RungTests
{
    private const string SingleInstructionExample =
        "XIC(SomeBit);";

    private const string SimpleTextExample =
        "[XIC(SomeBit),XIO(AnotherBit)]OTE(OutputBit);";

    private const string Test01 =
        "[XIC(Tag.Status.Active),XIC(Tag.Status.Enabled)][MOV(15000,Timer.PRE),TON(Timer,?,?)];";

    private const string Test02 =
        "[MOV(10,Constant),MOV(0.3,Exponent),GRT(Calculated,0)CPT(Error_SP,( Constant * Calculated ** Exponent) / Calculated * 100),LEQ(Calculated,0)MOV(0,Error_SP)];";

    private const string Test03 =
        "GRT(SimpleInt,400)XIO(MultiDimensionalArray[1,3].3)CMP(ATN(_Test) > 1.0)[TON(TimerArray[0],?,?),OTU(TestComplexTag.SimpleMember.BoolMember)];";

    [Test]
    public void New_Default_ShouldHaveExpectedValues()
    {
        var rung = new Rung();

        rung.Number.Should().Be(0);
        rung.Type.Should().Be(RungType.Normal);
        rung.Text.Should().BeEmpty();
        rung.Comment.Should().BeNull();
        rung.Program.Should().BeNull();
        rung.Routine.Should().BeNull();
        rung.Document.Should().BeNull();
        rung.Reference.Should().Be("Rung[@Number='0']");
        rung.Scope.Level.Should().Be(ScopeLevel.None);
        rung.Scope.Container.Should().BeEmpty();
    }

    [Test]
    public void New_ValidTextAndCommand_ShouldHaveExpectedValues()
    {
        var rung = new Rung(SimpleTextExample, "This is a test rung");

        rung.Number.Should().Be(0);
        rung.Text.Should().Be(SimpleTextExample);
        rung.Comment.Should().Be("This is a test rung");
        rung.Type.Should().Be(RungType.Normal);
    }

    /*[Test]
    public void IsBalanced_ValidText_ShouldBeTrue()
    {
        var text = new NeutralText(Test00);

        text.IsBalanced.Should().BeTrue();
    }

    [Test]
    public void IsBalanced_Unbalanced_ShouldBeFalse()
    {
        var text = new NeutralText("XIC(SomeTag");

        text.IsBalanced.Should().BeFalse();
    }*/

    [Test]
    public void Instructions_DefaultRung_ShouldBeEmpty()
    {
        var rung = new Rung();

        var result = rung.Instructions();

        result.Should().BeEmpty();
    }

    [Test]
    public void Instructions_SingleInstruction_ShouldHaveExpectedCount()
    {
        var rung = new Rung(SingleInstructionExample);

        var instructions = rung.Instructions();

        instructions.Should().HaveCount(1);
    }

    [Test]
    public void Instructions_SimpleTextWithMultipleInstruction_ShouldHaveExpectedCount()
    {
        var rung = new Rung(SimpleTextExample);

        var instructions = rung.Instructions();

        instructions.Should().HaveCount(3);
    }

    [Test]
    public void Instructions_CustomInstructions_ReturnsExpected()
    {
        var text = new Rung("[XIC(Tag.Status.Active),XIC(Tag.Status.Enabled)][MOV(15000,Timer.PRE),aoiTIMER(Timer,?,?)];");

        var instructions = text.Instructions();

        instructions.Should().HaveCount(4);
    }

    [Test]
    public void Instructions_SingleInstruction_ShouldEqualOriginal()
    {
        var rung = new Rung("XIC(SomeBit)");

        var result = rung.Instructions();

        result.First().Should().Be(rung);
    }

    /*[Test]
    public void InstructionsBy_WithExistingInstructionPresent_ShouldContainExpectedText()
    {
        var text = new NeutralText(
            "[XIC(Tag.Status.Active),XIC(Tag.Status.Enabled)][MOV(15000,Timer.PRE),TON(Timer,?,?)];");

        var result = text.Instructions("XIC").ToList();

        result.Should().Contain(Instruction.Parse("XIC(Tag.Status.Active)"));
        result.Should().Contain(Instruction.Parse("XIC(Tag.Status.Enabled)"));
    }

    [Test]
    public void InstructionsBy_WithExistingInstructionPresent_ShouldHaveExpectedCount()
    {
        var text = new NeutralText(
            "[XIC(Tag.Status.Active),XIC(Tag.Status.Enabled)][MOV(15000,Timer.PRE),TON(Timer,?,?)];");

        var result = text.Instructions(Instruction.XIC("Tag.Status.Active"));

        result.Should().HaveCount(1);
    }

    [Test]
    public void Keywords_TextWithKeywords_ShouldHaveExpectedCount()
    {
        var text = new NeutralText("if Tag >= 10 then");

        var keywords = text.Keywords().ToList();

        keywords.Should().HaveCount(2);
    }

    [Test]
    public void Keywords_TextWithNoKeywords_ShouldBeEmpty()
    {
        var text = new NeutralText("[XIC(SomeBit),XIO(AnotherBit)]OTE(OutputBit);");

        var keywords = text.Keywords().ToList();

        keywords.Should().BeEmpty();
    }

    [Test]
    public void Tags_SimpleTextWithMultipleTags_ShouldHaveExpectedCount()
    {
        var text = new NeutralText("[XIC(SomeBit),XIO(AnotherBit)]OTE(OutputBit);");

        var tagNames = text.Tags();

        tagNames.Should().HaveCount(3);
    }

    [Test]
    public void TagsIn_OTE_ShouldHaveExpectedTagName()
    {
        var text = new NeutralText("[XIC(SomeBit),XIO(AnotherBit)]OTE(OutputBit);");

        var tagNames = text.TagsIn("OTE");

        tagNames.Should().HaveCount(1);
    }

    [Test]
    public void StructuredText_New_ShouldNotBeNull()
    {
        var text = new NeutralText("SimpleBool := TestComplexTag.SimpleMember.BoolMember;");

        text.Should().NotBeNull();
    }

    [Test]
    public void StructuredText_ShouldHaveExpected()
    {
        var text = new NeutralText("SimpleBool := TestComplexTag.SimpleMember.BoolMember;");

        text.IsEmpty.Should().BeFalse();
        text.IsBalanced.Should().BeTrue();
        text.Instructions().Should().BeEmpty();
        text.Tags().Should().HaveCount(2);
    }*/

    [Test]
    [Description("GitHub Issue #52: A tag with a bit index reference tag should parse correctly")]
    public void Instructions_BitReferenceIndexTag_ShouldReturnExpectedInstruction()
    {
        var rung = new Rung("XIC(DintTest.[Offset]);");

        var instructions = rung.Instructions().ToList();

        instructions.Should().HaveCount(1);
        instructions[0].Tags.Should().Contain("DintTest.[Offset]");
    }

    [Test]
    public void EquivalentTo_AreEqual_ShouldBeTrue()
    {
        var first = new Rung("XIC(SomeTag)OTE(AnotherTag);");
        var second = new Rung("XIC(SomeTag)OTE(AnotherTag);");

        var result = first.EquivalentTo(second);

        result.Should().BeTrue();
    }

    [Test]
    public void EquivalentTo_AreNotEqual_ShouldBeFalse()
    {
        var first = new Rung("XIC(SomeTag)OTL(AnotherTag);");
        var second = new Rung("XIC(SomeTag)OTE(AnotherTag);");

        var result = first.EquivalentTo(second);

        result.Should().BeFalse();
    }

    [Test]
    public Task Serialize_Default_ShouldBeVerified()
    {
        var rung = new Rung();

        var xml = rung.Serialize().ToString();

        return VerifyXml(xml);
    }

    [Test]
    public Task Serialize_WithText_ShouldBeVerified()
    {
        var rung = new Rung("XIC(MyTag)[OTE(SomeOutput)TMR(TimerTag,?,?)];");

        var xml = rung.Serialize().ToString();

        return VerifyXml(xml);
    }

    [Test]
    public Task SetComment_DefaultRung_ShouldBeValid()
    {
        var rung = new Rung
        {
            Comment = "This is a test comment"
        };

        var xml = rung.Serialize().ToString();

        return VerifyXml(xml);
    }

    [Test]
    public Task SetCommentThenText_ShouldBeValid()
    {
        var rung = new Rung
        {
            Comment = "This is a test comment",
            Text = "AFI;"
        };

        var xml = rung.Serialize().ToString();

        return VerifyXml(xml);
    }

    [Test]
    public Task SetTextToNullThenCommentThenTextAgainShouldBeValid()
    {
        var rung = new Rung
        {
            Text = null!,
            Comment = "This is a test comment"
        };

        rung.Text = "AFI;";

        var xml = rung.Serialize().ToString();

        return VerifyXml(xml);
    }
}