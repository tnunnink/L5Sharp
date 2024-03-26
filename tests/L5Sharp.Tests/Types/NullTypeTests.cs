using FluentAssertions;

namespace L5Sharp.Tests.Types;

[TestFixture]
public class NullTypeTests
{
    [Test]
    public void HasExpectedValues()
    {
        var type = NullType.Instance;

        type.Name.Should().Be("NULL");
        type.Members.Should().BeEmpty();
    }

    [Test]
    public Task SerializeShouldBeVerified()
    {
        var type = NullType.Instance;

        var xml = type.Serialize().ToString();

        return Verify(xml);
    }
}