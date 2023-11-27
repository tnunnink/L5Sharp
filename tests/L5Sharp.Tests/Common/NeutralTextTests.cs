using AutoFixture;
using FluentAssertions;
using L5Sharp.Common;

namespace L5Sharp.Tests.Common
{
    [TestFixture]
    public class NeutralTextTests
    {
        private const string Test01 =
            "[MOV(10,Constant),MOV(0.3,Exponent),GRT(Calculated,0)CPT(Error_SP,( Constant * Calculated ** Exponent) / Calculated * 100),LEQ(Calculated,0)MOV(0,Error_SP)];";

        private const string Test02 =
            "GRT(SimpleInt,400)XIO(MultiDimensionalArray[1,3].3)CMP(ATN(_Test) > 1.0)[TON(TimerArray[0],?,?),OTU(TestComplexTag.SimpleMember.BoolMember)];";
        

        [Test]
        public void Method_Scenario_ExpectedBehavior()
        {
           
        }
        
        [Test]
        public void New_NullText_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new NeutralText(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void New_EmptyString_ShouldBeSemiColon()
        {
            var text = new NeutralText(string.Empty);

            text.ToString().Should().Be(";");
        }

        [Test]
        public void New_SomeString_ShouldNotBeNull()
        {
            var fixture = new Fixture();

            var text = new NeutralText(fixture.Create<string>());

            text.Should().NotBeNull();
        }

        [Test]
        public void New_ValidText_ShouldNotBeNull()
        {
            var text = new NeutralText("[XIC(SomeBit),XIO(AnotherBit)]OTE(OutputBit);");

            text.Should().NotBeNull();
        }
        
        [Test]
        public void IsBalanced_ValidText_ShouldBeTrue()
        {
            var text = new NeutralText("[XIC(SomeBit),XIO(AnotherBit)]OTE(OutputBit);");
            
            text.IsBalanced.Should().BeTrue();
        }
        
        [Test]
        public void IsBalanced_Unbalanced_ShouldBeFalse()
        {
            var text = new NeutralText("XIC(SomeTag");

            text.IsBalanced.Should().BeFalse();
        }
        
        [Test]
        public void IsBalanced_TestText_ShouldBeTrue()
        {
            var text = new NeutralText(Test01);

            text.IsBalanced.Should().BeTrue();
        }

        [Test]
        public void Empty_WhenCalled_ShouldBeEmptyString()
        {
            var text = NeutralText.Empty;

            text.Should().BeEquivalentTo(new NeutralText(string.Empty));
        }

        [Test]
        public void Contains_HasEmptyText_ShouldBeFalse()
        {
            var text = NeutralText.Empty;

            var result = text.Contains("XIC(Tag.Status.Enabled)");

            result.Should().BeFalse();
        }

        [Test]
        public void Contains_HasInstructionText_ShouldBeTrue()
        {
            var text = new NeutralText(
                "[XIC(Tag.Status.Active),XIC(Tag.Status.Enabled)][MOV(15000,Timer.PRE),TON(Timer,?,?)];");

            var result = text.Contains("XIC(Tag.Status.Enabled)");

            result.Should().BeTrue();
        }

        [Test]
        public void Contains_HasTagName_ShouldBeTrue()
        {
            var text = new NeutralText(
                "[XIC(Tag.Status.Active),XIC(Tag.Status.Enabled)][MOV(15000,Timer.PRE),TON(Timer,?,?)];");

            var result = text.Contains("Tag.Status.Enabled");

            result.Should().BeTrue();
        }
        
        [Test]
        public void Instructions_Empty_ShouldBeEmpty()
        {
            var text = NeutralText.Empty;

            var result = text.Instructions();

            result.Should().BeEmpty();
        }
        
        [Test]
        public void Instructions_SimpleTextWithMultipleInstruction_ShouldHaveExpectedCount()
        {
            var text = new NeutralText("[XIC(SomeBit),XIO(AnotherBit)]OTE(OutputBit);");

            var instructions = text.Instructions();

            instructions.Should().HaveCount(3);
        }

        [Test]
        public void Instructions_SingleInstruction_ShouldHaveExpectedCount()
        {
            var text = new NeutralText("XIC(SomeBit)");

            var instructions = text.Instructions();

            instructions.Should().HaveCount(1);
        }

        [Test]
        public void Instructions_CustomInstructions_ReturnsExpected()
        {
            var text = new NeutralText(
                "[XIC(Tag.Status.Active),XIC(Tag.Status.Enabled)][MOV(15000,Timer.PRE),aoiTIMER(Timer,?,?)];");

            var instructions = text.Instructions();

            instructions.Should().HaveCount(4);
        }

        [Test]
        public void Instructions_SingleInstruction_ShouldEqualOriginal()
        {
            var text = new NeutralText("XIC(SomeBit)");

            var result = text.Instructions();

            result.First().Should().BeEquivalentTo(text);
        }
        
        [Test]
        public void InstructionsBy_WithExistingInstructionPresent_ShouldContainExpectedText()
        {
            var text = new NeutralText(
                "[XIC(Tag.Status.Active),XIC(Tag.Status.Enabled)][MOV(15000,Timer.PRE),TON(Timer,?,?)];");

            var result = text.Instructions("XIC").ToList();

            result.Should().Contain("XIC(Tag.Status.Active)");
            result.Should().Contain("XIC(Tag.Status.Enabled)");
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
        }
    }
}