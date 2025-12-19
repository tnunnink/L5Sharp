using FluentAssertions;
using L5Sharp.Core;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests.Generators.Data;

[TestFixture]
public class ComplexTypeTests
{
    [Test]
    public Task New_DefaultInstance_ShouldBeVerified()
    {
        var type = new ComplexType();

        return VerifyXml(type.Serialize().ToString());
    }

    [Test]
    public void Create_ValidTypeName_ShouldNotBeNull()
    {
        var type = LogixType.Create("ComplexType");

        type.Should().NotBeNull();
    }

    [Test]
    public void Create_GenericOverload_ShouldNotBeNullAndHaveExpectedMembers()
    {
        var type = LogixType.Create<ComplexType>();

        type.Should().NotBeNull();
        type.SimpleMember.BoolMember.Should().Be(false);
        type.TimeMember.PRE.Should().Be(0);
    }

    [Test]
    public Task Create_SetMembers_ShouldBeVerified()
    {
        var type = new ComplexType();

        type.SimpleMember.DintMember = 12345;
        type.AlarmMember.HHLimit = 14.32f;
        type.AOIType.Config = "2#0110_0101_0001_1011";
        type.StringMember = "This is a test";
        
        return VerifyXml(type.Serialize().ToString());
    }
}