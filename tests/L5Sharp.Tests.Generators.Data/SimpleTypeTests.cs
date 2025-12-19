using FluentAssertions;
using L5Sharp.Core;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests.Generators.Data;

[TestFixture]
public class SimpleTypeTests
{
    [Test]
    public Task New_DefaultInstance_ShouldBeVerified()
    {
        var type = new SimpleType();

        return VerifyXml(type.Serialize().ToString());
    }

    [Test]
    public void Create_ValidTypeName_ShouldNotBeNull()
    {
        var type = LogixType.Create("SimpleType");

        type.Should().NotBeNull();
    }

    [Test]
    public void Create_GenericOverload_ShouldNotBeNullAndHaveExpectedMembers()
    {
        var type = LogixType.Create<SimpleType>();

        type.Should().NotBeNull();
        type.BoolMember.Should().Be(false);
        type.DintMember.Should().Be(0);
    }
}