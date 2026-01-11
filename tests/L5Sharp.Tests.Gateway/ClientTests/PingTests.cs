namespace L5Sharp.Tests.Gateway.ClientTests;

[TestFixture]
public class PingTests : PlcTestBase
{
    [Test]
    public async Task Ping_ValidReachableAddress_ShouldBeTrue()
    {
        using var client = CreateClient();

        var result = await client.Ping();

        Assert.That(result, Is.True);
    }
}