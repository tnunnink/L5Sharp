using FluentAssertions;
using L5Sharp.Common;
using L5Sharp.Enums;

namespace L5Sharp.Tests.Common;

[TestFixture]
public class ArgumentTests
{
    [Test]
    public void New_FromTagNameString_ShouldNotBeNull()
    {
        Argument argument = new TagName("Test");

        argument.Should().NotBeNull();
    }

    [Test]
    public void New_FromAtomicValue_ShouldNotBeNull()
    {
        Argument argument = 100;

        argument.Should().NotBeNull();
    }

    [Test]
    public void Method_Scenario_ExpectedBehavior()
    {
        Instruction.XIC.Of("TestTag.Something", 100);
        
        Instruction.OR.Of("TestTag.Something", 100);
    }
}