using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
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
        public void XIC_ValidTagName_ShouldBeExpected()
        {
            var instruction = Instruction.XIC("Test");

            instruction.Name.Should().Be("XIC");
            instruction.Operands.Should().HaveCount(1);
            //instruction.Operands.Should().Contain("Test");
            instruction.Operands.First().Should().BeEquivalentTo("Test");
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