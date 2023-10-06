using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp.Tests.Types;

[TestFixture]
public class NullTypeTests
{
    [Test]
    public void HasExpectedValues()
    {
        var type = NullType.Instance;

        type.Name.Should().Be("NULL");
        type.Family.Should().Be(DataTypeFamily.None);
        type.Class.Should().Be(DataTypeClass.Unknown);
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