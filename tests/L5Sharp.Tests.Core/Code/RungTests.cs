using FluentAssertions;

namespace L5Sharp.Tests.Core.Code;

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
        rung.Text.Should().Be(";");
        rung.Comment.Should().BeNull();
        rung.Program.Should().BeNull();
        rung.Routine.Should().BeNull();
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
    [TestCase(Test01, 4)]
    [TestCase(Test02, 6)]
    [TestCase(Test03, 5)]
    public void Instructions_SimpleTextExample_ReturnsExpected(string text, int count)
    {
        var rung = new Rung(text);

        var result = rung.Instructions();

        result.Should().HaveCount(count);
    }

    [Test]
    public void Instructions_SingleInstruction_ShouldContainExpectedText()
    {
        var rung = new Rung("XIC(SomeBit)");

        var result = rung.Instructions();

        result.Should().Contain("XIC(SomeBit)");
    }

    [Test]
    public void Instructions_FilterByKey_ShouldContainExpectedText()
    {
        var rung = new Rung(Test01);

        var result = rung.Instructions().Where(i => i.Key == "XIC").ToList();

        result.Should().Contain("XIC(Tag.Status.Active)");
        result.Should().Contain("XIC(Tag.Status.Enabled)");
    }

    [Test]
    [Description("GitHub Issue #52: A tag with a bit index reference tag should parse correctly")]
    public void Instructions_BitReferenceIndexTag_ShouldReturnExpectedInstruction()
    {
        var rung = new Rung("XIC(DintTest.[Offset]);");

        var instructions = rung.Instructions().ToList();

        instructions.Should().HaveCount(1);
        instructions[0].Arguments.Should().Contain("DintTest.[Offset]");
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

    [TestCase("MOV(Flow_Rate, Flow_Rate_Scaled);", new[] { "Flow_Rate", "Flow_Rate_Scaled" })]
    [TestCase("ADD(Tank_Level.PV, Tank_Level.Offset, Tank_Level.Corrected);", new[] { "Tank_Level.PV", "Tank_Level.Offset", "Tank_Level.Corrected" })]
    [TestCase("MOV(Motor1.Status, PanelLights.Green);", new[] { "Motor1.Status", "PanelLights.Green" })]
    [TestCase("TON(Motor1_Timer, Timer_Preset, Timer_Accum);", new[] { "Motor1_Timer", "Timer_Preset", "Timer_Accum" })]
    [TestCase("MOV(5000, Motor1_Timer.PRE);", new[] { "Motor1_Timer.PRE" })]
    public void Tags_RungWithMultipleArgs_ShouldCountAllTagReferences(string text, string[] expectedTags)
    {
        var rung = new Rung(text);

        var tags = rung.Tags().ToList();

        tags.Should().HaveCount(expectedTags.Length);
        foreach (var expected in expectedTags)
        {
            tags.Should().Contain(t => (string)t == expected);
        }
    }

    [TestCase("XIC(Motor1_Start)OTE(Motor1_Start);", new[] { "Motor1_Start" }, new[] { 2 })]
    [TestCase("XIC(Motor1_Start)XIO(Motor1_Stop)OTE(Motor1_Start);", new[] { "Motor1_Start", "Motor1_Stop" }, new[] { 2, 1 })]
    [TestCase("XIC(Motor1_Status.Running)OTE(Motor1_Status.Running);", new[] { "Motor1_Status.Running" }, new[] { 2 })]
    public void Tags_RungWithDuplicateTagInMultipleInstructions_ShouldCountEachOccurrence(
        string text, string[] tagNames, int[] expectedCounts)
    {
        var rung = new Rung(text);

        var tags = rung.Tags().ToList();

        tags.Should().HaveCount(expectedCounts.Sum());
        for (var i = 0; i < tagNames.Length; i++)
        {
            tags.Count(t => (string)t == tagNames[i]).Should().Be(expectedCounts[i]);
        }
    }

    [Test]
    public void Tags_RungWithBranchAndNestedInstructions_ShouldCountAllReferences()
    {
        var rung = new Rung("[XIC(Start),XIO(Stop)]OTE(Motor);MOV(Motor_Status, Log);");

        var tags = rung.Tags().ToList();

        tags.Should().HaveCount(5);
        tags.Count(t => (string)t == "Start").Should().Be(1);
        tags.Count(t => (string)t == "Stop").Should().Be(1);
        tags.Count(t => (string)t == "Motor").Should().Be(1);
        tags.Count(t => (string)t == "Motor_Status").Should().Be(1);
        tags.Count(t => (string)t == "Log").Should().Be(1);
    }
}