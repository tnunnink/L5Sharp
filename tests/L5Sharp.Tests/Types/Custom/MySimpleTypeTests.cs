using FluentAssertions;

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

    [Test]
    public Task Serialize_UpdatedMember_ShouldBeVerified()
    {
        var type = new MySimpleType();

        type.M1 = true;
        type.M2 = 12;
        type.M3 = 100;
        type.M4 = 10000;
        type.M5 = 123456789;
        type.M6 = 1.23f;
        
        var xml = type.Serialize().ToString();

        return Verify(xml);
    }
}