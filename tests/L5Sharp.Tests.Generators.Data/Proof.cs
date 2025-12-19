using FluentAssertions;
using L5Sharp.Core;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests.Generators.Data;

public class Tests
{
    [Test]
    public Task Testing()
    {
        var type = new SimpleType();

        return VerifyXml(type.Serialize().ToString());
    }

    [Test]
    public void IsRegistered()
    {
        var type = LogixType.Create("SimpleType");

        type.Should().NotBeNull();
    }

    [Test]
    public void AndInFactIShouldBeAbleToCreateItByType()
    {
        var type = LogixType.Create<SimpleType>();

        type.Should().NotBeNull();
        type.BoolMember.Should().Be(false);
        type.DintMember.Should().Be(0);
    }
}