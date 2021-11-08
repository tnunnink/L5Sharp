using FluentAssertions;
using L5Sharp.Instructions;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class NeutralTextTests
    {
        [Test]
        public void New_ValidInstruction_ShouldHaveExpectedProperties()
        {
            var text = new NeutralText(new MOV());

            text.Instruction.Should().Be(nameof(MOV));
            text.Signature.Should().Be("MOV(,)");
            text.Arguments.Should().BeEmpty();
        }
    }
}