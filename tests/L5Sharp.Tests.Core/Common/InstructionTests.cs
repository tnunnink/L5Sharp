using FluentAssertions;

namespace L5Sharp.Tests.Core.Common
{
    [TestFixture]
    public class InstructionTests
    {
        [Test]
        public void New_Null_ShouldThrowException()
        {
            FluentActions.Invoking(() => _ = new Instruction(null!)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void New_EmptyString_ShouldThrowException()
        {
            FluentActions.Invoking(() => _ = new Instruction(string.Empty)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void New_KeyNoArgs_ShouldHaveExpectedValues()
        {
            var instruction = new Instruction("Test", []);

            instruction.Should().Be("Test()");
            instruction.Key.Should().Be("Test");
            instruction.Arguments.Should().BeEmpty();
            instruction.IsDesctructive.Should().BeTrue();
            instruction.IsConditional.Should().BeFalse();
            instruction.IsNative.Should().BeFalse();
        }

        [Test]
        public void New_KeyAndArgs_ShouldHaveExpectedValues()
        {
            var instruction = new Instruction("Test", "arg1", "arg2", 100);

            instruction.Should().Be("Test(arg1,arg2,100)");
            instruction.Key.Should().Be("Test");
            instruction.Arguments.Should().HaveCount(3);
            instruction.IsDesctructive.Should().BeTrue();
            instruction.IsConditional.Should().BeFalse();
            instruction.IsNative.Should().BeFalse();
        }

        [Test]
        public void New_NativeInstruction_ShouldHaveExpectedValues()
        {
            var instruction = new Instruction("XIC", "MyTag");

            instruction.Should().Be("XIC(MyTag)");
            instruction.Key.Should().Be("XIC");
            instruction.Arguments.Should().HaveCount(1);
            instruction.IsConditional.Should().BeTrue();
            instruction.IsDesctructive.Should().BeFalse();
            instruction.IsNative.Should().BeTrue();
        }

        [Test]
        [TestCase("XIC(MyTagKey);", "XIC", 1)]
        [TestCase("TON(SomeTimer,5000,0);", "TON", 3)]
        [TestCase("TON(SomeTimer,?,?);", "TON", 3)]
        [TestCase("CMP(ATN(_Test) > 1.0);", "CMP", 1)]
        [TestCase("JSR(Routine,2,in1,in2,out1,out2,out3);", "JSR", 7)]
        [TestCase("SBR(Routine,in1,in2);", "SBR", 3)]
        [TestCase("RET(Routine,out1,out2);", "RET", 3)]
        public void New_ValidFormats_ShouldHaveExpectedKeyAndArgumentCount(string text, string key, int args)
        {
            var instruction = new Instruction(text);

            instruction.Should().NotBeNull();
            instruction.Key.Should().Be(key);
            instruction.Arguments.Should().HaveCount(args);
        }

        [Test]
        [TestCase("Test")]
        [TestCase("()")]
        [TestCase("(SomeTag > 1.0)/100")]
        [TestCase("ABS(SomeTag) / 100  > 1")]
        public void IsValid_InvalidFormats_ShouldBeFalse(string text)
        {
            var instruction = new Instruction(text);

            instruction.IsValid.Should().BeFalse();
        }

        [Test]
        public void Unknown_WhenCalled_ShouldBeExpected()
        {
            var instruction = Instruction.Unkown;

            instruction.Should().Be("UNK");
            instruction.Key.Should().Be("UNK");
            instruction.Arguments.Should().BeEmpty();
            instruction.IsConditional.Should().BeFalse();
            instruction.IsDesctructive.Should().BeTrue();
            instruction.IsNative.Should().BeFalse();
        }

        [Test]
        public void XIC_WhenCalled_ShouldBeExpectedProperties()
        {
            var instruction = Instruction.XIC("MyTag");

            instruction.Should().Be("XIC(MyTag)");
            instruction.Key.Should().Be("XIC");
            instruction.Arguments.Should().HaveCount(1);
            instruction.IsConditional.Should().BeTrue();
            instruction.IsDesctructive.Should().BeFalse();
            instruction.IsNative.Should().BeTrue();
        }

        [Test]
        public void OTE_WhenCalled_ShouldBeExpectedProperties()
        {
            var instruction = Instruction.OTE("MyTag");

            instruction.Should().Be("OTE(MyTag)");
            instruction.Key.Should().Be("OTE");
            instruction.Arguments.Should().HaveCount(1);
            instruction.IsDesctructive.Should().BeTrue();
            instruction.IsNative.Should().BeTrue();
        }

        [Test]
        public void TON_ValidArgs_ShouldHaveExpectedValues()
        {
            var instruction = Instruction.TON("SomeTimer", 5000, 0);

            instruction.Should().Be("TON(SomeTimer,5000,0)");
            instruction.Key.Should().Be("TON");
            instruction.Arguments.Should().HaveCount(3);
            instruction.IsDesctructive.Should().BeTrue();
            instruction.IsNative.Should().BeTrue();
        }

        [Test]
        public void JSR_WhenCalled_ShouldBeExpectedProperties()
        {
            var instruction = Instruction.JSR("RoutineName", 1, "In1", "Out1");

            instruction.Should().Be("JSR(RoutineName,1,In1,Out1)");
            instruction.Key.Should().Be("JSR");
            instruction.Arguments.Should().HaveCount(4);
            instruction.IsDesctructive.Should().BeTrue();
            instruction.IsNative.Should().BeTrue();
        }

        [Test]
        public void EVENT_WhenCalled_ShouldBeExpectedProperties()
        {
            var instruction = Instruction.EVENT("Event");

            instruction.Should().Be("EVENT(Event)");
            instruction.Key.Should().Be("EVENT");
            instruction.Arguments.Should().HaveCount(1);
            instruction.IsDesctructive.Should().BeTrue();
            instruction.IsNative.Should().BeTrue();
        }

        [Test]
        public void Arguments_SimpleInstruction_ShouldHaveExpected()
        {
            var instruction = Instruction.MOVE("MyTagValue", "SomeTagName.Member.Value");

            var args = instruction.Arguments.ToArray();

            args.Should().HaveCount(2);
            args[0].Should().Be("MyTagValue");
            args[1].Should().Be("SomeTagName.Member.Value");
        }

        [Test]
        public void Arguments_TaskReference_ShouldBeExpected()
        {
            var instruction = Instruction.EVENT("MyTask");

            var args = instruction.Arguments.ToArray();

            args.Should().HaveCount(1);
        }

        [Test]
        public void Arguments_RoutineReference_ShouldBeExpected()
        {
            var instruction = Instruction.JSR("Routine", 1, "In1", "Out1");

            var args = instruction.Arguments.ToArray();

            args.Should().HaveCount(4);
            args[0].Should().Be("Routine");
            args[1].Should().Be(1);
            args[2].Should().Be("In1");
            args[3].Should().Be("Out1");
        }

        [Test]
        public void Arguments_SystemInstruction_ShouldBeExpected()
        {
            var instruction = Instruction.GSV("Program", "MyProgram", "LastScanTime", "MyTagName");

            var args = instruction.Arguments.ToArray();

            args.Should().HaveCount(4);
        }

        [Test]
        public void With_NewTagArguments_ShouldUpdateArgument()
        {
            var instruction = Instruction.XIC("MyTag");

            var updated = instruction.With("NewTag");

            updated.Should().Be("XIC(NewTag)");
            updated.Arguments.Should().Contain("NewTag");
        }

        [Test]
        public void Append_ValidArgument_ShouldBeExpected()
        {
            var test = new Instruction("Test");

            var result = test.Append("arg1");

            result.Arguments.Should().HaveCount(1);
            result.Arguments.Should().Contain("arg1");
        }

        [Test]
        public void Append_MultipleArgs_ShouldBeExpected()
        {
            var test = new Instruction("Test");

            var result = test.Append("arg1").Append(123);

            result.Arguments.Should().HaveCount(2);
            result.Arguments.Should().Contain("arg1");
            result.Arguments.Should().Contain(123);
        }

        [Test]
        public void Parse_WithComments_ShouldSkipComments()
        {
            var text = new NeutralText("XIC(MyTag) // trailing comment\n/* multi-line\ncomment */ OTE(MyOtherTag)");

            var instructions = Instruction.Parse(text).ToList();

            instructions.Should().HaveCount(2);
            instructions[0].Key.Should().Be("XIC");
            instructions[1].Key.Should().Be("OTE");
        }

        [Test]
        [TestCase("XIC(MyTagKey);", 1)]
        [TestCase("TON(SomeTimer,5000,0);", 1)]
        [TestCase("JSR(Routine,2,in1,in2,out1,out2,out3);", 1)]
        [TestCase("XIC(MySimpleTag) XIO(MyArrayTag[1]) OTE(MyComplexTag.Member.12);", 3)]
        public void Read_ValidText_ShouldHaveExpectedCount(string text, int count)
        {
            var instructions = Instruction.Parse(text).ToArray();

            instructions.Should().HaveCount(count);
        }

        [Test]
        public void GetArgument_ValidOperand_ShouldNotBeNull()
        {
            var instruction = Instruction.ADD(1, 1, "Test");

            var argument = instruction.Arguments.ToArray()[1];

            argument.Should().Be("1");
        }

        [Test]
        public void Equals_AreEqual_ShouldBeTrue()
        {
            var first = Instruction.XIC("MyTag");
            var second = Instruction.XIC("MyTag");

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void Equals_SameInstances_ShouldBeTrue()
        {
            var first = Instruction.XIC("MyTag");

            // ReSharper disable once EqualExpressionComparison
            var result = first.Equals(first);

            result.Should().BeTrue();
        }

        [Test]
        public void Equals_Null_ShouldBeFalse()
        {
            var first = Instruction.XIC("MyTag");

            // ReSharper disable once EqualExpressionComparison
            var result = first.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void Equals_EqualString_ShouldBeTrue()
        {
            var first = Instruction.XIC("MyTag");

            // ReSharper disable once SuspiciousTypeConversion.Global
            var result = first.Equals("XIC(MyTag)");

            result.Should().BeTrue();
        }

        [Test]
        public void EqualsOperator_EqualInstances_ShouldBeTure()
        {
            var first = Instruction.XIC("MyTag");
            var second = Instruction.XIC("MyTag");

            var result = first == second;

            result.Should().BeTrue();
        }

        [Test]
        public void NotEqualsOperator_EqualInstances_ShouldBeFalse()
        {
            var first = Instruction.XIC("MyTag");
            var second = Instruction.XIC("MyTag");

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ReturnsHashOfInstructionText()
        {
            var instruction = Instruction.XIC("MyTag");

            var result = instruction.GetHashCode();

            result.Should().Be(StringComparer.OrdinalIgnoreCase.GetHashCode("XIC(MyTag)"));
        }

        [Test]
        public void ToString_WhenCalled_ShouldBeText()
        {
            var text = Instruction.OTE("MyTag").ToString();

            text.Should().Be("OTE(MyTag)");
        }

        [TestCase("MOV(Motor1.Status, PanelLights.Green)", 2)]
        [TestCase("ADD(Tank_Level.PV, Tank_Level.Offset, Tank_Level.Corrected)", 3)]
        public void Parse_CommaSeparatedArgsWithWhitespace_ShouldParseAllAsReferences(string text, int expectedCount)
        {
            var instructions = Instruction.Parse(text).ToList();

            var arguments = instructions[0].Arguments.ToList();

            arguments.Should().HaveCount(expectedCount);
            arguments.Should().AllSatisfy(a => a.IsReference.Should().BeTrue());
        }
    }
}