using System;
using FluentAssertions;
using L5Sharp.Core;
using NUnit.Framework;

namespace L5Sharp.Tests.Core
{
    [TestFixture]
    public class InstructionTests
    {
        [Test]
        public void New_NullName_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new Instruction(null!)).Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void New_NullOperands_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new Instruction("XIC", null!)).Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void New_Valid_ShouldNotBeNull()
        {
            var instruction = new Instruction("XIC");

            instruction.Should().NotBeNull();
        }

        [Test]
        public void New_Overloaded_ShouldHaveExpected()
        {
            var instruction = new Instruction("OTE", new TagName("SomeTagName"));

            instruction.Name.Should().Be("OTE");
            instruction.Operands.Should().HaveCount(1);
        }
        
        [Test]
        public void Parse_ValidArguments_ShouldHaveExpectedProperties()
        {
            var instruction = Instruction.Parse("XIC(SomeBit)");

            instruction.Name.Should().Be("XIC");
            instruction.Operands.Should().HaveCount(1);
        }
    }
}