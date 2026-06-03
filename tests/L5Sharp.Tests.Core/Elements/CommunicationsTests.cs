using FluentAssertions;

namespace L5Sharp.Tests.Core.Elements;

[TestFixture]
public class CommunicationsTests
{
    [Test]
    public void New_Default_ShouldNotBeNull()
    {
        var communications = new Communications();

        communications.Should().NotBeNull();
    }

    [Test]
    public void New_Default_ShouldHaveExpectedDefaults()
    {
        var communications = new Communications();

        communications.ConfigTag.Should().NotBeNull();
        communications.Connections.Should().BeEmpty();
    }

    [Test]
    public Task New_Default_ShouldBeVerified()
    {
        var communications = new Communications();

        return Verify(communications.Serialize().ToString());
    }
}
