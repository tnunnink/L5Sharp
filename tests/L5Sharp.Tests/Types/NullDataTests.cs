using FluentAssertions;

namespace L5Sharp.Tests.Types;

[TestFixture]
public class NullDataTests
{
    [Test]
    public void HasExpectedValues()
    {
        var type = NullData.Instance;

        type.Name.Should().Be("NULL");
        type.Members.Should().BeEmpty();
    }

    [Test]
    public Task SerializeShouldBeVerified()
    {
        var type = NullData.Instance;

        var xml = type.Serialize().ToString();

        return Verify(xml);
    }
}