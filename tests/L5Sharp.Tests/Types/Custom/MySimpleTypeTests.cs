using FluentAssertions;
using L5Sharp.Enums;

namespace L5Sharp.Tests.Types.Custom;

[TestFixture]
public class MySimpleTypeTests
{
    [Test]
    public void New_Default_ShouldNotBeNull()
    {
        var type = new MySimpleType();

        type.Should().NotBeNull();
    }

    [Test]
    public void New_Default_ShouldHaveExpectedValue()
    {
        var type = new MySimpleType();

        type.Name.Should().Be("MySimpleType");
        type.Class.Should().Be(DataTypeClass.User);
        type.M1.Should().Be(0);
        type.M2.Should().Be(0);
        type.M3.Should().Be(0);
        type.M4.Should().Be(0);
        type.M5.Should().Be(0);
        type.M6.Should().Be(0);
    }
    
    [Test]
    public Task Serialize_Default_ShouldBeVerified()
    {
        var type = new MySimpleType();

        var xml = type.Serialize().ToString();

        return Verify(xml);
    }
}