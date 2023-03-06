using FluentAssertions;
using L5Sharp.Enums;

namespace L5Sharp.Tests.Enums
{
    [TestFixture]
    public class InstructionTests
    {
        [Test]
        public void List_WhenCalled_ReturnsAllInstructions()
        {
            var list = Instruction.All();

            list.Should().NotBeEmpty();
        }
        [Test]
        public void XIC_WhenCalled_ShouldBeExpectedProperties()
        {
            var instruction = Instruction.XIC;

            instruction.Name.Should().Be("XIC");
            instruction.Signature.Should().Contain("XIC");
            instruction.Destructive.Should().BeFalse();
        }

        [Test]
        public void Of_XIC_ShouldBeExpectedOutput()
        {
            var xic = Instruction.XIC;

            var text = xic.Of("MyTagName");

            text.ToString().Should().Be("XIC(MyTagName)");
        }
        
        [Test]
        public void Of_TON_ShouldBeExpectedOutput()
        {
            var text = Instruction.TON.Of("SomeTimer", 5000, 0);

            text.ToString().Should().Be("TON(SomeTimer,5000,0)");
        }
    }
}