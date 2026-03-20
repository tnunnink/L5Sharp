using FluentAssertions;
using L5Sharp.Gateway;

namespace L5Sharp.Tests.Gateway.ClientTests;

[TestFixture]
public class BasicClientTests
{
    [Test]
    public void New_IPAndSlot_ShouldNotBeNull()
    {
        using var client = new PlcClient("10.11.19.205", 1);

        client.Should().NotBeNull();
    }
}