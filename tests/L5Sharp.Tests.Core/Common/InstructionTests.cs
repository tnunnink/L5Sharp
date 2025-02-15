using FluentAssertions;

namespace L5Sharp.Tests.Core.Common
{
    [TestFixture]
    public class InstructionTests
    {
        [Test]
        public void New_ValidKey_ShouldNotBeNull()
        {
            var instruction = Instruction.New("Test");

            instruction.Should().NotBeNull();
        }

        [Test]
        public void New_ValidKey_ShouldHaveExpectedDefaults()
        {
            var instruction = Instruction.New("Test");

            instruction.Key.Should().Be("Test");
            instruction.Signature.Should().Be("Test()");
            instruction.Arguments.Should().BeEmpty();
            instruction.Operands.Should().BeEmpty();
            instruction.Text.Should().Be("Test()");
            instruction.IsConditional.Should().BeFalse();
            instruction.IsValid.Should().BeTrue();
        }

        [Test]
        public void Keys_WhenCalled_ShouldNotBeEmpty()
        {
            var known = Instruction.Keys();

            known.Should().NotBeEmpty();
        }

        [Test]
        public void XIC_WhenCalled_ShouldBeExpectedProperties()
        {
            var instruction = Instruction.XIC("MyTag");

            instruction.Key.Should().Be("XIC");
            instruction.Signature.Should().Be("XIC(data_bit)");
            instruction.Arguments.Should().HaveCount(1);
            instruction.Operands.Should().HaveCount(1);
            instruction.Text.Should().Be("XIC(MyTag)");
            instruction.IsConditional.Should().BeTrue();
            instruction.IsValid.Should().BeTrue();
        }

        [Test]
        public void OTE_WhenCalled_ShouldBeExpectedProperties()
        {
            var instruction = Instruction.OTE("MyTag");

            instruction.Key.Should().Be("OTE");
            instruction.Signature.Should().Contain("OTE(data_bit)");
            instruction.Arguments.Should().HaveCount(1);
            instruction.Operands.Should().HaveCount(1);
            instruction.Text.Should().Be("OTE(MyTag)");
            instruction.IsConditional.Should().BeFalse();
            instruction.IsValid.Should().BeTrue();
        }

        [Test]
        public void TON_ValidArgs_ShouldHaveExpectedValues()
        {
            var instruction = Instruction.TON("SomeTimer", 5000, 0);

            instruction.Key.Should().Be("TON");
            instruction.Signature.Should().Contain("TON(timer,preset,accum)");
            instruction.Arguments.Should().HaveCount(3);
            instruction.Operands.Should().HaveCount(3);
            instruction.Text.Should().Be("TON(SomeTimer,5000,0)");
            instruction.IsConditional.Should().BeFalse();
            instruction.IsValid.Should().BeTrue();
        }

        [Test]
        public void JSR_WhenCalled_ShouldBeExpectedProperties()
        {
            var instruction = Instruction.JSR("Routine", 1, "In1", "Out1");

            instruction.Key.Should().Be("JSR");
            instruction.Signature.Should()
                .Contain("JSR(routine_name,number_of_inputs,input_1,input_n,return_1,return_n)");
            instruction.Arguments.Should().HaveCount(4);
            instruction.Operands.Should().HaveCount(6);
            instruction.Text.Should().Be("JSR(Routine,1,In1,Out1)");
            instruction.IsConditional.Should().BeFalse();
            instruction.IsValid.Should().BeTrue();
        }

        [Test]
        public void EVENT_WhenCalled_ShouldBeExpectedProperties()
        {
            var instruction = Instruction.EVENT("MyTask");

            instruction.Key.Should().Be("EVENT");
            instruction.Signature.Should().Contain("EVENT(task)");
            instruction.Arguments.Should().HaveCount(1);
            instruction.Operands.Should().HaveCount(1);
            instruction.Text.Should().Be("EVENT(MyTask)");
            instruction.IsConditional.Should().BeFalse();
            instruction.IsValid.Should().BeTrue();
        }

        [Test]
        public void Tags_SimpleInstruction_ShouldHaveExpected()
        {
            var instruction = Instruction.MOVE("MyTagValue", "SomeTagName.Member.Value");

            var tags = instruction.Tags;

            tags.Should().HaveCount(2);
            tags[0].Should().Be("MyTagValue");
            tags[1].Should().Be("SomeTagName.Member.Value");
        }

        [Test]
        public void Tags_TaskReference_ShouldBeExpected()
        {
            var instruction = Instruction.EVENT("MyTask");

            var tags = instruction.Tags;

            tags.Should().BeEmpty();
        }

        [Test]
        public void Tags_RoutineReference_ShouldBeExpected()
        {
            var instruction = Instruction.JSR("Routine", 1, "In1", "Out1");

            var tags = instruction.Tags;

            tags.Should().HaveCount(2);
            tags[0].Should().Be("In1");
            tags[1].Should().Be("Out1");
        }

        [Test]
        public void Tags_SystemInstruction_ShouldBeExpected()
        {
            var instruction = Instruction.GSV("Program", "MyProgram", "LastScanTime", "MyTagName");

            var tags = instruction.Tags;

            tags.Should().HaveCount(1);
            tags[0].Should().Be("MyTagName");
        }

        [Test]
        public void References_NormalInstruction_ShouldBeExpected()
        {
            var instruction = Instruction.MOVE("MyTagValue", "SomeTagName.Member.Value");

            var references = instruction.References;

            references.Should().HaveCount(2);
            references[0].Type.Should().Be(ScopeType.Tag);
            references[1].Type.Should().Be(ScopeType.Tag);
        }

        [Test]
        public void References_TaskReference_ShouldBeExpected()
        {
            var instruction = Instruction.EVENT("MyTask");

            var references = instruction.References;

            references.Should().HaveCount(1);
            references[0].Type.Should().Be(ScopeType.Task);
        }

        [Test]
        public void References_RoutineReference_ShouldBeExpected()
        {
            var instruction = Instruction.JSR("Routine", 1, "In1", "Out1");

            var references = instruction.References;

            references.Should().HaveCount(3);
            references[0].Type.Should().Be(ScopeType.Routine);
            references[1].Type.Should().Be(ScopeType.Tag);
            references[2].Type.Should().Be(ScopeType.Tag);
        }

        [Test]
        public void References_SystemInstruction_ShouldBeExpected()
        {
            var instruction = Instruction.GSV("Program", "MyProgram", "LastScanTime", "MyTagName");

            var references = instruction.References;

            references.Should().HaveCount(2);
            references[0].Type.Should().Be(ScopeType.Program);
            references[0].Name.Should().Be("MyProgram");
            references[1].Type.Should().Be(ScopeType.Tag);
            references[1].Name.Should().Be("MyTagName");
        }

        [Test]
        public void Of_ValidArguments_ShouldHaveExpectedTextAndArgumentsAndBeValid()
        {
            var instruction = Instruction.XIC("MyTag");

            instruction = instruction.With("NewTag");

            instruction.Arguments.Should().HaveCount(1);
            instruction.Text.Should().Be("XIC(NewTag)");
            instruction.IsValid.Should().BeTrue();
        }

        [Test]
        public void Of_InvalidArgs_ShouldHaveExpectedTextAndArgumentsAndNotBeValid()
        {
            var instruction = Instruction.XIC("MyTag");

            instruction = instruction.With("NewTag", "InvalidTag");

            instruction.Arguments.Should().HaveCount(2);
            instruction.Text.Should().Be("XIC(NewTag,InvalidTag)");
            instruction.IsValid.Should().BeFalse();
        }

        [Test]
        [TestCase("XIC(MyTagKey);", "XIC", 1)]
        [TestCase("TON(SomeTimer,5000,0);", "TON", 3)]
        [TestCase("TON(SomeTimer,?,?);", "TON", 3)]
        [TestCase("CMP(ATN(_Test) > 1.0);", "CMP", 1)]
        [TestCase("JSR(Routine,2,in1,in2,out1,out2,out3);", "JSR", 7)]
        [TestCase("SBR(Routine,in1,in2);", "SBR", 3)]
        [TestCase("RET(Routine,out1,out2);", "RET", 3)]
        public void Parse_ValidInput_ShouldHaveExpectedKeyAndArguments(string text, string key, int args)
        {
            var instruction = Instruction.Parse(text);

            instruction.Should().NotBeNull();
            instruction.Key.Should().Be(key);
            instruction.Arguments.Should().HaveCount(args);
        }

        [Test]
        public void Parse_ExpressionTypeInput_ShouldThrowNewException()
        {
            FluentActions.Invoking(() => Instruction.Parse("(SomeTag > 1.0)/100")).Should().Throw<FormatException>();
        }

        [Test]
        public void GetArgument_ValidOperand_ShouldNotBeNull()
        {
            var instruction = Instruction.ADD(1, 1, "Test");

            var argument = instruction.Argument("source_B");

            argument.Should().NotBeNull();
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
        public void GetHasCode_WhenCalled_ReturnsHasOfKey()
        {
            var instruction = Instruction.XIC("MyTag");

            var result = instruction.GetHashCode();

            result.Should().Be(instruction.Text.GetHashCode());
        }

        [Test]
        public void ToString_WhenCalled_ShouldBeText()
        {
            var text = Instruction.OTE("MyTag").ToString();

            text.Should().Be("OTE(MyTag)");
        }
    }
}