using FluentAssertions;
using L5Sharp.Common;

namespace L5Sharp.Tests.Common
{
    [TestFixture]
    public class InstructionTests
    {
        [Test]
        public void New_ValidKey_ShouldNotBeNull()
        {
            var instruction = new Instruction("Test");

            instruction.Should().NotBeNull();
        }

        [Test]
        public void New_ValidKey_ShouldHaveExpectedDefaults()
        {
            var instruction = new Instruction("Test");
            
            instruction.Key.Should().Be("Test");
            instruction.Signature.Should().Be("()");
            instruction.IsConditional.Should().BeFalse();
            instruction.CallsRoutine.Should().BeFalse();
            instruction.CallsTask.Should().BeFalse();
            instruction.Arguments.Should().BeEmpty();
            instruction.Text.Should().Be("Test()");
        }
        
        [Test]
        public void Known_WhenCalled_ShouldNotBeEmpty()
        {
            var known = Instruction.Known();

            known.Should().NotBeEmpty();
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
            var instruction = Instruction.XIC;

            instruction.Key.Should().Be("XIC");
            instruction.Signature.Should().Contain("()");
            instruction.IsConditional.Should().BeTrue();
            instruction.CallsRoutine.Should().BeFalse();
            instruction.CallsTask.Should().BeFalse();
            instruction.Arguments.Should().BeEmpty();
            instruction.Text.Should().Be("XIC()");
        }
        
        [Test]
        public void OTE_WhenCalled_ShouldBeExpectedProperties()
        {
            var instruction = Instruction.OTE;

            instruction.Key.Should().Be("OTE");
            instruction.Signature.Should().Contain("()");
            instruction.IsConditional.Should().BeFalse();
            instruction.CallsRoutine.Should().BeFalse();
            instruction.CallsTask.Should().BeFalse();
            instruction.Arguments.Should().BeEmpty();
            instruction.Text.Should().Be("OTE()");
        }
        
        [Test]
        public void CallsRoutine_JSR_ShouldBeTrue()
        {
            var instruction = Instruction.JSR;
            
            instruction.CallsRoutine.Should().BeTrue();
        }
        
        [Test]
        public void CallsTask_EVENT_ShouldBeTrue()
        {
            var instruction = Instruction.EVENT;
            
            instruction.CallsTask.Should().BeTrue();
        }

        [Test]
        public void Of_XIC_ShouldHaveExpectedValues()
        {
            var instruction = Instruction.XIC.Of("MyTagKey");

            instruction.Key.Should().Be("XIC");
            instruction.Signature.Should().Be("(MyTagKey)");
            instruction.IsConditional.Should().BeTrue();
            instruction.CallsRoutine.Should().BeFalse();
            instruction.CallsTask.Should().BeFalse();
            instruction.Arguments.Should().HaveCount(1);
            instruction.Text.Should().Be("XIC(MyTagKey)");
        }

        [Test]
        public void Of_TON_ShouldHaveExpectedValues()
        {
            var instruction = Instruction.TON.Of("SomeTimer", 5000, 0);

            instruction.Key.Should().Be("TON");
            instruction.Signature.Should().Be("(SomeTimer,5000,0)");
            instruction.IsConditional.Should().BeFalse();
            instruction.CallsRoutine.Should().BeFalse();
            instruction.CallsTask.Should().BeFalse();
            instruction.Arguments.Should().HaveCount(3);
            instruction.Text.Should().Be("TON(SomeTimer,5000,0)");
        }

        [Test]
        [TestCase("XIC(MyTagKey);", "XIC", 1)]
        [TestCase("TON(SomeTimer,5000,0);", "TON", 3)]
        [TestCase("TON(SomeTimer,?,?);", "TON", 3)]
        [TestCase("CMP(ATN(_Test) > 1.0);", "CMP", 1)]
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
            FluentActions.Invoking(() => Instruction.Parse("(SomeTag > 1.0)/100")).Should().Throw<ArgumentException>();
        }

        [Test]
        public void IsEquivalent_EqualInstructionSignatures_ShouldBeTrue()
        {
            var first  = Instruction.XIC.Of("MyTag");
            var second = Instruction.XIC.Of("MyTag");

            var result = first.IsEquivalent(second);

            result.Should().BeTrue();
        }
        
        [Test]
        public void IsEquivalent_SameKeyDifferentSignature_ShouldBeFalse()
        {
            var first  = Instruction.XIC.Of("MyTag");
            var second = Instruction.XIC.Of("MyOtherTag");

            var result = first.IsEquivalent(second);

            result.Should().BeFalse();
        }
        
        [Test]
        public void IsEquivalent_DifferentKeySameSignature_ShouldBeFalse()
        {
            var first  = Instruction.XIC.Of("MyTag");
            var second = Instruction.OTE.Of("MyTag");

            var result = first.IsEquivalent(second);

            result.Should().BeFalse();
        }

        [Test]
        public void Equals_EqualInstances_ShouldBeTrue()
        {
            var first = Instruction.XIC;
            var second = Instruction.XIC;

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void Equals_SameInstances_ShouldBeTrue()
        {
            var first = Instruction.XIC;

            // ReSharper disable once EqualExpressionComparison
            var result = first.Equals(first);

            result.Should().BeTrue();
        }

        [Test]
        public void Equals_Null_ShouldBeFalse()
        {
            var first = Instruction.XIC;

            // ReSharper disable once EqualExpressionComparison
            var result = first.Equals(null);

            result.Should().BeFalse();
        }

        [Test]
        public void Equals_EqualString_ShouldBeTrue()
        {
            var first = Instruction.TON;

            // ReSharper disable once SuspiciousTypeConversion.Global
            var result = first.Equals("TON");

            result.Should().BeTrue();
        }

        [Test]
        public void EqualsOperator_EqualInstances_ShouldBeTure()
        {
            var first = Instruction.XIC;
            var second = Instruction.XIC;

            var result = first == second;

            result.Should().BeTrue();
        }
        
        [Test]
        public void NotEqualsOperator_EqualInstances_ShouldBeFalse()
        {
            var first = Instruction.XIC;
            var second = Instruction.XIC;

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHasCode_WhenCalled_ReturnsHasOfKey()
        {
            var instruction = Instruction.XIC;
            
            var result = instruction.GetHashCode();
            
            result.Should().Be(instruction.Key.GetHashCode());
        }

        [Test]
        public void ToString_WhenCalled_ShouldBeKey()
        {
            Instruction.OTE.ToString().Should().Be("OTE");
        }
    }
}