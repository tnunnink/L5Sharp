using FluentAssertions;
using L5Sharp.Instructions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class NeutralTextTests
    {
        [Test]
        public void CheckItOut()
        {
            var text = new NeutralText(Logix.Instruction.MOV);

            text.Instruction.Should().Be(Logix.Instruction.MOV.Name);
            text.Text.Should().Be("MOV(?,?)");
        }

        [Test]
        public void GenericTests()
        {
            var text = NeutralText.Create<MOV>();
            
            text.Assign(m => m.Source, new Tag("Test", Logix.DataType.Dint));
        }
    }
}