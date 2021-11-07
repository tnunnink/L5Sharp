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
            var text = new NeutralText(Logix.Instruction.MOV);

            text.Instruction.Should().Be(Logix.Instruction.MOV.Name);
            text.Signature.Should().Be("MOV(,)");
            text.Arguments.Should().BeEmpty();
        }

        [Test]
        public void GenericTests()
        {
            var text = NeutralText.Create<MOV>(10, new Tag<Dint>("Test"));
            
            text.Assign(m => m.Destination, new Tag("New Test", Logix.DataType.Dint));
        }
    }
}