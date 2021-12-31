using System;
using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class InstructionTests
    {
        [Test]
        public void FromText_NullName_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => Instruction.FromText(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void FromText_ValidArguments_ShouldNotBeNull()
        {
            var instruction = Instruction.FromText("XIC(SomeBit)");

            instruction.Should().NotBeNull();
        }

        [Test]
        public void FromText_ValidArguments_ShouldHaveExpectedProperties()
        {
            var instruction = Instruction.FromText("XIC(SomeBit)");

            instruction.Name.Should().Be("XIC");
            instruction.Arguments.Should().HaveCount(1);
            instruction.Arguments.Should().Contain(a => a.Reference == "SomeBit");
        }
        
        [Test]
        public void ToText_WhenCalled_ShouldHaveExpectedValue()
        {
            var instruction = Instruction.FromText("XIC(SomeBit)");

            var text = instruction.ToText();

            text.Should().Be("XIC(SomeBit)");
        }
        
        [Test]
        public void Of_WhenCalled_ShouldHaveExpectedValue()
        {
            var instruction = Instruction.FromText("XIC(SomeBit)");

            instruction = instruction.Of("ANewBit");

            instruction.ToText().Should().Be("XIC(ANewBit)");
        }
    }
}