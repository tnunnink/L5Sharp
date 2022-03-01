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
            FluentActions.Invoking(() => Instruction.Parse(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void FromText_ValidArguments_ShouldNotBeNull()
        {
            var instruction = Instruction.Parse("XIC(SomeBit)");

            instruction.Should().NotBeNull();
        }

        [Test]
        public void FromText_ValidArguments_ShouldHaveExpectedProperties()
        {
            var instruction = Instruction.Parse("XIC(SomeBit)");

            instruction.Name.Should().Be("XIC");
            instruction.Arguments.Should().HaveCount(1);
            instruction.Arguments.Should().Contain(a => a.Reference == "SomeBit");
        }
    }
}