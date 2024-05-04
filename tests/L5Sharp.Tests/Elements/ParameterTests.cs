using FluentAssertions;

namespace L5Sharp.Tests.Elements;

[TestFixture]
public class ParameterTests
{
    [Test]
    public void ToMember_InputAtomicParameter_ShouldBeExpected()
    {
        var parameter = new Parameter("Test", new DINT(123));
        
        var member = parameter.ToMember();

        member.Name.Should().Be("Test");
        member.Value.Should().Be(new INT(123));
    }

    [Test]
    public void METHOD()
    {
    }
}