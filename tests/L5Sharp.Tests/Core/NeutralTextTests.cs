using AutoFixture;
using FluentAssertions;
using L5Sharp.Common;
using L5Sharp.Enums;
using NUnit.Framework.Internal;

namespace L5Sharp.Tests.Core
{
    [TestFixture]
    public class NeutralTextTests
    {
        private const string TestText =
            "[MOV(10,Flow_Comparison_PctError_Constant),MOV(0.3,Flow_Comparison_PctError_Exponent),GRT(Calculated_Avg_Flow,0)CPT(Flow_Comparison_PctError_SP,( Flow_Comparison_PctError_Constant * Calculated_Avg_Flow ** Flow_Comparison_PctError_Exponent) / Calculated_Avg_Flow * 100),LEQ(Calculated_Avg_Flow,0)MOV(0,Flow_Comparison_PctError_SP)];";

        [Test]
        public void New_NullText_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new NeutralText(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void New_EmptyString_ShouldBeEqualToEmpty()
        {
            var text = new NeutralText(string.Empty);

            text.Should().BeEquivalentTo(NeutralText.Empty);
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
        public void IsBalanced_Unbalanced_ShouldBeFalse()
        {
            var text = new NeutralText("XIC(SomeTag");

            text.IsBalanced.Should().BeFalse();
        }

        [Test]
        public void IsBalanced_TestText_ShouldBeTrue()
        {
            var text = new NeutralText(TestText);

            text.IsBalanced.Should().BeTrue();
        }

        [Test]
        public void IsInstruction_TestText_ShouldBeFalse()
        {
            var text = new NeutralText(TestText);

            text.IsSingle.Should().BeFalse();
        }

        [Test]
        public void IsInstruction_SingleInstruction_ShouldBeTrue()
        {
            var text = new NeutralText("XIC(SomeTag)");

            text.IsSingle.Should().BeTrue();
        }

        [Test]
        public void Empty_WhenCalled_ShouldBeEmptyString()
        {
            var text = NeutralText.Empty;

            text.Should().BeEquivalentTo(new NeutralText(string.Empty));
        }

        [Test]
        public void ContainsKey_Empty_ShouldBeFalse()
        {
            var text = NeutralText.Empty;

            var result = text.ContainsKey("XIC");

            result.Should().BeFalse();
        }

        [Test]
        public void ContainsKey_ValidInstruction_ShouldBeTrue()
        {
            var text = new NeutralText(
                "[XIC(Tag.Status.Active),XIC(Tag.Status.Enabled)][MOV(15000,Timer.PRE),TON(Timer,?,?)];");

            var result = text.ContainsKey("XIC");

            result.Should().BeTrue();
        }

        [Test]
        public void ContainsKey_CustomInstruction_ShouldBeTrue()
        {
            var text = new NeutralText(
                "[XIC(Tag.Status.Active),XIC(Tag.Status.Enabled)][MOV(15000,Timer.PRE),aoiTIMER(Timer,?,?)];");

            var result = text.ContainsKey("aoiTIMER");

            result.Should().BeTrue();
        }

        [Test]
        public void ContainsSignature_Empty_ShouldBeFalse()
        {
            var text = NeutralText.Empty;

            var result = text.ContainsSignature(Instruction.XIC);

            result.Should().BeFalse();
        }

        [Test]
        public void ContainsSignature_ValidInstruction_ShouldBeTrue()
        {
            var text = new NeutralText(
                "[XIC(Tag.Status.Active),XIC(Tag.Status.Enabled)][MOV(15000,Timer.PRE),TON(Timer,?,?)];");

            var result = text.ContainsSignature(Instruction.XIC);

            result.Should().BeTrue();
        }

        [Test]
        public void ContainsText_Empty_ShouldBeFalse()
        {
            var text = NeutralText.Empty;

            var result = text.ContainsText("XIC(Tag.Status.Enabled)");

            result.Should().BeFalse();
        }

        [Test]
        public void ContainsText_ValidTagName_ShouldBeTrue()
        {
            var text = new NeutralText(
                "[XIC(Tag.Status.Active),XIC(Tag.Status.Enabled)][MOV(15000,Timer.PRE),TON(Timer,?,?)];");

            var result = text.ContainsText("XIC(Tag.Status.Enabled)");

            result.Should().BeTrue();
        }

        [Test]
        public void ContainsTag_Empty_ShouldBeFalse()
        {
            var text = NeutralText.Empty;

            var result = text.ContainsTag("Tag.Status.Enabled");

            result.Should().BeFalse();
        }

        [Test]
        public void ContainsTag_ValidTagName_ShouldBeTrue()
        {
            var text = new NeutralText(
                "[XIC(Tag.Status.Active),XIC(Tag.Status.Enabled)][MOV(15000,Timer.PRE),TON(Timer,?,?)];");

            var result = text.ContainsTag("Tag.Status.Enabled");

            result.Should().BeTrue();
        }

        [Test]
        public void Split_SimpleTextWithMultipleInstruction_ShouldHaveExpectedCount()
        {
            var text = new NeutralText("[XIC(SomeBit),XIO(AnotherBit)]OTE(OutputBit);");

            var instructions = text.Split();

            instructions.Should().HaveCount(3);
        }

        [Test]
        public void Split_SingleInstruction_ShouldHaveExpectedCount()
        {
            var text = new NeutralText("XIC(SomeBit)");

            var instructions = text.Split();

            instructions.Should().HaveCount(1);
        }

        [Test]
        public void Split_SingleInstruction_ShouldEqualOriginal()
        {
            var text = new NeutralText("XIC(SomeBit)");

            var result = text.Split();

            result.First().Should().BeEquivalentTo(text);
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

            var tagNames = text.TagsIn(Instruction.OTE);

            tagNames.Should().HaveCount(1);
        }

        [Test]
        public void Split_WithExistingInstructionPresent_ShouldContainExpectedText()
        {
            var text = new NeutralText(
                "[XIC(Tag.Status.Active),XIC(Tag.Status.Enabled)][MOV(15000,Timer.PRE),TON(Timer,?,?)];");

            var result = text.SplitBy(Instruction.XIC).ToList();

            result.Should().Contain("XIC(Tag.Status.Active)");
            result.Should().Contain("XIC(Tag.Status.Enabled)");
        }

        [Test]
        public void Split_WithExistingInstructionPresent_ShouldHaveExpectedCount()
        {
            var text = new NeutralText(
                "[XIC(Tag.Status.Active),XIC(Tag.Status.Enabled)][MOV(15000,Timer.PRE),TON(Timer,?,?)];");

            var result = text.SplitBy(Instruction.XIC);

            result.Should().HaveCount(2);
        }

        [Test]
        public void Operands_Empty_ShouldBeEmpty()
        {
            var text = NeutralText.Empty;

            var result = text.Operands();

            result.Should().BeEmpty();
        }

        [Test]
        public void Operands_WhenCalled_ReturnsExpectedCount()
        {
            var text = new NeutralText(TestText);

            var operands = text.Operands();

            operands.Should().HaveCount(12);
        }

        [Test]
        public void OperandsByKey_Default_ReturnsExpectedCount()
        {
            var text = new NeutralText(TestText);

            var results = text.OperandsByKey().ToList();

            results.Should().HaveCount(6);

            foreach (var pair in results)
            {
                pair.Key.Should().NotBeEmpty();
                pair.Value.Should().NotBeEmpty();
            }
        }

        [Test]
        public void OperandsByKey_MOV_ReturnsExpectedCount()
        {
            var text = new NeutralText(TestText);

            var results = text.OperandsByKey(Instruction.MOV).ToList();

            results.Should().HaveCount(3);

            foreach (var pair in results)
            {
                pair.Key.Should().Be(Instruction.MOV);
                pair.Value.Should().HaveCount(2);
            }
        }

        [Test]
        public void OperandsByKey_stringMOV_ReturnsExpectedCount()
        {
            var text = new NeutralText(TestText);

            var results = text.OperandsByKey("MOV").ToList();

            results.Should().HaveCount(3);

            foreach (var pair in results)
            {
                pair.Key.Should().Be(Instruction.MOV);
                pair.Value.Should().HaveCount(2);
            }
        }

        [Test]
        public void Instructions_WhenCalled_ReturnsExpected()
        {
            var text = new NeutralText(
                "[XIC(Tag.Status.Active),XIC(Tag.Status.Enabled)][MOV(15000,Timer.PRE),TON(Timer,?,?)];");

            var instructions = text.Instructions();

            instructions.Should().HaveCount(4);
        }

        [Test]
        public void Instructions_Empty_ShouldBeEmpty()
        {
            var text = NeutralText.Empty;

            var result = text.Instructions();

            result.Should().BeEmpty();
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
        public void Keys_Empty_ShouldBeEmpty()
        {
            var text = NeutralText.Empty;

            var result = text.Keys();

            result.Should().BeEmpty();
        }

        [Test]
        public void Keys_ValidInstructions_ShouldReturnExpectedCount()
        {
            var text = new NeutralText(TestText);

            var result = text.Keys();

            result.Should().HaveCount(6);
        }

        [Test]
        public void References()
        {
            var text = new NeutralText(TestText);

            var references = text.References().ToList();

            references.Should().NotBeEmpty();
        }

        [Test]
        public void ReferencesArrayTest()
        {
            var text = new NeutralText("MOV(Archive_Previous_Daily_Mtr09.CLOSE_TIME,Daily_CloseTime_LS[8])");

            var references = text.References().ToList();

            references.Should().NotBeEmpty();
        }

        [Test]
        public void HasPattern_ConcatenatedXICOTE_ShouldBeTrue()
        {
            var text = new NeutralText(
                "[XIC(Input_Data.Pt00.Data)OTE(Ch0.ChData),XIC(Input_Data.Pt00.Fault)OTE(Ch0.ChFault)];");

            var result = text.HasPattern(string.Concat(Instruction.XIC.Signature, Instruction.OTE.Signature));

            result.Should().BeTrue();
        }

        [Test]
        public void SplitBy_ValidPattern_ShouldWork()
        {
            var text = new NeutralText(
                "[XIC(Input_Data.Pt00.Data)OTE(Ch0.ChData),XIC(Input_Data.Pt00.Fault)OTE(Ch0.ChFault)];");

            var result = text.SplitBy(string.Concat(Instruction.XIC.Signature, Instruction.OTE.Signature)).ToList();

            result.Should().HaveCount(2);
        }

        [Test]
        public void Operands_CptInstruction_ShouldHaveCountTwo()
        {
            var text = new NeutralText(
                "CPT(UDT.alg_Value,((ai_Signal - UDT.sp_RawMin) / (UDT.sp_RawMax - UDT.sp_RawMin)) * (UDT.sp_EUMax - UDT.sp_EUMin) + UDT.sp_EUMin);");

            var operands = text.Operands().ToList();

            operands.Should().HaveCount(2);
        }
    }
}